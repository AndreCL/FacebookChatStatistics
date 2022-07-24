using OxyPlot;
using System.Windows.Input;
using System.Windows;
using FbChatClient.Functions;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using FbChatClient.Models;
using System.ComponentModel;

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
                        
        PlotFunctions.GetBarSeries(OverviewPlot, amount: 20, year:2022, messageHandler);

        int x = 0;
        int y = 0;

        for (int month = 1; month <= messageHandler.Last.Month; month++)
        {
            YearPlot yp = new YearPlot();
            yp.Column = y;
            yp.Row = x;
            yp.Plot = new PlotModel();

            PlotFunctions.GetBarSeries(yp.Plot, amount: 10, year: 2022, month: month, messageHandler);

            y++;
            if (y == 3)
            {
                y = 0;
                x++;
            }

            YearPlots.Add(yp);
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

