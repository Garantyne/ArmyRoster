using ArmyRoster.Model;
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
using System.Windows.Shapes;

namespace ArmyRoster.Forms
{
    /// <summary>
    /// Логика взаимодействия для UnitInfoForm.xaml
    /// </summary>
    public partial class UnitInfoForm : Window
    {
        List<Army> armyList;
        public UnitInfoForm(List<Army> armyList, string name)
        {
            InitializeComponent();
            this.armyList = armyList;
            Title = name;
            unitNameTextBlock.Text = name;
        }

        private void commandCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            commandStackPanel.Visibility = Visibility.Visible;
        }

        private void commandCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            commandStackPanel.Visibility = Visibility.Collapsed;
        }
    }
}
