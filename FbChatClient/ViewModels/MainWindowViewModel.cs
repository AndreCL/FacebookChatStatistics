using OxyPlot;
using OxyPlot.Series;
using System.Linq;
using System.Collections.Generic;
using OxyPlot.Axes;
using System;
using System.Windows.Input;
using System.Windows;
using FbChatClient.Functions;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using FbChatClient.Models;
using System.ComponentModel;
using OxyPlot.Legends;

namespace FbChatClient.ViewModels;
public class MainWindowViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string name)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(name));
        }
    }

    private readonly MessageHandler messageHandler;

    public ICommand ScreenShotCommand { get; set; }
    public ICommand DirectoryCommand { get; set; }

    public ObservableCollection<YearPlot> YearPlots { get; set; }

    private string _logText;

    public string LogText
    {
        get { return _logText; }
        set
        {
            _logText = value;
            OnPropertyChanged("LogText");
        }
    }

    public PlotModel OverviewPlot { get; set; }

    public MainWindowViewModel()
    {
        ScreenShotCommand = new RelayCommand<FrameworkElement>(OnScreenShotCommandAsync);

        DirectoryCommand = new RelayCommand(OnDirectoryCommandAsync);

        messageHandler = new MessageHandler("C:\\Data\\inbox");

        YearPlots = new ObservableCollection<YearPlot>();
        OverviewPlot = new PlotModel();
    }

    private void Load()
    {
        //Clear plots
        YearPlots.Clear();

        LogText = $"{messageHandler.FileGymnastics.InboxFolderLocation} | " +
            $"Name: {messageHandler.MyName} | Number of chats: {messageHandler.NumberOfChats} | " +
            $"{messageHandler.First} - {messageHandler.Last}";
                        
        GetBarSeries(OverviewPlot, 20);

        int x = 0;
        int y = 0;

        for (int year = messageHandler.Last.Year; year >= messageHandler.First.Year; year--)
        {
            YearPlot yp = new YearPlot();
            yp.Column = y;
            yp.Row = x;
            yp.Plot = new PlotModel();
            
            GetBarSeries(yp.Plot, year, 10);

            y++;
            if (y == 3)
            {
                y = 0;
                x++;
            }

            YearPlots.Add(yp);
        }
    }

    private void GetBarSeries(PlotModel plotModel, int number)
    {
        var names = messageHandler.GetTopSenders(excludeMe: true);

        var itemsSource1 = new List<BarItem>();
        var labels1 = new List<string>();

        var itemsSource2 = new List<BarItem>();

        var filterednames = names.OrderByDescending(x => x.Value).Take(Math.Min(number, names.Count));

        plotModel.Title = "All time";

        plotModel.Legends.Add(new Legend()
        {
            LegendPlacement = LegendPlacement.Inside,
            LegendPosition = LegendPosition.BottomCenter,
            LegendOrientation = LegendOrientation.Horizontal,
            LegendBorderThickness = 0
        });

        foreach (var name in filterednames)
        {
            //received
            itemsSource1.Insert(0, new BarItem { Value = name.Value });
            labels1.Insert(0, name.Key);

            //sent
            var sent = messageHandler.GetNumberOfSentForName(name.Key);
            itemsSource2.Insert(0, new BarItem { Value = sent});
        }

        var barSeries1 = new BarSeries()
        {
            Title = "Received",
            ItemsSource = itemsSource1,
            StrokeColor = OxyColors.Black, 
            StrokeThickness = 1,
            
        };

        var barSeries2 = new BarSeries()
        {
            Title = "Sent",
            ItemsSource = itemsSource2,
            StrokeColor = OxyColors.Black,
            StrokeThickness = 1,
            FillColor = OxyColors.Blue
        };

        plotModel.Series.Add(barSeries2);
        plotModel.Series.Add(barSeries1);

        plotModel.Axes.Add(new CategoryAxis
        {
            Position = AxisPosition.Left,
            Key = "Name",
            ItemsSource = labels1
        });        

        plotModel.InvalidatePlot(true);
    }

    private void GetBarSeries(PlotModel plotModel, int year, int number)
    {
        var names = messageHandler.GetTopSenders(year: year, excludeMe: true);

        var itemsSource1 = new List<BarItem>();
        var labels1 = new List<string>();

        var itemsSource2 = new List<BarItem>();

        var filterednames = names.OrderByDescending(x => x.Value).Take(Math.Min(number, names.Count));

        if (filterednames.Count() > 0)
        {
            plotModel.Title = $"{year}";

            plotModel.Legends.Add(new Legend()
            {
                LegendPlacement = LegendPlacement.Inside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0
            });

            foreach (var name in filterednames)
            {
                //received
                itemsSource1.Insert(0, new BarItem { Value = name.Value });
                labels1.Insert(0, name.Key);

                //sent
                var sent = messageHandler.GetNumberOfSentForName(name.Key, year);
                itemsSource2.Insert(0, new BarItem { Value = sent });
            }

            var barSeries1 = new BarSeries()
            {
                Title = "Received",
                ItemsSource = itemsSource1,
                StrokeColor = OxyColors.Black,
                StrokeThickness = 1
            };

            var barSeries2 = new BarSeries()
            {
                Title = "Sent",
                ItemsSource = itemsSource2,
                StrokeColor = OxyColors.Black,
                StrokeThickness = 1,
                FillColor = OxyColors.Blue
            };

            plotModel.Series.Add(barSeries2);
            plotModel.Series.Add(barSeries1);


            plotModel.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Left,
                Key = "Name",
                ItemsSource = labels1
            });

            plotModel.InvalidatePlot(true);
        }
    }

    private async void OnScreenShotCommandAsync(FrameworkElement frameworkElement)
    {
        var result = await Screenshot.TryScreenshotToClipboardAsync(frameworkElement);
        if (result == true)
        {
            // Success
        }
    }

    private void OnDirectoryCommandAsync()
    {
        var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
        if (dialog.ShowDialog().GetValueOrDefault())
        {
            LogText = "Loading...";

            messageHandler.FileGymnastics.InboxFolderLocation = dialog.SelectedPath;

            messageHandler.Load();

            Load();
        }
    }
}

