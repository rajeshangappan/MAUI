namespace Maui.TabView.Control
{
    public class MauiTabHeader : Border
    {
        #region Fields

        /// <summary>
        /// Defines the Selector
        /// </summary>
        internal BoxView Selector;

        /// <summary>
        /// Header Tab Page
        /// </summary>
        internal MauiTabPage XFParentTabPage;
        private bool _isSelected;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MauiTabHeader"/> class.
        /// </summary>
        public MauiTabHeader()
        {
            Padding = 0;
            Margin = 0;
            StrokeThickness = 0;
        }

        public static readonly BindableProperty IsSelectedProperty =
       BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(MauiTabHeader), false);


        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title
        {
            get => HeaderLabel.Text;
            set
            {
                HeaderLabel = new Label
                {
                    Text = value,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                };
                Content = HeaderLabel;
            }
        }

        /// <summary>
        /// IsSelected
        /// </summary>
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        /// <summary>
        /// Gets or sets the HeaderLabel
        /// </summary>
        internal Label HeaderLabel { get; set; }

        #endregion
    }
}
