using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FbChatClient.Functions.PlotFunctions;
internal class MessagePlotFunctions : BasePlotFunctions
{
	internal static void GetMessageBarSeries(PlotModel plotModel, int amount, MessageHandler messageHandler)
	{
		var names = messageHandler.GetTopSenders(excludeMe: true);

		var receivedItemsSource = new List<BarItem>();
		var labels = new List<string>();
		var sentItemsSource = new List<BarItem>();

		var filterednames = names.OrderByDescending(x => x.Value).Take(Math.Min(amount, names.Count));

		if(filterednames.Count() > 0)
		{
			plotModel.Title = "All time";

			FormatLegend(plotModel);

			foreach (var name in filterednames)
			{
				//received
				receivedItemsSource.Insert(0, new BarItem { Value = name.Value });
				labels.Insert(0, name.Key);

				//sent
				var sent = messageHandler.GetNumberOfSentForName(name.Key);
				sentItemsSource.Insert(0, new BarItem { Value = sent });
			}

			ProcessMessageData(plotModel, receivedItemsSource, sentItemsSource, labels);
		}		
	}

	internal static void GetMessageBarSeries(PlotModel plotModel, int amount, int year, MessageHandler messageHandler)
	{
		var names = messageHandler.GetTopSenders(year: year, excludeMe: true);

		var receivedItemsSource = new List<BarItem>();
		var labels = new List<string>();
		var sentItemsSource = new List<BarItem>();

		var filterednames = names.OrderByDescending(x => x.Value).Take(Math.Min(amount, names.Count));

		if (filterednames.Count() > 0)
		{
			plotModel.Title = $"{year}";

			FormatLegend(plotModel);

			foreach (var name in filterednames)
			{
				//received
				receivedItemsSource.Insert(0, new BarItem { Value = name.Value });
				labels.Insert(0, name.Key);

				//sent
				var sent = messageHandler.GetNumberOfSentForName(name.Key, year);
				sentItemsSource.Insert(0, new BarItem { Value = sent });
			}

			ProcessMessageData(plotModel, receivedItemsSource, sentItemsSource, labels);
		}
	}

	internal static void GetMessageBarSeries(PlotModel plotModel, int amount, int year, int month, MessageHandler messageHandler)
	{
		var names = messageHandler.GetTopSenders(year: year, excludeMe: true, month: month);

		var receivedItemsSource = new List<BarItem>();
		var labels = new List<string>();
		var sentItemsSource = new List<BarItem>();

		var filterednames = names.OrderByDescending(x => x.Value).Take(Math.Min(amount, names.Count));

		if (filterednames.Count() > 0)
		{
			plotModel.Title = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)}";

			FormatLegend(plotModel);

			foreach (var name in filterednames)
			{
				//received
				receivedItemsSource.Insert(0, new BarItem { Value = name.Value });
				labels.Insert(0, name.Key);

				//sent
				var sent = messageHandler.GetNumberOfSentForName(name.Key, year: year, month: month);
				sentItemsSource.Insert(0, new BarItem { Value = sent });
			}

			ProcessMessageData(plotModel, receivedItemsSource, sentItemsSource, labels);
		}
	}
	
	private static void ProcessMessageData(PlotModel plotModel, List<BarItem> receivedItemsSource, List<BarItem> sentItemsSource, List<string> labels)
	{
		var barSeries1 = new BarSeries()
		{
			Title = "Received",
			ItemsSource = receivedItemsSource,
			StrokeColor = OxyColors.Black,
			StrokeThickness = 1
		};

		var barSeries2 = new BarSeries()
		{
			Title = "Sent",
			ItemsSource = sentItemsSource,
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
			ItemsSource = labels
		});

		DeactivateZoom(plotModel);

		plotModel.InvalidatePlot(true);
	}
}
