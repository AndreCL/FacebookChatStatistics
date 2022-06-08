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

namespace FbChatClient.ViewModels;
public class MainWindowViewModel
{
    private readonly MessageHandler messageHandler;

    public ICommand ScreenShotCommand { get; set; }
   
    public ObservableCollection<YearPlot> YearPlots { get; set; }
    
    public string LogText { get; set; }

    public PlotModel OverviewPlot { get; private set; }

    public MainWindowViewModel()
    {
        ScreenShotCommand = new RelayCommand<FrameworkElement>(OnScreenShotCommandAsync);

        messageHandler = new MessageHandler("C:\\Data");
        messageHandler.Load();

        YearPlots = new ObservableCollection<YearPlot>();

        Load();
    }

    private void Load()
    {
        LogText = $"Name: {messageHandler.MyName} | Number of chats: {messageHandler.NumberOfChats} | {messageHandler.First} - {messageHandler.Last}";

        OverviewPlot = new PlotModel();
        GetBarSeries(OverviewPlot, 20);

        int x = 0;
        int y = 0;

        for(int year = messageHandler.Last.Year; year >= messageHandler.First.Year; year--)
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

        var itemsSource = new List<BarItem>();
        var labels = new List<string>();

        var filterednames = names.OrderByDescending(x => x.Value).Take(Math.Min(number, names.Count));

        plotModel.Title = "All time";

        foreach (var name in filterednames)
        {
            itemsSource.Insert(0, new BarItem { Value = name.Value, Color = ColorFromValue(name.Value) });
            labels.Insert(0, name.Key);
        }

        var barSeries = new BarSeries()
        {
            ItemsSource = itemsSource
        };

        plotModel.Series.Add(barSeries);

        plotModel.Axes.Add(new CategoryAxis
        {
            Position = AxisPosition.Left,
            Key = "Name",
            ItemsSource = labels
        });
    }

    private OxyColor ColorFromValue(int value)
    {
        if (value < 1000)
        {
            return OxyColors.Red;
        }
        if (value < 10000)
        {
            return OxyColors.Orange;
        }
        else
        {
            return OxyColors.Green;
        }
    }

    private void GetBarSeries(PlotModel plotModel, int year, int number)
    {
        var names = messageHandler.GetTopSenders(year: year, excludeMe: true);

        var itemsSource = new List<BarItem>();
        var labels = new List<string>();

        var filterednames = names.OrderByDescending(x => x.Value).Take(Math.Min(number, names.Count));

        if (filterednames.Count() > 0)
        {
            plotModel.Title = $"{year}";

            foreach (var name in filterednames)
            {
                itemsSource.Insert(0, new BarItem { Value = name.Value, Color = ColorFromValue(name.Value) });
                labels.Insert(0, name.Key);
            }

            var barSeries = new BarSeries()
            {
                ItemsSource = itemsSource
            };

            plotModel.Series.Add(barSeries);

            plotModel.Axes.Add(new CategoryAxis
            {
                Position = AxisPosition.Left,
                Key = "Name",
                ItemsSource = labels
            });
        }
    }

    private async void OnScreenShotCommandAsync(FrameworkElement frameworkElement)
    {
        var result = await Screenshot.TryScreenshotToClipboardAsync(frameworkElement);
        if (result == true)
        {
            // Success.
        }
    }
}

