using Aspose.Cells;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyRoster.Service
{
    public static class ArmyRosterService
    {
        public static void ParseFile(List<string> armyList)
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
                    armyList.Add(v);
                }
            }
        }

        public static void CreateFile(List<string> armyList, string armyName)
        {


            Workbook workbook = new Workbook();
            Worksheet worksheet = workbook.Worksheets[0];
            worksheet.Name = armyName;//присваиваем имя первой странице(что бы было понятно что за армия
            string armiListPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\armyList\\";// указываем директорию в которую будем сохранять армию
            workbook.Save(armiListPath + armyName + ".xlsx", SaveFormat.Xlsx);//сохраняем армию и создаем название файла
            armyList.Add(armiListPath + armyName + ".xlsx");
        }
    }
}
