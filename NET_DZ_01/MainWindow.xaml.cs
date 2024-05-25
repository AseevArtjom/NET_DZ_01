using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

namespace NET_DZ_01
{
    // Сделать простую оконную версию TCP-клиента для отправки сообщения удаленному серверу
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            string ipString = textBoxIP.Text;
            int port = int.Parse(PortTextBox.Text);
            string message = MessageTextBox.Text;


            try
            {
                using (Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    IPAddress ip = IPAddress.Parse(ipString);
                    IPEndPoint endPoint = new IPEndPoint(ip, port);

                    clientSocket.Connect(endPoint);
                    if (clientSocket.Connected)
                    {
                        byte[] msgBuffer = Encoding.ASCII.GetBytes(message);
                        clientSocket.Send(msgBuffer);

                        byte[] buffer = new byte[1024];
                        int receivedBytes = clientSocket.Receive(buffer);

                        string response = Encoding.ASCII.GetString(buffer, 0, receivedBytes);
                        TBResponse.Text = response;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }
    }
}
