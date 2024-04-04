using ArmyRoster.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ArmyRoster.Forms
{
    /// <summary>
    /// Логика взаимодействия для ArmyListForm.xaml
    /// </summary>
    public partial class ArmyListForm : Window
    {
        List<Army> armyList;
        List<Button> listButton = new List<Button>();
        public ArmyListForm(List<Army> armyList)
        {
            InitializeComponent();
            this.armyList = armyList;

        }

        private void addArmyButton_Click(object sender, RoutedEventArgs e)
        {
            if (addArmyTextBox.Text.Length > 0)
            {

                armyList.Add(new Army { NameArmy = addArmyTextBox.Text });
                listButton.Add(new Button());
                listButton[listButton.Count - 1].SetValue(Grid.ColumnProperty, 0);
                listButton[listButton.Count - 1].Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));//прозрачные кнопки
                listButton[listButton.Count - 1].HorizontalAlignment = HorizontalAlignment.Stretch;
                listButton[listButton.Count - 1].SetValue(Grid.RowProperty, listButton.Count - 1);
                listButton[listButton.Count - 1].Content = armyList[armyList.Count - 1].ToString();
                listButton[listButton.Count - 1].Height = 50; listButton[listButton.Count - 1].Width = 100;
                listButton[listButton.Count - 1].Click += selectArmy_Click;
                //newButton.Margin = new Thickness(0, 0, 0, 0);
                stackPanel1.Children.Add(listButton[listButton.Count - 1]);
                //gridArmyList.Children.Add(newButton);
            }
        }

        private void selectArmy_Click(object sender, RoutedEventArgs e)
        {
            UnitsListForm unitInfoForm = new UnitsListForm(armyList,((Button)sender).Content.ToString());
            unitInfoForm.Show();
        }
    }
}
