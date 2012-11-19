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
        /// In idea int move should correspond to cards on table (cards on table +1)
        /// </summary>
        /// <param name="move"></param>
        public void setGameArea(int move) {
            GameAreaGrid.DataContext = _view.CardsOnTable;
            GameAreaGrid.IsTapEnabled = true;
            GameAreaGrid.AllowDrop = true;
            //switch (move) { 
            //    case 1:
            //        stkpMove1.AllowDrop = true; break;
            //    case 2:
            //        stkpMove11.AllowDrop = true; break;
            //    case 3:
            //        stkpMove2.AllowDrop = true; break;
            //    case 4:
            //        stkpMove21.AllowDrop = true; break;
            //    case 5:
            //        stkpMove3.AllowDrop = true; break;
            //    case 6:
            //        stkpMove31.AllowDrop = true; break;
            //    case 7:
            //        stkpMove4.AllowDrop = true; break;
            //    case 8:
            //        stkpMove41.AllowDrop = true; break;
            //    case 9:
            //        stkpMove5.AllowDrop = true; break;
            //    case 10:
            //        stkpMove51.AllowDrop = true; break;
            //    case 11:
            //        stkpMove6.AllowDrop = true; break;
            //    case 12:
            //        stkpMove61.AllowDrop = true; break;
            //}
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _view = e.Parameter as PlayerView;
            setGameArea(_view.CardsOnTable.Count +1);
            //setCurrentPlayersSettings();
            gridPlayer1.DataContext = _view.CurrentPlayer;
            player1name.Text = _view.CurrentPlayer.Name;
            addImagesToCards();
            foreach (Card kaart in _view.CurrentPlayer.Hand)
            {
                gwPl1Hand.Items.Add(kaart.Image);
            }
            //////////////////////////////////////////////////////////////////////////////////7
            gwPl1Hand.CanDragItems = true;
            gwPl1Hand.CanReorderItems = true;
            gwPl1Hand.IsHitTestVisible = true;
            gwPl1Hand.AllowDrop = true;
            ///////////////////////////////////////////////////////////////////////////////////
            if (_view.MoveIndex == _view.CurrentPlayer.Id)
                rect1Action.Visibility = Windows.UI.Xaml.Visibility.Visible;
            else
                rect1Action.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            //display trump
            addImageToCard(_view.Trump);
            vbxTrump.DataContext = _view.Trump;
            imgTrump.Source = _view.Trump.Image.Source;
            
            
            if (_view.OtherPlayers.Count >= 1)
            {
                gridPlayer2.DataContext = _view.OtherPlayers.ElementAt(0);
                player2_name.Text = _view.OtherPlayers.ElementAt(0).Name;
                if (_view.MoveIndex == _view.OtherPlayers.ElementAt(0).Id)
                    rect2Action.Visibility = Windows.UI.Xaml.Visibility.Visible;
                else
                    rect2Action.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
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
                if (_view.MoveIndex == _view.OtherPlayers.ElementAt(1).Id)
                    rect3Action.Visibility = Windows.UI.Xaml.Visibility.Visible;
                else
                    rect3Action.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
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
                if (_view.MoveIndex == _view.OtherPlayers.ElementAt(2).Id)
                    rect4Action.Visibility = Windows.UI.Xaml.Visibility.Visible;
                else
                    rect4Action.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
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
                if (_view.MoveIndex == _view.OtherPlayers.ElementAt(3).Id)
                    rect5Action.Visibility = Windows.UI.Xaml.Visibility.Visible;
                else
                    rect5Action.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else { 
                gridPlayer6.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                gridPlayer5.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            if (_view.OtherPlayers.Count >= 5)
            {
                gridPlayer6.DataContext = _view.OtherPlayers.ElementAt(4);
                player6name.Text = _view.OtherPlayers.ElementAt(4).Name;
                if (_view.MoveIndex == _view.OtherPlayers.ElementAt(4).Id)
                    rect6Action.Visibility = Windows.UI.Xaml.Visibility.Visible;
                else
                    rect6Action.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
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
                addImageToCard(kaart);
            }

        }

        public void addImageToCard(Card c) {
            StringBuilder fileName = new StringBuilder();
            fileName.Append(@"/Assets/images/");
            fileName.Append(c.Kind.ToString());
            fileName.Append(((int)c.Rank).ToString());
            fileName.Append(".png");
            string fn = fileName.ToString();
            c.Image.Source = ImageFromRelativePath(this, fn);
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

        private void btnEndGame_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            int id = _view.endPressed();
            if (id == 0)
                Frame.Navigate(typeof(OptionsPage), _view.CurrentPlayer.Name);
            else
                return;
        }

        public void setCurrentPlayersSettings()
        {
            if (_view.CurrentPlayer.Id == _view.MoveIndex)
            {
                gwPl1Hand.IsEnabled = true;
                gwPl1Hand.IsRightTapEnabled = true;
                gwPl1Hand.IsItemClickEnabled = true;
                gwPl1Hand.IsTapEnabled = true;
                gwPl1Hand.SelectionMode = Windows.UI.Xaml.Controls.ListViewSelectionMode.Single;
                gwPl1Hand.CanDragItems = true;
                gwPl1Hand.CanReorderItems = true;
            }
            else {
                //gwPl1Hand.IsEnabled = false;
                gwPl1Hand.IsRightTapEnabled = false;
                gwPl1Hand.IsItemClickEnabled = false;
                gwPl1Hand.IsTapEnabled = false;
                gwPl1Hand.CanDragItems = true;
                gwPl1Hand.CanReorderItems = true;
            }
        }

        private void dragEnter_GameArea(object sender, Windows.UI.Xaml.DragEventArgs e)
        {
            //TODO
        }
       
     

        private void cardTapped(object sender, TappedRoutedEventArgs e)
        {
            if (_view.CardsOnTable.Count % 2 == 0)
            {
                
            }
            else
            {

            }
        }
    }
}
