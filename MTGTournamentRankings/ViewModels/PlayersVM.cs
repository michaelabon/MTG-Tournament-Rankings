using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace MTGTournamentRankings
{
    public class PlayersVM : INotifyPropertyChanged
    {
        public PlayersVM()
        {
            this.Items = new ObservableCollection<PlayerDataVM>();
        }

        /// <summary>
        /// A collection for PlayerDataVM objects.
        /// </summary>
        public ObservableCollection<PlayerDataVM> Items { get; private set; }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        readonly IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        private const string playerDataKey = "PlayerData";
        public void SaveData()
        {
            List<PlayerDataVM> players = Items.ToList();
            settings[playerDataKey] = players;
        }

        /// <summary>
        /// Creates and adds a few PlayerDataVM objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            if (settings.Contains(playerDataKey))
            {
                List<PlayerDataVM> players = (List<PlayerDataVM>) settings[playerDataKey];
                foreach(var player in players)
                {
                    this.Items.Add(player);
                }
            }

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}