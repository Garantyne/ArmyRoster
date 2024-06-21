using ArmyRoster.Model;
using Aspose.Cells;
using Aspose.Cells.Charts;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ArmyRoster.Service
{
    internal class UnitListFormService
    {
        delegate void My_Click(object sender, RoutedEventArgs e);
        public static bool delete(Workbook workbook, StackPanel stackPanel1,
                            string unitName, List<Button> listButton,
                            string filePath)
        {
            Aspose.Cells.Worksheet sheet = workbook.Worksheets[0];
            for (int i = 0, j = i; i < stackPanel1.Children.Count; i++, j = i)
            {

                Aspose.Cells.Cell cell = sheet.Cells[$"A{i + 1}"];
                if (cell.StringValue == unitName)
                {
                    sheet.Cells.DeleteRow(i);
                    //Удаляем саму кнопку с экрана
                    stackPanel1.Children.Remove(
                        listButton[listButton
                                            .FindIndex(button => button.Content.ToString() == unitName)]
                        );
                    //а тут удаляем из листбатона
                    listButton.RemoveAt(
                        listButton
                                .FindIndex(button => button.Content.ToString() == unitName)
                        );
                    sheet.Cells.DeleteRows(i + 1, 1);
                    
                    workbook.Save(filePath);
                    j++; //ввел эту переменную что бы проверить её на то была ли проведена операция удаления, и если да то она будет больше I
                    // и тогда у нас сработает следующее условие которое выведет что юнит с такимименем небыл найден
                    break;
                }
                if (j > i)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
