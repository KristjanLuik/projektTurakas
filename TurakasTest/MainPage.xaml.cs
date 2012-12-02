using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TurakasTest
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public ViewModel _view;
        protected string WPFSPECIAL = "A";

        public MainPage(ViewModel model)
        {
            this.InitializeComponent();
            _view = model;
            _view.PropertyChanged += new PropertyChangedEventHandler(PropertyChangedInPlayerView);
        }
        
        void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData != null)
                this._view = e.ExtraData as ViewModel;
        }

        /// <summary>
        /// In idea int move should correspond to cards on table (cards on table +1)
        /// </summary>
        /// <param name="move"></param>
        public void setGameArea(int move)
        {
            GameAreaGrid.DataContext = _view.CardsOnTable;
            GameAreaGrid.IsEnabled = true;
            GameAreaGrid.AllowDrop = true;
            frameGameEnd.Visibility = System.Windows.Visibility.Collapsed;

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected void OnLoaded(object sender, RoutedEventArgs e)
        {
            NavigationService ns = this.NavigationService;
            if (this.NavigationService != null)
                this.NavigationService.LoadCompleted += new LoadCompletedEventHandler(NavigationService_LoadCompleted);
            _view.setPageRef(this);
            setGameArea(_view.CardsOnTable.Count + 1);
            //setCurrentPlayersSettings();
            gridPlayer1.DataContext = _view.CurrentPlayer;
            player1name.Text = _view.CurrentPlayer.Name;
            setHandDisplay();
            //////////////////////////////////////////////////////////////////////////////////7
            gwPl1Hand.IsEnabled = true;
            gwPl1Hand.IsHitTestVisible = true;
            gwPl1Hand.AllowDrop = true;
            ///////////////////////////////////////////////////////////////////////////////////
            if (_view.MoveIndex == _view.CurrentPlayer.Id)
                rect1Action.Visibility = System.Windows.Visibility.Visible;
            else
                rect1Action.Visibility = System.Windows.Visibility.Collapsed;
            //display trump
            addImageToCard(_view.Trump);
            vbxTrump.DataContext = _view.Trump;
            imgTrump.Source = _view.Trump.Image.Source;


            if (_view.OtherPlayers.Count >= 1)
            {
                gridPlayer2.DataContext = _view.OtherPlayers.ElementAt(0);
                player2_name.Text = _view.OtherPlayers.ElementAt(0).Name;
                if (_view.MoveIndex == _view.OtherPlayers.ElementAt(0).Id || _view.HitIndex == _view.OtherPlayers.ElementAt(0).Id)
                    rect2Action.Visibility = System.Windows.Visibility.Visible;
                else
                    rect2Action.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                gridPlayer6.Visibility = System.Windows.Visibility.Collapsed;
                gridPlayer5.Visibility = System.Windows.Visibility.Collapsed;
                gridPlayer4.Visibility = System.Windows.Visibility.Collapsed;
                gridPlayer3.Visibility = System.Windows.Visibility.Collapsed;
                gridPlayer2.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (_view.OtherPlayers.Count >= 2)
            {
                gridPlayer3.DataContext = _view.OtherPlayers.ElementAt(1);
                player3_name.Text = _view.OtherPlayers.ElementAt(1).Name;
                if (_view.MoveIndex == _view.OtherPlayers.ElementAt(1).Id || _view.HitIndex == _view.OtherPlayers.ElementAt(1).Id)
                    rect3Action.Visibility = System.Windows.Visibility.Visible;
                else
                    rect3Action.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                gridPlayer6.Visibility = System.Windows.Visibility.Collapsed;
                gridPlayer5.Visibility = System.Windows.Visibility.Collapsed;
                gridPlayer4.Visibility = System.Windows.Visibility.Collapsed;
                gridPlayer3.Visibility = System.Windows.Visibility.Collapsed;

            }
            if (_view.OtherPlayers.Count >= 3)
            {
                gridPlayer4.DataContext = _view.OtherPlayers.ElementAt(2);
                player4name.Text = _view.OtherPlayers.ElementAt(2).Name;
                if (_view.MoveIndex == _view.OtherPlayers.ElementAt(2).Id || _view.HitIndex == _view.OtherPlayers.ElementAt(2).Id)
                    rect4Action.Visibility = System.Windows.Visibility.Visible;
                else
                    rect4Action.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                gridPlayer6.Visibility = System.Windows.Visibility.Collapsed;
                gridPlayer5.Visibility = System.Windows.Visibility.Collapsed;
                gridPlayer4.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (_view.OtherPlayers.Count >= 4)
            {
                gridPlayer5.DataContext = _view.OtherPlayers.ElementAt(3);
                player5name.Text = _view.OtherPlayers.ElementAt(3).Name;
                if (_view.MoveIndex == _view.OtherPlayers.ElementAt(3).Id || _view.HitIndex == _view.OtherPlayers.ElementAt(3).Id)
                    rect5Action.Visibility = System.Windows.Visibility.Visible;
                else
                    rect5Action.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                gridPlayer6.Visibility = System.Windows.Visibility.Collapsed;
                gridPlayer5.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (_view.OtherPlayers.Count >= 5)
            {
                gridPlayer6.DataContext = _view.OtherPlayers.ElementAt(4);
                player6name.Text = _view.OtherPlayers.ElementAt(4).Name;
                if (_view.MoveIndex == _view.OtherPlayers.ElementAt(4).Id || _view.HitIndex == _view.OtherPlayers.ElementAt(4).Id)
                    rect6Action.Visibility = System.Windows.Visibility.Visible;
                else
                    rect6Action.Visibility = System.Windows.Visibility.Collapsed;
            }
            else { gridPlayer6.Visibility = System.Windows.Visibility.Collapsed; }
        }

        private void btnDisplay_Click(object sender, RoutedEventArgs e)
        {
            _view.deal();
            addImagesToCards();
            foreach (Card kaart in _view.CurrentPlayer.Hand)
            {
                gwPl1Hand.Items.Add(kaart.Image);
            }
            gwPl1Hand.IsManipulationEnabled = true;
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

        public void addImageToCard(Card c)
        {
            if (c == null)
            {
                return;
            }
            StringBuilder fileName = new StringBuilder();
            StringBuilder iName = new StringBuilder();
            fileName.Append(@"/Assets/images/");
            fileName.Append(c.Kind.ToString());
            iName.Append(WPFSPECIAL);
            iName.Append(((int)c.Kind).ToString());
            fileName.Append(((int)c.Rank).ToString());
            iName.Append(((int)c.Rank).ToString());
            fileName.Append(".png");
            string fn = fileName.ToString();
            //c.Image.Source = ImageFromRelativePath(this, fn);
            BitmapImage img = new BitmapImage(new Uri(fn, UriKind.Relative));
           
            c.Image.Source = img;
            c.Image.Name = iName.ToString();
        }


        /// <summary>
        /// Ugly workaround
        /// </summary>
        private void setHandDisplay()
        {
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
        public static System.Windows.Media.Imaging.BitmapImage ImageFromRelativePath(FrameworkElement parent, string path)
        {
            //var uri = new Uri(parent.u  BaseUri, path);
            
           System.Windows.Media.Imaging.BitmapImage result = new System.Windows.Media.Imaging.BitmapImage();
           // result.UriSource = uri;
            return result;
        }

        private void btnEndGame_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            long id = _view.endPressed();
            if (id == 0)
            {
                SelectPage back = new SelectPage(_view.CurrentPlayer.Name);
                NavigationService.Navigate(back);
            }
            else
                return;
        }

        public void setCurrentPlayersSettings()
        {
            if (_view.CurrentPlayer.Id == _view.MoveIndex)
            {
                gwPl1Hand.IsEnabled = true;
                gwPl1Hand.IsManipulationEnabled = true;
            }
            else
            {
                //gwPl1Hand.IsEnabled = false;
                //gwPl1Hand.IsRightTapEnabled = false;
                gwPl1Hand.IsManipulationEnabled = true;
            }
        }

        private void dragEnter_GameArea(object sender, System.Windows.DragEventArgs e)
        {
            //TODO
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private StackPanel getNextSlot(int move)
        {
            switch (move)
            {
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
        //private void cardTapped(object sender, TappedRoutedEventArgs e)
        //{
        //    if (_view.CardsOnTable.Count % 2 == 0 && _view.CurrentPlayer.Id == _view.MoveIndex)
        //    {
        //        Card tappedCard = new Card(gwPl1Hand.SelectedItem as Image);
        //        addImageToCard(tappedCard);
        //        StackPanel slot = getNextSlot(_view.MoveNr);
        //        (slot.Children.FirstOrDefault() as Image).Source = (gwPl1Hand.SelectedItem as Image).Source;
        //        _view.moveMade(tappedCard);
        //        setHandDisplay();
        //    }
        //    else
        //    {
        //        if (_view.CardsOnTable.Count % 2 == 1 && _view.CurrentPlayer.Id == _view.HitIndex)
        //        {
        //            Card tappedCard = new Card(gwPl1Hand.SelectedItem as Image);
        //            addImageToCard(tappedCard);
        //            StackPanel slot = getNextSlot(_view.MoveNr);
        //            (slot.Children.FirstOrDefault() as Image).Source = (gwPl1Hand.SelectedItem as Image).Source;
        //            _view.hitMade(tappedCard);
        //            setHandDisplay();
        //        }

        //    }
        //}

        void PropertyChangedInPlayerView(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "RaisePropertyChanged":
                    updateView();
                    break;
            }
        }

        private void updateView()
        {
            
            if (_view.HitIndex != _view.CurrentPlayer.Id)
            {
                int j = _view.findPlayerIndexById(_view.HitIndex);
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Colors.HotPink;
                getRectByNumber(j).Fill = brush;
            }
            else
            {
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = _view.CurrentPlayer.Color;
                rect1Action.Fill = brush;
            }
            if (_view.MoveIndex != _view.CurrentPlayer.Id)
            {
                int j = _view.findPlayerIndexById(_view.HitIndex);
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = Colors.Green;
                getRectByNumber(j).Fill = brush;
            }
            else
            {
                SolidColorBrush brush = new SolidColorBrush();
                brush.Color = _view.CurrentPlayer.Color;
                rect1Action.Fill = brush;
            }
        }

        private Rectangle getRectByNumber(int index) {
            switch (index)
            {
                case 0:
                    return rect1Action;
                case 1:
                    return rect2Action;
                case 2:
                    return rect3Action;
                case 3:
                    return rect4Action;
                case 4:
                    return rect5Action;
                default:
                    return rect6Action;
            }
        }

        private void updateGameArea(Card c)
        {

            addImageToCard(c);
            StackPanel slot = getNextSlot(_view.MoveNr);
            System.Collections.IEnumerator enumer= slot.Children.GetEnumerator();
            enumer.MoveNext();
            (enumer.Current as Image).Source = c.Image.Source;
        }

        public static void notifyGameAreaUpdate(Card card, MainPage element)
        {

            element.updateGameArea(card);
        }

        private void Card_Clicked(object sender, MouseButtonEventArgs e)
        {
             if (_view.CardsOnTable.Count % 2 == 0 && _view.CurrentPlayer.Id == _view.MoveIndex)
             {
                 Card tappedCard = new Card(gwPl1Hand.SelectedItem as Image);
                 addImageToCard(tappedCard);
                 StackPanel slot = getNextSlot(_view.MoveNr);
                 firstOrDefault(slot).Source = (gwPl1Hand.SelectedItem as Image).Source;
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
                     firstOrDefault(slot).Source = (gwPl1Hand.SelectedItem as Image).Source;
                     _view.hitMade(tappedCard);
                     setHandDisplay();
                 }

             }
        }

        private Image firstOrDefault(StackPanel slot) {
            System.Collections.IEnumerator enumerator = slot.Children.GetEnumerator();
            enumerator.MoveNext();
            return (enumerator.Current as Image);
        }

    }
}

