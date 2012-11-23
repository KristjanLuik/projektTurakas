using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using System.Text;
using Windows.UI.Xaml.Media.Animation;
using Turakas.classes;
using Turakas.ViewModel;
using System.ComponentModel;
using System.Collections.Specialized;
using TurakasLibrary;

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
//        Style style = new Style {TargetType = typeof(Rectangle)};
//style.Setters.Add(new Setter(Shape.FillProperty, Brushes.Red));
//style.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));

//Application.Current.Resources["key1"] = style;
        /// <summary>
        /// In idea int move should correspond to cards on table (cards on table +1)
        /// </summary>
        /// <param name="move"></param>
        public void setGameArea(int move) {
            GameAreaGrid.DataContext = _view.CardsOnTable;
            GameAreaGrid.IsTapEnabled = true;
            GameAreaGrid.AllowDrop = true;
            frameGameEnd.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _view = e.Parameter as PlayerView;
            _view.setPageRef(this);
            setGameArea(_view.CardsOnTable.Count +1);
            //setCurrentPlayersSettings();
            gridPlayer1.DataContext = _view.CurrentPlayer;
            player1name.Text = _view.CurrentPlayer.Name;
            setHandDisplay();
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
                if (_view.MoveIndex == _view.OtherPlayers.ElementAt(0).Id || _view.HitIndex == _view.OtherPlayers.ElementAt(0).Id)
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
                if (_view.MoveIndex == _view.OtherPlayers.ElementAt(1).Id || _view.HitIndex == _view.OtherPlayers.ElementAt(1).Id)
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
                if (_view.MoveIndex == _view.OtherPlayers.ElementAt(2).Id || _view.HitIndex == _view.OtherPlayers.ElementAt(2).Id)
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
                if (_view.MoveIndex == _view.OtherPlayers.ElementAt(3).Id || _view.HitIndex == _view.OtherPlayers.ElementAt(3).Id)
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
                if (_view.MoveIndex == _view.OtherPlayers.ElementAt(4).Id || _view.HitIndex == _view.OtherPlayers.ElementAt(4).Id)
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
            StringBuilder iName = new StringBuilder();
            fileName.Append(@"/Assets/images/");
            fileName.Append(c.Kind.ToString());
            iName.Append(((int)c.Kind).ToString());
            fileName.Append(((int)c.Rank).ToString());
            iName.Append(((int)c.Rank).ToString());
            fileName.Append(".png");
            string fn = fileName.ToString();
            c.Image.Source = ImageFromRelativePath(this, fn);
            c.Image.Name = iName.ToString();
        }
       
        
        /// <summary>
        /// Ugly workaround
        /// </summary>
        private void setHandDisplay() {
            gwPl1Hand.Items.Clear();
            addImagesToCards();
            gwPl1Hand.DataContext = _view.CurrentPlayer.Hand;
            foreach (Card kaart in _view.CurrentPlayer.Hand)
            {
                gwPl1Hand.Items.Add(kaart.Image);
            }
        }

        /// <summary>
        /// Thank you StackOverFlow! Uses relative URI string to get the BitmapImage from file.
        /// </summary>
        /// <param name="parent">FrameworkElement assotiated with image</param>
        /// <param name="path">String representing relative URI</param>
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
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private StackPanel getNextSlot(int move) {
            switch (move) { 
                case 1:
                    return stkpMove1;
                case 2:
                    return stkpMove11;
                case 3:
                    return stkpMove2;
                case 4:
                    return stkpMove21;
                case 5:
                    return stkpMove3;
                case 6:
                    return stkpMove31;
                case 7:
                    return stkpMove4;
                case 8:
                    return stkpMove41;
                case 9:
                    return stkpMove5;
                case 10:
                    return stkpMove51;
                case 11:
                    return stkpMove6;
                default:
                    return stkpMove61;
        }
        }
        //private void GridViewDragItemsStarting(object sender, DragItemsStartingEventArgs e)
        //{
        //    var item = e.Items.FirstOrDefault();
        //    if (item == null)
        //        return;
        //    var card = gwPl1Hand.SelectedItem;
        //    e.Data.Properties.Add("item", item);
        //    e.Data.Properties.Add("card", card);
        //    e.Data.Properties.Add("gridSource", sender);
        //}

        //private void GridViewDrop(object sender, DragEventArgs e)
        //{
        //    object gridSource;
        //    e.Data.Properties.TryGetValue("gridSource", out gridSource);

        //    //if (gridSource == sender)
        //    //  return;

        //    object sourceItem;
        //    e.Data.Properties.TryGetValue("item", out sourceItem);
        //    // if (sourceItem == null)
        //    //   return;
        //    object movedCard;
        //    e.Data.Properties.TryGetValue("card", out movedCard);

        //    _view.CardsOnTable.Add((Card)movedCard);
        //}

        private void cardTapped(object sender, TappedRoutedEventArgs e)
        {
            if (_view.CardsOnTable.Count % 2 == 0 && _view.CurrentPlayer.Id == _view.MoveIndex)
            {
                Card tappedCard = new Card(gwPl1Hand.SelectedItem as Image);
                addImageToCard(tappedCard);
                StackPanel slot = getNextSlot(_view.MoveNr);
                (slot.Children.FirstOrDefault() as Image).Source = (gwPl1Hand.SelectedItem as Image).Source;
                _view.moveMade(tappedCard);
                setHandDisplay();
            }
            else
            {
                if (_view.CardsOnTable.Count % 2 == 1 && _view.CurrentPlayer.Id == _view.HitIndex)
                {
                    Card tappedCard = new Card(gwPl1Hand.SelectedItem as Image);
                    addImageToCard(tappedCard);
                    StackPanel slot = getNextSlot(_view.MoveNr);
                    (slot.Children.FirstOrDefault() as Image).Source = (gwPl1Hand.SelectedItem as Image).Source;
                    _view.hitMade(tappedCard);
                    setHandDisplay();
                }

            }
        }
        void PropertyChangedInPlayerView(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CardsOnTable":
                    //updateGameArea(_view.CardsOnTable);
                    break;
            }
        }
        
        private void updateGameArea(Card c)
        {
            
            addImageToCard(c);
            StackPanel slot = getNextSlot(_view.MoveNr);
            (slot.Children.FirstOrDefault() as Image).Source = c.Image.Source;
        }

        public static void notifyGameAreaUpdate(Card card, MainPage element){
           
            element.updateGameArea(card);
        }

        private void gameAreaTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

        }

       
    }
}
