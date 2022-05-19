using OxyPlot;
using OxyPlot.Series;
using System.Linq;
using System.Collections.Generic;
using OxyPlot.Axes;
using System;
using System.Windows.Input;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using FbChatClient.Functions;

namespace FbChatClient;
public class MainWindowViewModel
{
    private MessageHandler mh;

    public ICommand ScreenShotCommand { get; set; }

    public string LogText { get; set; }

    public MainWindowViewModel()
    {
        this.ScreenShotCommand = new RelayCommand<FrameworkElement>(this.OnScreenShotCommandAsync);

        mh = new MessageHandler("C:\\Data");        

        mh.Load();

        Load();
    }

    private void Load()
    {
        LogText = $"Name: {mh.MyName} | Number of chats: {mh.NumberOfChats} | {mh.First} - {mh.Last}";

        this.Model1 = new PlotModel();
        GetBarSeries(Model1, 20);

        this.Model2 = new PlotModel();
        GetBarSeries(Model2, 2022, 10);

        this.Model3 = new PlotModel();
        GetBarSeries(Model3, 2021, 10);

        this.Model4 = new PlotModel();
        GetBarSeries(Model4, 2020, 10);

        this.Model5 = new PlotModel();
        GetBarSeries(Model5, 2019, 10);

        this.Model6 = new PlotModel();
        GetBarSeries(Model6, 2018, 10);

        this.Model7 = new PlotModel();
        GetBarSeries(Model7, 2017, 10);

        this.Model8 = new PlotModel();
        GetBarSeries(Model8, 2016, 10);

        this.Model9 = new PlotModel();
        GetBarSeries(Model9, 2015, 10);

        this.Model10 = new PlotModel();
        GetBarSeries(Model10, 2014, 10);

        this.Model11 = new PlotModel();
        GetBarSeries(Model11, 2013, 10);

        this.Model12 = new PlotModel();
        GetBarSeries(Model12, 2012, 10);

        this.Model13 = new PlotModel();
        GetBarSeries(Model13, 2011, 10);

        this.Model14 = new PlotModel();
        GetBarSeries(Model14, 2010, 10);

        this.Model15 = new PlotModel();
        GetBarSeries(Model15, 2009, 10);
    }

    private void GetBarSeries(PlotModel plotModel, int number)
    {
        var names = mh.GetTopSenders(excludeMe: true);

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
        var names = mh.GetTopSenders(year: year, excludeMe: true);

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

    public PlotModel Model1 { get; private set; }
    public PlotModel Model2 { get; private set; }
    public PlotModel Model3 { get; private set; }
    public PlotModel Model4 { get; private set; }
    public PlotModel Model5 { get; private set; }
    public PlotModel Model6 { get; private set; }
    public PlotModel Model7 { get; private set; }
    public PlotModel Model8 { get; private set; }
    public PlotModel Model9 { get; private set; }
    public PlotModel Model10 { get; private set; }
    public PlotModel Model11 { get; private set; }
    public PlotModel Model12 { get; private set; }
    public PlotModel Model13 { get; private set; }
    public PlotModel Model14 { get; private set; }
    public PlotModel Model15 { get; private set; }
    public PlotModel Model16 { get; private set; }

    
}

