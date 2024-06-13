using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ArmyRoster.Forms;
using ArmyRoster.Model;
using ArmyRoster.Activation;
using System.IO;
using System.Collections;
using Aspose.Cells;
using ArmyRoster.Service;


namespace ArmyRoster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<string> armyList;
        List<Button> listButton = new List<Button>();
        Army army;
        public MainWindow()
        {
            armyList = new List<string>();
            InitializeComponent();
            ArmyRosterService.ParseFile(armyList);
            army = new Army();
            army.Units = new List<UnitsW40K>();
            InitialArmyName();
        }

        private void ShowActivationWindow()
        {
            ActivationWindow actWin = new ActivationWindow();
            actWin.Show();
        }

        private void addArmyButton_Click(object sender, RoutedEventArgs e)
        {
            if (addArmyTextBox.Text.Length > 0)
            {
                listButton.Add(new Button());
                listButton[listButton.Count - 1].SetValue(Grid.ColumnProperty, 0);
                listButton[listButton.Count - 1].Background = new SolidColorBrush(Colors.Azure);//прозрачные кнопки
                listButton[listButton.Count - 1].HorizontalAlignment = HorizontalAlignment.Stretch;
                listButton[listButton.Count - 1].SetValue(Grid.RowProperty, listButton.Count - 1);
                listButton[listButton.Count - 1].Content = addArmyTextBox.Text;
                listButton[listButton.Count - 1].Height = 50; listButton[listButton.Count - 1].Width = 200;
                listButton[listButton.Count - 1].Foreground = new SolidColorBrush(Colors.DarkGreen);
                listButton[listButton.Count - 1].FontFamily = new FontFamily("Monotype Corsiva");
                listButton[listButton.Count - 1].Click += selectArmy_Click;
                stackPanel1.Children.Add(listButton[listButton.Count - 1]);
                army.NameArmy = addArmyTextBox.Text;
                //создаем файл 
                ArmyRosterService.CreateFile(armyList, addArmyTextBox.Text);
            }
        }
        //тут создаем кнопки с уже имеющимися армиями
        private void InitialArmyName()
        {
            for (int i = 0; i < armyList.Count; i++)
            {
                listButton.Add(new Button());
                listButton[listButton.Count - 1].SetValue(Grid.ColumnProperty, 0);
                listButton[listButton.Count - 1].Background = new SolidColorBrush(Colors.Azure);//прозрачные кнопки
                listButton[listButton.Count - 1].HorizontalAlignment = HorizontalAlignment.Stretch;
                listButton[listButton.Count - 1].SetValue(Grid.RowProperty, listButton.Count - 1);
                listButton[listButton.Count - 1].Content = armyList[i].Substring(armyList[armyList.Count - 1].IndexOf("\\armyList\\") + 10);
                listButton[listButton.Count - 1].Height = 50; listButton[listButton.Count - 1].Width = 200;
                listButton[listButton.Count - 1].Foreground = new SolidColorBrush(Colors.DarkGreen);
                listButton[listButton.Count - 1].FontFamily = new FontFamily("Monotype Corsiva");
                listButton[listButton.Count - 1].Click += selectArmy_Click;
                stackPanel1.Children.Add(listButton[listButton.Count - 1]);
            }
        }

        private void selectArmy_Click(object sender, RoutedEventArgs e)
        {
            army.NameArmy = ((Button)sender).Content.ToString();
            UnitsListForm unitInfoForm = new UnitsListForm(armyList, ((Button)sender).Content.ToString(), army);
            unitInfoForm.Show();
        }

        private void exitArmyButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void deleteArmyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show($"Вы уверены что хоитте удалить армию{addArmyTextBox.Text}? Все данные исчезнут безвозвратно",
                    "Удаление", (System.Windows.Forms.MessageBoxButtons)MessageBoxButton.OKCancel, (System.Windows.Forms.MessageBoxIcon)MessageBoxImage.Warning);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    File.Delete(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\armyList\\" + addArmyTextBox.Text + ".xlsx");
                    var str = armyList.FindIndex(item => item.Contains(addArmyTextBox.Text));
                    armyList.RemoveAt(str);
                    listButton.Clear();
                    stackPanel1.Children.Clear();
                    InitialArmyName();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка, возможно вы ввели неправильно название файла или файлс таким именем не существует",
                    "Ошибка", MessageBoxButton.OK);
            }
        }

        private void infoArmyButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Добро пожаловать в редактор армии для Warhammer \n" +
                "Тут вы можете создавать свои арммии и наполнять их юнитами для более быстрого и простого доступа к ним " +
                "нежели в различных книгах правил и кодексах.\nДля того что бы создать армию, просто введите её название" +
                "в поле для ввода в правом верхнем углу и нажмите кнопку 'Добавить армию', после чего," +
                "вы увидете как появится кнопка, с названием вашей вашей армии в середине экрана. Кликните по ней, что бы продолжить",
                "Подсказка", MessageBoxButton.OK);
        }

        private void transferArmyButton_Click(object sender, RoutedEventArgs e)
        {
            ArmyTransferForm taf = new ArmyTransferForm();
            taf.ShowDialog();
        }
    }
}
