using Domain;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace StartUpWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateViewModel();
        }

        private void UpdateViewModel()
        {
            var viewModel = (MultipleSeriesVm)DataContext;
            var roundsCount = viewModel.RoundsCount;
            var gamesCount = viewModel.GamesCount;
            var series = new SeriesCollection();
            var seriaValuesByPlayersCount = new Dictionary<int, List<Tuple<int, double>>>();

            viewModel.DescriptionBlock = $"Усредненные результаты при количестве игр {gamesCount} и {roundsCount} раундов в каждой игре";

            for (int currentPlayersCount = 1; currentPlayersCount < viewModel.MaxPlayersCount + 1; currentPlayersCount++)
            {
                var emptyValue = new List<Tuple<int, double>>();
                seriaValuesByPlayersCount.Add(currentPlayersCount, emptyValue);

                for (int currnetWipLimit = 0; currnetWipLimit <= viewModel.MaxWipLimit; currnetWipLimit++)
                {
                    var doneCardsCountSum = 0;

                    for (int gameNum = 0; gameNum < gamesCount; gameNum++)
                    {
                        doneCardsCountSum += GameBuilder
                            .CreateGame((uint)currnetWipLimit)
                            .WithPlayers(currentPlayersCount)
                            .PlayRounds(roundsCount)
                            .DoneCardsCount();
                    }

                    var doneCardsCount = (double)doneCardsCountSum / (double)gamesCount;

                    seriaValuesByPlayersCount[currentPlayersCount].Add(new Tuple<int, double>(currnetWipLimit, doneCardsCount));
                }
            }

            viewModel.Labels = seriaValuesByPlayersCount.First().Value.Select(x => x.Item1 == 0 ? "no limit" : x.Item1.ToString()).ToArray();

            foreach (var item in seriaValuesByPlayersCount)
            {
                series.Add(new LineSeries
                {
                    Title = $"{item.Key} игроков",
                    PointGeometry = DefaultGeometries.Circle,
                    Fill = Brushes.Transparent,
                    Values = new ChartValues<double>(item.Value.Select(x => x.Item2))
                });
            }

            viewModel.Series = series;
        }
    }
}
