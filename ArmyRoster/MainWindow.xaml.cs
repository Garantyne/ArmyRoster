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

namespace ArmyRoster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        List<string> listArmy = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            //ShowActivationWindow();
            ParseFile();
        }

        private void ShowActivationWindow()
        {
            ActivationWindow actWin = new ActivationWindow();
            actWin.Show();
        }

        private void newArmyButton_Click(object sender, RoutedEventArgs e)
        {            
                     
            
        }

        private void ParseFile()
        {
            string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;//получаем полное название родительской дирректории
            string directoryPath = System.IO.Path.Combine(projectDirectory, "armyList");//заходим в папку арми лист где у нас данные по армиям хранятся
            //тут сканим на наличие уже готовых армий
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            else
            {
                foreach (var v in Directory.EnumerateFiles(directoryPath, "*.xlsx"))
                {
                    listArmy.Add(v);
                }
            }
        }

        private void getListMyArmys_Click(object sender, RoutedEventArgs e)
        {
            ArmyListForm armyListForm = new ArmyListForm(listArmy);
            armyListForm.ShowDialog();
        }
    }
}