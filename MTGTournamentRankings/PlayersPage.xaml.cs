using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace MTGTournamentRankings
{
    public partial class PlayersPage : PhoneApplicationPage
    {
        // Constructor
        public PlayersPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.PlayersViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Handle selection changed on ListBox
        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (MainListBox.SelectedIndex == -1)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/PlayerDetailsPage.xaml?selectedItem=" + MainListBox.SelectedIndex, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            MainListBox.SelectedIndex = -1;
        }

        // Load data for the PlayersViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.PlayersViewModel.IsDataLoaded)
            {
                App.PlayersViewModel.LoadData();
            }
        }

        private void ButtonAddPlayer_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/PlayerDetailsPage.xaml", UriKind.Relative));
        }
    }
}