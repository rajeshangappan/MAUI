using Maui.TabView.Control;
using System;

namespace BTMSample;

public partial class CustomTabView : ContentPage
{
    #region PRIVATE_VARIABLES

    /// <summary>
    /// Defines the m_index.
    /// </summary>
    private int m_index = 0;

    #endregion

    #region PUBLIC_PPTY

    /// <summary>
    /// Gets or sets the Index.
    /// </summary>
    public int Index
    {
        get => m_index;
        set
        {
            m_index = value;
            OnPropertyChanged(nameof(Index));
        }
    }

    #endregion
    public CustomTabView()
	{
		InitializeComponent();
        BindingContext = this;
        Index = 1;
        Tabcomponent.TabClicked += Tabcomponent_TabClicked;
    }

    private void Tabcomponent_TabClicked(object sender, OnTabClickedEventArgs args)
    {
        //throw new NotImplementedException();
    }

    /// <summary>
    /// The Button_Clicked.
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/>.</param>
    /// <param name="e">The e<see cref="System.EventArgs"/>.</param>
    private void Button_Clicked(object sender, System.EventArgs e)
    {
        var control = Tabcomponent;
        Index = (control.SelectedIndex + 1) % 4;
        //testing.Add(createTab());
    }

    /// <summary>
    /// The createTab.
    /// </summary>
    /// <returns>The <see cref="XFTabControl"/>.</returns>
    private MauiTabControl createTab()
    {
        MauiTabControl tab = new MauiTabControl
        {
            //Apply the Styles to Tab
            HeaderColor = Color.FromRgba("#00ff00"),
            HeaderHeight = 30,
            VerticalOptions = LayoutOptions.Fill,
            //Change the Header Position.
            Position = Position.Top
        };

        MauiTabPage page1 = new MauiTabPage();

        //Create the Header title

        page1.Header.Title = "Tab1";



        Tab1ContentPage contentpage = new Tab1ContentPage
        {
            Content = new Label
            {
                Text = "This Page Displays Tab1 Content"
            }
        };

        //Assign the contentpage to first tab.

        page1.CustomContentPage = contentpage;

        //Add the Page to tab control

        tab.AddPage(page1);

        //Create the Tab Page

        MauiTabPage page2 = new MauiTabPage();

        //Assign the header Text
        Label headerLabel = new Label
        {
            Text = "Tab2"
        };

        page2.Header.Content = headerLabel;

        // Page content
        Label content2 = new Label
        {
            Text = "This Page Displays Tab2 Content"
        };

        //Assign the tab body content.

        page2.Content = content2;

        //Add the Page to tab control

        tab.AddPage(page2);

        tab.TabClicked += Tab_TabClicked;

        return tab;
    }

    /// <summary>
    /// The Tab_TabClicked.
    /// </summary>
    /// <param name="sender">The sender<see cref="object"/>.</param>
    /// <param name="args">The args<see cref="OnTabClickedEventArgs"/>.</param>
    private void Tab_TabClicked(object sender, OnTabClickedEventArgs args)
    {
    }
}