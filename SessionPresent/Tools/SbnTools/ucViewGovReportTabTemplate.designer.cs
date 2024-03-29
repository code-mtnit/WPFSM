
namespace SessionPresent.Tools.SbnTools
{
    partial class UcViewGovReportTabTemplate
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpPictures = new System.Windows.Forms.TabPage();
            this.ucViewGovReportPic1 = new SessionPresent.Tools.SbnTools.UcViewGovReportPic();
            this.tpWord = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.ucWordDocEntityProp1 = new SessionPresent.Tools.SbnTools.FolderWordDocument.ucWordDocEntityProp();
            this.button5 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tpPictures.SuspendLayout();
            this.tpWord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tpPictures);
            this.tabControl1.Controls.Add(this.tpWord);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tabControl1.RightToLeftLayout = true;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1096, 526);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.TabControl1Selected);
            // 
            // tpPictures
            // 
            this.tpPictures.BackColor = System.Drawing.Color.Transparent;
            this.tpPictures.Controls.Add(this.ucViewGovReportPic1);
            this.tpPictures.Location = new System.Drawing.Point(4, 4);
            this.tpPictures.Name = "tpPictures";
            this.tpPictures.Padding = new System.Windows.Forms.Padding(3);
            this.tpPictures.Size = new System.Drawing.Size(922, 500);
            this.tpPictures.TabIndex = 1;
            this.tpPictures.Text = "تصاویر گزارش ";
            // 
            // ucViewGovReportPic1
            // 
            this.ucViewGovReportPic1.AllowEditImage = false;
            this.ucViewGovReportPic1.AllowFilip = false;
            this.ucViewGovReportPic1.AllowMagnifair = true;
            this.ucViewGovReportPic1.AllowMeargeImage = false;
            this.ucViewGovReportPic1.AllowMoveImage = false;
            this.ucViewGovReportPic1.AllowNavigator = true;
            this.ucViewGovReportPic1.AllowOpenSaveImage = false;
            this.ucViewGovReportPic1.AllowPrint = false;
            this.ucViewGovReportPic1.AllowRemoveImage = false;
            this.ucViewGovReportPic1.AllowRotate = true;
            this.ucViewGovReportPic1.AllowScanImage = false;
            this.ucViewGovReportPic1.AllowSlideView = false;
            this.ucViewGovReportPic1.AllowVerticalHorizontal = false;
            this.ucViewGovReportPic1.AllowViewContinusePages = true;
            this.ucViewGovReportPic1.AllowZoom = true;
            this.ucViewGovReportPic1.BackColor = System.Drawing.Color.White;
            this.ucViewGovReportPic1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucViewGovReportPic1.CurrentViewImage = null;
            this.ucViewGovReportPic1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucViewGovReportPic1.IsContinusePagesMode = false;
            this.ucViewGovReportPic1.IsEditMode = false;
            this.ucViewGovReportPic1.Location = new System.Drawing.Point(3, 3);
            this.ucViewGovReportPic1.Name = "ucViewGovReportPic1";
            this.ucViewGovReportPic1.ReadOnly = true;
            this.ucViewGovReportPic1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ucViewGovReportPic1.Size = new System.Drawing.Size(916, 494);
            this.ucViewGovReportPic1.TabIndex = 0;
            this.ucViewGovReportPic1.ThumbnailsView = false;
            this.ucViewGovReportPic1.ThumbsStripLocation = Sbn.Controls.Imaging.StripLocation.Right;
            // 
            // tpWord
            // 
            this.tpWord.Controls.Add(this.splitContainer1);
            this.tpWord.Controls.Add(this.ucWordDocEntityProp1);
            this.tpWord.Location = new System.Drawing.Point(4, 4);
            this.tpWord.Margin = new System.Windows.Forms.Padding(0);
            this.tpWord.Name = "tpWord";
            this.tpWord.Size = new System.Drawing.Size(1088, 500);
            this.tpWord.TabIndex = 0;
            this.tpWord.Text = "متن گزارش دفتر هیئت دولت";
            this.tpWord.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.webBrowser1);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.button5);
            this.splitContainer1.Panel2.Controls.Add(this.button4);
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Panel2.Controls.Add(this.button3);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.splitContainer1.Size = new System.Drawing.Size(1088, 500);
            this.splitContainer1.SplitterDistance = 439;
            this.splitContainer1.TabIndex = 2;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1088, 439);
            this.webBrowser1.TabIndex = 1;
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            this.webBrowser1.Validated += new System.EventHandler(this.webBrowser1_Validated);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button4.Location = new System.Drawing.Point(686, 7);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(129, 47);
            this.button4.TabIndex = 4;
            this.button4.Text = "جستجو";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.textBox1.Location = new System.Drawing.Point(909, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(167, 27);
            this.textBox1.TabIndex = 3;
            this.textBox1.Visible = false;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button3.Location = new System.Drawing.Point(275, 7);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(264, 47);
            this.button3.TabIndex = 2;
            this.button3.Text = "ابتدای گزارش";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button2.Location = new System.Drawing.Point(3, 7);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 47);
            this.button2.TabIndex = 1;
            this.button2.Text = "قبلی";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button1.Location = new System.Drawing.Point(138, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 47);
            this.button1.TabIndex = 0;
            this.button1.Text = "بعدی";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ucWordDocEntityProp1
            // 
            this.ucWordDocEntityProp1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ucWordDocEntityProp1.Location = new System.Drawing.Point(0, 348);
            this.ucWordDocEntityProp1.Margin = new System.Windows.Forms.Padding(0);
            this.ucWordDocEntityProp1.Name = "ucWordDocEntityProp1";
            this.ucWordDocEntityProp1.ReadOnly = false;
            this.ucWordDocEntityProp1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ucWordDocEntityProp1.Size = new System.Drawing.Size(627, 152);
            this.ucWordDocEntityProp1.TabIndex = 0;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.button5.Location = new System.Drawing.Point(545, 7);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(135, 47);
            this.button5.TabIndex = 5;
            this.button5.Text = "اندازه متن %100";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // UcViewGovReportTabTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Name = "UcViewGovReportTabTemplate";
            this.Size = new System.Drawing.Size(1096, 526);
            this.Load += new System.EventHandler(this.UcViewGovReportTabTemplateLoad);
            this.tabControl1.ResumeLayout(false);
            this.tpPictures.ResumeLayout(false);
            this.tpWord.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tpWord;
        public SessionPresent.Tools.SbnTools.FolderWordDocument.ucWordDocEntityProp ucWordDocEntityProp1;
        public System.Windows.Forms.TabControl tabControl1;
        public UcViewGovReportPic ucViewGovReportPic1;
        public System.Windows.Forms.TabPage tpPictures;
        public System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button5;
    }
}
