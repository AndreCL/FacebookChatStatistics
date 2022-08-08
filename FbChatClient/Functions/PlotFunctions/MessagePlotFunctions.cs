﻿using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FbChatClient.Functions.PlotFunctions;
internal static class MessagePlotFunctions
{
	internal static void GetMessageBarSeries(PlotModel plotModel, int amount, MessageHandler messageHandler)
	{
		var names = messageHandler.GetTopSenders(excludeMe: true);

		var itemsSource1 = new List<BarItem>();
		var labels1 = new List<string>();

		var itemsSource2 = new List<BarItem>();

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
			itemsSource1.Insert(0, new BarItem { Value = name.Value });
			labels1.Insert(0, name.Key);

			//sent
			var sent = messageHandler.GetNumberOfSentForName(name.Key);
			itemsSource2.Insert(0, new BarItem { Value = sent });
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

		DeactivateZoom(plotModel);

		plotModel.InvalidatePlot(true);
	}

	internal static void GetMessageBarSeries(PlotModel plotModel, int amount, int year, MessageHandler messageHandler)
	{
		var names = messageHandler.GetTopSenders(year: year, excludeMe: true);

		var itemsSource1 = new List<BarItem>();
		var labels1 = new List<string>();

		var itemsSource2 = new List<BarItem>();

		var filterednames = names.OrderByDescending(x => x.Value).Take(Math.Min(amount, names.Count));

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

			DeactivateZoom(plotModel);

			plotModel.InvalidatePlot(true);
		}
	}

	internal static void GetMessageBarSeries(PlotModel plotModel, int amount, int year, int month, MessageHandler messageHandler)
	{
		var names = messageHandler.GetTopSenders(year: year, excludeMe: true, month: month);

		var itemsSource1 = new List<BarItem>();
		var labels1 = new List<string>();

		var itemsSource2 = new List<BarItem>();

		var filterednames = names.OrderByDescending(x => x.Value).Take(Math.Min(amount, names.Count));

		if (filterednames.Count() > 0)
		{
			plotModel.Title = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)}";

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
				var sent = messageHandler.GetNumberOfSentForName(name.Key, year: year, month: month);
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

			DeactivateZoom(plotModel);

			plotModel.InvalidatePlot(true);
		}
	}

	private static void DeactivateZoom(PlotModel plotModel)
	{
		plotModel.Axes[0].IsZoomEnabled = false;
		plotModel.Axes[0].IsPanEnabled = false;

		LinearAxis xAxis = new LinearAxis() { Position = AxisPosition.Bottom, MinimumPadding = 0 };
		xAxis.IsZoomEnabled = false;
		xAxis.IsPanEnabled = false;
		plotModel.Axes.Add(xAxis);
	}
}
