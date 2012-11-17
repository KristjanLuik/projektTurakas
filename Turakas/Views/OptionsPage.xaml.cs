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
using Windows.UI.Xaml.Media.Animation;
using Windows.Networking.Connectivity;
using Windows.Networking.Sockets;
using TurakasLibrary;


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Turakas.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class OptionsPage : Turakas.Common.LayoutAwarePage
    {
        string connectionProfileList;
        private Game _game;

        public Game Game
        {
            get { return _game; }
            set { _game = value; }
        }

        public OptionsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void btnhelp_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HelpPage));
        }

        private void stkStart_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            frameStart.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //populate joiners list
            //Game.getCurrentPlayer().
        }
        

        private void stkJoin_Click(object sender, RoutedEventArgs e)
        {
            //TODO display all initiated games (not yet started)
        }

       

        private void updateVisuals(Button button,String eventDescription)
        {
            switch (eventDescription.ToLower())
            {
                case "exited":
                   
                        lobby.Background = new SolidColorBrush(Windows.UI.Colors.Red);
                    break;
                case "moved":

                    button.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Azure);
                    break;
                default:
                    button.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Green);
                    break;
            }
        }

        private void btnhelp_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
           
            
        }

        private void stkStart_PointerExited(object sender, PointerRoutedEventArgs e)
        {

        }

        private void stkStart_PointerEntered(object sender, PointerRoutedEventArgs e)
        {

        }

        
    }
}
