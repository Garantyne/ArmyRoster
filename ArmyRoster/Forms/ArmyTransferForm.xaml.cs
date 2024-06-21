using ArmyRoster.Service;
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
            service = new TransferService();
        }
        private byte[] arm;
        private bool is_Transfer = false;
        private TransferService service;

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
                    service.SelectArmy(ref arm, path);
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
                if (await service.TransferArmy(ipTextBox.Text, portTextBox.Text, arm))
                {

                }
                else
                {
                    MessageBox.Show("Ошибка соединения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
       

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if(await service.SummonArmy(ipTextBox.Text, portTextBox.Text))
            {

            }
            else
            {
                MessageBox.Show("Ошибка соединения! Не удалось принять армию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
