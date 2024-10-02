using System.Diagnostics.Eventing.Reader;
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
using System.Windows.Threading;

namespace CookiesPr
{
    public partial class MainWindow : Window
    {
        private int cookies = 0; // Track the number of cookies
        private int cookiesPerClick = 1; // Cookies earned per click
        private int upgradeCost = 10; // Cost of each click power upgrade
        private int autoClickerCost = 100; // Cost of the auto clicker upgrade
        private DispatcherTimer autoClickerTimer; // Timer for the auto clicker
        private bool isAutoClickerActive = false; // Track whether the auto-clicker is active
        private int autoClickDuration = 10; // Auto-clicker will run for 10 seconds
        private int autoClickCounter = 0; // Counter for auto-click events

        public MainWindow()
        {
            InitializeComponent();
            UpdateCookieMeter();
            autoClickerTimer = new DispatcherTimer();
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
                upgradeCost *= 2; // Increase the cost for the next upgrade
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
            // Check if the player has enough cookies for the auto clicker upgrade
            if (cookies >= autoClickerCost && !isAutoClickerActive)
            {
                cookies -= autoClickerCost; // Deduct the cost
                UpdateCookieMeter(); // Update the UI display

                StartAutoClicker(); // Start the auto-clicker
            }
            else if (isAutoClickerActive)
            {
                MessageBox.Show("Auto Clicker is already active!"); // Notify player
            }
            else
            {
                MessageBox.Show("Not enough cookies to buy Auto Clicker!"); // Notify player
            }
        }

        private void StartAutoClicker()
        {
            // Set auto-clicker as active
            isAutoClickerActive = true;
            autoClickCounter = 0; // Reset the auto-click counter

            // Initialize the DispatcherTimer to simulate clicks every second
            autoClickerTimer = new DispatcherTimer();
            autoClickerTimer.Interval = TimeSpan.FromSeconds(1); // Set interval to 1 second
            autoClickerTimer.Tick += AutoClick; // Hook up the event to simulate clicks
            autoClickerTimer.Start();
        }
        private void AutoClick(object? sender, EventArgs e) // Allow nullable sender
        {
            if (autoClickCounter < autoClickDuration)
            {
                cookies += cookiesPerClick;
                UpdateCookieMeter();
                autoClickCounter++;
            }
            else
            {
                autoClickerTimer.Stop();
                isAutoClickerActive = false;
            }
        }
        private void SettingsIcon_Click(object sender, RoutedEventArgs e)
        {
            // Logic for settings icon click (if implemented)
        }
    }
}


