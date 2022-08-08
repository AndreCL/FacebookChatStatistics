using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FbChatClient.Functions.PlotFunctions;
internal static class CallPlotFunctions
{
	internal static void GetCallBarSeries(PlotModel plotModel, int amount, MessageHandler messageHandler)
	{
		var names = messageHandler.GetTopCallers();

		var itemsSource = new List<BarItem>();
		var labels = new List<string>();

		var filterednames = names.OrderByDescending(x => x.Value).Take(Math.Min(amount, names.Count));

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
			itemsSource.Insert(0, new BarItem { Value = name.Value });
			labels.Insert(0, name.Key);
		}

		var barSeries = new BarSeries()
		{
			Title = "Minutes",
			StrokeColor = OxyColors.Black,
			StrokeThickness = 1,

		};

		plotModel.Series.Add(barSeries);

		plotModel.Axes.Add(new CategoryAxis
		{
			Position = AxisPosition.Left,
			Key = "Name",
			ItemsSource = labels,
			IsZoomEnabled = false
		});

		plotModel.InvalidatePlot(true);
	}
}
