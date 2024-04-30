using Abonents.Models;
using CsvHelper;
using DataBaseLogic.Models;
using DataBaseLogic.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Abonents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDataService _dataService;

        public MainWindow(IDataService dataService)
        {
            _dataService = dataService;

            InitializeComponent();

            SetDataContextAsync();
        }


        private async void SetDataContextAsync()
        {
            var abonents = await _dataService.GetAbonentInfoAsync();

            DataContext = new AbonentInfoViewModel() { Abonents = abonents };
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow searchWindow = new SearchWindow();

            string phoneNumber = null;

            if (searchWindow.ShowDialog() != true)
            {
                phoneNumber = (string)searchWindow.DataContext;

                if (phoneNumber != null)
                {
                    var data = (AbonentInfoViewModel)DataContext;

                    var searchResult = data.Abonents.Where(abonent => abonent.HomePhoneNumber == phoneNumber ||
                                                                            abonent.WorkPhoneNumber == phoneNumber ||
                                                                            abonent.MobilePhoneNumber == phoneNumber);


                    if (!searchResult.Any())
                    {
                        MessageBox.Show("Нет абонентов, удовлетворяющих критерию поиска", "Поиск по номеру", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        DataContext = new AbonentInfoViewModel
                        {
                            Abonents = searchResult
                        };
                    }
                }

            }

        }

        private void ExportToCsvButton_Click(object sender, RoutedEventArgs e)
        {
            var data = dataGrid.Items.Cast<AbonentModel>().ToList();

            string fileName = $"report_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

            using (var writer = new StreamWriter(fileName))
            using (var csv = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {
                
                csv.WriteRecords(data);
            }

            MessageBox.Show($"Файл {fileName} успешно создан.", "Выгрузка CSV", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async void StreetsButton_Click(object sender, RoutedEventArgs e)
        {
            StreetsWindow streetsWindow = new StreetsWindow();

            var streetsInfo = await _dataService.GetStreetInfoAsync();

            var streetInfoModel = new StreetInfoVewModel() { Streets = streetsInfo };

            streetsWindow.DataContext = streetInfoModel;

            streetsWindow.ShowDialog();
        }

       

       
    }
}