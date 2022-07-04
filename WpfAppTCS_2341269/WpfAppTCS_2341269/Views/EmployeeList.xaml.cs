using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using WpfAppTCS_2341269.ViewModels;

namespace WpfAppTCS_2341269.Views
{
    /// <summary>
    /// Interaction logic for EmployeeList.xaml
    /// </summary>
    public partial class EmployeeList : UserControl
    {
        public EmployeeList()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();
        }
        async void FillDataGrid()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", SD.token);
                    HttpResponseMessage responseMessage = await client.GetAsync(SD.EmployeeListAPI);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var jsonString = await responseMessage.Content.ReadAsStringAsync();
                        Root returnList = JsonConvert.DeserializeObject<Root>(jsonString);
                        gridEmployee.ItemsSource = returnList.data;
                    }
                    else
                    {
                        MessageBox.Show(SD.EmployeeListAPI + " : " + responseMessage.ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EmployeePage page = new EmployeePage();
            page.ShowDialog();
            FillDataGrid();
        }

        Datum datum = new Datum();

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (datum.name == null)
            {
                MessageBox.Show("Please select Employee List then Click the Update button ");
            }
            else
            {
                EmployeePage page = new EmployeePage();
                page.datumData = datum;
                page.ShowDialog();
                FillDataGrid();
            }

        }

        private void gridEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            datum = (Datum)gridEmployee.SelectedItem;
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datum == null)
                {
                    MessageBox.Show("Please Select Employee List");
                }
                else
                {
                    if (MessageBox.Show("Are you sure to delete ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", SD.token);
                            HttpResponseMessage responseMessage = await client.DeleteAsync(SD.EmployeeAPI + datum.id);
                            if (responseMessage.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Data Deleted");
                                FillDataGrid();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
