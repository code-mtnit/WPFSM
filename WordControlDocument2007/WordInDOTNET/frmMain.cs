using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Microsoft.Office.Interop.Word;
using Application = System.Windows.Forms.Application;

namespace WordInDOTNET
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtClientName;
		private System.Windows.Forms.TextBox txtRecNo;
		private System.Windows.Forms.Button btnClientName;
		private System.Windows.Forms.Button btnRecNo;
		private System.Windows.Forms.Button btnCurrentDate;
		private System.Windows.Forms.Button btnMarkError;
		private System.Windows.Forms.Button btnRemoveError;
		private System.Windows.Forms.Button btnSaveDocument;
		private System.Windows.Forms.Button btnRemoveBookMarks;
		private System.Windows.Forms.Button btnPrintView;
		private System.Windows.Forms.Button btnNormalView;
		private System.Windows.Forms.Button btnWebView;
		private System.Windows.Forms.Button btnShowMenuBar;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        
        private Sbn.AdvancedControls.WordControlDocument.WordControlDocument wordControlDocument1;
        private Button button10;
        private Button button11;
        private Button button12;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
        
		public frmMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

		  
            
		    //
		    // TODO: Add any constructor code after InitializeComponent call
		    //
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
         //   string ss = Clipboard.GetText();
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtClientName = new System.Windows.Forms.TextBox();
            this.txtRecNo = new System.Windows.Forms.TextBox();
            this.btnClientName = new System.Windows.Forms.Button();
            this.btnRecNo = new System.Windows.Forms.Button();
            this.btnCurrentDate = new System.Windows.Forms.Button();
            this.btnMarkError = new System.Windows.Forms.Button();
            this.btnRemoveError = new System.Windows.Forms.Button();
            this.btnSaveDocument = new System.Windows.Forms.Button();
            this.btnRemoveBookMarks = new System.Windows.Forms.Button();
            this.btnPrintView = new System.Windows.Forms.Button();
            this.btnNormalView = new System.Windows.Forms.Button();
            this.btnWebView = new System.Windows.Forms.Button();
            this.btnShowMenuBar = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.wordControlDocument1 = new Sbn.AdvancedControls.WordControlDocument.WordControlDocument();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button1.Location = new System.Drawing.Point(868, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "باز کردن سند";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(860, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Client\'s Name";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(860, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Record Number";
            // 
            // txtClientName
            // 
            this.txtClientName.Location = new System.Drawing.Point(868, 68);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Size = new System.Drawing.Size(100, 20);
            this.txtClientName.TabIndex = 6;
            // 
            // txtRecNo
            // 
            this.txtRecNo.Location = new System.Drawing.Point(868, 124);
            this.txtRecNo.Name = "txtRecNo";
            this.txtRecNo.Size = new System.Drawing.Size(100, 20);
            this.txtRecNo.TabIndex = 7;
            // 
            // btnClientName
            // 
            this.btnClientName.Location = new System.Drawing.Point(972, 68);
            this.btnClientName.Name = "btnClientName";
            this.btnClientName.Size = new System.Drawing.Size(32, 16);
            this.btnClientName.TabIndex = 9;
            this.btnClientName.Text = "-->";
            this.btnClientName.Click += new System.EventHandler(this.btnClientName_Click);
            // 
            // btnRecNo
            // 
            this.btnRecNo.Location = new System.Drawing.Point(972, 124);
            this.btnRecNo.Name = "btnRecNo";
            this.btnRecNo.Size = new System.Drawing.Size(32, 16);
            this.btnRecNo.TabIndex = 10;
            this.btnRecNo.Text = "-->";
            this.btnRecNo.Click += new System.EventHandler(this.btnRecNo_Click);
            // 
            // btnCurrentDate
            // 
            this.btnCurrentDate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCurrentDate.Location = new System.Drawing.Point(860, 164);
            this.btnCurrentDate.Name = "btnCurrentDate";
            this.btnCurrentDate.Size = new System.Drawing.Size(128, 24);
            this.btnCurrentDate.TabIndex = 11;
            this.btnCurrentDate.Text = "تاریخ جاری سیستم";
            this.btnCurrentDate.Click += new System.EventHandler(this.btnCurrentDate_Click);
            // 
            // btnMarkError
            // 
            this.btnMarkError.Location = new System.Drawing.Point(884, 204);
            this.btnMarkError.Name = "btnMarkError";
            this.btnMarkError.Size = new System.Drawing.Size(75, 23);
            this.btnMarkError.TabIndex = 12;
            this.btnMarkError.Text = "Mark Error";
            this.btnMarkError.Click += new System.EventHandler(this.btnMarkError_Click);
            // 
            // btnRemoveError
            // 
            this.btnRemoveError.Location = new System.Drawing.Point(860, 244);
            this.btnRemoveError.Name = "btnRemoveError";
            this.btnRemoveError.Size = new System.Drawing.Size(152, 23);
            this.btnRemoveError.TabIndex = 15;
            this.btnRemoveError.Text = "Remove Error Highlighting";
            this.btnRemoveError.Click += new System.EventHandler(this.btnRemoveError_Click);
            // 
            // btnSaveDocument
            // 
            this.btnSaveDocument.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveDocument.Location = new System.Drawing.Point(876, 324);
            this.btnSaveDocument.Name = "btnSaveDocument";
            this.btnSaveDocument.Size = new System.Drawing.Size(112, 23);
            this.btnSaveDocument.TabIndex = 16;
            this.btnSaveDocument.Text = "ذخیره نامه";
            this.btnSaveDocument.Click += new System.EventHandler(this.btnSaveDocument_Click);
            // 
            // btnRemoveBookMarks
            // 
            this.btnRemoveBookMarks.Location = new System.Drawing.Point(860, 284);
            this.btnRemoveBookMarks.Name = "btnRemoveBookMarks";
            this.btnRemoveBookMarks.Size = new System.Drawing.Size(152, 23);
            this.btnRemoveBookMarks.TabIndex = 17;
            this.btnRemoveBookMarks.Text = "Remove bookmarks";
            this.btnRemoveBookMarks.Click += new System.EventHandler(this.btnRemoveBookMarks_Click);
            // 
            // btnPrintView
            // 
            this.btnPrintView.Location = new System.Drawing.Point(876, 372);
            this.btnPrintView.Name = "btnPrintView";
            this.btnPrintView.Size = new System.Drawing.Size(120, 23);
            this.btnPrintView.TabIndex = 18;
            this.btnPrintView.Text = "Print Layout View";
            this.btnPrintView.Click += new System.EventHandler(this.btnPrintView_Click);
            // 
            // btnNormalView
            // 
            this.btnNormalView.Location = new System.Drawing.Point(876, 401);
            this.btnNormalView.Name = "btnNormalView";
            this.btnNormalView.Size = new System.Drawing.Size(120, 23);
            this.btnNormalView.TabIndex = 19;
            this.btnNormalView.Text = "Normal View";
            this.btnNormalView.Click += new System.EventHandler(this.btnNormalView_Click);
            // 
            // btnWebView
            // 
            this.btnWebView.Location = new System.Drawing.Point(876, 430);
            this.btnWebView.Name = "btnWebView";
            this.btnWebView.Size = new System.Drawing.Size(120, 23);
            this.btnWebView.TabIndex = 20;
            this.btnWebView.Text = "Web View";
            this.btnWebView.Click += new System.EventHandler(this.btnWebView_Click);
            // 
            // btnShowMenuBar
            // 
            this.btnShowMenuBar.Location = new System.Drawing.Point(868, 485);
            this.btnShowMenuBar.Name = "btnShowMenuBar";
            this.btnShowMenuBar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnShowMenuBar.Size = new System.Drawing.Size(136, 23);
            this.btnShowMenuBar.TabIndex = 21;
            this.btnShowMenuBar.Text = "Show MenuBar";
            this.btnShowMenuBar.Click += new System.EventHandler(this.btnShowMenuBar_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button2.Location = new System.Drawing.Point(884, 530);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 23);
            this.button2.TabIndex = 25;
            this.button2.Text = "جدید";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button3.Location = new System.Drawing.Point(884, 559);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 23);
            this.button3.TabIndex = 26;
            this.button3.Text = "addBookmark";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button4.Location = new System.Drawing.Point(884, 588);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(96, 23);
            this.button4.TabIndex = 27;
            this.button4.Text = "بستن";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button5.Location = new System.Drawing.Point(884, 617);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(96, 62);
            this.button5.TabIndex = 28;
            this.button5.Text = "جدول";
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button6.Location = new System.Drawing.Point(884, 685);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(96, 37);
            this.button6.TabIndex = 29;
            this.button6.Text = "Stream";
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button7.Location = new System.Drawing.Point(765, 707);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(96, 23);
            this.button7.TabIndex = 30;
            this.button7.Text = "تصویر";
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button8.Location = new System.Drawing.Point(633, 707);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(96, 23);
            this.button8.TabIndex = 31;
            this.button8.Text = "ClipBorad";
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button9.Location = new System.Drawing.Point(283, 707);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(96, 23);
            this.button9.TabIndex = 32;
            this.button9.Text = "frmWord";
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // wordControlDocument1
            // 
            this.wordControlDocument1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.wordControlDocument1.DocumentChanged = true;
            this.wordControlDocument1.DocumnetFileType = Sbn.AdvancedControls.WordControlDocument.FileTypes.doc;
            this.wordControlDocument1.ImageFileType = Sbn.AdvancedControls.WordControlDocument.FileTypes.doc;
            this.wordControlDocument1.IsInGettingImageState = false;
            this.wordControlDocument1.IsNewInstance = true;
            this.wordControlDocument1.IsViewDocumentMap = false;
            this.wordControlDocument1.Location = new System.Drawing.Point(12, 19);
            this.wordControlDocument1.Name = "wordControlDocument1";
            this.wordControlDocument1.ReadOnly = false;
            this.wordControlDocument1.Size = new System.Drawing.Size(842, 682);
            this.wordControlDocument1.TabIndex = 33;
            this.wordControlDocument1.AfterPrintDocument += new System.Drawing.Printing.PrintEventHandler(this.wordControlDocument1_AfterPrintDocument);
            // 
            // button10
            // 
            this.button10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button10.Location = new System.Drawing.Point(12, 707);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(96, 23);
            this.button10.TabIndex = 34;
            this.button10.Text = "MDI";
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button11.Location = new System.Drawing.Point(492, 707);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(96, 23);
            this.button11.TabIndex = 35;
            this.button11.Text = "تنظیمات";
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button12.Location = new System.Drawing.Point(114, 707);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(163, 23);
            this.button12.TabIndex = 32;
            this.button12.Text = "WordDocViewer";
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1016, 734);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.wordControlDocument1);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnShowMenuBar);
            this.Controls.Add(this.btnWebView);
            this.Controls.Add(this.btnNormalView);
            this.Controls.Add(this.btnPrintView);
            this.Controls.Add(this.btnRemoveBookMarks);
            this.Controls.Add(this.btnSaveDocument);
            this.Controls.Add(this.btnRemoveError);
            this.Controls.Add(this.btnMarkError);
            this.Controls.Add(this.btnCurrentDate);
            this.Controls.Add(this.btnRecNo);
            this.Controls.Add(this.btnClientName);
            this.Controls.Add(this.txtRecNo);
            this.Controls.Add(this.txtClientName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "frmMain";
            this.Text = "Main";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}

		private void frmMain_Load(object sender, System.EventArgs e)
		{
		}

        public void ListBookmarks()
        {
            StringWriter sw = new StringWriter();

            string[] bmarkArray = new string[20];
            int i = 0;
            foreach (Bookmark bmrk in wordControlDocument1.document.Bookmarks)
            {
                sw.WriteLine("Name: {0}, Contents: {1}", bmrk.Name, bmrk.Range.Text);
                bmarkArray[i] = bmrk.Name;
                i++;
                
            }
            MessageBox.Show(sw.ToString(), "Bookmarks and Contents");
        }

		/// <summary>
		/// Loads the document. If it is already loaded, it will first unload and load again.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_Click(object sender, System.EventArgs e)
		{

			string filNm;
			openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "MS-Word Files (*.docx,*.dotx) | *.docx;*.dotx;";
			if(openFileDialog1.ShowDialog() == DialogResult.OK)
			{
            filNm = openFileDialog1.FileName;

            this.wordControlDocument1.OpenWordDocument(filNm);
			}
			else return;
            //MessageBox.Show("لطفا تا بارگذاری کامل سند کمی صبر نمایید");
            //try
            //{
            //    objWinWordControl.CloseControl();

            //}
            //catch{}
            //finally
            //{ 
            //    objWinWordControl.document=null;
            //    WinWordControl.WinWordControl.wd=null;
            //    WinWordControl.WinWordControl.wordWnd=0;
            //}
            //try
            //{

            //    //Load the template used for testing.
            //    objWinWordControl.LoadDocument(filNm);
            //}
            //catch(Exception ex){String err = ex.Message;}
            //btnClientName.Enabled=true;
            //btnRecNo.Enabled=true;
            //btnCurrentDate.Enabled=true;
            //ListBookmarks();
		}

		private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{

            

			wordControlDocument1.RestoreCommandBars();
            wordControlDocument1.CloseActiveDocument();
		}

		
		/// <summary>
		/// Searches the bookmark by name and places text at the text
		/// </summary>
		/// <param name="BookMarkName"></param>
		/// <param name="BookMarkText"></param>
 
		private void WriteToBookMark(string BookMarkName, string BookMarkText)
		{
			try
			{
                Document wd = wordControlDocument1.document;
				Microsoft.Office.Interop.Word.Application wa = wd.Application;
				int bookmark_cnt = wd.Bookmarks.Count;
				int i;
				for(i=1;i<=bookmark_cnt;i++)
				{
                    //object o = (object)i;
                    //if(BookMarkName.ToLower().Trim() == wd.Bookmarks.Item(ref o).Name.ToLower().Trim())
                    //{
                    //    wd.Bookmarks.Item(ref o).Select();
                    //    wa.Selection.TypeText(BookMarkText);
                    //}
				}
			}
			catch(Exception ex)
			{
				String err = ex.Message;
			}
		}


		/// <summary>
		/// Custom: Writes the client name at the bookmark.
		/// This template is pre-defined with 3 bookmarks.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClientName_Click(object sender, System.EventArgs e)
		{
			WriteToBookMark("b",txtClientName.Text);
            //bmrkClientName
			btnClientName.Enabled=false;
		}

		/// <summary>
		/// Custom: Writes the client name at the bookmark.
		/// This template is pre-defined with 3 bookmarks.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRecNo_Click(object sender, System.EventArgs e)
		{
			WriteToBookMark("a",txtRecNo.Text);
            //bmrkRecNo
			btnRecNo.Enabled=false;
		}

		/// <summary>
		/// Custom: Writes the client name at the bookmark.
		/// This template is pre-defined with 3 bookmarks.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCurrentDate_Click(object sender, System.EventArgs e)
		{
			WriteToBookMark("bmrkCurrentDate",DateTime.Now.ToString());
			btnCurrentDate.Enabled=false;
		}


		/// <summary>
		/// Mark the error by selecting some text
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnMarkError_Click(object sender, System.EventArgs e)
		{
			try
			{
                string strText = wordControlDocument1.document.Application.Selection.Text;
				frmMarkError f= new frmMarkError();
				f.txtChanged.Text = strText;
				f.txtOriginal.Text = strText;
				DialogResult dr= f.ShowDialog();

				if(dr==DialogResult.OK)
				{
                    wordControlDocument1.document.Application.Selection.Text = f.txtChanged.Text;
                    wordControlDocument1.document.Application.Selection.FormattedText.HighlightColorIndex = WdColorIndex.wdYellow;
					string bkmrkname = "bkmrk_err_" + DateTime.Now.Day.ToString().PadLeft(2,'0') + DateTime.Now.Month.ToString().PadLeft(2,'0')+ DateTime.Now.Year.ToString()+ DateTime.Now.Hour.ToString().PadLeft(2,'0')+ DateTime.Now.Minute.ToString().PadLeft(2,'0')+ DateTime.Now.Second.ToString().PadLeft(2,'0')+ DateTime.Now.Ticks.ToString().PadLeft(8,'0');
                    object o = wordControlDocument1.document.Application.Selection.Range;
                    wordControlDocument1.document.Bookmarks.Add(bkmrkname, ref o);
                    wordControlDocument1.document.Application.Selection.FormattedText.HighlightColorIndex = WdColorIndex.wdYellow;
				}

				// Do something else like making entry to database.
			}
			catch{}
		}

		/// <summary>
		/// Remove the Error bookmarks
		/// This might be required if the document is being sent for further quality check.
		/// In that case, the errors marked by current QA must not be shown to next QA
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRemoveError_Click(object sender, System.EventArgs e)
		{
			try
			{
				string BookMarkName="";
                Document wd = wordControlDocument1.document;
				Microsoft.Office.Interop.Word.Application wa = wd.Application;
				int bookmark_cnt = wd.Bookmarks.Count;
				int i;
				for(i=1;i<=bookmark_cnt;i++)
				{
                    //object o = (object)i;
                    //BookMarkName=wd.Bookmarks.Item(ref o).Name;
                    //if(BookMarkName.Substring(0,10)=="bkmrk_err_")
                    //{
                    //    wd.Bookmarks.Item(ref o).Select();
                    //    wa.Selection.FormattedText.HighlightColorIndex = Word.WdColorIndex.wdNoHighlight;
                    //}
				}

				//Do something else
			}
			catch(Exception ex)
			{
				String err = ex.Message;
			}		
		}

		/// <summary>
		/// Save the document. Calls the Word's save method
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSaveDocument_Click(object sender, System.EventArgs e)
		{
			try
			{
				//objWinWordControl.docum();
                byte[] b = null;
                
                var f = this.wordControlDocument1.Save();
               // this.wordControlDocument1.LoadWordDocument(b);
			}
			catch
			{}
		}

		/// <summary>
		/// Clear all the bookmarks. 
		/// This may be required before submitting the transcript to the client.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRemoveBookMarks_Click(object sender, System.EventArgs e)
		{
			try
			{
                Document wd = wordControlDocument1.document;
				Microsoft.Office.Interop.Word.Application wa = wd.Application;
				int bookmark_cnt = wd.Bookmarks.Count;
				int i;
				for(i=1;i<=bookmark_cnt;i++)
				{
                    //object o = (object)wd.Bookmarks.Count;
                    //wd.Bookmarks.Item(ref o).Delete();
				}

				MessageBox.Show("Bookmarks removed");
			}
			catch(Exception ex)
			{
				String err = ex.Message;
				MessageBox.Show("Error occured while removing bookmarks");
			}		
		}

		/// <summary>
		/// Change the view
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrintView_Click(object sender, System.EventArgs e)
		{
			try
			{
                wordControlDocument1.document.ActiveWindow.ActivePane.View.Type = WdViewType.wdPrintView;
			}
			catch{}
		}


		/// <summary>
		/// Change the view
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNormalView_Click(object sender, System.EventArgs e)
		{
			try
			{
                wordControlDocument1.document.ActiveWindow.ActivePane.View.Type = WdViewType.wdNormalView;
			}
			catch{}
		}


		/// <summary>
		/// Change the view
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnWebView_Click(object sender, System.EventArgs e)
		{
			try
			{
                wordControlDocument1.document.ActiveWindow.ActivePane.View.Type = WdViewType.wdWebView;
			}
			catch{}
		}

		/// <summary>
		/// If you want to show the menubar to the user. 
		/// (Useful in cases where too many functionalities of word are being used)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnShowMenuBar_Click(object sender, System.EventArgs e)
		{
			try
			{
                wordControlDocument1.document.ActiveWindow.Application.CommandBars["Menu Bar"].Enabled = !this.wordControlDocument1.document.ActiveWindow.Application.CommandBars["Menu Bar"].Enabled;
				//objWinWordControl.document.ActiveWindow.Application.CommandBars["Menu Bar"].Enabled=true;
			}
			catch{}
		}

        private void button2_Click(object sender, EventArgs e)
        {

            //WordControlDocument.WinWordLoader ww = new WordControlDocument.WinWordLoader();
            ////Word.Application ww1 = new Word.Application();
            ////ww1.Visible = true;

            //ww.Application.Visible = true;
           // ww.Application.ShowMe();
           // byte[] b = this.wordControlDocument1.GetStreamOfCurrentDocument();
          //  this.wordControlDocument1.SaveAndCloseDocument(ref b);

           this.wordControlDocument1.CreatNewDocument2();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.wordControlDocument1.AddNewBookMark("test");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.wordControlDocument1.CloseActiveDocument();
        }

        private void button5_Click(object sender, EventArgs e)
        {
           // this.wordControlDocument1.CreateTable(5, 5);

            string[] strArry1 = new string[3];
            string[] strArry2 = new string[3];

            strArry1[0] = "ردیف";
            strArry1[1] = "1";
            strArry1[2] = "2";
            strArry2[2] = "تصویب شد";
            strArry2[1] = "تصویب شد";
            strArry2[0] = "تصویب شدارتنیالتنسبیلنیملتبنیمتلشمنیبتلشمنتلبمشنتلبشمینکتلبمینشلتبمنشیتلشمبنلوئدرتنارتثهشلتشنمیب";

            ArrayList arList = new ArrayList();
            arList.Add(strArry1);
            arList.Add(strArry2);
            this.wordControlDocument1.CreateTable(arList);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            byte[] bb = wordControlDocument1.CurrentStream;

            this.wordControlDocument1.OpenWordDocument(bb  ,"");


            
        }

        private void button7_Click(object sender, EventArgs e)
        {
           this.wordControlDocument1.PrintSelectedDocumentToImage(true , true);
          //  this.wordControlDocument1.PrintSelectedDocument();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.wordControlDocument1.ReadOnly = !this.wordControlDocument1.ReadOnly;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            frmWord frm = new frmWord();
           
            frm.Show();
            frm.wordControlDocumentNew21.CreatNewDocument2();

            //if (frm.ShowDialog() == DialogResult.OK)
            //{ 

            //}

            //frm.Dispose();
        }

        private void wordControlDocument1_AfterPrintDocument(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            MessageBox.Show("Print" + " " + sender.ToString() + " " + "Pages");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MDITest frm = new MDITest();
            frm.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            wordControlDocument1.SetSetting();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var frm = new frmWordDocViewer();

            frm.Show();
            frm.wordControlDocumentNew21.CreatNewDocument2();
        }
		
	}
}



/*
 
 

*/