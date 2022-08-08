using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;

namespace FbChatClient.Functions.PlotFunctions;

internal class BasePlotFunctions
{
	protected static void DeactivateZoom(PlotModel plotModel)
	{
		foreach (var axis in plotModel.Axes)
		{
			axis.IsZoomEnabled = false;
			axis.IsPanEnabled = false;
		}		

		LinearAxis xAxis = new LinearAxis() { Position = AxisPosition.Bottom, MinimumPadding = 0 };
		xAxis.IsZoomEnabled = false;
		xAxis.IsPanEnabled = false;
		plotModel.Axes.Add(xAxis);
	}

	protected static void FormatLegend(PlotModel plotModel)
	{
		plotModel.Legends.Add(new Legend()
		{
			LegendPlacement = LegendPlacement.Inside,
			LegendPosition = LegendPosition.BottomCenter,
			LegendOrientation = LegendOrientation.Horizontal,
			LegendBorderThickness = 0
		});
	}
}
