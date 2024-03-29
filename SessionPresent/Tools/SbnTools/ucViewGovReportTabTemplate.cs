using System;
using System.Windows.Forms;
using Sbn.Products.GEP.GEPObject;
using System.Runtime.InteropServices;

namespace SessionPresent.Tools.SbnTools
{



    public partial class UcViewGovReportTabTemplate : UserControl
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        public event EventHandler<SessionItemEventArgs> ViewWordDocChanged;


        public int xPos { get; set; }
        public int yPos { get; set; }

        public bool IsViewWordDocument
        {
            get
            {
                if (tabControl1.SelectedTab == tpWord)
                    return true;

                return false;
            }
            set
            {
                if (value)
                {
                    tabControl1.SelectedTab = tpWord;

                }
                else
                {
                    tabControl1.SelectedTab = tpPictures;
                }
            }
        }

        public void OnViewWordDocChanged(SessionItemEventArgs e)
        {
            EventHandler<SessionItemEventArgs> handler = ViewWordDocChanged;
            if (handler != null) handler(this, e);
        }


        public GovernmentReport CurrentObject = new GovernmentReport();

        bool _readOnly;

        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                _readOnly = value;
                ucViewGovReportPic1.ReadOnly = value;
                ucWordDocEntityProp1.ReadOnly = value;
            }
        }

        public UcViewGovReportTabTemplate()
        {
            InitializeComponent();
        }

        public GovernmentReport Save(GovernmentReport obj)
        {

            return null;
        }

        public void FillObject(GovernmentReport gRep)
        {
            if (CurrentObject == gRep)
                return;
          //if (!SCUtility.CheckIsChangedObject(CurrentObject , gRep))
          //    return;

            ClearData();

            if (gRep == null)
                gRep = new GovernmentReport();

            ReadOnly = true;

            ucViewGovReportPic1.FillObject(gRep, false);

            CurrentObject = gRep;

         
        }

        public void FillObject(Offer off)
        {
            if (off != null && off.GovernReports != null && off.GovernReports.Count > 0)
                FillObject(off.GovernReports[0]);

        }


        public void RefreshAll()
        { 

        }

        public void ClearData()
        {
            CurrentObject = new GovernmentReport();
          //  this.ucOfferCommissionEntityProp1.ClearData();
            ucViewGovReportPic1.ClearData();
            ucWordDocEntityProp1.ClearData();
            tabControl1.SelectedTab = tpPictures;
        }
      
        private void UcViewGovReportTabTemplateLoad(object sender, EventArgs e)
        {
           // this.ucViewGovReportPic1.ThumbnailsView = false;

        }

        public void TabControl1Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tpPictures)
            {
                if (ucViewGovReportPic1.CurrentObject == null || ucViewGovReportPic1.CurrentObject.ID < 0)
                    ucViewGovReportPic1.FillObject(CurrentObject , false);

                OnViewWordDocChanged(new SessionItemEventArgs(false));

                IsViewWordDocument = false;

            }

            if (e.TabPage == tpWord && CurrentObject.ID > 0)
            {
                OnViewWordDocChanged(new SessionItemEventArgs(true));

                if (CurrentObject.WordDoc != null && CurrentObject.WordDoc.ID > 0)
                {
                    /*
                     ucWordDocEntityProp1.FillObject(CurrentObject.WordDoc, false, ReadOnly);

                     if (!ucWordDocEntityProp1.wordControlDocument1.wd.ActiveWindow.ActivePane.View.FullScreen)
                          ucWordDocEntityProp1.wordControlDocument1.SetFullScreenView();

                     ucWordDocEntityProp1.wordControlDocument1.wd.ActiveWindow.View.Panning = true;
                     */
                    if(IsViewWordDocument == false)
                        IsViewWordDocument = true;

                    System.Uri uri = new System.Uri(CurrentObject.WordDoc.FileVersions[0]._PhysicalPath + "\\Stream.mht");

                    if (webBrowser1.Url != uri)
                    {
                        
                        if (!System.IO.File.Exists(CurrentObject.WordDoc.FileVersions[0]._PhysicalPath + "\\Stream.mht"))
                            System.IO.File.Copy(CurrentObject.WordDoc.FileVersions[0]._PhysicalPath + "\\Stream.dat", CurrentObject.WordDoc.FileVersions[0]._PhysicalPath + "\\Stream.mht");


                        webBrowser1.Navigate(uri);

                    }

                    


                }
                else
                {
                    ucWordDocEntityProp1.ClearData();
                       
                }
            }

          
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab == tpWord)
            {
                TabControlEventArgs ee = new TabControlEventArgs(this.tabControl1.SelectedTab, 1, TabControlAction.Selected);

                TabControl1Selected(sender, ee);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.webBrowser1.GoForward();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.webBrowser1.GoBack();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SearchEle(this.webBrowser1.Document.Body, this.textBox1.Text);
        }

        public bool SearchEle(HtmlElement ele, string text)
        {
            foreach (HtmlElement child in ele.Children)
            {
                if (SearchEle(child, text))
                    return true;
            }
            if (!string.IsNullOrEmpty(ele.InnerText) && ele.InnerText.Contains(text))
            {
                ele.ScrollIntoView(true);
                return true;
            }

            return false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            try
            {
                SetFocus(webBrowser1.Handle);
                webBrowser1.Document.Focus();
                System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-us");
                InputLanguage inputLanguage = InputLanguage.FromCulture(ci);
                InputLanguage.CurrentInputLanguage = inputLanguage;
                //System.Threading.Thread.CurrentThread.CurrentCulture = ci;

                SendKeys.SendWait("^(f)");
                System.Threading.Thread.Sleep(2000);

                foreach (InputLanguage lang in InputLanguage.InstalledInputLanguages)
                {
                    if (lang.LayoutName.ToLower() == "persian")
                        InputLanguage.CurrentInputLanguage = lang ;
                }
                /*
                System.Globalization.CultureInfo cifa = new System.Globalization.CultureInfo("fa-IR");
                InputLanguage inputLanguagefa = InputLanguage.FromCulture(cifa);
                InputLanguage.CurrentInputLanguage = inputLanguagefa;
                */
            }
            catch 
            { 
            
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {

            this.webBrowser1.Document.Window.ScrollTo(0, 0);
            
        }

        private void webBrowser1_Validated(object sender, EventArgs e)
        {
            var doc = webBrowser1.Document;
            if(doc != null)
                ((System.Windows.Forms.HtmlDocument)doc).Window.ScrollTo(xPos, yPos);

        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {

   
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var browser = this.webBrowser1.ActiveXInstance as SHDocVw.InternetExplorer;
            browser.ExecWB(SHDocVw.OLECMDID.OLECMDID_OPTICAL_ZOOM, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT, 100, IntPtr.Zero);



        }
    }
}
