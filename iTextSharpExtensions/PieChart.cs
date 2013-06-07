using iTextSharp.text;
using System;

public class PieChart
{
    private double[] values;
    private double[] angles;
    private string[] captions;
    private int length;
    private BaseColor[] chartcolors;
    private double totalValues = 0;

    private void CalculateAngles()
    {
        this.angles = new double[this.values.Length];

        this.totalValues = 0;
        foreach (var v in this.values)
            this.totalValues += v;

        int counter = 0;
        foreach (var v in this.values)
            this.angles[counter++] = v * 360 / this.totalValues;
    }

    /// <summary>
    /// Expects chart values and captions for the chart
    /// </summary>
    /// <param name="svalues">chart values</param>
    /// <param name="scaptions">captions (label) for the various segments in order of values</param>
    public PieChart(double[] svalues, string[] scaptions)
    {
        if (svalues.Length != scaptions.Length)
        {
            throw (new Exception("Length of values must be equal to the length of captions of chart."));
        }

        if (svalues.Length > 10)
        {
            throw (new Exception("Pie chart does not support items more than 10."));
        }

        this.length = svalues.Length;
        this.values = svalues;
        this.captions = scaptions;

        this.CalculateAngles();

        this.chartcolors = new BaseColor[length];
        this.chartcolors[0] = BaseColor.RED;
        if (length > 1) this.chartcolors[1] = BaseColor.GREEN;
        if (length > 2) this.chartcolors[2] = BaseColor.BLUE;
        if (length > 3) this.chartcolors[3] = BaseColor.BLACK;
        if (length > 4) this.chartcolors[4] = BaseColor.YELLOW;
        if (length > 5) this.chartcolors[5] = BaseColor.ORANGE;
        if (length > 6) this.chartcolors[6] = BaseColor.CYAN;
        if (length > 7) this.chartcolors[7] = BaseColor.MAGENTA;
        if (length > 8) this.chartcolors[8] = BaseColor.PINK;
        if (length > 9) this.chartcolors[9] = BaseColor.LIGHT_GRAY;
    }

    /// <summary>
    /// Expects chart values, captions and colors for the chart
    /// </summary>
    /// <param name="svalues">chart values</param>
    /// <param name="scaptions">captions (label) for the various segments in order of values</param>
    /// <param name="schartcolors">colors to be used for the charts in order of values and captions</param>
    public PieChart(double[] svalues, string[] scaptions, System.Drawing.Color[] schartcolors)
    {
        if (schartcolors == null)
        {
            throw (new Exception("Chart colors cannot be null."));
        }

        if (svalues.Length != scaptions.Length || svalues.Length != schartcolors.Length)
        {
            throw (new Exception("Length of values, chart colors must be equal to the length of captions of chart."));
        }

        this.length = svalues.Length;
        this.values = svalues;
        this.captions = scaptions;
        this.chartcolors = Array.ConvertAll(schartcolors, new Converter<System.Drawing.Color, BaseColor>(PieChart.DoubleToFloat));

        this.CalculateAngles();
    }

    /// <summary>
    /// Expects chart values, captions and colors for the chart
    /// </summary>
    /// <param name="svalues">chart values</param>
    /// <param name="scaptions">captions (label) for the various segments in order of values</param>
    /// <param name="schartcolors">colors to be used for the charts in order of values and captions</param>
    public PieChart(double[] svalues, string[] scaptions, BaseColor[] schartcolors)
    {
        if (schartcolors == null)
        {
            throw (new Exception("Chart colors cannot be null."));
        }

        if (svalues.Length != scaptions.Length || svalues.Length != schartcolors.Length)
        {
            throw (new Exception("Length of values, chart colors must be equal to the length of captions of chart."));
        }

        this.length = svalues.Length;
        this.values = svalues;
        this.captions = scaptions;
        this.chartcolors = schartcolors;

        this.CalculateAngles();
    }

    private static BaseColor DoubleToFloat(System.Drawing.Color c)
    {
        return new BaseColor(c);
    }

    public double[] Values { get { return values; } }
    public string[] Captions { get { return captions; } }
    public BaseColor[] ChartColors { get { return this.chartcolors; } }
    public int Length { get { return length; } }
    public double TotalValues { get { return this.totalValues; } }
    public double[] Angles { get { return this.angles; } }

}
