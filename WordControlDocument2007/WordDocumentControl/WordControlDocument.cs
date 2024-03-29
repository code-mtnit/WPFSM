﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using System.Printing;
using System.Collections;
using System.IO;
using System.Drawing.Printing;
using Microsoft.Office;
using Microsoft.Office.Interop.Word;
using Microsoft.Win32;
using Adjustments = Microsoft.Office.Core.Adjustments;
using Application = Microsoft.Office.Interop.Word.Application;
using MessageBox = System.Windows.Forms.MessageBox;
using Shape = Microsoft.Office.Core.Shape;
using Microsoft.Office.Core;


namespace Sbn.AdvancedControls.WordControlDocument
{
    public enum FileExtension
    {
        doc = 0,
        docx = 1,
        mht = 2,
        tif = 3,
        tiff = 4,
        pdf = 5
    }

    /// <summary>
    /// WinWordControl allows you to load doc-Files to your
    /// own application without any loss, because it uses 
    /// the real WinWord.
    /// </summary>
    public class WordControlDocument : System.Windows.Forms.UserControl
    {
        public static string TempDirectory = @"C:\Sayban\Temp";

        #region "API usage declarations"

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);




        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetFocus(IntPtr hWnd);


        [DllImport("user32.dll")]
        public static extern int FindWindow(string strclassName, string strWindowName);

        [DllImport("user32.dll")]
        static extern int SetParent(int hWndChild, int hWndNewParent);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        static extern bool SetWindowPos(
            int hWnd,               // handle to window
            int hWndInsertAfter,    // placement-order handle
            int X,                  // horizontal position
            int Y,                  // vertical position
            int cx,                 // width
            int cy,                 // height
            uint uFlags             // window-positioning options
            );

        [DllImport("user32.dll", EntryPoint = "MoveWindow")]
        static extern bool MoveWindow(
            int hWnd,
            int X,
            int Y,
            int nWidth,
            int nHeight,
            bool bRepaint
            );

        [DllImport("user32.dll", EntryPoint = "DrawMenuBar")]
        static extern Int32 DrawMenuBar(
            Int32 hWnd
            );

        [DllImport("user32.dll", EntryPoint = "GetMenuItemCount")]
        static extern Int32 GetMenuItemCount(
            Int32 hMenu
            );

        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        static extern Int32 GetSystemMenu(
            Int32 hWnd,
            bool bRevert
            );

        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        static extern Int32 RemoveMenu(
            Int32 hMenu,
            Int32 nPosition,
            Int32 wFlags
            );

        static int GWL_STYLE = -16;
        const UInt32 WS_POPUP = 0x80000000;
        const UInt32 WS_CHILD = 0x40000000;



        private const int MF_BYPOSITION = 0x400;
        private const int MF_REMOVE = 0x1000;


        const int SWP_DRAWFRAME = 0x20;
        const int SWP_NOMOVE = 0x2;
        const int SWP_NOSIZE = 0x1;
        const int SWP_NOZORDER = 0x4;


        #endregion

        public FileExtension Extension { get; set; }

        BackgroundWorker BGWorker = new BackgroundWorker();

        /* I was testing wheater i could fix some exploid bugs or not.
         * I left this stuff in here for people who need to know how to 
         * interface the Win32-API

        [StructLayout(LayoutKind.Sequential)]
            public struct RECT 
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
		
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(int hwnd, ref RECT rc);
		
        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(
            int hWnd, 
            int msg, 
            int wParam, 
            int lParam
        );
        */


        /// <summary>
        /// Change. Made the following variables public.
        /// </summary>
        public byte[] CurrentStream
        {
            get
            {
                var result = GetStreamOfCurrentDocument();
                return result;
            }
        }

        public event PrintEventHandler AfterPrintDocument;
        public event PrintEventHandler AfterGetImageFromDocument;

        private string _CurrentText = "";
        public string CurrentText
        {
            get
            {
                return _CurrentText;
            }
        }

        public string ExtractWordText()
        {
            Save();
            var strPath = CurrentFileInfo2.FullName;
            CloseActiveDocument();
            string text = ExtractWordText(strPath);
            OpenWordDocument(strPath);

            return text;
        }

        public string ExtractWordText(string strPath)
        {
            string text = "";
            try
            {
                //var loader = new Sbn.Words.GetDocText.Doc.TextLoader(strPath);// new TextLoader(dlgOpenFile.FileName);
                //if (loader.LoadText(out text))
                //{
                //}
                document.ActiveWindow.Selection.WholeStory();
                document.ActiveWindow.Selection.Copy();
                System.Windows.Forms.IDataObject data = System.Windows.Forms.Clipboard.GetDataObject();
                text = data.GetData(System.Windows.Forms.DataFormats.UnicodeText).ToString();
            }
            catch { }

            return text;
        }

        public Document document;
        //public Microsoft.Office.Interop.Word._Document document;
        public Application wd = null;
        //public Microsoft.Office.Interop.Word._Application wd = null;
        public int wordWnd = 0;

        public FileInfo BaseFileInfo;
        public FileInfo CurrentFileInfo2;

        private bool deactivateevents = false;

        bool _IsInGettingImageState = false;
        public bool IsInGettingImageState
        {
            get { return _IsInGettingImageState; }
            set { _IsInGettingImageState = value; }
        }

        bool _DocumentChanged = false;
        public bool DocumentChanged
        {
            get
            {
                if (BaseFileInfo == null)
                    return true;

                return _DocumentChanged;
            }
            set
            {
                _DocumentChanged = value;

            }
        }

        bool _IsNewInstance = false;
        public bool IsNewInstance
        {
            get { return _IsNewInstance; }
            set { _IsNewInstance = value; }
        }

        bool _ReadOnly = false;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private PrintDialog printDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;

        public bool ReadOnly
        {
            get { return _ReadOnly; }
            set
            {
                _ReadOnly = value;
                try
                {

                    if (document != null)
                    {
                        // wd.ActiveDocument.Final = value;

                        // ISSUE: explicit reference operation
                        // ISSUE: variable of a reference type
                        object NoReset = false;
                        object UseIRM = System.Reflection.Missing.Value;
                        object EnforceStyleLock = System.Reflection.Missing.Value;
                        object Password = "IsReadOnly";
                        if (value)
                        {
                            document.Protect(WdProtectionType.wdAllowOnlyReading, NoReset, Password, UseIRM, EnforceStyleLock);
                            document.ActiveWindow.View.Zoom.PageFit = WdPageFit.wdPageFitBestFit;//.wdPageFitTextFit;// .wdPageFitBestFit;
                        }
                        else
                        {
                            if (document.ProtectionType != WdProtectionType.wdNoProtection)
                                document.Unprotect(ref Password);
                        }

                        OnResize();
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// needed designer variable
        /// </summary>
        private Container components = null;

        public WordControlDocument()
        {
            InitializeComponent();
            IsNewInstance = true;

        }
        public int _clickCount = 0;
        private void Wd_WindowSelectionChange(Selection Sel)
        {

            if (Sel.Hyperlinks != null && !_gobackClick)
            {
                _clickCount++;
            }
            _gobackClick = false;
            //throw new NotImplementedException();
        }

        //<summary>
        //cleanup Ressources
        //</summary>
        protected override void Dispose(bool disposing)
        {
            CloseWordApp();
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary>
        /// !do not alter this code! It's designer code
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // WordControlDocumentNew
            // 
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Name = "WordControlDocumentNew";
            this.Size = new System.Drawing.Size(826, 563);
            this.Resize += new System.EventHandler(this.OnResize);
            this.ResumeLayout(false);

        }
        #endregion


        /// <summary>
        /// Preactivation
        /// It's usefull, if you need more speed in the main Program
        /// so you can preload Word.
        /// </summary>
        public void PreActivate()
        {
            if (wd == null) wd = new Application();
        }

        /// <summary>
        /// Close the current Document in the control --> you can 
        /// load a new one with LoadDocument
        /// </summary>
        public bool CloseActiveDocument()
        {
            CurrentFileInfo2 = null;
            BaseFileInfo = null;
            if (wd == null)
                return true;

            if (document == null)
                return true;

            deactivateevents = true;
            try
            {
                object dummy = (object)false;

                if (wd.Documents != null && wd.Documents.Count > 0)
                {
                    //wd.ActiveDocument.Close(ref dummy, ref dummy, ref dummy);
                }

                if (ReadOnly)
                    ReadOnly = false;

                document.Close(ref dummy, ref dummy, ref dummy);
                document = null;
            }
            catch (Exception ex)
            {
                return false;
            }

            deactivateevents = false;
            return true;
        }

        public void CloseWordApp()
        {
            if (wd == null)
                return;

            try
            {
                CloseActiveDocument();
                //object dummy = null;
                //object dummy2 = (object)false;
                object unknow = Type.Missing;
                object saveChanges = WdSaveOptions.wdDoNotSaveChanges;//.wdPromptToSaveChanges;
                // Change the line below.
                if (wd.Documents.Count == 0)
                {
                    try
                    {
                        wd.Quit(ref saveChanges, ref unknow, ref unknow);
                    }
                    catch (Exception) { }
                    finally
                    {
                        CleanUp();
                    }
                }
            }
            catch { }
        }


        public void CleanUp()
        {

            if (document != null)
                while (Marshal.ReleaseComObject(document) > 0) ;


            ExecuteCommandASync("TaskKill /PID " + CurrentWordAppPID + " /f");


            //if (!(System.Environment.OSVersion.Version.Major >= 6 && System.Environment.OSVersion.Version.Minor >= 2))
            //{
            //    if (wd != null)
            //        while (Marshal.ReleaseComObject(wd) > 0) ;
            //}
            wd = null;
        }

        //object _PublicClipBoard = null;

        //public object PublicClipBoard
        //{
        //    get { return _PublicClipBoard; }
        //    set
        //    {
        //        if (value != null && value.ToString() != "")
        //            _PublicClipBoard = value; 
        //    }
        //}

        /// <summary>
        /// catches Word's close event 
        /// starts a Thread that send a ESC to the word window ;)
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="test"></param>
        private void OnClose(Document doc, ref bool cancel)
        {
            //   PublicClipBoard =Clipboard.GetText();
            if (!deactivateevents && doc == document)
            {
                cancel = true;
            }
        }

        /// <summary>
        /// catches Word's open event
        /// just close
        /// </summary>
        /// <param name="doc"></param>
        public void OnOpenDoc(Document doc)
        {
            //OnNewDoc(doc);
        }

        /// <summary>
        /// catches Word's newdocument event
        /// just close
        /// </summary>
        /// <param name="doc"></param>
        public void OnNewDoc(Document doc)
        {
            if (!deactivateevents)
            {
                deactivateevents = true;
                object dummy = null;
                doc.Close(ref dummy, ref dummy, ref dummy);
                deactivateevents = false;
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            CloseWordApp();
            base.OnHandleDestroyed(e);
        }

        /// <summary>
        /// catches Word's quit event
        /// normally it should not fire, but just to be shure
        /// safely release the internal Word Instance 
        /// </summary>
        private void OnQuit()
        {
            if (wd != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wd);
                wd = null;
            }

            wd = null;
        }

        public void SetSetting()
        {
            var vm = new WCSettingViewModel();
            vm.PrinterName = Properties.Settings.Default.ImagePrinter;
            vm.Extention = Properties.Settings.Default.Extention;
            var view = new WCSettingView();
            view.DataContext = vm;
            view.ShowDialog();
        }

        public void RefreshAll()
        {
            object dummy = null;
            object dummy2 = (object)false;

            try
            {
                document.Close(ref dummy, ref dummy, ref dummy);
            }
            catch { }

            try
            {
                //document.tr
                // Change the line below.
                wd.Quit(ref dummy2, ref dummy, ref dummy);
                wd = null;
                // wd = new Word.ApplicationClass();

                if (document != null) { }
            }
            catch { }
        }
        private bool _gobackClick = false;
        public void GoBack()
        {
            // if (wd != null)
            {
                _gobackClick = true;
                _clickCount--;
                bool p = SetForegroundWindow((IntPtr)wordWnd);
                var ff = SetFocus((IntPtr)wordWnd);
                SendKeys.SendWait("%{LEFT}");
            }
        }

        public void GoForward()
        {
            _clickCount++;

            bool p = SetForegroundWindow((IntPtr)wordWnd);
            //var ff = SetFocus((IntPtr)wordWnd);  

            //Thread.Sleep(100);
            //wd.Activate();
            ////Focus();
            //wd.ActiveWindow.SetFocus();
            //wd.ActiveWindow.ActivePane.Activate();
            //wd.ActiveDocument.Activate();
            SendKeys.SendWait("%{RIGHT}");
        }

        public void GoFirstDoc()
        {
            // if (wd != null)
            {
                bool p = SetForegroundWindow((IntPtr)wordWnd);
                var ff = SetFocus((IntPtr)wordWnd);
                SendKeys.SendWait("^{HOME}");
            }
        }

        public int CurrentWordAppPID;
        public Application InitialNewInstance()
        {
            IsNewInstance = false;
            wd = null;
            wordWnd = 0;

            var plist = from p in Process.GetProcessesByName("WINWORD") select p.Id;

            wd = new Microsoft.Office.Interop.Word.Application();

            var plist2 = from p in Process.GetProcessesByName("WINWORD") select p.Id;
            var CurrentPIDTemp = (from p in plist2 where !plist.Contains(p) select p).ToList();
            if (CurrentPIDTemp != null && CurrentPIDTemp.Count > 0)
            {
                CurrentWordAppPID = CurrentPIDTemp[0];
                //var proc = Process.GetProcessById(CurrentWordAppPID);
                //wordWnd = proc.MainWindowHandle.ToInt32();
                //var ss = FindWindow("Opusapp", null);
            }

            BGWorker.DoWork += new DoWorkEventHandler(BGWorker_DoWork);
            BGWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BGWorker_RunWorkerCompleted);

            try
            {
                wd.CommandBars.AdaptiveMenus = false;
                //wd.DocumentBeforeClose += new ApplicationEvents4_DocumentBeforeCloseEventHandler(OnClose);
                wd.DocumentBeforeClose += OnClose;

                wd.DocumentOpen += new ApplicationEvents4_DocumentOpenEventHandler(OnOpenDoc);
                //  wd.NewDocument += new ApplicationEvents4_NewDocumentEventHandler(OnNewDoc);
                // wd.DocumentOpen += new Word.ApplicationEvents2_DocumentOpenEventHandler(OnOpenDoc);
                // wd.Quit += new ApplicationEvents4_QuitEventHandler(OnQuit);

                //wd.ApplicationEvents2_Event_Quit += new ApplicationEvents2_QuitEventHandler(OnQuit);
                wd.DocumentBeforePrint += new ApplicationEvents4_DocumentBeforePrintEventHandler(wd_DocumentBeforePrint);
                wd.DocumentChange += new ApplicationEvents4_DocumentChangeEventHandler(wd_DocumentChange);
                wd.WindowSelectionChange += Wd_WindowSelectionChange;

            }
            catch { }

            return wd;
        }

        public void InitalInOtherForm()
        {

            if (wordWnd == 0)
            {
                string caption = wd.Caption;
                wd.Caption = "NewInstance" + DateTime.Now.Ticks;

                wordWnd = FindWindow("Opusapp", wd.Caption);
                wd.Caption = caption;
            }


            if (wordWnd != 0)
            {
                var proc = Process.GetProcessById(CurrentWordAppPID);
                if (proc.MainWindowHandle.ToInt32() != 0)
                {
                    if (wordWnd == proc.MainWindowHandle.ToInt32())
                    {
                        var nn = SetParent(wordWnd, this.Handle.ToInt32());
                    }
                }
                else
                {
                    var nn = SetParent(wordWnd, this.Handle.ToInt32());
                }



                //var style = GetWindowLong((IntPtr)wordWnd, GWL_STYLE);

                //style &= (int)~WS_POPUP;    //remove the POPUP style

                //style |= (int)WS_CHILD;      //Add the Child style

                //SetWindowLong((IntPtr)wordWnd, GWL_STYLE, style);

                //UInt32 style = (UInt32)GetWindowLong((IntPtr)wordWnd, GWL_STYLE);

                //style &= ~WS_CHILD;    //remove the Child style

                //style |= WS_POPUP;      //Add the POPUP style

                //SetWindowLong((IntPtr)wordWnd, GWL_STYLE, (int)style);


                # region

                try
                {
                    wd.ActiveWindow.DisplayRightRuler = true;
                    wd.ActiveWindow.DisplayScreenTips = true;
                    wd.ActiveWindow.DisplayVerticalRuler = true;
                    wd.ActiveWindow.DisplayRightRuler = true;
                    wd.ActiveWindow.ActivePane.DisplayRulers = true;
                    wd.ActiveWindow.ActivePane.View.Type = WdViewType.wdPrintView;
                    //wd.ActiveWindow.ActivePane.View.Type = Word.WdViewType.wdPrintView;//wdWebView; // .wdNormalView;
                }
                catch
                {

                }




                /// Code Added
                /// Disable the specific buttons of the command bar
                /// By default, we disable/hide the menu bar
                /// The New/Open buttons of the command bar are disabled
                /// Other things can be added as required (and supported ..:) )
                /// Lots of commented code in here, if somebody needs to disable specific menu or sub-menu items.
                /// 


                //int counter = wd.ActiveWindow.Application.CommandBars.Count;
                //for (int i = 1; i <= counter; i++)
                //{
                //    try
                //    {

                //        String nm = wd.ActiveWindow.Application.CommandBars[i].Name;

                //        if (nm == "Standard")
                //        {

                //            int count_control = wd.ActiveWindow.Application.CommandBars[i].Controls.Count;
                //            for (int j = 1; j <= count_control; j++)
                //            {                              

                //                //if (wd.ActiveWindow.Application.CommandBars[i].Controls[j].Caption.Contains("New"))
                //                //{
                //                //    wd.ActiveWindow.Application.CommandBars[i].Controls[j].Enabled = false;
                //                //    wd.ActiveWindow.Application.CommandBars[i].Controls[j].Visible = false;
                //                //    var oi = wd.ActiveWindow.Application.CommandBars[i].Controls[j].Id;
                //                //}


                //                //if (wd.ActiveWindow.Application.CommandBars[i].Controls[j].Caption.Contains("Save"))
                //                //{
                //                //    wd.ActiveWindow.Application.CommandBars[i].Controls[j].Enabled = false;
                //                //    wd.ActiveWindow.Application.CommandBars[i].Controls[j].Visible = false;
                //                //}
                //               //  MessageBox.Show(wd.ActiveWindow.Application.CommandBars[i].Controls[j].Caption.ToString());

                //                wd.ActiveWindow.Application.CommandBars[i].Controls[j].Enabled = true;
                //               // wd.ActiveWindow.Application.CommandBars[i].Controls[j].Visible = true;

                //            }
                //        }

                //        if (nm == "Menu Bar")
                //        {
                //            //To disable the menubar, use the following (1) line
                //            wd.ActiveWindow.Application.CommandBars[i].Enabled = false;


                //            /// If you want to have specific menu or sub-menu items, write the code here. 
                //            /// Samples commented below

                //            //							MessageBox.Show(nm);
                //            int count_control=wd.ActiveWindow.Application.CommandBars[i].Controls.Count;
                //            //MessageBox.Show(count_control.ToString());						

                //            for(int j=1;j<=count_control;j++)
                //            {
                //                //MessageBox.Show(wd.ActiveWindow.Application.CommandBars[i].Controls[j].Caption.ToString());

                //                /// The following can be used to disable specific menuitems in the menubar	
                //                 wd.ActiveWindow.Application.CommandBars[i].Controls[j].Enabled=true;

                //                //MessageBox.Show(wd.ActiveWindow.Application.CommandBars[i].Controls[j].ToString());
                //                //MessageBox.Show(wd.ActiveWindow.Application.CommandBars[i].Controls[j].Caption);
                //                //MessageBox.Show(wd.ActiveWindow.Application.CommandBars[i].Controls[j].accChildCount.ToString());

                //                ///The following can be used to disable some or all the sub-menuitems in the menubar

                //                ////Office.CommandBarPopup c;
                //                ////c = (Office.CommandBarPopup)wd.ActiveWindow.Application.CommandBars[i].Controls[j];
                //                ////
                //                ////for(int k=1;k<=c.Controls.Count;k++)
                //                ////{
                //                ////	//MessageBox.Show(k.ToString()+" "+c.Controls[k].Caption + " -- " + c.Controls[k].DescriptionText + " -- " );
                //                ////	try
                //                ////	{
                //                ////		c.Controls[k].Enabled=false;
                //                ////		c.Controls["Close Window"].Enabled=false;
                //                ////	}
                //                ////	catch
                //                ////	{
                //                ////
                //                ////	}
                //                ////}
                //                //    wd.ActiveWindow.Application.CommandBars[i].Controls[j].Control	 Controls[0].Enabled=false;
                //                }
                //        }

                //        nm = "";
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.ToString());
                //    }
                //}

                # endregion

                // Show the word-document
                try
                {
                    if (wd.Visible == false)
                    {
                        wd.Visible = true;
                        wd.Activate();
                    }
                }
                catch
                {
                    // MessageBox.Show("Error: do not load the document into the control until the parent window is shown!");
                }

                SetWindowPos(this.wordWnd, this.Handle.ToInt32(), 0, 0, this.Bounds.Width, this.Bounds.Height, 39U);
                // SetWindowPos(wordWnd, this.Handle.ToInt32(), 0, 0, this.Bounds.Width, this.Bounds.Height, SWP_NOZORDER | SWP_NOMOVE | SWP_DRAWFRAME | SWP_NOSIZE);
                //Call onresize--I dont want to write the same lines twice
                OnResize();
                /// We want to remove the system menu also. The title bar is not visible, but we want to avoid accidental minimize, maximize, etc ..by disabling the system menu(Alt+Space)
                try
                {
                    int hMenu = GetSystemMenu(wordWnd, false);
                    if (hMenu > 0)
                    {
                        int menuItemCount = GetMenuItemCount(hMenu);
                        RemoveMenu(hMenu, menuItemCount - 1, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 2, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 3, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 4, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 5, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 6, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 7, MF_REMOVE | MF_BYPOSITION);
                        RemoveMenu(hMenu, menuItemCount - 8, MF_REMOVE | MF_BYPOSITION);
                        DrawMenuBar(wordWnd);
                    }
                }
                catch { }

                Parent.Focus();
            }
            deactivateevents = false;
            ////////////////////////////
            try
            {
                if (wd.Documents != null && wd.Documents.Count > 0)
                {
                    wd.ActiveWindow.ActivePane.DisplayRulers = true;
                    wd.ActiveWindow.DisplayRightRuler = true;
                    wd.ActiveWindow.DisplayScreenTips = false;
                    wd.ActiveWindow.DisplayVerticalRuler = true;

                    wd.ActiveWindow.ActivePane.View.Type = WdViewType.wdPrintView;
                }
            }
            catch { }
        }

        void BGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            blCheckPrintEnd = true;
        }

        bool blCheckPrintEnd = false;


        void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            while (!blCheckPrintEnd)
            {
                int ii = wd.BackgroundPrintingStatus;

                if (ii > 0)
                {

                    if (IsInGettingImageState)
                    {
                        if (AfterGetImageFromDocument != null)
                        {
                            AfterGetImageFromDocument(ii, new PrintEventArgs());
                        }
                    }
                    else
                    {
                        if (this.AfterPrintDocument != null)
                        {
                            this.AfterPrintDocument(ii, new PrintEventArgs());
                        }
                    }

                    blCheckPrintEnd = true;
                }
            }
        }

        void wd_DocumentChange()
        {

        }

        void wd_DocumentBeforePrint(Document Doc, ref bool Cancel)
        {
            blCheckPrintEnd = false;
            BGWorker.RunWorkerAsync();
        }

        public Image GetImageFromDocument()
        {
            return null;
        }

        public void NewDocument()
        {
            string fileName = GetUniqePath("NewDocument", Extension);
            object newTemplate = false;
            object missing = System.Reflection.Missing.Value;
            using (Novacode.DocX doc = Novacode.DocX.Create(fileName))
            {
                // Save this document to disk.
                doc.Save();
            }
            OpenWordDocument(fileName);

            return;

            deactivateevents = true;
            //            filename = t_filename;

            if (wd == null || IsNewInstance)
                InitialNewInstance(); // wd = new Word.ApplicationClass();

            if (document != null && wd.Documents.Count > 0)
            {
                try
                {
                    object dummy = null;
                    wd.Documents.Close(ref dummy, ref dummy, ref dummy);
                }
                catch { }
            }

            try
            {
                if (wd == null)
                {
                    throw new WordInstanceException();
                }

                if (wd.Documents == null)
                {
                    throw new DocumentInstanceException();
                }

                if (wd != null && wd.Documents != null)
                {
                    document = wd.Documents.AddOld(ref missing, ref newTemplate);
                }

                if (document == null)
                {
                    throw new ValidDocumentException();
                }
            }
            catch { }

            InitalInOtherForm();

        }

        /// <summary>
        /// restores Word.
        /// If the program crashed somehow.
        /// Sometimes Word saves it's temporary settings :(
        /// </summary>
        public void RestoreWord()
        {
            try
            {
                int counter = wd.ActiveWindow.Application.CommandBars.Count;
                for (int i = 0; i < counter; i++)
                {
                    try
                    {
                        wd.ActiveWindow.Application.CommandBars[i].Enabled = true;
                    }
                    catch { }
                }
            }
            catch { }
        }

        /// <summary>
        /// internal resize function
        /// utilizes the size of the surrounding control
        /// 
        /// optimzed for Word2000 but it works pretty good with WordXP too.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnResize()
        {
            //The original one that I used is shown below. Shows the complete window, but its buttons (min, max, restore) are disabled
            //// MoveWindow(wordWnd,0,0,this.Bounds.Width,this.Bounds.Height,true);
            // wordWnd = FindWindow("Opusapp", null);

            ///Change below
            ///The following one is better, if it works for you. We donot need the title bar any way. Based on a suggestion.
            int borderWidth = SystemInformation.Border3DSize.Width;
            int borderHeight = SystemInformation.Border3DSize.Height;
            int captionHeight = SystemInformation.CaptionHeight;
            int statusHeight = SystemInformation.ToolWindowCaptionHeight;

            // var p = this.PointToScreen(this.Location);

            var b = MoveWindow(
                wordWnd,
                -2 * borderWidth,
                -2 * borderHeight - captionHeight,
                this.Bounds.Width + 4 * borderWidth - 6,
                this.Bounds.Height + captionHeight + 4 * borderHeight + statusHeight - 24,
                true);
        }

        private void OnResize(object sender, System.EventArgs e)
        {
            if (sender is WordControlDocument)
            {
                if (((WordControlDocument)sender).wordWnd != this.wordWnd)
                {
                    return;
                }
            }
            OnResize();
        }

        /// Required. 
        /// Without this, the command bar buttons that have been disabled 
        /// will remain disabled permanently (does not occur at every machine or every time)
        //public void RestoreCommandBars()
        //{
        //try
        //{
        //    int counter = wd.ActiveWindow.Application.CommandBars.Count;
        //    for(int i = 1; i <= counter;i++)
        //    {
        //        try
        //        {

        //            String nm=wd.ActiveWindow.Application.CommandBars[i].Name;
        //            if(nm=="Standard")
        //            {
        //                int count_control=wd.ActiveWindow.Application.CommandBars[i].Controls.Count;
        //                for(int j=1;j<=2;j++)
        //                {
        //                    wd.ActiveWindow.Application.CommandBars[i].Controls[j].Enabled=true;
        //                }
        //            }
        //            if(nm=="Menu Bar")
        //            {
        //                wd.ActiveWindow.Application.CommandBars[i].Enabled=true;
        //            }
        //            nm="";
        //        }
        //        catch(Exception ex)
        //        {
        //            MessageBox.Show(ex.ToString());						
        //        }
        //    }
        //}
        //catch{}
        //}

        #region CustomMethod


        #region CreatNewDocument
        public void CreatNewDocument2()
        {
            if (document == null | CloseActiveDocument())
            {
                //Start Word and create a new document.
                try
                {
                    NewDocument();
                }
                catch (Exception ex) { String err = ex.Message; }
                //this.CurrentStream = GetStreamOfCurrentDocument();
                //LoadWordDocument(this.CurrentStream);
            }
        }
        #endregion

        #region OpenWordDocument

        //  باز کردن یک سند
        public string OpenWordDocument()
        {
            string filNm;
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "MS-Word Files (*.docx,*.dotx)|*.docx;*.dotx|Single File Web Page (*.mht;*.mhtml)|*.*|All|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filNm = openFileDialog1.FileName;

            }
            else
                return "";

            try
            {
                CloseActiveDocument();

            }
            catch { }
            finally
            {
                document = null;
                // WinWordControl.WinWordControl.wd = null;
                //*   WinWordControl.WinWordControl.wordWnd = 0;
            }
            try
            {
                //Load the template word document
                OpenWordDocument(filNm);

            }
            catch (Exception ex) { String err = ex.Message; }
            ListBookmarks();

            return filNm;
        }

        public void OpenWordDocument(byte[] FileBuffer, string title, FileExtension extension)
        {

            CloseActiveDocument();
            try
            {
                if (FileBuffer != null && FileBuffer.Length > 0)
                {
                    var strFilePath = GetUniqePath(title, extension);
                    File.WriteAllBytes(strFilePath, FileBuffer);

                    OpenWordDocument(strFilePath);


                }
            }
            catch (System.Threading.ThreadAbortException exception)
            {
                throw;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "باز کردن Word");
            }
        }

        public void OpenWordDocument(string t_filename)
        {
            deactivateevents = true;
            if (wd == null)
                InitialNewInstance();// wd = new Word.ApplicationClass();

            if (wd == null)
            {
                throw new WordInstanceException();
            }

            InitalInOtherForm();
            CloseActiveDocument();
            if (wd.Documents == null)
            {
                throw new DocumentInstanceException();
            }

            //object fileName = t_filename;
            //object newTemplate = false;
            //object docType = 0;
            //object isVisible = true;

            Object Template = t_filename;
            Object NewTemplate = false;
            Object DocumentType = WdNewDocumentType.wdNewBlankDocument;
            Object Visible = true;
            try
            {
                if (wd != null && wd.Documents != null)
                {
                    //document = wd.Documents.Add(ref fileName, ref newTemplate, ref docType, ref isVisible);
                    //document = wd.Documents.Add(ref Template, ref NewTemplate, ref DocumentType, ref Visible);
                    document = wd.Documents.Open(t_filename);

                    if (document == null)
                    {
                        CurrentFileInfo2 = null;
                        BaseFileInfo = null;
                        throw new ValidDocumentException();
                    }

                    document.ActiveWindow.View.Zoom.PageFit = WdPageFit.wdPageFitBestFit;
                    CurrentFileInfo2 = new FileInfo(t_filename);
                    if (BaseFileInfo == null)
                        BaseFileInfo = new FileInfo(t_filename);

                    OnResize();


                    try
                    {
                        if (wd.Visible == false)
                        {
                            wd.Visible = true;
                            wd.Activate();
                        }
                    }
                    catch
                    {
                        //MessageBox.Show("Error: do not load the document into the control until the parent window is shown!");
                    }

                    PrintView();
                }
            }
            catch (COMException ex)
            {
                MessageBox.Show("خطا در فرآخوانی" + "\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در فرآخوانی" + "\n" + ex.Message);
            }

            deactivateevents = false;
        }
        #endregion

        #region SaveDocument

        byte[] GetStreamOfCurrentDocument()
        {
            if (document == null)
                return null;

            Save();
            _CurrentText = ExtractWordText(CurrentFileInfo2.FullName);

            var strTempFile = Path.GetDirectoryName(CurrentFileInfo2.FullName) + "\\" + Path.GetFileNameWithoutExtension(CurrentFileInfo2.FullName) + "TempCopy" + Path.GetExtension(CurrentFileInfo2.FullName);
            var TempFile = CurrentFileInfo2.CopyTo(strTempFile, true);

            byte[] fileBuffer = System.IO.File.ReadAllBytes(TempFile.FullName);

            if (TempFile.Exists)
                TempFile.Delete();

            return fileBuffer;
        }

        //public string ExportWordDocText()
        //{
        //    GetStreamOfCurrentDocument();
        //    return this.CurrentText;
        //}

        string GetUniqePath(string title, FileExtension extension)
        {

            title = title.Replace("\\", "");
            title = title.Replace("\n", "");
            title = title.Replace("\r", "");
            title = title.Replace("/", "");
            title = title.Replace(":", "");
            title = title.Replace("*", "");
            title = title.Replace("?", "");
            title = title.Replace("؟", "");
            title = title.Replace("\"", "");
            title = title.Replace("<", "");
            title = title.Replace(">", "");
            title = title.Replace("|", "");



            if (System.IO.Directory.Exists(TempDirectory) == false)
                System.IO.Directory.CreateDirectory(TempDirectory);

            return string.Format(@"{0}\{1}_{2}.{3}", TempDirectory,
                                                     title,
                                                     new Random().NextDouble().ToString().Substring(2),
                                                     extension.ToString());
        }

        /// <summary>
        /// ذخیره کردن یک سند
        /// </summary>
        public void SaveAs()
        {
            try
            {
                document.Save();
            }
            catch { }
        }

        public FileInfo Save()
        {
            try
            {
                var strTempPath = "";
                if (CurrentFileInfo2 != null && CurrentFileInfo2.Exists)
                {
                    string path = Path.GetDirectoryName(CurrentFileInfo2.FullName);
                    string name = Path.GetFileNameWithoutExtension(CurrentFileInfo2.FullName);
                    strTempPath = path + "\\" + name + "." + Extension.ToString();
                }
                else
                {
                    strTempPath = GetUniqePath("NewFile", Extension);
                }

                ////this.CurrentFilePath = sFilename;
                object FileName = strTempPath;// sFilename;
                object FileFormat = System.Reflection.Missing.Value;
                if (Extension == FileExtension.mht)
                    FileFormat = WdSaveFormat.wdFormatWebArchive;
                //object LockComments = System.Reflection.Missing.Value;//false;
                //object AddToRecentFiles = System.Reflection.Missing.Value;// true;
                //object ReadOnlyRecommended = System.Reflection.Missing.Value;// false;
                //object EmbedTrueTypeFonts = System.Reflection.Missing.Value;//false;
                //object SaveNativePictureFormat = System.Reflection.Missing.Value;// true;
                //object SaveFormsData = System.Reflection.Missing.Value;//true;
                //object SaveAsAOCELetter = System.Reflection.Missing.Value;//false;
                //object Encoding = System.Reflection.Missing.Value;//Office.MsoEncoding.msoEncodingAutoDetect ;
                //object InsertLineBreaks = System.Reflection.Missing.Value;//false;
                //object AllowSubstitutions = System.Reflection.Missing.Value;// false;
                ////object LineEnding = Word.wdline.wdCRLF;
                //object AddBiDiMarks = System.Reflection.Missing.Value;// false;
                object Password = System.Reflection.Missing.Value;

                //wd.ActiveDocument.SaveAs(ref FileName, ref FileFormat, ref LockComments,
                //    ref Password, ref AddToRecentFiles, ref Password,
                //    ref ReadOnlyRecommended, ref EmbedTrueTypeFonts,
                //    ref SaveNativePictureFormat, ref SaveFormsData,
                //    ref SaveAsAOCELetter);

                wd.ActiveDocument.SaveAs(FileName: FileName, Password: ref Password, FileFormat: ref FileFormat);
                PrintView();
                CurrentFileInfo2 = new FileInfo(strTempPath);
                if (BaseFileInfo == null)
                {
                    BaseFileInfo = new FileInfo(strTempPath);
                }

                //  CloseActiveDocument();



                //try
                //{
                //    string text = "";
                //    Sbn.Words.GetDocText.Doc.TextLoader loader = new Sbn.Words.GetDocText.Doc.TextLoader(this.CurrentFilePath);// new TextLoader(dlgOpenFile.FileName);
                //    if (loader.LoadText(out text))
                //    {
                //        this.CurrentText = text;

                //    }
                //}
                //catch
                //{
                //}

                //fileBuffer = System.IO.File.ReadAllBytes(sFilename);

                //if (!closeAfterSave && fileBuffer != null)
                //{
                //    LoadWordDocument(fileBuffer);
                //}

                return CurrentFileInfo2;
            }
            catch (Exception ex)
            {
                //this.CloseActiveDocument();
                // throw;
                MessageBox.Show("خطا در ذخیره سازی" + "\n" + ex.Message);
            }

            return null;
        }
        #endregion

        public bool IsViewDocumentMap
        {
            set
            {
                if (wd != null && wd.ActiveWindow != null)
                {
                    wd.ActiveWindow.DocumentMap = value;
                }
            }
            get
            {
                if (wd != null && wd.ActiveWindow != null)
                {
                    return wd.ActiveWindow.DocumentMap;
                }
                return false;
            }
        }

        public void SetFullScreenView()
        {
            try
            {
                //  document.ActiveWindow.DocumentMap = false;
                document.ActiveWindow.ActivePane.View.FullScreen = true;
                document.ActiveWindow.View.Zoom.PageFit = WdPageFit.wdPageFitTextFit;// .wdPageFitBestFit;
                document.ActiveWindow.View.Panning = true;
                document.ActiveWindow.View.DisplayPageBoundaries = false;//.ShowTabs = false; عدم نمایش سربرگها
                document.ActiveWindow.View.DisplayBackgrounds = false;

                //   document.ActiveWindow.DocumentMapPercentWidth = 20;
                // document.ActiveWindow.DocumentMap = true;

                //document.Content.PageSetup.SectionDirection = WdSectionDirection.wdSectionDirectionRtl;
                //document.Paragraphs.ReadingOrder = WdReadingOrder.wdReadingOrderRtl;
                //var ff = document.ActiveWindow.

                // document.Paragraphs.Last.Range.Text

                //document.Content.ContentControls.

                //wd.ActiveWindow.ToggleRibbon(); //فعال یا غیر فعال نمودن منوی ریبون


                // document.ActiveWindow.DocumentMapPercentWidth = 10;
                // document.ActiveWindow.View.Zoom.Creator
                // document.ActiveWindow.View.Zoom.PageFit = WdPageFit.wdPageFitTextFit;
                //  document.ActiveWindow.View.Zoom.Percentage -= 30;
            }
            catch { }
        }

        #region PrintView
        public void PrintView()
        {
            try
            {
                document.ActiveWindow.ActivePane.View.Type = WdViewType.wdPrintView;
                document.ActiveWindow.DocumentMap = false;
                document.ActiveWindow.View.Panning = false;
            }
            catch { }
        }
        #endregion

        #region NormalView
        public void NormalView()
        {
            try
            {
                document.ActiveWindow.ActivePane.View.Type = WdViewType.wdNormalView;
            }
            catch { }
        }
        #endregion

        #region WebView
        public void WebView()
        {
            try
            {
                document.ActiveWindow.ActivePane.View.Type = WdViewType.wdWebView;
            }
            catch { }
        }
        #endregion

        #region PrintPreview
        //پیش نمایش چاپ 
        public void PrintPreview()
        {
            document.PrintPreview();
        }
        #endregion

        #region CreateTable

        public void CreateTable(ArrayList arr)
        {
            int row = ((Array)arr[0]).Length;
            int column = arr.Count;

            if (document == null)
                return;
            // Move to start of document.
            object rng1 = (object)document.Application.Selection.Range;

            Object defaultTableBehavior = Type.Missing;
            Object autoFitBehavior = Type.Missing;
            Table tbl = document.Application.Selection.Range.Tables.Add(document.Application.Selection.Range, row, column, ref defaultTableBehavior, ref autoFitBehavior);
            // Format the table and apply a style.

            //tbl.Range.Borders.OutsideColor = Word.WdColor.wdColorGold;
            //tbl.Borders.OutsideColor = Word.WdColor.wdColorGold;
            //tbl.Shading.BackgroundPatternColor = Word.WdColor.wdColorGold;
            //tbl.Shading.ForegroundPatternColor = Word.WdColor.wdColorPink;
            //   tbl.Range.Borders.InsideColor = Word.WdColor.wdColorGold;
            tbl.Range.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;// Word.WdLineStyle.wdLineStyleDouble;
            tbl.Range.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;


            // tbl.Columns.DistributeWidth();

            tbl.AutoFitBehavior(WdAutoFitBehavior.wdAutoFitContent);
            for (int i = 0; i < column; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    object objj = ((Array)arr[i]).GetValue(j);
                    if (objj != null)
                    {
                        Cell cc = tbl.Cell(j + 1, i + 1);
                        cc.Range.Text = objj.ToString();
                        //cc.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                        cc.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;

                    }

                }
            }

            // Insert text in cells.
            //   tbl.Cell(1, 1).Range.Text = "Document Property";
            //tbl.Cell(1, 2).Range.Text = "value";
            //tbl.Cell(2, 1).Range.Text = "Number of Words";
            //tbl.Cell(2, 2).Range.Text = objWinWordControl.document.Words.Count.ToString();
            //tbl.Cell(3, 1).Range.Text = "Number of Characters";
            //tbl.Cell(3, 2).Range.Text = objWinWordControl.document.Characters.Count.ToString();
            //tbl.Select();
            tbl.Range.Font.Size = 12;
            try
            {
                tbl.Range.Font.Name = "B Zar";
            }
            catch { }
        }
        // روال ایجاد جدول در سند ورد
        public void CreateTable(int row, int column)
        {
            if (document == null)
                return;
            // Move to start of document.
            object rng1 = (object)document.Application.Selection.Range;

            // Insert some text and paragraph marks.
            //objWinWordControl.document.Application.Selection.Range.InsertBefore("Document Statistics");
            //objWinWordControl.document.Application.Selection.Range.Font.Name = "Verdana";
            //objWinWordControl.document.Application.Selection.Range.Font.Size = 16;
            //objWinWordControl.document.Application.Selection.Range.InsertParagraphAfter();
            //objWinWordControl.document.Application.Selection.Range.InsertParagraphAfter();
            //objWinWordControl.document.Application.Selection.Range.SetRange(objWinWordControl.document.Application.Selection.Range.End, objWinWordControl.document.Application.Selection.Range.End);


            // Add the table.
            Object defaultTableBehavior = Type.Missing;
            Object autoFitBehavior = Type.Missing;
            Table tbl = document.Application.Selection.Range.Tables.Add(document.Application.Selection.Range, row, column, ref defaultTableBehavior, ref autoFitBehavior);
            // Format the table and apply a style.
            tbl.Range.Font.Size = 12;
            tbl.Range.Font.Name = "B Zar";
            //tbl.Range.Borders.OutsideColor = Word.WdColor.wdColorGold;
            //tbl.Borders.OutsideColor = Word.WdColor.wdColorGold;
            //tbl.Shading.BackgroundPatternColor = Word.WdColor.wdColorGold;
            //tbl.Shading.ForegroundPatternColor = Word.WdColor.wdColorPink;
            tbl.Range.Borders.InsideColor = WdColor.wdColorGold;
            tbl.Range.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleDouble;
            tbl.Range.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;

            tbl.Columns.DistributeWidth();


            // Insert text in cells.
            tbl.Cell(1, 1).Range.Text = "Document Property";
            //tbl.Cell(1, 2).Range.Text = "value";
            //tbl.Cell(2, 1).Range.Text = "Number of Words";
            //tbl.Cell(2, 2).Range.Text = objWinWordControl.document.Words.Count.ToString();
            //tbl.Cell(3, 1).Range.Text = "Number of Characters";
            //tbl.Cell(3, 2).Range.Text = objWinWordControl.document.Characters.Count.ToString();
            //tbl.Select();
        }
        #endregion

        #region InsertParagraph
        // روال ایجاد یک پاراگراف جدید
        public void InsertParagraph()
        {
            object rng1 = (object)document.Application.Selection.Range;
            document.Paragraphs.Add(ref rng1);
            //objWinWordControl.document.Application.Selection.Range.InsertParagraphAfter();
        }
        #endregion


        #region BookmarkText
        // روال یافتن لیست بوک مارکهای یک سند
        public void ListBookmarks()
        {
            if (document == null)
                return;

            StringWriter sw = new StringWriter();

            string[] bmarkArray = new string[20];
            int i = 0;
            foreach (Bookmark bmrk in document.Bookmarks)
            {
                sw.WriteLine("Name: {0}, Contents: {1}", bmrk.Name, bmrk.Range.Text);
                bmarkArray[i] = bmrk.Name;
                i += 1;

            }


            //MessageBox.Show(sw.ToString(), "بوک مارکها و محتویاتشان");
        }

        // نوشتن درون یک بوک مارک که از قبل مقدار دارد
        public void ReplaceBookmarkText(string BookmarkName, string NewText)
        {
            if (document.Bookmarks.Exists(BookmarkName))
            {

                //Object name = BookmarkName;
                //Range rng = document.Bookmarks.Item[ref name].Range;
                //rng.Text = NewText;
                //Object range = rng;
                //document.Bookmarks.Add(BookmarkName, ref range);
            }
        }

        #region AddNewBookMark
        // روال ایجاد یک بوک مارک جدید
        public void AddNewBookMark(string BookMarkName)
        {
            if (document == null)
                return;
            // Display Bookmarks.
            document.Bookmarks.ShowHidden = true;
            object rng1 = (object)document.Application.Selection.Range;
            document.Bookmarks.Add(BookMarkName, ref rng1);
            document.ActiveWindow.View.ShowBookmarks = true;

        }


        public void AddNewBookMarkInShapeContainer(string BookMarkName, string bookMarkText)
        {
            if (document == null)
                return;
            // Display Bookmarks.
            document.Bookmarks.ShowHidden = false;

            var bmSape = document.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, 200, 100, 200, 200);
            // var bmSape = document.Shapes.AddPicture(TempDirectory + "//" + "TempAddImage.jpeg", 200, 100, 200, 200);
            //  var bmSape = document.Shapes.AddTextEffect(MsoPresetTextEffect.msoTextEffect1, "fdfdf", "Arial", 20, MsoTriState.msoFalse, MsoTriState.msoFalse,10,10);
            bmSape.Line.Visible = MsoTriState.msoFalse;
            bmSape.Fill.Visible = MsoTriState.msoFalse;



            bmSape.TextFrame.ContainingRange.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleNone;
            bmSape.TextFrame.TextRange.Font.SizeBi = 16;
            bmSape.TextFrame.TextRange.Text = bookMarkText;
            bmSape.TextFrame.TextRange.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            bmSape.TextFrame.TextRange.HighlightColorIndex = WdColorIndex.wdTurquoise;



            var bm = document.Bookmarks.Add(BookMarkName, bmSape.TextFrame.TextRange);
            //bm.Range.
            // bmSape.TextFrame.ContainingRange.Font.Shading.BackgroundPatternColor = WdColor.wdColorYellow;
            document.ActiveWindow.View.ShowBookmarks = true;




        }


        #endregion

        #region WriteToBookMark

        public void WriteToBookMark(string BookMarkName, Image img)
        {
            if (document.Bookmarks.Exists(BookMarkName))
            {
                img.Save(TempDirectory + "//" + "TempAddImage.jpeg");

                Object name = BookMarkName;

                Bookmark bm = document.Bookmarks[name];//.get_Item(ref name);
                bm.Range.HighlightColorIndex = WdColorIndex.wdNoHighlight;
                Range rng = bm.Range;
                bm.Range.Text = "";
                var bmTemp = document.Bookmarks.Add(BookMarkName, rng);

                bmTemp.Range.InlineShapes.AddPicture(TempDirectory + "//" + "TempAddImage.jpeg", false, true);

                bmTemp.Range.HighlightColorIndex = WdColorIndex.wdNoHighlight;
                document.ActiveWindow.View.ShowBookmarks = false;
                //bm.Range.Text = "";

                document.Content.Select();
            }
        }

        public void WriteToBookMark(string BookMarkName, string BookMarkText)
        {
            try
            {
                Document wd = document;
                Application wa = wd.Application;
                int bookmark_cnt = wd.Bookmarks.Count;
                int i;
                for (i = 1; i <= bookmark_cnt; i++)
                {
                    object o = (object)i;
                    if (BookMarkName.ToLower().Trim() == wd.Bookmarks.get_Item(ref o).Name.ToLower().Trim())
                    {
                        wd.Bookmarks.get_Item(ref o).Select();
                        //wa.Selection.TypeText(BookMarkText);
                        int L = BookMarkText.Length;
                        int index = 0;
                        using (StringReader reader = new StringReader(BookMarkText))
                        {
                            var text = reader.ReadToEnd();
                            var paragraphs = text.Split('\n');
                            foreach (var item in paragraphs)
                            {
                                index++;
                                wa.Selection.TypeText(item);
                                if (index > 1)
                                    wa.Selection.TypeText("\n");
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                String err = ex.Message;
            }
        }
        #endregion
        #endregion

        #region PrintSelectedDocument
        // روال چاپ سند
        public void PrintSelectedDocument()
        {

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                Object background = Type.Missing;
                Object append = Type.Missing;
                Object range = Type.Missing;
                Object outputFileName = document.Name.ToString();
                Object from = Type.Missing;
                Object to = Type.Missing;
                Object item = Type.Missing;
                Object copies = Type.Missing;
                Object pages = Type.Missing;
                Object pageType = Type.Missing;
                Object printToFile = Type.Missing;
                Object collate = Type.Missing;
                //Object fileName = Type.Missing;
                Object activePrinterMacGX = Type.Missing;
                Object manualDuplexPrint = Type.Missing;
                Object printZoomColumn = Type.Missing;
                Object printZoomRow = Type.Missing;
                Object printZoomPaperWidth = Type.Missing;
                Object printZoomPaperHeight = Type.Missing;

                document.PrintOut(ref background, ref append,
                    ref range, ref outputFileName, ref from, ref to,
                    ref item, ref copies, ref pages, ref pageType,
                    ref printToFile, ref collate, ref activePrinterMacGX,
                    ref manualDuplexPrint, ref printZoomColumn, ref printZoomRow,
                    ref printZoomPaperWidth, ref printZoomPaperHeight);
            }
        }

        public Collection<PrintQueue> GetAllPrinter()
        {
            LocalPrintServer localPrintServer1 = new LocalPrintServer();

            // Retrieving collection of local printer on user machine
            PrintQueueCollection localPrinterCollection1 = localPrintServer1.GetPrintQueues();

            IEnumerator<PrintQueue> all = localPrinterCollection1.GetEnumerator();
            Collection<PrintQueue> AllPrinter = new Collection<PrintQueue>();

            while (all.MoveNext())
            {
                AllPrinter.Add(all.Current);
            }

            return AllPrinter;
        }

        FileInfo GetLastPrintedFile()
        {
            FileInfo RsultPath = null;

            foreach (string strpath in Registry.CurrentUser.GetSubKeyNames())
            {
                if (strpath.ToUpper() == "SOFTWARE")
                {
                    using (RegistryKey tempkey = Registry.CurrentUser.OpenSubKey(strpath))
                    {
                        foreach (string strpath1 in tempkey.GetSubKeyNames())
                        {
                            if (strpath1 == "ImagePrinter")
                            {
                                using (RegistryKey tempKey2 = tempkey.OpenSubKey(strpath1, true))
                                {
                                    object obj = tempKey2.GetValue("Path");
                                    System.IO.DirectoryInfo dInfo = new DirectoryInfo(obj.ToString());

                                    DateTime dt = new DateTime();
                                    foreach (FileInfo fInfo in dInfo.GetFiles())
                                    {
                                        if (fInfo.Name.Contains(document.Name))
                                        {
                                            if (dt < fInfo.LastWriteTime)
                                            {
                                                dt = fInfo.LastWriteTime;
                                                RsultPath = fInfo;//
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return RsultPath;
        }

        #region PrintSelectedDocumentToImage
        /// <summary>
        /// روال چاپ سند
        /// </summary>
        /// <param name="OpenReultFileDir"></param>
        /// <param name="ShowSelectPath"></param>
        /// <returns></returns>
        public Image PrintSelectedDocumentToImageOld(bool OpenReultFileDir, bool ShowSelectPath)
        {
            if (document == null)
                return null;

            # region CheckExistPrinter
            Collection<PrintQueue> AllPrinter = GetAllPrinter();
            bool HasMicrosoftOfficeDocumentImageWriter = false;
            bool HasImagePrinter = false;
            bool PrintWithFax = false;
            PrintQueue selectedPrinter = null;
            foreach (PrintQueue pq in AllPrinter)
            {
                if (pq.FullName == "Microsoft Office Document Image Writer")
                {
                    HasMicrosoftOfficeDocumentImageWriter = true;
                    selectedPrinter = pq;
                    break;
                }

                if (pq.FullName == "ImagePrinter")
                {
                    HasImagePrinter = true;
                    selectedPrinter = pq;
                    break;
                }
            }

            if (selectedPrinter == null)// !HasImagePrinter && !HasMicrosoftOfficeDocumentImageWriter)
            {
                //if (MessageBox.Show("هیچ پرینتر تصویری یافت نشد" + "\n" + "آیا مایل به استفاده از چاپگر فکس با کیفیت کمتر هستید", "خطا", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                //    return null;

                PrintWithFax = true;
            }
            # endregion CheckExistPrinter


            string sFilename = "Pic" + new Random().NextDouble().ToString().Substring(5, 3) + ".tiff";

            string strSavePath = TempDirectory + "\\" + sFilename;

            PrintDocument pd = new PrintDocument();
            pd.DocumentName = sFilename;

            if (!ShowSelectPath && !System.IO.Directory.Exists(TempDirectory))
            {
                System.IO.Directory.CreateDirectory(TempDirectory);
            }

            IsInGettingImageState = true;

            if (HasImagePrinter)
            {
                #region ImagePrinter
                try
                {
                    document.Application.ActivePrinter = "ImagePrinter";// "Microsoft Office Document Image Writer";

                    Object background = Type.Missing;
                    Object append = Type.Missing;
                    Object range = Type.Missing;
                    Object outputFileName = Type.Missing;// path + "\\" + FileName + ".tiff"; //objWinWordControl.document.Name.ToString();
                    Object from = Type.Missing;
                    Object to = Type.Missing;
                    Object item = Type.Missing;
                    Object copies = Type.Missing;
                    Object pages = Type.Missing;
                    Object pageType = Type.Missing;
                    Object printToFile = false;
                    Object collate = Type.Missing;
                    //Object fileName = Type.Missing;
                    Object activePrinterMacGX = Type.Missing;
                    Object manualDuplexPrint = Type.Missing;
                    Object printZoomColumn = Type.Missing;
                    Object printZoomRow = Type.Missing;
                    Object printZoomPaperWidth = Type.Missing;
                    Object printZoomPaperHeight = Type.Missing;

                    document.PrintOut(ref background, ref append,
                        ref range, ref outputFileName, ref from, ref to,
                        ref item, ref copies, ref pages, ref pageType,
                        ref printToFile, ref collate, ref activePrinterMacGX,
                        ref manualDuplexPrint, ref printZoomColumn, ref printZoomRow,
                        ref printZoomPaperWidth, ref printZoomPaperHeight);


                    #region CheckEndPrintJob

                    try
                    {
                        while (wd.BackgroundPrintingStatus > 0)
                        {
                            System.Threading.Thread.Sleep(250);
                        }
                    }
                    catch
                    { }

                    //try
                    //{
                    //    PrintQueue printQueue = null;

                    //    LocalPrintServer localPrintServer = new LocalPrintServer();


                    //    // Retrieving collection of local printer on user machine
                    //    PrintQueueCollection localPrinterCollection =
                    //        localPrintServer.GetPrintQueues();

                    //    foreach (PrintQueue ppqq in AllPrinter)
                    //    {
                    //        if (ppqq.FullName == "ImagePrinter")
                    //        {
                    //            printQueue = ppqq;
                    //            break;
                    //        }
                    //    }


                    //    //   printQueue = localPrintServer.DefaultPrintQueue;

                    //    bool jobEnd = false;


                    //    System.Printing.PrintJobInfoCollection jbs = printQueue.GetPrintJobInfoCollection();

                    //    System.Collections.IEnumerator JobsEnumerator =
                    //                        jbs.GetEnumerator();
                    //    while (JobsEnumerator.MoveNext())
                    //    {
                    //        PrintSystemJobInfo jbinf = (PrintSystemJobInfo)JobsEnumerator.Current;


                    //        if (jbinf.IsDeleting)
                    //        {
                    //            continue;
                    //        }



                    //        if (jbinf.IsCompleted)
                    //            break;



                    //        //while (jbinf.IsPrinting)// !jbinf.IsCompleted || jbinf.IsSpooling)
                    //        //{
                    //        //    try
                    //        //    {

                    //        //        // jbinf = (PrintSystemJobInfo)JobsEnumerator.Current;
                    //        //        System.Threading.Thread.Sleep(2000);
                    //        //    }
                    //        //    catch
                    //        //    { }
                    //        //}
                    //    }

                    //}
                    //catch
                    //{

                    //}
                    #endregion CheckEndPrintJob

                    System.Threading.Thread.Sleep(2000);
                    FileInfo RsultPath = GetLastPrintedFile();// null;

                    if (RsultPath == null)
                    {

                        System.Threading.Thread.Sleep(1000);
                        RsultPath = GetLastPrintedFile();// null;
                    }

                    string path = RsultPath.DirectoryName + "\\" + RsultPath.Name;

                    if (!path.Contains(".tiff"))
                    {
                        if (!path.Contains(".tif"))
                            path += ".tiff";
                    }

                    try
                    {
                        if (OpenReultFileDir)
                        {
                            ShowInFolder(path);
                        }
                    }
                    catch { }

                    int i = 1;
                    while (i < 10)
                    {
                        if (File.Exists(path))
                        {
                            try
                            {
                                Image img = Image.FromFile(path);
                                IsInGettingImageState = false;
                                return img;
                            }
                            catch { }
                        }
                        else { }
                        System.Threading.Thread.Sleep(1000);
                        i++;
                    }
                }
                catch { }
                #endregion ImagePrinter
            }
            else if (HasMicrosoftOfficeDocumentImageWriter || PrintWithFax)
            {
                #region MicrosoftOfficeDocumentImageWriter
                try
                {
                    if (ShowSelectPath && saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        strSavePath = saveFileDialog1.FileName + ".tiff";
                    }

                    if (selectedPrinter != null)
                        document.Application.ActivePrinter = selectedPrinter.FullName;
                    else
                        document.Application.ActivePrinter = "Fax";

                    Object background = Type.Missing;
                    Object append = Type.Missing;
                    Object range = Type.Missing;
                    Object outputFileName = strSavePath;// saveFileDialog1.FileName + ".tiff"; //objWinWordControl.document.Name.ToString();
                    Object from = Type.Missing;
                    Object to = Type.Missing;
                    Object item = Type.Missing;
                    Object copies = Type.Missing;
                    Object pages = Type.Missing;
                    Object pageType = Type.Missing;
                    Object printToFile = true;// Type.Missing;
                    Object collate = Type.Missing;

                    //Object fileName = Type.Missing;
                    Object activePrinterMacGX = Type.Missing;
                    Object manualDuplexPrint = Type.Missing;
                    Object printZoomColumn = Type.Missing;
                    Object printZoomRow = Type.Missing;
                    Object printZoomPaperWidth = Type.Missing;
                    Object printZoomPaperHeight = Type.Missing;

                    document.PrintOut(ref background, ref append,
                        ref range, ref outputFileName, ref from, ref to,
                        ref item, ref copies, ref pages, ref pageType,
                        ref printToFile, ref collate, ref activePrinterMacGX,
                        ref manualDuplexPrint, ref printZoomColumn, ref printZoomRow,
                        ref printZoomPaperWidth, ref printZoomPaperHeight);


                    #region checkEndJob


                    try
                    {
                        while (wd.BackgroundPrintingStatus > 0)
                        {
                            System.Threading.Thread.Sleep(250);
                        }
                    }
                    catch
                    { }


                    //try
                    //{
                    //    PrintQueue printQueue = null;

                    //    foreach (PrintQueue ppqq in AllPrinter)
                    //    {
                    //        if (ppqq.FullName == "Microsoft Office Document Image Writer")
                    //        {
                    //            printQueue = ppqq;
                    //            break;
                    //        }
                    //    }

                    //    bool jobEnd = false;


                    //    System.Printing.PrintJobInfoCollection jbs = printQueue.GetPrintJobInfoCollection();


                    //    System.Collections.IEnumerator JobsEnumerator =
                    //                        jbs.GetEnumerator();
                    //    while (JobsEnumerator.MoveNext())
                    //    {
                    //        PrintSystemJobInfo jbinf = (PrintSystemJobInfo)JobsEnumerator.Current;


                    //        if (jbinf.IsDeleting)
                    //        {
                    //            continue;
                    //        }


                    //        if (jbinf.IsCompleted)
                    //        {
                    //            break;
                    //        }


                    //        System.Threading.Thread.Sleep(2000);

                    //    }
                    //    System.Threading.Thread.Sleep(2000);

                    //}
                    //catch
                    //{

                    //}

                    #endregion checkEndJob

                    FileInfo RsultPath = new FileInfo(outputFileName.ToString());
                    string path = RsultPath.FullName;// RsultPath.DirectoryName + "\\" + RsultPath.Name;
                    if (!path.Contains(".tiff"))
                    {
                        path += ".tiff";
                    }

                    Image img = Image.FromFile(path);
                    try
                    {
                        if (OpenReultFileDir)
                        {
                            ShowInFolder(path);
                        }
                    }
                    catch { }

                    IsInGettingImageState = false;
                    return img;
                }
                catch { }
                #endregion MicrosoftOfficeDocumentImageWriter
            }

            IsInGettingImageState = false;
            return null;
        }

        public Image PrintSelectedDocumentToImage(bool OpenReultFileDir, bool ShowSelectPath)
        {
            if (document == null)
                return null;

            string sFilename = document.Name + Properties.Settings.Default.Extention;
            string strSavePath = TempDirectory + "\\" + sFilename;

            if (!ShowSelectPath && !System.IO.Directory.Exists(TempDirectory))
            {
                System.IO.Directory.CreateDirectory(TempDirectory);
            }

            IsInGettingImageState = true;

            try
            {
                document.Application.ActivePrinter = Properties.Settings.Default.ImagePrinter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + Properties.Settings.Default.ImagePrinter);
                return null;
            }

            //Object fileName = Type.Missing;
            Object background = Type.Missing;
            Object append = Type.Missing;
            Object range = Type.Missing;
            Object outputFileName = Type.Missing;// strSavePath;// saveFileDialog1.FileName + ".tiff"; //objWinWordControl.document.Name.ToString();
            Object from = Type.Missing;
            Object to = Type.Missing;
            Object item = Type.Missing;
            Object copies = Type.Missing;
            Object pages = Type.Missing;
            Object pageType = Type.Missing;
            Object printToFile = Type.Missing;// true;//
            Object collate = Type.Missing;


            if (Properties.Settings.Default.ImagePrinter == "Fax" ||
            Properties.Settings.Default.ImagePrinter == "Microsoft Office Document Image Writer")
            {

                saveFileDialog1.FileName = sFilename;
                if (ShowSelectPath && saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    strSavePath = saveFileDialog1.FileName;// + ".tif";
                }
                else
                {
                    Random rnd = new Random();
                    sFilename = "Pic" + rnd.NextDouble().ToString().Substring(5, 3) + Properties.Settings.Default.Extention;
                    strSavePath = TempDirectory + "\\" + sFilename;
                }

                outputFileName = strSavePath;
                printToFile = true;
            }

            //Object fileName = Type.Missing;
            Object activePrinterMacGX = Type.Missing;
            Object manualDuplexPrint = Type.Missing;
            Object printZoomColumn = Type.Missing;
            Object printZoomRow = Type.Missing;
            Object printZoomPaperWidth = Type.Missing;
            Object printZoomPaperHeight = Type.Missing;

            document.PrintOut(ref background, ref append,
                ref range, ref outputFileName, ref from, ref to,
                ref item, ref copies, ref pages, ref pageType,
                ref printToFile, ref collate, ref activePrinterMacGX,
                ref manualDuplexPrint, ref printZoomColumn, ref printZoomRow,
                ref printZoomPaperWidth, ref printZoomPaperHeight);


            #region MicrosoftOfficeDocumentImageWriter
            try
            {
                #region checkEndJob
                try
                {
                    while (wd.BackgroundPrintingStatus > 0)
                    {
                        System.Threading.Thread.Sleep(250);
                    }
                }
                catch
                { }


                PrintQueue _currentPrintQueue = null;
                foreach (var pc in GetAllPrinter())
                {
                    if (pc.FullName == Properties.Settings.Default.ImagePrinter)
                    {
                        _currentPrintQueue = pc;
                        break;
                    }
                }

                if (_currentPrintQueue != null)
                {
                    while (_currentPrintQueue.IsBusy || _currentPrintQueue.IsPrinting || _currentPrintQueue.IsProcessing || _currentPrintQueue.NumberOfJobs > 0)
                    {
                        _currentPrintQueue.Refresh();
                    }
                }

                #endregion checkEndJob

                outputFileName = strSavePath;



                //FileInfo RsultPath = new FileInfo(outputFileName.ToString());
                string path = outputFileName.ToString();// RsultPath.DirectoryName + "\\" + RsultPath.Name;
                //if (!path.Contains(".tiff"))
                //{
                //    path += ".tiff";
                //}

                //while (!File.Exists(path) )
                //{

                //}
                System.Threading.Thread.Sleep(1000);

                Image img = Image.FromFile(path);

                //using (FileStream imgstream = new FileStream(path,FileMode.Open,FileAccess.Read))
                //{
                //    if (imgstream != null)
                //        img = Image.FromStream(imgstream);
                //    img.Save("C:\\Sayban\\Temp\\1.tif");
                //}
                try
                {
                    // img.Save("C:\\Sayban\\Temp\\1.tif");
                    if (OpenReultFileDir)
                    {
                        ShowInFolder(path);
                        // System.Diagnostics.Process.Start(RsultPath.DirectoryName);
                    }
                }
                catch
                { }

                IsInGettingImageState = false;
                return img;

            }
            catch
            {

            }
            #endregion MicrosoftOfficeDocumentImageWriter

            IsInGettingImageState = false;
            return null;
        }

        public Collection<Image> SaveToImage()
        {
            var pages = document.ComputeStatistics(WdStatistic.wdStatisticPages);

            var list = ConvertDocToByteArrayListStatic(System.Drawing.Imaging.ImageFormat.Png);
            Collection<Image> mColImages = new Collection<Image>();

            for (int i = 0; i < pages; i++)
            {
                try
                {
                    using (var ms = new MemoryStream((byte[])(list[i])))
                    {
                        var image = System.Drawing.Image.FromStream(ms);
                        //var pngTarget = Path.ChangeExtension(target, "png");
                        //image.Save(pngTarget, System.Drawing.Imaging.ImageFormat.Png);
                        mColImages.Add(image);
                    }
                }
                catch { }
            }

            //for (int i = 0; i < mColImages.Count; i++)
            //{
            //    mColImages[i].Save(i + ".tiff");
            //}

            return mColImages;
        }

        public void ExportToPdf(string filePath)
        {
            if (document == null)
                return;

            document.ExportAsFixedFormat(filePath, WdExportFormat.wdExportFormatPDF);
        }

        public List<byte[]> ConvertDocToByteArrayListStatic(System.Drawing.Imaging.ImageFormat format)
        {
            List<byte[]> listOfImageArrays = new List<byte[]>();
            //var documentPath = "";


            //Opens the word document and fetch each page and converts to image
            foreach (Microsoft.Office.Interop.Word.Window window in document.Windows)
            {
                foreach (Microsoft.Office.Interop.Word.Pane pane in window.Panes)
                {
                    for (var i = 1; i <= pane.Pages.Count; i++)
                    {
                        Microsoft.Office.Interop.Word.Page page = null;
                        bool populated = false;
                        dynamic bits = null;

                        while (!populated)
                        {
                            try
                            {
                                // This !@#$ variable won't always be ready to spill its pages. If you step through
                                // the code, should always work.  If you just execute it, it will crash.  So what
                                // I am doing is letting the code to catch up a little by letting the thread sleep
                                // for X amount of milliseconds.  The second time around, this variable should populate ok.
                                page = pane.Pages[i];
                                populated = true;
                            }
                            catch (COMException ex)
                            {
                                Thread.Sleep(1);
                            }
                        }
                        try
                        {
                            bits = page.EnhMetaFileBits;
                        }
                        catch (COMException ex)
                        {
                            // EnhMetaFileBits may be blank becase there is no pane.Pages[2] availabe, even if
                            // pane.pages.Count clearly shows it as possible.  When that happens, we are done
                            // and should return what we have.  If that's not the case, throw an exception.
                            if (ex.ErrorCode == -2146822347)
                            {
                                return listOfImageArrays;
                            }
                            throw ex;
                        }

                        try
                        {
                            using (var ms = new MemoryStream((byte[])(bits)))
                            {
                                // ----------==={ MemoryStream to byte[] array ]===----------

                                // Method 1
                                /*
                                var image = System.Drawing.Image.FromStream(ms);
                                ImageConverter converter = new ImageConverter();
                                listOfImageArrays.Add((byte[])converter.ConvertTo(image, typeof(byte[])));
                                */

                                // Method 2, with automatic resize
                                var image = System.Drawing.Image.FromStream(ms);
                                byte[] imageData = new byte[0];
                                using (MemoryStream stream = new MemoryStream())
                                {
                                    image.Save(stream, format);
                                    stream.Close();
                                    imageData = stream.ToArray();
                                    listOfImageArrays.Add(imageData);
                                }

                                // Method 3, with manual resize
                                /*
                                var image = System.Drawing.Image.FromStream(ms);
                                //lblError.Text += "Physical Dimension: " + image.PhysicalDimension.ToString() + "<br>Height: " + image.Height.ToString() + "<br>Width: " + image.Width.ToString() + "<br>H Res: " + image.HorizontalResolution.ToString() + "<br>V Res: " + image.VerticalResolution.ToString() + "<br><br><br>";
                                float width = Convert.ToInt32(hfIdCardMaxWidth.Value);
                                float height = Convert.ToInt32(hfIdCardMaxHeight.Value);
                                float scale = Math.Min(width / image.Width, height / image.Height);
                                int resizedWidth = (int)Math.Round(image.Width * scale);
                                int resizedHeight = (int)Math.Round(image.Height * scale);
                                Bitmap myBitmap = new Bitmap(image, new Size(resizedWidth, resizedHeight));
                                myBitmap.SetResolution(600f, 600f);
                                byte[] imageData = new byte[0];
                                using (MemoryStream stream = new MemoryStream())
                                {
                                    myBitmap.Save(stream, ImageFormat.Png);
                                    stream.Close();
                                    imageData = stream.ToArray();
                                    listOfImageArrays.Add(imageData);
                                }
                                */
                            }
                        }
                        catch (System.Exception ex)
                        {

                            throw ex;
                        }
                    }
                }
            }
            return listOfImageArrays;
        }

        public void AddImageToBody(Image img)
        {
            if (img == null)
            {
                img = Image.FromFile("logo_template.png");
            }
            if (img == null)
                return;

            img.Save(TempDirectory + "//" + "TempAddImage.jpeg");


            //AddNewBookMarkInShapeContainer("bmSignImage", "تصویر امضا");
            //WriteToBookMark("bmSignImage", img);


            // InlineShape shape = wd.Selection.InlineShapes.AddPicture(TempDirectory + "//" + "TempAddImage.jpeg");

            var shape = document.Shapes.AddPicture(TempDirectory + "//" + "TempAddImage.jpeg", 200, 300, 100, 200);

            //  ExportToPdf();
        }

        public void AddBarCode(string str)
        {
            foreach (Section sec in document.Sections)
            {
                var footer = sec.Footers[WdHeaderFooterIndex.wdHeaderFooterPrimary];
                var top = footer.Range.Information[WdInformation.wdVerticalPositionRelativeToPage];

                var textBox = footer.Shapes.AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, 5, (float)top - 80, 20, 80);
                textBox.TextFrame.ContainingRange.Text = str;
                textBox.TextFrame.Orientation = MsoTextOrientation.msoTextOrientationVertical;
                textBox.TextFrame.MarginBottom = 0;
                textBox.TextFrame.MarginLeft = 0;
                textBox.TextFrame.MarginRight = 0;
                textBox.TextFrame.MarginTop = 0;
            }
        }

        public void ShowInFolder(string filePath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "explorer.exe";
            startInfo.Arguments = "/select,\"" + filePath + "\"";

            Process.Start(startInfo);
        }

        #endregion

        #endregion


        public void ExecuteCommandSync(object command)
        {
            try
            {
                var pInfo = new ProcessStartInfo("cmd", "/c " + command);
                pInfo.RedirectStandardOutput = true;
                pInfo.UseShellExecute = false;
                pInfo.CreateNoWindow = true;

                var process = new Process();
                process.StartInfo = pInfo;
                process.Start();

                var strREsult = process.StandardOutput.ReadToEnd();
            }
            catch { }
        }

        public void ExecuteCommandASync(string command)
        {
            try
            {
                var trd = new Thread(new ParameterizedThreadStart(ExecuteCommandSync));
                trd.IsBackground = true;
                trd.Priority = ThreadPriority.AboveNormal;
                trd.Start(command);
            }
            catch (ThreadStartException) { throw; }
            catch (ThreadAbortException) { }
            catch (Exception) { }
        }

        public static FileExtension ConvertToFileExtension(string fileExtension)
        {
            AdvancedControls.WordControlDocument.FileExtension type = AdvancedControls.WordControlDocument.FileExtension.doc;
            Enum.TryParse<Sbn.AdvancedControls.WordControlDocument.FileExtension>(fileExtension, false, out type);
            return type;
        }

        #endregion CustomMethod
    }
}
