using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace ArmyRoster.Forms
{
    /// <summary>
    /// Логика взаимодействия для ArmyTransferForm.xaml
    /// </summary>
    public partial class ArmyTransferForm : Window
    {
        public ArmyTransferForm()
        {
            InitializeComponent();
        }
        private byte[] arm;
        private bool is_Transfer = false;

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ЧТо бы передать армию введите IP адрес, номер порта, выберете армию и нажмите кнопку передать," +
                "после чего ваша армия будет отправлена другому игроку\nЧто бы принять армию," +
                "так же введите IP адрес порт и нажмите кнопку `Призвать армию` после чего дождитесь пока армия скачается", "Помощь",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            if ((bool)dialog.ShowDialog())
            {
                string path = dialog.FileName;
                if (path != null)
                {
                    arm = File.ReadAllBytes(path);
                }
                else
                {
                    MessageBox.Show("Путь к файлу пуст вы уверены что выбрали армию для передачи?",
                        "Ошибка передачи", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            is_Transfer = !is_Transfer;
            if (is_Transfer)
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    IPAddress ip = IPAddress.Parse(ipTextBox.Text);
                    IPEndPoint endpoint = new IPEndPoint(ip, int.Parse(portTextBox.Text));
                    try
                    {
                        socket.Connect(endpoint);
                        if (socket.Connected)
                        {
                            socket.Send(arm);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка соединения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            is_Transfer = !is_Transfer;
            if (is_Transfer)
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    IPAddress ip = IPAddress.Parse(ipTextBox.Text);
                    IPEndPoint endpoint = new IPEndPoint(ip, int.Parse(portTextBox.Text));

                    socket.Bind(endpoint);
                    socket.Listen(2);

                    try
                    {
                        using (Socket ns = await socket.AcceptAsync())
                        {
                            Console.WriteLine(ns.RemoteEndPoint.ToString());

                            // Получаем размер файла
                            byte[] sizeInfo = new byte[4];
                            await ns.ReceiveAsync(sizeInfo, SocketFlags.None);
                            int fileSize = BitConverter.ToInt32(sizeInfo, 0);

                            // Читаем содержимое файла из сокета
                            byte[] fileData = new byte[fileSize];
                            int totalRead = 0;
                            while (totalRead < fileSize)
                            {
                                int read = await ns.ReceiveAsync(new ArraySegment<byte>(fileData, totalRead, fileSize - totalRead), SocketFlags.None);
                                if (read == 0)
                                {
                                    throw new Exception("Соединение закрыто до окончания передачи файла.");
                                }
                                totalRead += read;
                            }

                            var dialog = new Microsoft.Win32.SaveFileDialog();
                            if (dialog.ShowDialog() == true) // Исправлено с DialogResult на true
                            {
                                // Сохраняем файл
                                File.WriteAllBytes(dialog.FileName, fileData);
                            }

                            ns.Shutdown(SocketShutdown.Both);
                            ns.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
