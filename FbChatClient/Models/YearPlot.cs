using OxyPlot;
using System.ComponentModel;

namespace FbChatClient.Models;
public class YearPlot : INotifyPropertyChanged
{
    private int row;
    private int column;
    private PlotModel plot = new PlotModel();

    public event PropertyChangedEventHandler? PropertyChanged = (o, e) => { };

    public int Row
    {
        get { return row; }
        set
        {
            row = value;
            PropertyChanged(this, new PropertyChangedEventArgs("Row"));
        }
    }

    public int Column
    {
        get { return column; }
        set
        {
            column = value;
            PropertyChanged(this, new PropertyChangedEventArgs("Column"));
        }
    }

    public PlotModel Plot
    {
        get { return plot; }
        set
        {
            plot = value;
            PropertyChanged(this, new PropertyChangedEventArgs("Plot"));
        }
    }
}
