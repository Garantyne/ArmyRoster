using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyRoster.Model
{
    public class UnitInfoW40K :IUnitInfo
    {
        public string MState1 {  get; set; }
        public string TState1 {  get; set; }
        public string SVState1 {  get; set; }
        public string WState1 {  get; set; }
        public string LDState1 {  get; set; }
        public string OCState1 {  get; set; }

        public string MState2 {  get; set; }
        public string TState2 {  get; set; }
        public string SVState2 {  get; set; }
        public string WState2 {  get; set; }
        public string LDState2 {  get; set; }
        public string OCState2 {  get; set; }

        public string RangeWeapon {  get; set; }
        public string MeleeWeapon {  get; set; }
        public string KeyWord {  get; set; }
        public string Abilities {  get; set; }
        public string InvulnerableSave {  get; set; }
        public string UnitComposition {  get; set; }
        public string WargearOptions {  get; set; }
        public string Leader {  get; set; }

        public UnitInfoW40K() { }

        public void Load()
        {

        }

        public void Save(bool flag, string path, string name)
        {
            Workbook workBook = new Workbook();
            workBook.LoadFromFile(path);
            Worksheet worksheet = workBook.Worksheets[0];

            int i = 0;
            string str ;//почему то напрямую в ворклисте не хочет значение сравнивать
            do{
                i++;
                str = worksheet.Range[i, 1].Value;

            } while (str != "");

                if (flag)
            {
                worksheet.Range[i, 1].Value = name;
                worksheet.Range[i, 2].Value = MState1;
                worksheet.Range[i, 3].Value = MState2;
                worksheet.Range[i, 4].Value = SVState1;
                worksheet.Range[i, 5].Value = SVState2;
                worksheet.Range[i, 6].Value = TState1;
                worksheet.Range[i, 7].Value = TState2;
                worksheet.Range[i, 8].Value = WState1;
                worksheet.Range[i, 9].Value = WState2;
                worksheet.Range[i, 10].Value = LDState1;
                worksheet.Range[i, 11].Value = LDState2;
                worksheet.Range[i, 12].Value = OCState1;
                worksheet.Range[i, 13].Value = OCState2;
                worksheet.Range[i, 14].Value = InvulnerableSave;
                worksheet.Range[i, 15].Value = KeyWord;
                worksheet.Range[i, 16].Value = RangeWeapon;
                worksheet.Range[i, 17].Value = MeleeWeapon;
                worksheet.Range[i, 18].Value = Abilities;
                worksheet.Range[i, 19].Value = Leader;
                worksheet.Range[i, 20].Value = WargearOptions;
                worksheet.Range[i, 21].Value = UnitComposition;
            }
            else {
                worksheet.Range[i, 1].Value = name;
                worksheet.Range[i, 2].Value = MState1;
                worksheet.Range[i, 4].Value = SVState1;
                worksheet.Range[i, 6].Value = TState1;
                worksheet.Range[i, 8].Value = WState1;
                worksheet.Range[i, 10].Value = LDState1;
                worksheet.Range[i, 12].Value = OCState1;
                worksheet.Range[i, 14].Value = InvulnerableSave;
                worksheet.Range[i, 15].Value = KeyWord;
                worksheet.Range[i, 16].Value = RangeWeapon;
                worksheet.Range[i, 17].Value = MeleeWeapon;
                worksheet.Range[i, 18].Value = Abilities;
                worksheet.Range[i, 19].Value = Leader;
                worksheet.Range[i, 20].Value = WargearOptions;
                worksheet.Range[i, 21].Value = UnitComposition;
            }
            workBook.SaveToFile(path);
        }
    }
}
