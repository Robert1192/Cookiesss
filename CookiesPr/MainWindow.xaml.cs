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
    private int cookies = 0; // Track the number of cookies
    private int cookiesPerClick = 1; // Cookies earned per click
    private int upgradeCost = 10; // Cost of each upgrade

    public MainWindow()
    {
        InitializeComponent();
        UpdateCookieMeter();
    }

    // Method to update the cookie meter display
    private void UpdateCookieMeter()
    {
        CookieMeter.Text = $"Cookies: {cookies} (Click Power: {cookiesPerClick})";
    }

    private void CookieDisplay_Click(object sender, RoutedEventArgs e)
    {
        cookies += cookiesPerClick; // Increase cookies by the current click power
        UpdateCookieMeter(); // Update the UI display
        }

        private void UpgradePowerClick_Click(object sender, RoutedEventArgs e)
        {
            if (cookies >= upgradeCost) // Check if the player has enough cookies
            {
                cookies -= upgradeCost; // Deduct the cost
                cookiesPerClick++; // Increase clicking power by 1
                upgradeCost += 5; // Increase the cost for the next upgrade
                UpgradePowerClick.Content = $"ClickPower ({upgradeCost})";
                UpdateCookieMeter(); // Update the UI display
            }
            else
            {
                MessageBox.Show("Not enough cookies to upgrade!"); // Notify the player
            }
        }


        private void UpgradeAutoClick_Click(object sender, RoutedEventArgs e)
    {
        // Logic for auto click upgrade (if implemented)
        // You can add similar logic as UpgradePowerClick_Click for auto clicks
    }

    private void SettingsIcon_Click(object sender, RoutedEventArgs e)
    {
        // Logic for settings icon click (if implemented)
    }
}
}