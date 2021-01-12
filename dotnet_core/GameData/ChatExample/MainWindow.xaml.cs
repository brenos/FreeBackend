using ChatClient.Models.v1;
using ChatExample.Hub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IChatHub _chatHub { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (!isEmptyFields())
            {
                _chatHub = new ChatHub(getUri(), TxtPlayerId.Text, TxtGuildId.Text);
                LblStatusConnection.Content = "Connecting to server!";
                string statusConn = await _chatHub.ConnectAsync((message) => ReceiveMessage(message));
                LblStatusConnection.Content = statusConn;
            }
        }

        private void BtnSendMessage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.PopupError.IsOpen = false;
        }

        private bool isEmptyFields()
        {
            List<string> emptyFields = new List<string>();
            if (TxtHost.Text.Trim().Length <= 0)
            {
                TxtHost.BorderBrush = Brushes.Red;
                emptyFields.Add("Host");
            }
            if (TxtPort.Text.Trim().Length <= 0)
            {
                TxtPort.BorderBrush = Brushes.Red;
                emptyFields.Add("Port");
            }
            if (TxtService.Text.Trim().Length <= 0)
            {
                TxtService.BorderBrush = Brushes.Red;
                emptyFields.Add("Service");
            }
            if (TxtGuildId.Text.Trim().Length <= 0)
            {
                TxtGuildId.BorderBrush = Brushes.Red;
                emptyFields.Add("GuildId");
            }
            if (TxtPlayerId.Text.Trim().Length <= 0)
            {
                TxtPlayerId.BorderBrush = Brushes.Red;
                emptyFields.Add("PlayerId");
            }
            if (emptyFields.Count > 0)
            {
                string emptyFieldsStr = "";
                foreach (var field in emptyFields)
                {
                    emptyFieldsStr += $"{field}, ";
                }
                string emptyFieldsError = $"{emptyFieldsStr.Substring(0, emptyFieldsStr.Length - 2)} is empty!";
                openPopup(emptyFieldsError);
                return true;
            }
            return false;
        }

        private void openPopup(string error)
        {
            TxtPopUpError.Text = error;
            PopupError.IsOpen = true;
        }

        private string getUri()
        {
            return $"{TxtHost.Text}:{TxtPort.Text}/{TxtService.Text}";
        }

        public void ReceiveMessage(Message message)
        {
            this.Dispatcher.Invoke(() =>
            {
                ListBoxItem itm = new ListBoxItem();
                itm.Content = $"{message.PlayerId}: {message.Text}";

                listBox.Items.Add(itm);
            });
        }
    }
}
