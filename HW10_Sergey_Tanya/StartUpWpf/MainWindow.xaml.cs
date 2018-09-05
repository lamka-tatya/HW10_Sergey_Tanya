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
            await UpdateViewModel();
        }

        private async Task UpdateViewModel()
        {
            var viewModel = (MultipleSeriesVm)DataContext;
            var roundsCount = viewModel.RoundsCount;
            var gamesCount = viewModel.GamesCount;
            var series = new SeriesCollection();

            viewModel.PlayBtnIsEnabled = false;
            viewModel.Series = new SeriesCollection();
            viewModel.DescriptionBlock = $"Идет построение...";
            viewModel.GameProgressMaximum = viewModel.MaxPlayersCount * (viewModel.MaxWipLimit + 1) * gamesCount;

            var seriaValuesByPlayersCount = await Task.Run(() => { return GetSeriaValues(viewModel, roundsCount, gamesCount); });

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

        private Dictionary<int, List<Tuple<int, double>>> GetSeriaValues(MultipleSeriesVm viewModel, int roundsCount, int gamesCount)
        {
            var seriaValuesByPlayersCount = new Dictionary<int, List<Tuple<int, double>>>();

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

                        viewModel.GameProgress++;
                    }

                    var doneCardsCount = (double)doneCardsCountSum / (double)gamesCount;

                    seriaValuesByPlayersCount[currentPlayersCount].Add(new Tuple<int, double>(currnetWipLimit, doneCardsCount));
                }
            }

            return seriaValuesByPlayersCount;
        }
    }
}
