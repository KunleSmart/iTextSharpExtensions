using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
public static class PdfContentByteExtensions {
    public static void DrawPieChart(this PdfContentByte canvas,
        PieChart chart,
        float x0,
        float y0,
        float r = 50f,
        Font font = null,
        bool showCaption = true) {

        if (chart.Values.Length != chart.Captions.Length) {
            return;
        }

        if (font == null) {
            font = FontFactory.GetFont(FontFactory.TIMES, 8);
        }

        canvas.SetLineWidth(0f);

        double _x1, _y1, _x2, _y2;
        float x1, y1, x2, y2;

        canvas.SetLineWidth(1f);
        float cRadius = (float)(r + 0.5);
        canvas.Circle(x0, y0, cRadius);
        canvas.SetColorStroke(BaseColor.GRAY);
        canvas.Stroke();

        canvas.SetLineWidth(0f);
        float rectX1 = x0 - r;
        float rectY1 = y0 - r;

        float xPoint = x0 + r;
        float yPoint = y0 + r;

        //canvas.Rectangle(rectX1, rectY1, 2 * r, 2 * r);
        //canvas.Stroke();

        double _startAngle = 0;
        double _endAngle = 0;

        float startAngle = 0;
        float endAngle = 0;

        float captionY = y0 + (chart.Values.Length - 1) * 6;
        double _percentage;
        string percentage;

        for (int counter = 0; counter < chart.Values.Length; counter++) {
            if (chart.TotalValues > 0)
                _percentage = chart.Angles[counter] * 100 / 360;
            else
                _percentage = 0;

            if (showCaption) {
                //captions from here
                canvas.SetColorStroke(chart.ChartColors[counter]);
                canvas.SetColorFill(chart.ChartColors[counter]);
                canvas.Rectangle(x0 + r + 10, captionY, 7, 7);
                canvas.ClosePathFillStroke();

                percentage = string.Format("{0:N}", _percentage);
                ColumnText text2 = new ColumnText(canvas);
                Phrase phrase = new Phrase(string.Format("{0} ({1}%)", chart.Captions[counter], percentage), font);
                text2.SetSimpleColumn(phrase, x0 + r + 20, captionY, x0 + r + 200, captionY, 0f, 0);
                text2.Go();

                captionY -= 12;
                if (_percentage == 0) {
                    continue;
                }
                //end of caption
            }

            if (chart.TotalValues <= 0)
                continue;

            if (_percentage <= 50) {
                //get coordinate on circle
                _x1 = x0 + r * Math.Cos(_startAngle * Math.PI / 180);
                _y1 = y0 + r * Math.Sin(_startAngle * Math.PI / 180);
                x1 = (float)_x1;
                y1 = (float)_y1;

                _endAngle += chart.Angles[counter];
                _x2 = x0 + r * Math.Cos(_endAngle * Math.PI / 180);
                _y2 = y0 + r * Math.Sin(_endAngle * Math.PI / 180);
                x2 = (float)_x2;
                y2 = (float)_y2;

                startAngle = (float)_startAngle;
                endAngle = (float)_endAngle;

                //set the colors to be used
                canvas.SetColorStroke(chart.ChartColors[counter]);
                canvas.SetColorFill(chart.ChartColors[counter]);

                //draw the triangle within the circle
                canvas.MoveTo(x0, y0);
                canvas.LineTo(x1, y1);
                canvas.LineTo(x2, y2);
                canvas.LineTo(x0, y0);
                canvas.ClosePathFillStroke();
                //draw the arc
                canvas.Arc(rectX1, rectY1, xPoint, yPoint, startAngle, (float)chart.Angles[counter]);
                canvas.ClosePathFillStroke();
                _startAngle += chart.Angles[counter];
            }
            else {
                //DO THE FIRST PART
                //get coordinate on circle
                _x1 = x0 + r * Math.Cos(_startAngle * Math.PI / 180);
                _y1 = y0 + r * Math.Sin(_startAngle * Math.PI / 180);
                x1 = (float)_x1;
                y1 = (float)_y1;

                _endAngle += 180;
                _x2 = x0 + r * Math.Cos(_endAngle * Math.PI / 180);
                _y2 = y0 + r * Math.Sin(_endAngle * Math.PI / 180);
                x2 = (float)_x2;
                y2 = (float)_y2;

                startAngle = (float)_startAngle;
                endAngle = (float)_endAngle;

                //set the colors to be used
                canvas.SetColorStroke(chart.ChartColors[counter]);
                canvas.SetColorFill(chart.ChartColors[counter]);

                //draw the triangle within the circle
                canvas.MoveTo(x0, y0);
                canvas.LineTo(x1, y1);
                canvas.LineTo(x2, y2);
                canvas.LineTo(x0, y0);
                canvas.ClosePathFillStroke();
                //draw the arc
                canvas.Arc(rectX1, rectY1, xPoint, yPoint, startAngle, 180);
                canvas.ClosePathFillStroke();

                //DO THE SECOND PART
                //get coordinate on circle
                _x1 = x0 + r * Math.Cos((_startAngle + 180) * Math.PI / 180);
                _y1 = y0 + r * Math.Sin((_startAngle + 180) * Math.PI / 180);
                x1 = (float)_x1;
                y1 = (float)_y1;

                _endAngle += chart.Angles[counter] - 180;
                _x2 = x0 + r * Math.Cos(_endAngle * Math.PI / 180);
                _y2 = y0 + r * Math.Sin(_endAngle * Math.PI / 180);
                x2 = (float)_x2;
                y2 = (float)_y2;

                startAngle = (float)_startAngle;
                endAngle = (float)_endAngle;

                //set the colors to be used
                canvas.SetColorStroke(chart.ChartColors[counter]);
                canvas.SetColorFill(chart.ChartColors[counter]);

                //draw the triangle within the circle
                canvas.MoveTo(x0, y0);
                canvas.LineTo(x1, y1);
                canvas.LineTo(x2, y2);
                canvas.LineTo(x0, y0);
                canvas.ClosePathFillStroke();
                //draw the arc
                canvas.Arc(rectX1, rectY1, xPoint, yPoint, startAngle + 180, (float)(chart.Angles[counter] - 180));
                canvas.ClosePathFillStroke();

                _startAngle += chart.Angles[counter];
            }

        }
    }

    //public static void DrawBarChart(this PdfContentByte canvas, BarChart chart, float locationX, float locationY, float width, float height, Font font = null)
    //{
    //    if (chart == null) return;
    //    if (chart.Values == null || chart.Captions == null) return;
    //    if (chart.Values.Length != chart.Captions.Length) return;

    //    //for (int i = 0; i < chart.Captions.Length; i++)
    //    //{
    //    //    if (chart.Captions[i] != null)
    //    //        if (chart.Captions[i].Trim().Length == 4)
    //    //            chart.Captions[i] = chart.Captions[i].Trim().Substring(2, 2);
    //    //}

    //    if (font == null)
    //        font = FontFactory.GetFont(FontFactory.TIMES, 8);

    //    int xInterval = 50;

    //    //get the maximum possible value
    //    float maxVal = 0;
    //    //for (int counter = 0; counter < chart.Values.Length; counter++)
    //    //    if (chart.Values[counter] > maxVal) maxVal = chart.Values[counter];

    //    maxVal = (float)1.2 * maxVal;
    //    string temp = string.Empty;
    //    float scale = maxVal / 300;

    //    //get the scales
    //    float[] scales = new float[5];
    //    float yVal = 0;
    //    for (int counter = 0; counter < 5; counter++)
    //    {
    //        //temp = ((5 - counter) * maxVal / 5000).ToString() + "000";
    //        scales[counter] = (5 - counter) * 1000 * (float)Math.Round((maxVal / 5000));
    //        yVal = counter * 55 + 45;

    //        //g.DrawString(string.Format("{0:N}", scales[counter]), f, System.Drawing.Brushes.Black, 0, yVal - 4);
    //    }

    //    //Draw the axes    
    //    //canvas.SetColorStroke(BaseColor.BLACK);
    //    //canvas.SetColorFill(chart.ChartColors[counter]);
    //    canvas.MoveTo(locationX, locationY);
    //    canvas.LineTo(locationX + width, locationY); //x axis
    //    canvas.MoveTo(locationX, locationY);
    //    canvas.LineTo(locationX, locationY + height); //y axis
    //    canvas.Stroke();

    //    //g.DrawLine(p, 100, 25, 100, 325);// y-axis            
    //    //g.DrawLine(p, 100, 325, 700, 325);//x-axis            

    //    //for (int i = 0; i < chart.ActualValues.Length; i++)
    //    //{
    //    //    height = (chart.TheoreticalValues[i] / scale);
    //    //    //height = (height / scale) * scale;
    //    //    canvas.Rectangle(x, y, w, h);
    //    //    canvas.Fill();
    //    //    canvas.Stroke();

    //    //    //g.DrawString(chart.Captions[i], f, System.Drawing.Brushes.Black, (xInterval * i) + 132 + (width / 4), 325);
    //    //}

    //    //ColumnText text2 = new ColumnText(canvas);
    //    //Phrase phrase = new Phrase("bar chart", font);
    //    //text2.SetSimpleColumn(phrase, 20, 700, 200, 700, 0f, 0);
    //    //text2.Go();

    //}



}

