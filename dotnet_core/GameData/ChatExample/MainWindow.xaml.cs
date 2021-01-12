using ChatExample.Hub;
using GameModels.Mongo.v1;
using GameServices.v1;
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
            try
            {
                if (!IsEmptyFields())
                {
                    LblStatusConnection.Background = Brushes.LightYellow;
                    _chatHub = new ChatHub(GetUri(), TxtPlayerId.Text, TxtGuildId.Text);
                    LblStatusConnection.Content = "Connecting to server!";
                    string statusConn = await _chatHub.ConnectAsync((message) => ReceiveMessage(message), OnClose);
                    LblStatusConnection.Content = statusConn;
                    LblStatusConnection.Background = Brushes.LightGreen;
                    BtnConnect.IsEnabled = false;
                    BtnSendMessage.IsEnabled = true;
                    TxtMssg.IsEnabled = true;
                    await GetLastMessages();
                }
            }
            catch (Exception ex)
            {
                LblStatusConnection.Content = ex.Message;
                LblStatusConnection.Background = Brushes.LightCoral;
            }
        }

        private async void BtnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (TxtMssg.Text.Trim().Length > 0)
            {
                TxtMssg.BorderBrush = Brushes.Gray;
                Message message = new Message
                {
                    PlayerId = TxtPlayerId.Text,
                    GuildId = TxtGuildId.Text,
                    Text = TxtMssg.Text
                };
                try
                {
                    await _chatHub.SendMessage(message);
                    TxtMssg.Text = "";
                }
                catch (Exception ex)
                {
                    AddListBoxItem($"Error on send message: {message.Text}", isError: true);
                }
            }
            else
            {
                OpenPopup("Por favor insira a mensagem");
                TxtMssg.BorderBrush = Brushes.Red;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.PopupError.IsOpen = false;
        }

        private async Task GetLastMessages()
        {
            try
            {
                IGuildChatService guildChatService = new GuildChatService();
                List<Message> messages = await guildChatService.GetLastMessages(TxtGuildId.Text);
                foreach (var message in messages)
                {
                    AddListBoxItem(GetMessage(message));
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public void ReceiveMessage(Message message)
        {
            this.Dispatcher.Invoke(() =>
            {
                AddListBoxItem(GetMessage(message));
            });
        }

        public async Task OnClose(Exception e)
        {
            this.Dispatcher.Invoke(() =>
            {
                LblStatusConnection.Content = $"-= Reconnecting =- Error: {e.Message}";
                BtnConnect.IsEnabled = true;
                BtnSendMessage.IsEnabled = false;
                TxtMssg.IsEnabled = false;
                LblStatusConnection.Background = Brushes.LightYellow;
            });
        }

        private void AddListBoxItem(string text, bool isError=false)
        {
            ListBoxItem itm = new ListBoxItem();
            itm.Content = text;
            itm.Background = isError ? Brushes.LightCoral : Brushes.LightGreen;

            listBox.Items.Add(itm);
        }

        private string GetMessage(Message message)
        {
            return $"{message.SendAt.ToString("G")} - {message.PlayerName}: {message.Text}";
        }

        private void OpenPopup(string error)
        {
            TxtPopUpError.Text = error;
            PopupError.IsOpen = true;
        }

        private string GetUri()
        {
            return $"{TxtHost.Text}:{TxtPort.Text}/{TxtService.Text}";
        }

        private bool IsEmptyFields()
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
                OpenPopup(emptyFieldsError);
                return true;
            }
            return false;
        }
    }
}
