using System;
using System.Collections.Generic;
using System.Linq;
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

namespace TurakasTest
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : System.Windows.Controls.Page
    {
        public String _playerName;
        public Application app = Application.Current;


        public LoginPage()
        {
            InitializeComponent();
        }

        private void input_enter_pressed(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter))
            {
                if (inputName.Text == "")
                    return;
                else
                {
                    _playerName = inputName.Text;
                    app.Properties["userName"] = _playerName;
                    SelectPage nextPage = new SelectPage(_playerName);
                    this.NavigationService.Navigate(nextPage);
                }
            }
            else { return; }
        }
    }
}
