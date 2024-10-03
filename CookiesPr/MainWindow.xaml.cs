using System;
using System.Windows;
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
        private DispatcherTimer randomEventTimer; // Timer for triggering random events
        private bool isAutoClickerActive = false; // Track whether the auto-clicker is active
        private int autoClickDuration = 10; // Auto-clicker will run for 10 seconds
        private int autoClickCounter = 0; // Counter for auto-click events
        private Random random; // Random instance for event selection

        public MainWindow()
        {
            InitializeComponent();
            random = new Random(); // Initialize random
            UpdateCookieMeter();

            // Initialize the DispatcherTimer for the auto clicker
            autoClickerTimer = new DispatcherTimer();
            autoClickerTimer.Interval = TimeSpan.FromSeconds(1); // Set interval to 1 second
            autoClickerTimer.Tick += AutoClick; // Hook up the event to simulate clicks

            // Set up a timer to trigger random events every 30 seconds
            randomEventTimer = new DispatcherTimer();
            randomEventTimer.Interval = TimeSpan.FromSeconds(30);
            randomEventTimer.Tick += TriggerRandomEvent; // Hook up random event trigger
            randomEventTimer.Start(); // Start the random event timer
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
                UpgradePowerClick.Content = $"ClickPower ($ {upgradeCost})";
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
            autoClickerTimer.Start(); // Start the auto-clicker timer
        }

        private void AutoClick(object? sender, EventArgs e) // Mark sender as nullable
        {
            if (autoClickCounter < autoClickDuration)
            {
                cookies += cookiesPerClick; // Simulate auto-click
                UpdateCookieMeter(); // Update the UI display
                autoClickCounter++; // Increment the auto-click counter
            }
            else
            {
                autoClickerTimer.Stop(); // Stop the timer when auto-click duration is reached
                isAutoClickerActive = false; // Reset the auto-clicker state
            }
        }

        // Random event trigger (either burn or overload cookies)
        private void TriggerRandomEvent(object? sender, EventArgs e) // Mark sender as nullable
        {
            int eventChoice = random.Next(2); // Randomly choose between 0 (Burn) and 1 (Add)
            if (eventChoice == 0)
            {
                BurnCookies(this, EventArgs.Empty); // Trigger the BurnCookies event
            }
            else
            {
                AddCookies(this, EventArgs.Empty); // Trigger the AddCookies event
            }
        }

        // Button click to manually trigger BurnCookies
        private void BurnCookies_Click(object sender, RoutedEventArgs e)
        {
            BurnCookies(this, EventArgs.Empty); // Manually trigger burn cookies
        }

        // Button click to manually trigger AddCookies
        private void AddCookies_Click(object sender, RoutedEventArgs e)
        {
            AddCookies(this, EventArgs.Empty); // Manually trigger add cookies
        }

        // Event: Burn 25% of cookies
        private void BurnCookies(object sender, EventArgs e)
        {
            // Ensure cookies do not go negative
            if (cookies > 0)
            {
                cookies = (int)(cookies * 0.75); // Decrease by 25%
                MessageBox.Show("Event: Cookie Burner! -25% Cookies Lost."); // Message shown in message box
                UpdateCookieMeter();
            }
            else
            {
                MessageBox.Show("No cookies to burn!"); // Notify that there are no cookies to burn
            }
        }

        // Event: Add 25% more cookies
        private void AddCookies(object sender, EventArgs e)
        {
            cookies = (int)(cookies * 1.25); // Increase by 25%
            MessageBox.Show("Event: Cookie Overload! +25% Cookies Added."); // Message shown in message box
            UpdateCookieMeter();
        }

        private void SettingsIcon_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Language: English , Sound: On, Music: On");
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            cookies = 0; // Reset the numbber of Cookies to 0
            UpdateCookieMeter(); //Update the UI display to reflect the reset
        }

    }
}
