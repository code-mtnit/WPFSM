using System;
using System.Windows.Forms;
using Sbn.Products.GEP.GEPObject;


namespace SessionPresent.Tools.SbnTools
{
    public partial class UcViewGovReportTabTemplate : UserControl
    {
        public event EventHandler<SessionItemEventArgs> ViewWordDocChanged;


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
                    tabControl1.SelectedTab = tpWord;
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

        private void TabControl1Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tpPictures)
            {
                if (ucViewGovReportPic1.CurrentObject == null || ucViewGovReportPic1.CurrentObject.ID < 0)
                    ucViewGovReportPic1.FillObject(CurrentObject , false);

                OnViewWordDocChanged(new SessionItemEventArgs(false));
            }

            if (e.TabPage == tpWord && CurrentObject.ID > 0)
            {
                OnViewWordDocChanged(new SessionItemEventArgs(true));

                if (CurrentObject.WordDoc != null && CurrentObject.WordDoc.ID > 0)
                {
                   
                    ucWordDocEntityProp1.FillObject(CurrentObject.WordDoc, false, ReadOnly);

                    if (!ucWordDocEntityProp1.wordControlDocument1.wd.ActiveWindow.ActivePane.View.FullScreen)
                         ucWordDocEntityProp1.wordControlDocument1.SetFullScreenView();

                    ucWordDocEntityProp1.wordControlDocument1.wd.ActiveWindow.View.Panning = true;


                   
                }
                else
                {
                    ucWordDocEntityProp1.ClearData();
                       
                }
            }

          
        }












       
    }
}
