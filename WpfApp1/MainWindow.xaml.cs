using System;
using System.Threading.Tasks;
using System.Windows;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using System.Threading;
using Microsoft.Win32;
using System.Reflection;
using System.ComponentModel;
using System.Drawing;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        // OxyPlot
        PlotModel plotModel { get; } = new PlotModel();
        LineSeries lineSeries { get; } = new LineSeries();
        bool cancelFlag = false;
        int period = 360;

        HarmonicSeries harmonics;

        public MainWindow()
        {
            InitializeComponent();

            harmonics = new HarmonicSeries(10, period);
            DataContext = harmonics;

            GraphSetup();
        }

        void GraphSetup()
        {
            var AxisX = new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                TitleFontSize = 16,
                Title = "Ось X"
            };

            var AxisY = new LinearAxis()
            {
                Position = AxisPosition.Left,
                TitleFontSize = 16,
                Title = "Ось Y"
            };

            plotModel.Axes.Add(AxisX);
            plotModel.Axes.Add(AxisY);
            plotModel.Background = OxyColors.White;

            //настройка графика
            lineSeries.StrokeThickness = 1.5;
            lineSeries.Color = OxyColor.FromRgb(0, 100, 205);

            plotModel.Series.Add(lineSeries);

            PlotView.Model = plotModel;
        }

        async void Draw_Button(object sender, RoutedEventArgs e)
        {
            //очистить график
            lineSeries.Points.Clear();
            cancelFlag = false;
            DrawButton.IsEnabled = false;
            StopButton.IsEnabled = true;

            //Токен для отмены асинхронности
            using (var tokenSource = new CancellationTokenSource())
            {
                await Draw(tokenSource);
            }
        }

        void Stop_Button(object sender, RoutedEventArgs e)
        {
            cancelFlag = true;
            StopButton.IsEnabled = false;
            DrawButton.IsEnabled = true;
        }
      
        async Task Draw(CancellationTokenSource tokenSource)
        {
            int deg = 0;
            int index = 0;
            while (true)
            {                
                foreach (var val in harmonics.Output)
                {
                    if (cancelFlag)
                    {
                        tokenSource.Cancel();
                        return;
                    }

                    index++;
                    if (index < 2 * period)
                    {
                        lineSeries.Points.Add(new DataPoint(deg, val));
                    }
                    else
                    {
                        lineSeries.Points.RemoveAt(0);
                        lineSeries.Points.Add(new DataPoint(deg, val));
                    }

                    plotModel.InvalidatePlot(true);
                    deg++;
                    await Task.Delay(10);
                }
            }
        }
    }
}

