using LiveCharts;
using System.ComponentModel;

namespace StartUpWpf
{
    public class MultipleSeriesVm : INotifyPropertyChanged
    {
        private string[] _labels;
        private int _maxPlayersCount = 10;
        private int _maxWipLimit = 5;
        private int _roundsCount = 15;
        private int _gamesCount = 1000;
        private string _descriptionBlock = "Для построения графиков нажмите \"Начать игру\"";
        private SeriesCollection _series;

        public int MaxPlayersCount
        {
            get => _maxPlayersCount;
            set => _maxPlayersCount = value;
        }
        public int MaxWipLimit
        {
            get => _maxWipLimit;
            set => _maxWipLimit = value;
        }

        public int RoundsCount
        {
            get => _roundsCount;
            set => _roundsCount = value;
        }

        public int GamesCount
        {
            get => _gamesCount;
            set => _gamesCount = value;
        }

        public string[] Labels
        {
            get => _labels;
            set
            {
                _labels = value;
                OnPropertyRaised("Labels");
            }
        }

        public string DescriptionBlock
        {
            get => _descriptionBlock;
            set
            {
                _descriptionBlock = value;
                OnPropertyRaised("DescriptionBlock");
            }
        }

        public SeriesCollection Series
        {
            get => _series;
            set
            {
                _series = value;
                OnPropertyRaised("Series");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised(string propertyname)
        {
            var propertyChangedEvent = PropertyChanged;
            if (propertyChangedEvent != null)
            {
                propertyChangedEvent(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
