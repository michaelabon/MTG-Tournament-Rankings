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

        private const string DefaultScore = "1600";
        private bool isNew;
        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string selectedIndex = "";
            NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex);
            int index;
            if (int.TryParse(selectedIndex, out index))
            {
                DataContext = App.PlayersViewModel.Items[index];
            }
            else
            {
                DataContext = new PlayerDataVM();
                isNew = true;
                PlayerScore.Text = DefaultScore;
                ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).IsEnabled = false;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ApplicationBar.Buttons[0] == null) { return; }
            PlayerDataVM player = DataContext as PlayerDataVM;
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
            PlayerDataVM player = DataContext as PlayerDataVM;
            if (player == null) { return; }

            player.LineOne = PlayerName.Text;
            player.LineTwo = PlayerScore.Text;
            
            if (isNew)
            {
                App.PlayersViewModel.Items.Add(player);
            }

            App.PlayersViewModel.SaveData();
            NavigationService.GoBack();
        }

        private void DeletePlayer_Click(object sender, EventArgs e)
        {
            PlayerDataVM player = DataContext as PlayerDataVM;
            if (player == null) { return; }

            string msg = String.Format("Are you sure you want to permanently delete {0}?", player.LineOne);
            const string caption = "Confirm delete?";
            if (MessageBox.Show(msg, caption, MessageBoxButton.OKCancel) != MessageBoxResult.OK)
            {
                return;
            }

            if (App.PlayersViewModel.Items.Remove(player))
            {
                App.PlayersViewModel.SaveData();
                NavigationService.GoBack();
            }
            else
            {
                // TODO: Handle the inability to delete a player.
            }
        }
    }
}