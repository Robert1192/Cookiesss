using System.Net;
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

namespace CookiesPr
{
    public partial class MainWindow : Window
    {
        private int cookiecount = 0; /// First number of cookies
        private int cookiePerClick = 1; /// Number of Cookies that get added
        private int UpgradeCost = 10; //Price of Upgrade

        public MainWindow()
        {
            InitializeComponent();
            UpdateCookieMeter();
        }
        private void CookieDisplay_Click(object sender, RoutedEventArgs e)
        {
            /// this event is triggered when the button is clicked 
            cookiecount++;
            CookieMeter.Text = cookiecount.ToString();
        }

        private void UpdateCookieMeter()
        {

            CookieMeter.Text = $"Cookies: {cookiecount} (Click Power : {cookiePerClick}";

        }
        private void CookieDisplayCLick(object sender, RoutedEventArgs e)
        {
            cookiecount += cookiePerClick; // increse cookies by the current power
            UpdateCookieMeter(); // Update the UI display

        }

        private void UpgradePowerClick_CLick(object sender, RoutedEventArgs e)
        {
            if (cookiecount >= UpgradeCost) //Check if the plaer has enough cookies
            {
                cookiecount -= UpgradeCost; // Take the cookies
                cookiePerClick++;
                UpgradeCost += 20; // Increase the upgrade cost
                UpdateCookieMeter(); // Update the UI
            }
            else
            {
                MessageBox.Show("Not enough cookies");
            }

        }


        private void SettingsIcon_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Language=English  Music=On Audio=On");
        }
    }
}