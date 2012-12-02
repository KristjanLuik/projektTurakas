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
using Turakas.ViewModel;
using System.Collections.ObjectModel;
using Turakas.ServiceReference1;


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Turakas.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class OptionsPage : Turakas.Common.LayoutAwarePage
    {
        string _playerName;
        public PlayerView view;

        public OptionsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _playerName = e.Parameter as string;

            if (!string.IsNullOrWhiteSpace(_playerName))
            {
                username.Text = "Hello, " + _playerName;
            }
            else
            {
                username.Text = "Name is required.  Go back and enter a name.";
            }
            frameMain.Visibility = Windows.UI.Xaml.Visibility.Visible;
            frameStart.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            frameJoin.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
      
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void btnhelp_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HelpPage));
        }

        private  void stkStart_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            frameStart.Visibility = Windows.UI.Xaml.Visibility.Visible;
            frameJoin.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            //Frame.Navigate(typeof(MainPage), _playerName);
            if (view == null || !view.CurrentPlayer.Name.Equals(_playerName))
            {
                view = new PlayerView(_playerName);
            }
            ObservableCollection<ServiceUser> joiners = new ObservableCollection<ServiceUser>();
            view.getJoiners();
            lbxAddToGame.ItemsSource = view.JoinersList;
            //populate joiners list
            
        }
        

        private void stkJoin_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            frameStart.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            frameJoin.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //Frame.Navigate(typeof(MainPage), _playerName);
            if (view == null || !view.CurrentPlayer.Name.Equals(_playerName))
            {
                view = new PlayerView(_playerName);
            }
            view.getGames();
            frameJoin.DataContext = view;
            lbxJoinGame.ItemsSource = view.GameList;
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
      
        private void btnPick_Click(object sender, RoutedEventArgs e)
        {
            if (lbxAddToGame.SelectedItems.Count != 0)
            {
                ObservableCollection<ServiceUser> selected = new ObservableCollection<ServiceUser>();
                for (int i = lbxAddToGame.SelectedItems.Count - 1; i >= 0; i--)
                {
                    selected.Add(lbxAddToGame.SelectedItems[i] as ServiceUser);
                }
                view.addNewPlayer(selected);
                // lbxAdded.DataContext = view.OtherPlayers;
                view.initGame();
                Frame.Navigate(typeof(MainPage), view);
            }
            else {
                /////////////////////////////////////////////////////////////////////////////////////////////////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                //DELETE IF PROJECT IS FINISHED!
                //throw new ArgumentException("For some reason listbox items did not get selected");
            }

        }

        private void btnPickGame_Click(object sender, RoutedEventArgs e)
        {
            if (lbxJoinGame.SelectedItems.Count != 0)
            {
                Game game = (Game) lbxJoinGame.SelectedItem;
                view.applyForSelectedGame(_playerName, game);
                frameWait.Visibility = Windows.UI.Xaml.Visibility.Visible;
                frameJoin.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                lbxWait.DataContext = view.getOtherApplyers(game);
            }
            else
            {
            }
        }

        
    }
}
