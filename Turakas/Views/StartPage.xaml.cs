using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Turakas.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartPage : Page
    {
        public  String _playerName;

        public StartPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void input_tapped(object sender, TappedRoutedEventArgs e)
        {
            if (inputName.Text == "")
                return;
            else {
                _playerName = inputName.Text;
                
                Frame.Navigate(typeof(OptionsPage), _playerName);
            }
        }

        private void input_enter_pressed(object sender, KeyRoutedEventArgs e)
        {
            if ((e.Key == Windows.System.VirtualKey.Enter))
            {
                if (inputName.Text == "")
                    return;
                else
                {
                    _playerName = inputName.Text;
                    Frame.Navigate(typeof(OptionsPage), _playerName);
                }
            }
            else { return; }
        }
    }
}
