using System;

public class BarChart
{
    public BarChart( float[,] values, string[,] scaptions)
    {
        if (values.Length != scaptions.Length )
        {
            throw (new Exception("Length of values must be equal to the length of captions of chart."));
        }

        this.Length = values.Length;
        this.Values = values;
        this.Captions = scaptions;
    }

    public float[,] Values { get; set; }
    public string[,] Captions { get; set; }
    public int Length { get; set; }
}