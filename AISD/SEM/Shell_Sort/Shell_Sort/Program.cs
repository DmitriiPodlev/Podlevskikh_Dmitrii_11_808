using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Shell_Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            // графики массива и линкед листа
            var arrayGraph = new Series();
            var linkedGraph = new Series();
            // номер файла
            int numberOfFile = 1;
            // далее счтиываем данные и строим графики зависимости от времени
            for (int i = 100; i <= 1000;)
            {
                GC.Collect();
                Generator.GenerateNumbers(i, numberOfFile);
                var str = File.ReadAllLines(String.Format("data{0}.txt", numberOfFile));
                var array = new int[str.Length];
                for (int j = 0; j < str.Length; j++)
                    array[j] = Int32.Parse(str[j]);
                DrawingGraph.MeasureTime(array, Array.ShellSortOnArray, arrayGraph);
                DrawingGraph.MeasureTime(array, LinkedList.ShellSortOnLinkedList, linkedGraph);
                i = i + 100;
                numberOfFile++;
            }
            var chart = DrawingGraph.MakeChart(arrayGraph, linkedGraph);
            var form = new Form();
            form.ClientSize = new Size(800, 600);
            form.Controls.Add(chart);
            Application.Run(form);
        }
    }
}
