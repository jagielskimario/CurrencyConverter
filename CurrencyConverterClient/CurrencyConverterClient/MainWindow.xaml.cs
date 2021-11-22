using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrencyConverterClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string CallRestApi(string currency)
        {
            RestClient client = new($"http://localhost:5000/api/getcurrencyword/{currency}");
            RestRequest request = new(Method.GET);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        private void SendButtonClick(object sender, RoutedEventArgs e)
        {
            responseTextBox.Text = CallRestApi(requestTextBox.Text);
        }

        private void requestTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendButtonClick(this, e);
            }
        }
    }
}
