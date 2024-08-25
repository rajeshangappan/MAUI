namespace Maui.TabView.Control
{
    public class MauiTabPage
    {
        #region Fields

        /// <summary>
        /// Defines the m_content
        /// </summary>
        internal ContentPage m_content;

        /// <summary>
        /// Defines the m_contentPage
        /// </summary>
        internal ContentPage m_contentPage;

        /// <summary>
        /// Defines the XFTabParent
        /// </summary>
        internal MauiTabControl XFTabParent;

        /// <summary>
        /// Defines the m_header
        /// </summary>
        private MauiTabHeader m_header;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MauiTabPage"/> class.
        /// </summary>
        public MauiTabPage()
        {
            m_content = new ContentPage();
            Header = new MauiTabHeader();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Content
        /// </summary>
        public View Content { get => m_content.Content; set => m_content.Content = value; }

        /// <summary>
        /// Gets or sets the CustomContentPage
        /// </summary>
        public ContentPage CustomContentPage { get => m_content; set => m_content = value; }

        /// <summary>
        /// Gets or sets the Header
        /// </summary>
        public MauiTabHeader Header
        {
            get => m_header;
            set
            {
                m_header = value;
                InitHeaderEvent();
            }
        }

        #endregion

        #region PRIVATE_METHODS

        /// <summary>
        /// The TapGestureRecognizer_Tapped
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="EventArgs"/></param>
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            OnTabClickedEventArgs args = new(this)
            {
                SelectedIndex = XFTabParent.MauiTabPages.IndexOf(this)
            };
            XFTabParent.OnTabClicked(args);
            XFTabParent.SelectTabPage(this, args.IgnoreSelectionUpdate);
        }

        #endregion

        /// <summary>
        /// The InitHeaderEvent
        /// </summary>
        internal void InitHeaderEvent()
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            m_header.GestureRecognizers.Add(tapGestureRecognizer);
            tapGestureRecognizer.Tapped -= TapGestureRecognizer_Tapped;
            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
        }
    }
}
