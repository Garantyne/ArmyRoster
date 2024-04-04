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

namespace ArmyRoster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Army> armyList = new List<Army>();
        public MainWindow()
        {
            InitializeComponent();
            ShowActivationWindow();
        }

        private void ShowActivationWindow()
        {
            ActivationWindow actWin = new ActivationWindow();
            actWin.Show();
        }

        private void newArmyButton_Click(object sender, RoutedEventArgs e)
        {
            CreateArmyForm createArmyForm = new CreateArmyForm(armyList);
            createArmyForm.ShowDialog();            
            
        }

        private void getListMyArmys_Click(object sender, RoutedEventArgs e)
        {
            ArmyListForm armyListForm = new ArmyListForm(armyList);
            armyListForm.ShowDialog();
        }
    }
}