﻿

#pragma checksum "C:\Users\katrepaabo\Desktop\Turakas\Turakas\Views\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "999EEB37E9A475BB2787450E595AB5DA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Turakas.Views
{
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 128 "..\..\Views\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).DragEnter += this.dragEnter_GameArea;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 199 "..\..\Views\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.cardTapped;
                 #line default
                 #line hidden
                #line 201 "..\..\Views\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).DragItemsStarting += this.GridViewDragItemsStarting;
                 #line default
                 #line hidden
                #line 201 "..\..\Views\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Drop += this.GridViewDrop;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 35 "..\..\Views\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.btnEndGame_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}

