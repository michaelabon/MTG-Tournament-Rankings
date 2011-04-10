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
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MTGTournamentRankings
{
    public partial class PlayerDetailsPage : PhoneApplicationPage
    {
        // Constructor
        public PlayerDetailsPage()
        {
            InitializeComponent();
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string selectedIndex = "";
            if (!NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex)) { return; }
            int index = int.Parse(selectedIndex);
            DataContext = App.PlayersViewModel.Items[index];
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ApplicationBar.Buttons[0] == null) { return; }
            PlayerViewModel player = DataContext as PlayerViewModel;
            if (player == null) { return; }

            int testIfNumeric;
            if (PlayerName.Text == player.LineOne && PlayerScore.Text == player.LineTwo)
            {
                ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).IsEnabled = false;
            }
            else if (PlayerName.Text == String.Empty || PlayerScore.Text == String.Empty)
            {
                ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).IsEnabled = false;
            }
            else if (!Int32.TryParse(PlayerScore.Text, out testIfNumeric))
            {
                ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).IsEnabled = false;                
            }
            else
            {
                ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).IsEnabled = true;
            }
        }

        private void SavePlayer_Click(object sender, EventArgs e)
        {
            PlayerViewModel player = DataContext as PlayerViewModel;
            if (player == null) { return; }

            player.LineOne = PlayerName.Text;
            player.LineTwo = PlayerScore.Text;
        }

        private void DeletePlayer_Click(object sender, EventArgs e)
        {
            PlayerViewModel player = DataContext as PlayerViewModel;
            if (player == null) { return; }

            string msg = String.Format("Are you sure you want to permanently delete {0}?", player.LineOne);
            const string caption = "Confirm delete?";
            if (MessageBox.Show(msg, caption, MessageBoxButton.OKCancel) != MessageBoxResult.OK)
            {
                return;
            }

            if (App.PlayersViewModel.Items.Remove(player))
            {
                NavigationService.GoBack();
            }
            else
            {
                // TODO: Handle the inability to delete a player.
            }
        }
    }
}