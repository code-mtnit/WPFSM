
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
            this.ucViewGovReportPic1 = new SessionPresent.Tools.SbnTools.UcViewGovReportPic();
            this.ucWordDocEntityProp1 = new SessionPresent.Tools.SbnTools.FolderWordDocument.ucWordDocEntityProp();
            this.tpPictures = new System.Windows.Forms.TabPage();
            this.tpWord = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tpPictures.SuspendLayout();
            this.tpWord.SuspendLayout();
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
            this.tabControl1.Size = new System.Drawing.Size(720, 526);
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
            this.tpPictures.Size = new System.Drawing.Size(712, 500);
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
            this.ucViewGovReportPic1.Size = new System.Drawing.Size(706, 494);
            this.ucViewGovReportPic1.TabIndex = 0;
            this.ucViewGovReportPic1.ThumbnailsView = false;
            this.ucViewGovReportPic1.ThumbsStripLocation = Sbn.Controls.Imaging.StripLocation.Right;
            // 
            // tpWord
            // 
            this.tpWord.Controls.Add(this.ucWordDocEntityProp1);
            this.tpWord.Location = new System.Drawing.Point(4, 4);
            this.tpWord.Margin = new System.Windows.Forms.Padding(0);
            this.tpWord.Name = "tpWord";
            this.tpWord.Size = new System.Drawing.Size(712, 500);
            this.tpWord.TabIndex = 0;
            this.tpWord.Text = "متن گزارش دفتر هیئت دولت";
            this.tpWord.UseVisualStyleBackColor = true;
            // 
            // ucWordDocEntityProp1
            // 
            this.ucWordDocEntityProp1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucWordDocEntityProp1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.ucWordDocEntityProp1.Location = new System.Drawing.Point(0, 0);
            this.ucWordDocEntityProp1.Margin = new System.Windows.Forms.Padding(0);
            this.ucWordDocEntityProp1.Name = "ucWordDocEntityProp1";
            this.ucWordDocEntityProp1.ReadOnly = false;
            this.ucWordDocEntityProp1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ucWordDocEntityProp1.Size = new System.Drawing.Size(712, 500);
            this.ucWordDocEntityProp1.TabIndex = 0;
            // 
            // UcViewGovReportTabTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Name = "UcViewGovReportTabTemplate";
            this.Size = new System.Drawing.Size(720, 526);
            this.Load += new System.EventHandler(this.UcViewGovReportTabTemplateLoad);
            this.tabControl1.ResumeLayout(false);
            this.tpPictures.ResumeLayout(false);
            this.tpWord.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tpWord;
        public SessionPresent.Tools.SbnTools.FolderWordDocument.ucWordDocEntityProp ucWordDocEntityProp1;
        public System.Windows.Forms.TabControl tabControl1;
        public UcViewGovReportPic ucViewGovReportPic1;
        public System.Windows.Forms.TabPage tpPictures;
    }
}
