using ArmyRoster.Model;
using Aspose.Cells;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace ArmyRoster.Forms
{
    /// <summary>
    /// Логика взаимодействия для UnitInfoForm.xaml
    /// </summary>
    public partial class UnitInfoForm : Window
    {
        private List<string> armyList;
        private Army army;
        private int indexOfunit;
        private List<IAddChild> interfaceElementsTextBox = new List<IAddChild> ();
        private List<IAddChild> interfaceElementsRichTextBox = new List<IAddChild> ();
        private string filePath;
        public UnitInfoForm(List<string> armyList, string name, int indexOfUnit, Army army)
        {
            InitializeComponent();
            this.armyList = armyList;
            Title = name;
            this.army = army;
            unitNameTextBlock.Text = name;
            this.indexOfunit = indexOfUnit;

            interfaceElementsTextBox.AddRange(new IAddChild[] { MTextbox1, MTextbox2, SVTextbox1, SVTextbox2, TTextbox1, TTextbox2,
                WTeatbox1, WTeatbox2, LDTextbox1, LDTextbox2, OCTextbox1, OCTextbox2, invulSaveTextBox, keyWordTextBox});

            interfaceElementsRichTextBox.AddRange(new IAddChild[] {rangeWeaponRichTextBox,meleeWeaponRichTextBox,
                 abilitiRichTextBox, leaderTextBox, wargearRichTextBox, compositionRichTextBox});
            SaveInFile();
        }

        private void commandCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            commandStackPanel2.Visibility = Visibility.Visible;
        }

        private void commandCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            commandStackPanel2.Visibility = Visibility.Collapsed;
        }

        private void SaveInFile()
        {
            filePath = armyList.Where(s=>s.Contains(army.NameArmy)).FirstOrDefault();
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл {filePath} не найден.");
            }

            // Загрузка документа Excel
            Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook(filePath);

            // Выбор первого рабочего листа
            Aspose.Cells.Worksheet worksheet = workbook.Worksheets[0];

            // Получение количества строк в первом столбце
            int rowCount = worksheet.Cells.MaxDataRow + 1;

            
                // Чтение содержимого первой ячейки (A)
            Cell cell = worksheet.Cells[indexOfunit, 1];
            for(int i = 1,  j = 0; i < 21; i++)
            {
                if (i < 15)
                {
                    object value1 = worksheet.Cells[indexOfunit, i].Value;
                    ((TextBox)interfaceElementsTextBox[i-1]).Text = value1 != null? value1.ToString() : "" ;
                }
                else
                {
                    object value1 = worksheet.Cells[indexOfunit, i].Value;
                    ((RichTextBox)interfaceElementsRichTextBox[j++]).AppendText(value1 != null ? value1.ToString() : " - ");
                }
            }
            
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            IUnitInfo info = new UnitInfoW40K() {
                MState1 = MTextbox1.Text,
                MState2 = MTextbox2.Text,
                SVState1 = SVTextbox1.Text,
                SVState2 = SVTextbox2.Text,
                TState1 = TTextbox1.Text,
                TState2 = TTextbox2.Text,
                WState1 = WTeatbox1.Text,
                WState2 = WTeatbox2.Text,
                LDState1 = LDTextbox1.Text,
                LDState2 = LDTextbox2.Text,
                OCState1 = OCTextbox1.Text,
                OCState2 = OCTextbox2.Text,
                RangeWeapon = new TextRange(rangeWeaponRichTextBox.Document.ContentStart, rangeWeaponRichTextBox.Document.ContentEnd).Text,
                MeleeWeapon = new TextRange(meleeWeaponRichTextBox.Document.ContentStart, meleeWeaponRichTextBox.Document.ContentEnd).Text,
                KeyWord = keyWordTextBox.Text,
                Abilities = new TextRange(abilitiRichTextBox.Document.ContentStart, abilitiRichTextBox.Document.ContentEnd).Text,
                InvulnerableSave = invulSaveTextBox.Text,
                WargearOptions = new TextRange( wargearRichTextBox.Document.ContentStart, wargearRichTextBox.Document.ContentEnd ).Text,
                UnitComposition = new TextRange(compositionRichTextBox.Document.ContentStart, compositionRichTextBox.Document.ContentEnd).Text,
                Leader = new TextRange(leaderTextBox.Document.ContentStart, leaderTextBox.Document.ContentEnd).Text,
            };
            info.Save((bool)commandCheckBox.IsChecked, filePath ,Title);
            MessageBox.Show($"Юнит {Name} успешно сохранен", "СОхранение", MessageBoxButton.OK);
        }
    }
}
