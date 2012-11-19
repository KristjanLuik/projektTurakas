using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
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
using System.Text;
using Windows.UI.Xaml.Media.Animation;
using Turakas.classes;
using Turakas.ViewModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Turakas.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public PlayerView _view;

        public MainPage()
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
            _view = e.Parameter as PlayerView;
            gridPlayer1.DataContext = _view.CurrentPlayer;
            player1name.Text = _view.CurrentPlayer.Name;
            if (_view.OtherPlayers.Count >= 1)
            {
                gridPlayer2.DataContext = _view.OtherPlayers.ElementAt(0);
                player2_name.Text = _view.OtherPlayers.ElementAt(0).Name;
            }
            else
            {
                gridPlayer6.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                gridPlayer5.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                gridPlayer4.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                gridPlayer3.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                gridPlayer2.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            if (_view.OtherPlayers.Count >= 2)
            {
                gridPlayer3.DataContext = _view.OtherPlayers.ElementAt(1);
                player3_name.Text = _view.OtherPlayers.ElementAt(1).Name;
            }
            else
            {
                gridPlayer6.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                gridPlayer5.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                gridPlayer4.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                gridPlayer3.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            if (_view.OtherPlayers.Count >= 3)
            {
                gridPlayer4.DataContext = _view.OtherPlayers.ElementAt(2);
                player4name.Text = _view.OtherPlayers.ElementAt(2).Name;
            }
            else
            {
                gridPlayer6.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                gridPlayer5.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                gridPlayer4.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            if (_view.OtherPlayers.Count >= 4)
            {
                gridPlayer5.DataContext = _view.OtherPlayers.ElementAt(3);
                player5name.Text = _view.OtherPlayers.ElementAt(3).Name;
            }
            else { 
                gridPlayer6.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                gridPlayer5.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            if (_view.OtherPlayers.Count >= 5)
            {
                gridPlayer6.DataContext = _view.OtherPlayers.ElementAt(4);
                player6name.Text = _view.OtherPlayers.ElementAt(4).Name;
            }
            else { gridPlayer6.Visibility = Windows.UI.Xaml.Visibility.Collapsed; }
        }

        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {
            _view.deal();
            addImagesToCards();
            foreach (Card kaart in _view.CurrentPlayer.Hand)
            {
                gwPl1Hand.Items.Add(kaart.Image);
            }
            gwPl1Hand.CanDragItems = true;
            gwPl1Hand.CanReorderItems = true;
            gwPl1Hand.AllowDrop = true;
            
        }
        /// <summary>
        /// Add images to players cards
        /// </summary>
        public void addImagesToCards()
        {
            foreach (Card kaart in _view.CurrentPlayer.Hand)
            {
                StringBuilder fileName = new StringBuilder();
                fileName.Append(@"/Assets/images/");
                fileName.Append(kaart.Kind.ToString());
                fileName.Append(((int)kaart.Rank).ToString());
                fileName.Append(".png");
                string fn = fileName.ToString();
                kaart.Image.Source = ImageFromRelativePath(this, fn);
            }

        }

        /// <summary>
        /// Thank you StackOverFlow! Uses relative URI string to get the BitmapImage from file.
        /// </summary>
        /// <param name="parent">FrameworkElement assotiated with image</param>
        /// <param name="path">String representing relative URI</param>
        /// <returns></returns>
        public static Windows.UI.Xaml.Media.Imaging.BitmapImage ImageFromRelativePath(FrameworkElement parent, string path)
        {
            var uri = new Uri(parent.BaseUri, path);
            Windows.UI.Xaml.Media.Imaging.BitmapImage result = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
            result.UriSource = uri;
            return result;
        }

    }
}
