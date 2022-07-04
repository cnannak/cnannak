using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows;
using WpfAppTCS_2341269.ViewModels;

namespace WpfAppTCS_2341269
{
    /// <summary>
    /// Interaction logic for EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Window
    {
        public EmployeePage()
        { 
            InitializeComponent();
        }

        public Datum datumData;

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtEmployeeName.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter the Name");
            }
            else
            {
                if (datumData != null)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", SD.token);

                        datumData.name = txtEmployeeName.Text.Trim();
                        datumData.email = txtemail.Text.Trim();
                        datumData.gender = cmbgender.SelectionBoxItem.ToString().ToLower();
                        datumData.status = cmbStatus.SelectionBoxItem.ToString().ToLower();

                        var data = JsonConvert.SerializeObject(datumData);

                        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                        HttpResponseMessage responseMessage = await client.PutAsync(SD.EmployeeAPI + datumData.id, content);
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Data Updated");
                        }
                    }

                }
                else
                {
                    Datum datum = new Datum();
                    datum.name = txtEmployeeName.Text.Trim();
                    datum.email = txtemail.Text.Trim();
                    datum.gender = cmbgender.SelectionBoxItem.ToString().ToLower();
                    datum.status = cmbStatus.SelectionBoxItem.ToString().ToLower();

                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", SD.token);

                        var data = JsonConvert.SerializeObject(datum);
                        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                        HttpResponseMessage responseMessage = await client.PostAsync(SD.EmployeeListAPI, content);
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Data Added");
                            txtEmployeeName.Clear();
                            txtemail.Clear();
                        }
                    }
                }
            }

                
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(datumData!=null)
            {
                txtEmployeeName.Text = datumData.name;
                txtemail.Text = datumData.email;
                cmbgender.SelectedIndex = (datumData.gender.ToLower().Equals("male")) ? 0 : 1;
                cmbStatus.SelectedIndex = (datumData.status.ToLower().Equals("active")) ? 0 : 1;
            }
        }
    }
}
