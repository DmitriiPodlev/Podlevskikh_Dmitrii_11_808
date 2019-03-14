using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Shell_Sort
{
    class DrawingGraph
    {
        // составляем графики
        public static Chart MakeChart(Series arrayGraph, Series linkedGrapgh)
        {
            var chart = new Chart();
            chart.ChartAreas.Add(new ChartArea());

            arrayGraph.ChartType = SeriesChartType.FastLine;
            arrayGraph.Color = Color.Red;

            linkedGrapgh.ChartType = SeriesChartType.FastLine;
            linkedGrapgh.Color = Color.Green;
            linkedGrapgh.BorderWidth = 3;

            chart.Series.Add(arrayGraph);
            chart.Series.Add(linkedGrapgh);
            chart.Dock = DockStyle.Fill;
            return chart;
        }
        // измеряем время, добавляем в график
        public static void MeasureTime(int[] array, Func<int[], int[]> searchProcedure, Series series)
        {
            var repetitions = 100;
            var watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < repetitions; i++)
                searchProcedure(array);
            watch.Stop();
            series.Points.Add(new DataPoint(array.Length, (float)watch.ElapsedTicks / repetitions));
        }
    }
}
