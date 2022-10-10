﻿
using System.Windows.Markup;
using Sbn.Products.GEP.GEPObject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SessionPresent.ViewModel;

namespace SessionPresent.Tools.SbnTools
{
    /// <summary>
    /// Interaction logic for OfferView.xaml
    /// </summary>
    public partial class OfferView : UserControl ,ISessionItemViewer
    {


        public Offer CurrentOffer;
        public OfferView()
        {
            InitializeComponent();
        }

        public void FillObject(object obj, object mvm)
        {
            var sItm = (obj as SessionItemViewModel);
            if (sItm != null)
            {
                var off = sItm.Object as Offer;
                CurrentOffer = off;
                UcViewGovReportTabTemplate1.FillObject(off);
            }
            
        }

        public void FillMetaData(ArrayList MetaData)
        {
            if (MetaData == null)
                return;
          
            int pagenumber = -1;
            bool IsVoewWordDoc = false;
            int MaxLoop = 0;
            foreach (BaseClass.ObjectMetaData omd in MetaData)
            {
                try
                {
                    switch (omd.Tag)
                    {
                        case "PageNumber":
                            if (int.TryParse(omd.Text, out pagenumber))
                            {
                                if (pagenumber >= 0 && !IsVoewWordDoc)
                                {
                                    UcViewGovReportTabTemplate1.ucViewGovReportPic1.BindingSource.Position = pagenumber;
                                }
                            }
                            break;
                        case "IsViewWordDoc":
                            if (bool.TryParse(omd.Text, out IsVoewWordDoc))
                            {
                                if (pagenumber >= 0 && IsVoewWordDoc)
                                {
                                    /*  comment 14010719
                                    UcViewGovReportTabTemplate1.tabControl1.SelectedTab = UcViewGovReportTabTemplate1.tabControl1.TabPages[1];
                                    while (UcViewGovReportTabTemplate1.ucWordDocEntityProp1.wordControlDocument1.document == null && MaxLoop++ < 5)
                                    {

                                        System.Threading.Thread.Sleep(1000);
                                    }

                                    if (UcViewGovReportTabTemplate1.ucWordDocEntityProp1.wordControlDocument1.document != null)
                                    {
                                        UcViewGovReportTabTemplate1.ucWordDocEntityProp1.wordControlDocument1.document.ActiveWindow.ActivePane.View.Type = Microsoft.Office.Interop.Word.WdViewType.wdPrintView;

                                        UcViewGovReportTabTemplate1.ucWordDocEntityProp1.wordControlDocument1.document.ActiveWindow.ActivePane.Pages[pagenumber].Rectangles[1].Range.Select();
                                    }
                                    */
                                }

                            }
                            break;
                        case "DocItemPositionWebBrowser":

                            var doc = UcViewGovReportTabTemplate1.webBrowser1.Document;

                            var p = Point.Parse(omd.Text);
                            if (doc != null && p != null)
                                ((System.Windows.Forms.HtmlDocument)doc).Window.ScrollTo((int)p.X, (int)p.Y);
                            break;
                        default:
                            break;
                    }
                }
                catch 
                {

                }
            }


            UcViewGovReportTabTemplate1.IsViewWordDocument = IsVoewWordDoc;

            
        }

        public Sbn.AdvancedControls.WordControlDocument.WordControlDocument getWordControl()
        {
            return this.UcViewGovReportTabTemplate1.ucWordDocEntityProp1.wordControlDocument1;
        }

        public ArrayList GetMetaData()
        {
            var metas = new ArrayList();
            var PageNumber = new BaseClass.ObjectMetaData();
            PageNumber.Tag = "PageNumber";
            PageNumber.Text = this.UcViewGovReportTabTemplate1.ucViewGovReportPic1.BindingSource.Position.ToString();

            

            var IsViewWordDoc = new BaseClass.ObjectMetaData();
            IsViewWordDoc.Tag = "IsViewWordDoc";
            IsViewWordDoc.Text = UcViewGovReportTabTemplate1.IsViewWordDocument.ToString();
    /*        commented 14010719
             if(UcViewGovReportTabTemplate1.IsViewWordDocument)
            {
                try
                {
                    var range = this.UcViewGovReportTabTemplate1.ucWordDocEntityProp1.wordControlDocument1.document.Range().GoTo(Microsoft.Office.Interop.Word.WdGoToItem.wdGoToPage, Microsoft.Office.Interop.Word.WdGoToDirection.wdGoToLast);
                    var numPages = range.get_Information(Microsoft.Office.Interop.Word.WdInformation.wdActiveEndPageNumber);
                    PageNumber.Text = numPages.ToString();
                }
                catch
                {

                }

            }*/

            if (UcViewGovReportTabTemplate1.IsViewWordDocument)
            {
                try
                {
                    var doc = UcViewGovReportTabTemplate1.webBrowser1.Document ;
                    if (doc != null)
                    {

                        var DocItemPosition = new BaseClass.ObjectMetaData();
                        DocItemPosition.Tag = "DocItemPositionWebBrowser";
                        DocItemPosition.Text = new Point( Double.Parse( ((System.Windows.Forms.HtmlDocument) doc).Window.Document.Body.GetAttribute("ScrollLeft").ToString()), Double.Parse(((System.Windows.Forms.HtmlDocument) doc).Window.Document.Body.GetAttribute("ScrollTop"))).ToString();

                        metas.Add(DocItemPosition);
                    }
                }
                catch
                {

                }




            }
            metas.Add(PageNumber);
            metas.Add(IsViewWordDoc);

            return metas;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UcViewGovReportTabTemplate1.ucViewGovReportPic1.ImageViewer.FitToWidth();
        }


        public string GetVotingMetaData()
        {
            return CurrentOffer._PhysicalPath;
        }


        public string GetBallotMetaData()
        {

            var govSessionMemOpinion = new GovSessionMemberOpinion();
            var xmlString = govSessionMemOpinion.GetXML("GovSessionMemberOpinion");
            return xmlString;
        }


        public void InitialVotingViewModel(IVotingViewModel votingViewModel)
        {

            if (votingViewModel == null)
                return;

            votingViewModel.ExternalObjectTitle = CurrentOffer.Title;
            votingViewModel.ExternalObjectId = (int) CurrentOffer.ID;
            votingViewModel.ExternalObjectAliasCode = CurrentOffer.OfficialCode;


            FlowDocument flDocument = new FlowDocument();
            flDocument.FontFamily = new FontFamily("B Nazanin");
            var par1 = new Paragraph(new Run("شماره : " + CurrentOffer.OfficialCode + "             " + CurrentOffer.OwnerOrgan.CorrelateOrgUnit.Title));
            
           // par1.FontFamily = new FontFamily("B Nazanin");
            par1.FontSize = 30;
            par1.Foreground = Brushes.DarkGreen;
            flDocument.Blocks.Add(par1);

            var par2 = new Paragraph(new Run(CurrentOffer.Title));
            par2.FontSize = 27;
            par2.Foreground = Brushes.Black;
            flDocument.Blocks.Add(par2);


           // var textRange = new TextRange(flDocument.ContentStart, flDocument.ContentEnd);

            votingViewModel.VotingSubject = XamlWriter.Save(flDocument);


        }
    }
}
