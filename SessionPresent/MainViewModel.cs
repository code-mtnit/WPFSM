﻿
using GalaSoft.MvvmLight.Command;
using MahApps.Metro;
using Sbn.Products.GEP.GEPObject;
using SessionPresent.Tools;
using SessionPresent.ViewModel;
using System.Drawing;
using System.Windows;
using System.Windows.Input;


namespace SessionPresent
{

    public class AccentColorMenuData
    {
        public string Name { get; set; }
        public System.Drawing.Brush BorderColorBrush { get; set; }
        public System.Drawing.Brush ColorBrush { get; set; }

        private ICommand changeAccentCommand;

        public ICommand ChangeAccentCommand
        {
            get { return this.changeAccentCommand ?? (changeAccentCommand = new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = x => this.DoChangeTheme(x) }); }
        }

        protected virtual void DoChangeTheme(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var accent = ThemeManager.GetAccent(this.Name);
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);
        }
    }

    public class AppThemeMenuData : AccentColorMenuData
    {
        protected override void DoChangeTheme(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var appTheme = ThemeManager.GetAppTheme(this.Name);
            ThemeManager.ChangeAppStyle(Application.Current, theme.Item2, appTheme);
        }
    }

    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : TreeViewItemViewModel
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
            :base(null,false)
        {
          
        }

      //  string _MainTitle = "دستور جلسه این هفته : تاریخ 9606333";

            public string Version { get; set; }
        public string Description { get; set; }

        private string _mainTitle = "";
        public string MainTitle
        {
            get { return _mainTitle; }
            set
            {
                _mainTitle = value;
                OnPropertyChanged("MainTitle");
            }
        }

        private string _MessageTitle = "";
        public string MessageTitle
        {
            get { return _MessageTitle; }
            set
            {
                _MessageTitle = value;
                OnPropertyChanged("MessageTitle");
            }
        }

        private int _MessageDealy = 0;
        public int MessageDealy
        {
            get { return _MessageDealy; }
            set
            {
                _MessageDealy = value;
                OnPropertyChanged("MessageDealy");
            }
        }

        private int _MessageDuration = 0;
        public int MessageDuration
        {
            get { return _MessageDuration; }
            set
            {
                _MessageDuration = value;
                OnPropertyChanged("MessageDuration");
            }
        }

        private System.Drawing.Color _MessageForeColor = System.Drawing.Color.Black;
        public System.Drawing.Color MessageForeColor
        {
            get { return _MessageForeColor; }
            set
            {
                _MessageForeColor = value;
                OnPropertyChanged("MessageForeColor");
            }
        }

        private Font _MessageFont = new Font(new FontFamily( "B Nazanin") , 18f);
        public Font MessageFont
        {
            get { return _MessageFont; }
            set
            {
                _MessageFont  = value;
                OnPropertyChanged("MessageFont");
            }
        }


        private System.Drawing.Color _MessageBackColor = System.Drawing.Color.White;
        public System.Drawing.Color MessageBackColor
        {
            get { return _MessageBackColor; }
            set
            {
                _MessageBackColor = value;
                OnPropertyChanged("MessageBackColor");
            }
        }

        bool _IsSessionManager = false;
        public bool IsSessionManager
        {
            get { return _IsSessionManager; }
            set
            {
                _IsSessionManager = value;
                OnPropertyChanged("IsSessionManager");
            }
        }


        bool _IsShowTreeView = false;
        public bool IsShowTreeView
        {
            get { return _IsShowTreeView; }
            set
            {
                _IsShowTreeView = value;
                OnPropertyChanged("IsShowTreeView");
            }
        }

        bool _IsProgressing = false;
        public bool IsProgressing
        {
            get { return _IsProgressing; }
            set
            {
                _IsProgressing = value;
                OnPropertyChanged("IsProgressing");
            }
        }

        private SessionItemViewModel _currentViewItem;
        public SessionItemViewModel CurrentViewItem
        {
            get { return _currentViewItem; }
            set
            {
                _currentViewItem = value;
                //if (_currentViewItem != null)
                //    _currentViewItem.OpenFolder(_currentViewItem.CurrnetViewFolder);
                OnPropertyChanged("CurrentViewItem");
                //if (_currentViewItem != null)
                //{
                //    _currentViewItem.IsExpanded = true;
                //    _currentViewItem.IsSelected = true;
                //    //OnOpenItemEvent(new SbnEventArgs(_currentViewItem.CurrnetViewFolder));
                //}


            }
        }

        private SessionItemViewModel _cutItem;
        public SessionItemViewModel CutItem
        {
            get { return _cutItem; }
            set
            {
                _cutItem = value;
                //if (_currentViewItem != null)
                //    _currentViewItem.OpenFolder(_currentViewItem.CurrnetViewFolder);
                OnPropertyChanged("CutItem");
                //if (_currentViewItem != null)
                //{
                //    _currentViewItem.IsExpanded = true;
                //    _currentViewItem.IsSelected = true;
                //    //OnOpenItemEvent(new SbnEventArgs(_currentViewItem.CurrnetViewFolder));
                //}


            }
        }

        private bool showMyTitleBar = true;
        public bool ShowMyTitleBar
        {
            get { return showMyTitleBar; }
            set
            {
                if (value.Equals(showMyTitleBar)) return;
                showMyTitleBar = value;
                OnPropertyChanged("ShowMyTitleBar");
            }
        }
    }
}