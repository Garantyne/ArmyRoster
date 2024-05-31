using ArmyRoster.Model;
using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

using System.Windows;
using System.Windows.Controls;

using System.Windows.Media;


namespace ArmyRoster.Forms
{
    /// <summary>
    /// Логика взаимодействия для ArmyListForm.xaml
    /// </summary>
    public partial class ArmyListForm : Window
    {
        private List<string> armyList;
        List<Button> listButton = new List<Button>();
        Army army;

        public ArmyListForm(List<string> armyList)
        {
            InitializeComponent();
            this.armyList = armyList;
            army = new Army();
            army.Units = new List<UnitsW40K>();
            InitialArmyName();
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
                Workbook workbook = new Workbook();
                Worksheet worksheet = workbook.Worksheets[0]; 
                worksheet.Name = addArmyTextBox.Text;//присваиваем имя первой странице(что бы было понятно что за армия
                string armiListPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\armyList\\";// указываем директорию в которую будем сохранять армию
                workbook.Save(armiListPath + addArmyTextBox.Text + ".xlsx", SaveFormat.Xlsx);//сохраняем армию и создаем название файла
                armyList.Add(armiListPath + addArmyTextBox.Text + ".xlsx");
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

        private void closeArmyButton_Click(object sender, RoutedEventArgs e)
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
    }
}
