using System.Collections.ObjectModel;

namespace Maui.TabView.Control
{
    #region Delegates

    /// <summary>
    /// The OnTabClickEventHandler
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/></param>
    /// <param name="args">The args<see cref="OnTabClickedEventArgs"/></param>
    public delegate void OnTabClickEventHandler(object sender, OnTabClickedEventArgs args);

    #endregion

    public class MauiTabControl : Border
    {
        #region PRIVATE_VARIABLES

        /// <summary>
        /// Defines the SelectedIndexProperty.
        /// </summary>
        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(MauiTabControl), 0, BindingMode.TwoWay, propertyChanged: IndexChanged);

        /// <summary>
        /// Defines the TabbedPagesProperty.
        /// </summary>
        public static BindableProperty TabbedPagesProperty = BindableProperty.Create(nameof(MauiTabPages), typeof(ObservableCollection<MauiTabPage>), typeof(MauiTabControl), null, BindingMode.TwoWay);

        /// <summary>
        /// Defines the HeaderSelectionBgColorProperty.
        /// </summary>
        public static BindableProperty HeaderSelectionBgColorProperty = BindableProperty.Create(nameof(MauiTabPages), typeof(Color), typeof(MauiTabControl), Colors.White, BindingMode.TwoWay);

        /// <summary>
        /// Defines the m_Parent.
        /// </summary>
        internal Grid m_Parent;

        /// <summary>
        /// Defines the SelectedPage.
        /// </summary>
        internal MauiTabPage SelectedPage;

        /// <summary>
        /// Swipe start and end position..
        /// </summary>
        private double _SwipeStartX = 0, _SwipeStartY = 0;

        /// <summary>
        /// Defines the m_Header.
        /// </summary>
        private Grid m_Header;

        /// <summary>
        /// Defines the m_headerColor.
        /// </summary>
        private Color m_headerColor;

        /// <summary>
        /// Defines the m_Selection.
        /// </summary>
        private Grid m_Selection;

        /// <summary>
        /// Gets or sets the XFTabBody.
        /// </summary>
        internal Grid XFTabBody { get; set; }

        #endregion

        #region PUBLIC_PPTY

        /// <summary>
        /// Gets or sets the HeaderColor.
        /// </summary>
        public Color HeaderColor
        {
            get => m_headerColor;
            set
            {
                m_headerColor = value;
                m_Header.BackgroundColor = value;
                m_Selection.BackgroundColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the HeaderHeight.
        /// </summary>
        public int HeaderHeight { get; set; }

        /// <summary>
        /// Gets or sets the HeaderTextColor.
        /// </summary>
        public Color HeaderTextColor { get; set; }

        /// <summary>
        /// Gets or sets the Position.
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// Gets or sets the SelectedIndex.
        /// </summary>
        public int SelectedIndex { get => (int)GetValue(SelectedIndexProperty); set => SetValue(SelectedIndexProperty, value); }

        /// <summary>
        /// Gets or sets the SelectedIndex.
        /// </summary>
        public Color HeaderSelectionBgColor { get => (Color)GetValue(HeaderSelectionBgColorProperty); set => SetValue(HeaderSelectionBgColorProperty, value); }

        /// <summary>
        /// Gets or sets the SelectionColor.
        /// </summary>
        public Color SelectionColor { get; set; } = Color.FromRgb(102, 153, 255);

        /// <summary>
        /// Gets or sets the SelectorHeight.
        /// </summary>
        public int SelectorHeight { get; set; } = 8;

        public bool ShowSelectionArea { get; set; } = true;

        /// <summary>
        /// Gets or sets the MauiTabPages.
        /// </summary>
        public ObservableCollection<MauiTabPage> MauiTabPages { get => (ObservableCollection<MauiTabPage>)GetValue(TabbedPagesProperty); set => SetValue(TabbedPagesProperty, value); }

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Initializes a new instance of the <see cref="MauiTabControl"/> class.
        /// </summary>
        public MauiTabControl()
        {
            Padding = 0;
            Margin = 0;
            StrokeThickness = 0;
            init();
            MauiTabPages = new ObservableCollection<MauiTabPage>();
            MauiTabPages.CollectionChanged += MauiTabPages_CollectionChanged;
        }

        #endregion

        #region Events

        /// <summary>
        /// Defines the TabClicked.
        /// </summary>
        public event OnTabClickEventHandler TabClicked;

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// The addTabPageContent.
        /// </summary>
        /// <param name="tabPage">The tabPage<see cref="MauiTabPage"/>.</param>
        private void addTabPageContent(MauiTabPage tabPage)
        {
            tabPage.XFTabParent = this;
            tabPage.Header.XFParentTabPage = tabPage;
            tabPage.Header.BackgroundColor = HeaderColor;
            tabPage.Header.Selector = new BoxView
            {
                BackgroundColor = HeaderColor
            };
            if (tabPage.Header.IsVisible)
            {
                m_Header.Add(tabPage.Header, m_Header.Children.Count, 0);
                m_Selection.Add(tabPage.Header.Selector, m_Selection.Children.Count, 0);
            }
        }

        /// <summary>
        /// The index changed.
        /// </summary>
        /// <param name="bindable">The bindable<see cref="BindableObject"/>.</param>
        /// <param name="oldValue">The oldValue<see cref="object"/>.</param>
        /// <param name="newValue">The newValue<see cref="object"/>.</param>
        private static void IndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (MauiTabControl)bindable;
            control.SelectTabPage((int)newValue);
        }

        /// <summary>
        /// The init.
        /// </summary>
        private void init()
        {
            m_Parent = new Grid { RowSpacing = 0, ColumnSpacing = 0 };
            XFTabBody = new Grid { RowSpacing = 0, ColumnSpacing = 0, HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill };
            m_Header = new Grid { RowSpacing = 0, ColumnSpacing = 0 };
            m_Selection = new Grid { RowSpacing = 0, ColumnSpacing = 0 };
        }

        /// <summary>
        /// The Tt_PanUpdated.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="PanUpdatedEventArgs"/>.</param>
        private void panGesture_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    _SwipeStartX = e.TotalX;
                    _SwipeStartY = e.TotalY;
                    break;
                case GestureStatus.Completed:
                    var xdiff = _SwipeStartX;
                    var ydiff = _SwipeStartY;

                    if (Math.Abs(xdiff) > Math.Abs(ydiff))
                    {
                        if (xdiff < 0 && (SelectedIndex + 1) < m_Header.Children.Count)
                        {
                            SelectedIndex = SelectedIndex + 1;
                        }
                        else if (xdiff > 0 && (SelectedIndex - 1) > -1)
                        {
                            SelectedIndex = SelectedIndex - 1;
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// The TabLayout.
        /// </summary>
        private void TabLayout()
        {
            if (Position == Position.Top)
            {
                if (ShowSelectionArea)
                {
                    m_Parent.RowDefinitions = new RowDefinitionCollection
                    {
                        new RowDefinition { Height = HeaderHeight == 0 ? GridLength.Auto : new GridLength(HeaderHeight, GridUnitType.Absolute) },
                        new RowDefinition { Height = new GridLength(8, GridUnitType.Absolute) },
                        new RowDefinition { Height = GridLength.Star}
                    };
                    m_Parent.Add(m_Header, 0, 0);
                    m_Parent.Add(m_Selection, 0, 1);
                    m_Parent.Add(XFTabBody, 0, 2);
                }
                else
                {
                    m_Parent.RowDefinitions = new RowDefinitionCollection
                    {
                        new RowDefinition { Height = HeaderHeight == 0 ? GridLength.Auto : new GridLength(HeaderHeight, GridUnitType.Absolute) },
                        new RowDefinition { Height = GridLength.Star}
                    };
                    m_Parent.Add(m_Header, 0, 0);
                    m_Parent.Add(XFTabBody, 0, 1);
                }

            }
            else
            {
                if (ShowSelectionArea)
                {
                    m_Parent.RowDefinitions = new RowDefinitionCollection
                    {
                        new RowDefinition { Height = GridLength.Star},
                        new RowDefinition { Height = new GridLength(8)},
                        new RowDefinition { Height = new GridLength(HeaderHeight) }
                    };
                    m_Parent.Add(XFTabBody, 0, 0);
                    m_Parent.Add(m_Selection, 0, 1);
                    m_Parent.Add(m_Header, 0, 2);
                }
                else
                {
                    m_Parent.RowDefinitions = new RowDefinitionCollection
                    {
                        new RowDefinition { Height = GridLength.Star},
                        new RowDefinition { Height = new GridLength(HeaderHeight) }
                    };
                    m_Parent.Add(XFTabBody, 0, 0);
                    m_Parent.Add(m_Header, 0, 1);
                }
            }
        }

        /// <summary>
        /// The MauiTabPages_CollectionChanged.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/>.</param>
        private void MauiTabPages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    var page = MauiTabPages[e.NewStartingIndex];
                    addTabPageContent(page);
                    break;
            }
        }

        #endregion

        #region PUBLIC_METHODS

        /// <summary>
        /// The AddPage.
        /// </summary>
        /// <param name="tabPage">The tabPage<see cref="MauiTabPage"/>.</param>
        public void AddPage(MauiTabPage tabPage)
        {
            MauiTabPages.Add(tabPage);
        }

        /// <summary>
        /// The SelectPage.
        /// </summary>
        /// <param name="index">The index<see cref="int"/>.</param>
        public void SelectTabPage(int index)
        {
            if (index > -1 && m_Header.Children.Count > index && m_Header.Children[index] is MauiTabHeader header)
            {
                var page = header.XFParentTabPage;
                SelectTabPage(page);
            }
        }

        /// <summary>
        /// The SelectTabPage.
        /// </summary>
        /// <param name="page">The page<see cref="MauiTabPage"/>.</param>
        public void SelectTabPage(MauiTabPage page, bool ignoreSelectionUpdate = false)
        {
            ClearExistingSelection();

            if (!ignoreSelectionUpdate)
            {
                UpdatePageSelection(page);
            }

            SelectedPage = page;
            XFTabBody.Clear();
            XFTabBody.Add(page.Content);
        }

        /// <summary>
        /// update the new selection header
        /// </summary>
        /// <param name="page"></param>

        public void UpdatePageSelection(MauiTabPage page)
        {
            page.Header.Selector.BackgroundColor = SelectionColor;
            page.Header.BackgroundColor = HeaderSelectionBgColor;
            if (page.Header.Content is Label label)
            {
                label.TextColor = SelectionColor;
            }
            page.Header.IsSelected = true;
        }

        /// <summary>
        /// Clear the existing selection
        /// </summary>

        public void ClearExistingSelection()
        {
            if (SelectedPage != null)
            {
                SelectedPage.Header.IsSelected = false;
                SelectedPage.Header.BackgroundColor = HeaderColor;
                SelectedPage.Header.Selector.BackgroundColor = HeaderColor;
                if (SelectedPage.Header.Content is Label label1)
                {
                    label1.TextColor = HeaderTextColor;
                }
            }
        }

        #endregion

        /// <summary>
        /// The OnParentSet.
        /// </summary>
        protected override void OnParentSet()
        {
            var panGesture = new PanGestureRecognizer();
            XFTabBody.GestureRecognizers.Add(panGesture);
            panGesture.PanUpdated += panGesture_PanUpdated;
            TabLayout();
            Content = m_Parent;
            SelectTabPage(SelectedIndex);
            SetHeaderColor();
        }

        /// <summary>
        /// The SetHeaderColor.
        /// </summary>
        internal void SetHeaderColor()
        {
            foreach (var page in MauiTabPages)
            {
                if (page.Header.HeaderLabel != null)
                {
                    page.Header.HeaderLabel.TextColor = HeaderTextColor;
                }
            }
        }

        /// <summary>
        /// The OnTabClicked.
        /// </summary>
        /// <param name="e">The e<see cref="OnTabClickedEventArgs"/>.</param>
        internal void OnTabClicked(OnTabClickedEventArgs e)
        {
            TabClicked?.Invoke(this, e);
        }
    }
}
