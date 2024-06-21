using ArmyRoster.Model;
using System;
using System.Collections;
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
using System.Windows.Shapes;
using ArmyRoster.Forms;
using Spire.Xls;
using System.Data;
using System.IO;
using Aspose.Cells;
using ArmyRoster.Service;

namespace ArmyRoster.Forms
{
    /// <summary>
    /// Логика взаимодействия для UnitsListForm.xaml
    /// </summary>
    public partial class UnitsListForm : Window
    {
        private List<Button>listButton = new List<Button>();
        private List<string> armyList;
        private Aspose.Cells.Workbook workbook;
        private string filePath;
        private Army army;
        public UnitsListForm(List<string> armyList, string Name, Army army)
        {
            InitializeComponent();
            Title = Name;
            this.armyList = armyList;
            this.army = army;
            ParseUnitsInFile();
        }

        private void addUnitButton_Click(object sender, RoutedEventArgs e)
        {
            if (unitNameTextBox.Text.Length > 0)
            {
                listButton.Add(new Button());
                listButton[listButton.Count - 1].SetValue(Grid.ColumnProperty, 0);
                listButton[listButton.Count - 1].Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));//прозрачные кнопки
                listButton[listButton.Count - 1].HorizontalAlignment = HorizontalAlignment.Stretch;
                listButton[listButton.Count - 1].SetValue(Grid.RowProperty, listButton.Count - 1);
                listButton[listButton.Count - 1].Content = unitNameTextBox.Text;
                listButton[listButton.Count - 1].Height = 50; listButton[listButton.Count - 1].Width = 200;
                listButton[listButton.Count - 1].Foreground = new SolidColorBrush(Colors.Silver);
                listButton[listButton.Count - 1].FontFamily = new FontFamily("Monotype Corsiva");
                listButton[listButton.Count - 1].FontSize = 25;
                listButton[listButton.Count - 1].Click += selectArmy_Click;                
                army.Units.Add(new UnitsW40K());
                army.Units[listButton.Count - 1].Name = unitNameTextBox.Text;               
                stackPanel1.Children.Add(listButton[listButton.Count - 1]);
            }
        }



        private void ParseUnitsInFile()
        {
            filePath = armyList.Where(s => s.Contains(Title)).FirstOrDefault();
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл {filePath} не найден.");
            }
            // Загрузка документа Excel
            workbook = new Aspose.Cells.Workbook(filePath);

            // Выбор первого рабочего листа
            Aspose.Cells.Worksheet worksheet = workbook.Worksheets[0];

            // Получение количества строк в первом столбце
            int rowCount = worksheet.Cells.MaxDataRow + 1;

            for (int i = 0; i < rowCount; i++)
            {
                // Чтение содержимого первой ячейки (A)
                Cell firstCell = worksheet.Cells[i, 0];

                // добавляем кнопку с именем которое в первой ячейке будет содержаться.   
                listButton.Add(new Button());
                listButton[listButton.Count - 1].SetValue(Grid.ColumnProperty, 0);
                listButton[listButton.Count - 1].Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));//прозрачные кнопки
                listButton[listButton.Count - 1].HorizontalAlignment = HorizontalAlignment.Stretch;
                listButton[listButton.Count - 1].SetValue(Grid.RowProperty, listButton.Count - 1);
                listButton[listButton.Count - 1].Content = firstCell.Value?.ToString() ?? "Ячейка A1 пуста";
                listButton[listButton.Count - 1].Height = 50; listButton[listButton.Count - 1].Width = 200;
                listButton[listButton.Count - 1].Foreground = new SolidColorBrush(Colors.Silver);
                listButton[listButton.Count - 1].FontFamily = new FontFamily("Monotype Corsiva");
                listButton[listButton.Count - 1].FontSize = 25;
                listButton[listButton.Count - 1].Click += selectArmy_Click;
                //newButton.Margin = new Thickness(0, 0, 0, 0);
                stackPanel1.Children.Add(listButton[listButton.Count - 1]);
                army.Units.Add(new UnitsW40K());
                army.Units[listButton.Count - 1].Name = firstCell.Value?.ToString() ?? "Ячейка A1 пуста";
            }
        }

        public void selectArmy_Click(object sender, RoutedEventArgs e)
        {
            
            UnitInfoForm unitInfoForm = new UnitInfoForm(armyList,
                                                        ((Button)sender).Content.ToString(),
                                                        listButton.IndexOf((Button)sender),
                                                        army
                                                        );
            unitInfoForm.Show();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void deleteUnitButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            Aspose.Cells.Worksheet sheet = workbook.Worksheets[0];
            for (int i = 0, j = i; i < stackPanel1.Children.Count; i++, j = i)
            {
                
                Aspose.Cells.Cell cell = sheet.Cells[$"A{i+1}"];
                if (cell.StringValue == unitNameTextBox.Text)
                {
                    sheet.Cells.DeleteRow(i);
                    //Удаляем саму кнопку с экрана
                    stackPanel1.Children.Remove(
                        listButton[listButton
                                            .FindIndex(button => button.Content.ToString() == unitNameTextBox.Text)]
                        );
                    //а тут удаляем из листбатона
                    listButton.RemoveAt(
                        listButton
                                .FindIndex(button => button.Content.ToString() == unitNameTextBox.Text)
                        );
                    sheet.Cells.DeleteRows(i + 1, 1);
                    MessageBox.Show($"Юнит {unitNameTextBox.Text} успешно удален");
                    workbook.Save(filePath);
                    j++; //ввел эту переменную что бы проверить её на то была ли проведена операция удаления, и если да то она будет больше I
                    // и тогда у нас сработает следующее условие которое выведет что юнит с такимименем небыл найден
                    break;
                }
                if (j > i)
                {
                    MessageBox.Show($"Юнит с именем {unitNameTextBox.Text} небыл найден");
                }
            
            }*/
            if (UnitListFormService.delete(workbook, stackPanel1, unitNameTextBox.Text, listButton, filePath))
            {
                MessageBox.Show($"Юнит {unitNameTextBox.Text} успешно удален");
            }
            else
            {

                MessageBox.Show($"Юнит с именем {unitNameTextBox.Text} небыл найден");
            }
        }
    }
}