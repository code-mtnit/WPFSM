

namespace Sbn.Controls.Imaging
{
    partial class ImageDocumentEditor
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
            Sbn.AdvancedControls.Imaging.SbnPaint.Pointer pointer1 = new Sbn.AdvancedControls.Imaging.SbnPaint.Pointer();
            Sbn.FramWork.Drawing.Ghost ghost1 = new Sbn.FramWork.Drawing.Ghost();
            Sbn.FramWork.Drawing.GhostAppearance ghostAppearance1 = new Sbn.FramWork.Drawing.GhostAppearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageDocumentEditor));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmnuItmUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuItmRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmnuItmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuItmRotate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuItmCW = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuItmCCW = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnApplay = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCancel = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.tsbtnRemoveLayer = new System.Windows.Forms.ToolStripButton();
            this.tsddItmPen = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tsbtnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.ucToolsSelector1 = new Sbn.AdvancedControls.Imaging.SbnPaint.ucToolsSelector();
            this.drawingPanel1 = new Sbn.AdvancedControls.Imaging.SbnPaint.DrawingPanel();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmnuItmUndo,
            this.tsmnuItmRedo,
            this.toolStripSeparator1,
            this.tsmnuItmDelete,
            this.tsmnuItmRotate});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.contextMenuStrip1.Size = new System.Drawing.Size(145, 98);
            this.contextMenuStrip1.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenuStrip1_Closed);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // tsmnuItmUndo
            // 
            this.tsmnuItmUndo.Name = "tsmnuItmUndo";
            this.tsmnuItmUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.tsmnuItmUndo.Size = new System.Drawing.Size(144, 22);
            this.tsmnuItmUndo.Text = "Undo";
            this.tsmnuItmUndo.Click += new System.EventHandler(this.tsmnuItmUndo_Click);
            // 
            // tsmnuItmRedo
            // 
            this.tsmnuItmRedo.Name = "tsmnuItmRedo";
            this.tsmnuItmRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.tsmnuItmRedo.Size = new System.Drawing.Size(144, 22);
            this.tsmnuItmRedo.Text = "Redo";
            this.tsmnuItmRedo.Click += new System.EventHandler(this.tsmnuItmRedo_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(141, 6);
            // 
            // tsmnuItmDelete
            // 
            this.tsmnuItmDelete.Name = "tsmnuItmDelete";
            this.tsmnuItmDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.tsmnuItmDelete.Size = new System.Drawing.Size(144, 22);
            this.tsmnuItmDelete.Text = "حذف";
            this.tsmnuItmDelete.Click += new System.EventHandler(this.tsmnuItmDelete_Click);
            // 
            // tsmnuItmRotate
            // 
            this.tsmnuItmRotate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmnuItmCW,
            this.tsmnuItmCCW});
            this.tsmnuItmRotate.Name = "tsmnuItmRotate";
            this.tsmnuItmRotate.Size = new System.Drawing.Size(144, 22);
            this.tsmnuItmRotate.Text = "چرخش";
            // 
            // tsmnuItmCW
            // 
            this.tsmnuItmCW.Name = "tsmnuItmCW";
            this.tsmnuItmCW.Size = new System.Drawing.Size(170, 22);
            this.tsmnuItmCW.Text = "90 درجه ساعتگرد";
            this.tsmnuItmCW.Click += new System.EventHandler(this.tsmnuItmCW_Click);
            // 
            // tsmnuItmCCW
            // 
            this.tsmnuItmCCW.Name = "tsmnuItmCCW";
            this.tsmnuItmCCW.Size = new System.Drawing.Size(170, 22);
            this.tsmnuItmCCW.Text = "90 درجه پادساعتگرد";
            this.tsmnuItmCCW.Click += new System.EventHandler(this.tsmnuItmCCW_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnApplay,
            this.tsbtnCancel,
            this.toolStripLabel1,
            this.toolStripComboBox1,
            this.tsbtnRemoveLayer,
            this.tsddItmPen,
            this.toolStripSeparator2,
            this.tsbtnZoomOut,
            this.tsbtnZoomIn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 486);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip1.Size = new System.Drawing.Size(885, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnApplay
            // 
            this.tsbtnApplay.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnApplay.Image = global::SbnImaging.Properties.Resources.tick;
            this.tsbtnApplay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnApplay.Name = "tsbtnApplay";
            this.tsbtnApplay.Size = new System.Drawing.Size(114, 22);
            this.tsbtnApplay.Text = "تأیید ویرایش تصویر";
            this.tsbtnApplay.Click += new System.EventHandler(this.tsbtnApplay_Click);
            // 
            // tsbtnCancel
            // 
            this.tsbtnCancel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCancel.Name = "tsbtnCancel";
            this.tsbtnCancel.Size = new System.Drawing.Size(95, 22);
            this.tsbtnCancel.Text = "انصراف از تغییرات";
            this.tsbtnCancel.Click += new System.EventHandler(this.tsbtnCancel_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(46, 22);
            this.toolStripLabel1.Text = "لایه ها : ";
            this.toolStripLabel1.Visible = false;
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox1.Visible = false;
            // 
            // tsbtnRemoveLayer
            // 
            this.tsbtnRemoveLayer.Image = global::SbnImaging.Properties.Resources.delete24;
            this.tsbtnRemoveLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRemoveLayer.Name = "tsbtnRemoveLayer";
            this.tsbtnRemoveLayer.Size = new System.Drawing.Size(68, 22);
            this.tsbtnRemoveLayer.Text = "حذف لایه";
            this.tsbtnRemoveLayer.Click += new System.EventHandler(this.tsbtnRemoveLayer_Click);
            // 
            // tsddItmPen
            // 
            this.tsddItmPen.Image = global::SbnImaging.Properties.Resources.BrushWidth24;
            this.tsddItmPen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddItmPen.Name = "tsddItmPen";
            this.tsddItmPen.Size = new System.Drawing.Size(48, 22);
            this.tsddItmPen.Text = "قلم";
            this.tsddItmPen.Visible = false;
            this.tsddItmPen.DropDownClosed += new System.EventHandler(this.tsddItmPen_DropDownClosed);
            this.tsddItmPen.DropDownOpening += new System.EventHandler(this.tsddItmPen_DropDownOpening);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnZoomOut
            // 
            this.tsbtnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnZoomOut.Image = global::SbnImaging.Properties.Resources.magifier_zoom_out;
            this.tsbtnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnZoomOut.Name = "tsbtnZoomOut";
            this.tsbtnZoomOut.Size = new System.Drawing.Size(23, 22);
            this.tsbtnZoomOut.Text = "ZoomOut";
            this.tsbtnZoomOut.Click += new System.EventHandler(this.tsbtnZoomOut_Click);
            // 
            // tsbtnZoomIn
            // 
            this.tsbtnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnZoomIn.Image = global::SbnImaging.Properties.Resources.magnifier_zoom_in;
            this.tsbtnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnZoomIn.Name = "tsbtnZoomIn";
            this.tsbtnZoomIn.Size = new System.Drawing.Size(23, 22);
            this.tsbtnZoomIn.Text = "ZoomIn";
            this.tsbtnZoomIn.Click += new System.EventHandler(this.tsbtnZoomIn_Click);
            // 
            // ucToolsSelector1
            // 
            ghostAppearance1.BackgroundColor1 = System.Drawing.Color.Transparent;
            ghostAppearance1.BackgroundColor2 = System.Drawing.Color.Transparent;
            ghostAppearance1.BorderColor = System.Drawing.Color.Black;
            ghostAppearance1.BorderWidth = 2F;
            ghostAppearance1.GrabberDimension = 6;
            ghostAppearance1.GradientAngle = 0F;
            ghostAppearance1.Image = null;
            ghostAppearance1.MarkerColor = System.Drawing.Color.Black;
            ghostAppearance1.MarkerDimension = 4;
            ghostAppearance1.Shape = null;
            ghost1.Appearance = ghostAppearance1;
            ghost1.Center = ((System.Drawing.PointF)(resources.GetObject("ghost1.Center")));
            ghost1.Color = System.Drawing.Color.Black;
            ghost1.Dimension = new System.Drawing.SizeF(0F, 0F);
            ghost1.HorizontalVersor = Sbn.FramWork.Drawing.HorizontalVersors.LeftRight;
            ghost1.IsEdited = false;
            ghost1.Location = ((System.Drawing.PointF)(resources.GetObject("ghost1.Location")));
            ghost1.Locked = false;
            ghost1.Marked = false;
            ghost1.Menu = null;
            ghost1.MirrorPoint = ((System.Drawing.PointF)(resources.GetObject("ghost1.MirrorPoint")));
            ghost1.Rotation = 0F;
            ghost1.Selected = false;
            ghost1.Shape = null;
            ghost1.VerticalVersor = Sbn.FramWork.Drawing.VerticalVersors.TopBottom;
            ghost1.Visible = true;
            pointer1.Ghost = ghost1;
            pointer1.MouseDownPoint = new System.Drawing.Point(0, 0);
            pointer1.MouseUpPoint = new System.Drawing.Point(0, 0);
            this.ucToolsSelector1.ActiveTool = pointer1;
            this.ucToolsSelector1.BackColor = System.Drawing.Color.LightGray;
            this.ucToolsSelector1.CurrentDrawingPanel = this.drawingPanel1;
            this.ucToolsSelector1.Dock = System.Windows.Forms.DockStyle.Right;
            this.ucToolsSelector1.Location = new System.Drawing.Point(885, 0);
            this.ucToolsSelector1.Name = "ucToolsSelector1";
            this.ucToolsSelector1.Size = new System.Drawing.Size(49, 511);
            this.ucToolsSelector1.TabIndex = 5;
            // 
            // drawingPanel1
            // 
            this.drawingPanel1.ActiveCursor = System.Windows.Forms.Cursors.Default;
            this.drawingPanel1.ActiveTool = pointer1;
            this.drawingPanel1.AutoScroll = true;
            this.drawingPanel1.BackColor = System.Drawing.Color.White;
            this.drawingPanel1.ContextMenuStrip = this.contextMenuStrip1;
            this.drawingPanel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.drawingPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawingPanel1.EnableWheelZoom = true;
            this.drawingPanel1.ImageBackColor = System.Drawing.Color.White;
            this.drawingPanel1.ImageBody = null;
            this.drawingPanel1.Location = new System.Drawing.Point(0, 0);
            this.drawingPanel1.Name = "drawingPanel1";
            this.drawingPanel1.Size = new System.Drawing.Size(885, 486);
            this.drawingPanel1.TabIndex = 0;
            this.drawingPanel1.Zoom = 1F;
            this.drawingPanel1.ZoomToCurserPosition = false;
            this.drawingPanel1.DeleteingShape += new System.EventHandler<System.EventArgs>(this.drawingPanel1_DeleteingShape);
            this.drawingPanel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.drawingPanel1_Scroll);
            // 
            // ImageDocumentEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.drawingPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.ucToolsSelector1);
            this.Name = "ImageDocumentEditor";
            this.Size = new System.Drawing.Size(934, 511);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Sbn.AdvancedControls.Imaging.SbnPaint.DrawingPanel drawingPanel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmnuItmUndo;
        private System.Windows.Forms.ToolStripMenuItem tsmnuItmRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmnuItmDelete;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripButton tsbtnRemoveLayer;
        private System.Windows.Forms.ToolStripButton tsbtnApplay;
        private System.Windows.Forms.ToolStripButton tsbtnCancel;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripButton tsbtnZoomIn;
        private System.Windows.Forms.ToolStripButton tsbtnZoomOut;
        private PenSelectorViewStrip penSelectorViewStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton tsddItmPen;
        private PenSelectorViewStrip penSelectorViewStrip3;
        private AdvancedControls.Imaging.SbnPaint.ucToolsSelector ucToolsSelector1;
        private System.Windows.Forms.ToolStripMenuItem tsmnuItmRotate;
        private System.Windows.Forms.ToolStripMenuItem tsmnuItmCW;
        private System.Windows.Forms.ToolStripMenuItem tsmnuItmCCW;

    }
}
