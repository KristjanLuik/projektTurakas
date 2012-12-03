using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TurakasServiceLibrary;

namespace TurakasTest
{
    /// <summary>
    /// Interaction logic for SelectPage.xaml
    /// </summary>
  
    public partial class SelectPage : Page
    {
        string _playerName;
        public ViewModel view;
        public Application app;

        public SelectPage(string name)
        {
            InitializeComponent();
            _playerName = name;
        }
        public void OnLoaded(object sender, RoutedEventArgs e)
        {
            
            _playerName = (string) Application.Current.Properties["userName"];

            if (!string.IsNullOrWhiteSpace(_playerName))
            {
                username.Text = "Hello, " + _playerName;
            }
            else
            {
                username.Text = "Name is required.  Go back and enter a name.";
            }
            frameMain.Visibility = System.Windows.Visibility.Visible;
            frameStart.Visibility = System.Windows.Visibility.Visible;
            frameJoin.Visibility = System.Windows.Visibility.Collapsed;
        }

        

        private void btnhelp_Click(object sender, RoutedEventArgs e)
        {
           // Frame.Navigate(typeof(HelpPage));
        }

        private void stkStart_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Visibility = System.Windows.Visibility.Collapsed;
            frameStart.Visibility = System.Windows.Visibility.Visible;
            frameJoin.Visibility = System.Windows.Visibility.Collapsed;
            //app.Properties["playerName"] = _playerName;
            //NavigationService.Navigate(typeof(MainPage));
            if (view == null || !view.CurrentPlayer.Name.Equals(_playerName))
            {
                view = new ViewModel(_playerName);
                view.Sp = this;
            }
            view.getJoiners();
            lbxAddToGame.ItemsSource = view.JoinersList;
            //populate joiners list

        }

        private void stkJoin_Click(object sender, RoutedEventArgs e)
        {
            frameMain.Visibility = System.Windows.Visibility.Collapsed;
            frameStart.Visibility = System.Windows.Visibility.Collapsed;
            frameJoin.Visibility = System.Windows.Visibility.Visible;
            //Frame.Navigate(typeof(MainPage), _playerName);
            if (view == null || !view.CurrentPlayer.Name.Equals(_playerName))
            {
                view = new ViewModel(_playerName);
                view.Sp = this;
            }
            view.getGames();
            frameJoin.DataContext = view;
            lbxJoinGame.ItemsSource = view.GameList;
        }

        private void btnPick_Click(object sender, RoutedEventArgs e)
        {
            if (lbxAddToGame.SelectedItems.Count != 0)
            {
                List<Player> selected = new List<Player>();
                for (int i = lbxAddToGame.SelectedItems.Count - 1; i >= 0; i--)
                {
                    selected.Add(lbxAddToGame.SelectedItems[i] as Player);
                }
                view.addNewPlayer(selected);
                // lbxAdded.DataContext = view.OtherPlayers;
                view.initGame();
                view.Client.getServiceInterface().turnPage(view.GameId);
                //MainPage page = new MainPage(view);
                //NavigationService.Navigate(page);
            }
            else
            {
            }

        }

        private void btnPickGame_Click(object sender, RoutedEventArgs e)
        {
            if (lbxJoinGame.SelectedItems.Count != 0)
            {
                Game game = (Game)lbxJoinGame.SelectedItem;
                view.applyForSelectedGame(_playerName, game);
                frameWait.Visibility = System.Windows.Visibility.Visible;
                frameJoin.Visibility = System.Windows.Visibility.Collapsed;
                lbxWait.DataContext = view.getOtherApplyers(game);
            }
            else
            {
            }
        }

    }
}
