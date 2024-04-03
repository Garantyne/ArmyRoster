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

namespace ArmyRoster
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void newArmyButton_Click(object sender, RoutedEventArgs e)
        {
            CreateArmyForm createArmyForm = new CreateArmyForm();
            createArmyForm.ShowDialog();            
            
        }
    }
}