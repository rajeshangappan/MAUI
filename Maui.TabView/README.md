# Maui.TabView

![BuildStatus](https://raw.githubusercontent.com/rajeshangappan/MAUI/be6d97d8a03212800c85b96542f1f8574866e92f/buildsuccess.svg)

This Package support in Maui all platforms
     
     Create the Customized tabbed page or view using Maui.Tabview control.
Maui.TabView Features
- Tabbed Page and Header Customization. (Tab Header Height/Color/Content).
- Tab contet Page customization - We can create a MAUI view control as content or contentpage
- Tab Header Positioning (Top/Bottom)
- Tab page/content change events - Event will trigger while selecting the tab. you can customize your header selections.
- Add tab view to any layouts like (Stack/absolute or grid etc.)
# New Features!
  - IsVisble Support added for TabHeader.
  - SelectIndex Support added.
# Tabbed Header Customization Sample Code
 - Add the header using title property.
 - Add the Header using Header Content property.
```
  //Assign the Content to tabheader
  Label label = new Label{Text="Tab1"};
  PageHeader1.Content = label;
  //Use the Title for Default Header
  PageHeader1.Title = "Tab1";
```
# Tabbed Page content Customization
 - Add the tabview content by using content.
 - Add the tabview content by using custom content page.
```
  //content
  Label label = new Label{Text="Tab Page content"};
  Page1.Content = label;
  //Assign the page to tabview
  ContentPage samplePage = new ContentPage();
  Page1.CustomContentPage = samplePage;
```
Sample Link : [TabviewSample](https://github.com/rajeshangappan/MAUI/tree/main/BTMSample)
# Output
![TabView](https://github.com/rajeshangappan/MAUI/tree/main/Maui.TabView/MauiTabview.gif)
