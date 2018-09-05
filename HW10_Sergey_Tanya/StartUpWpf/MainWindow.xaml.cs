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
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await UpdateViewModel();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await UpdateViewModel(new int[] { 3, 5, 10 });
        }

        private async Task UpdateViewModel(int[] playersCountsSet = null)
        {
            var viewModel = (MultipleSeriesVm)DataContext;
            var roundsCount = viewModel.RoundsCount;
            var gamesCount = viewModel.GamesCount;
            var playersCounts = (playersCountsSet != null && playersCountsSet.Length > 0) ? playersCountsSet : PlayersCountsSequence(1, viewModel.MaxPlayersCount);
            var series = new SeriesCollection();

            viewModel.PlayBtnIsEnabled = false;
            viewModel.Series = new SeriesCollection();
            viewModel.DescriptionBlock = $"Идет построение...";
            viewModel.GameProgressMaximum = playersCounts.Count() * (viewModel.MaxWipLimit + 1) * gamesCount;

            var seriaValuesByPlayersCount = await Task.Run(() => { return GetSeriaValues(viewModel, roundsCount, gamesCount, playersCounts); });

            viewModel.PlayBtnIsEnabled = true;
            viewModel.GameProgress = 0;
            viewModel.DescriptionBlock = $"Усредненные результаты при количестве игр {gamesCount} и {roundsCount} раундов в каждой игре";
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

        private Dictionary<int, List<Tuple<int, double>>> GetSeriaValues(MultipleSeriesVm viewModel, int roundsCount, int gamesCount, IEnumerable<int> playersCounts)
        {
            var seriaValuesByPlayersCount = new Dictionary<int, List<Tuple<int, double>>>();

            foreach (int currentPlayersCount in playersCounts)
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

                        viewModel.GameProgress++;
                    }

                    var doneCardsCount = (double)doneCardsCountSum / (double)gamesCount;

                    seriaValuesByPlayersCount[currentPlayersCount].Add(new Tuple<int, double>(currnetWipLimit, doneCardsCount));
                }
            }

            return seriaValuesByPlayersCount;
        }

        private IEnumerable<int> PlayersCountsSequence(int n1, int n2)
        {
            while (n1 <= n2)
            {
                yield return n1;
                n1 += 2;
            }
        }
    }
}
