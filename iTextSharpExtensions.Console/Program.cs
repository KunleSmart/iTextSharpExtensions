using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTextSharpExtensions.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = DateTime.Now.Millisecond.ToString() + "PieChart.pdf";
            string returnPath = "temp\\" + filename;
            string startupPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            string filePath = string.Format("{0}\\{1}", startupPath, returnPath);
            if (PieChartGenerator.Generate(filePath))
            //if (BarChartGenerator.Generate(filePath))
                Process.Start(filePath);

        }
    }

    class PieChartGenerator
    {
        public static bool Generate(string filename)
        {
            Document document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
            document.Open();

            PdfContentByte canvas = writer.DirectContent;

            Font font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);

            string[] captions = { "ATM", "CASH", "WEB", "MOBILE" };
            //double[] values = { 2, 60, 20, 18 };
            double[] values = { 5, 60, 35, 0 };
            PieChart chart = new PieChart(values, captions);
            canvas.DrawPieChart(chart, 110, 730, 100);
            canvas.DrawPieChart(chart, 410, 730, 60);

            string[] captions2 = { "CAPTION 1", "CAPTION 2", "CAPTION 3", "CAPTION 4", "CAPTION 5", "CAPTION 6", "CAPTION 7", "CAPTION 8", "CAPTION 9", "CAPTION 10" };
            double[] values2 = { 100, 80, 60, 40, 50, 60, 30, 0, 90, 40 };
            chart = new PieChart(values2, captions2);
            canvas.DrawPieChart(chart, 110, 520, 100, font);
            canvas.DrawPieChart(chart, 410, 520, 60);

            //string[] captions3 = { "CAPTION 1", "CAPTION 2", "CAPTION 3" };
            //double[] values3 = { 100, 80, 60 };
            //System.Drawing.Color[] colors = new System.Drawing.Color[] { System.Drawing.Color.Black, 
            //    System.Drawing.Color.CornflowerBlue, 
            //    System.Drawing.Color.Brown };

            //chart = new PieChart(values3, captions3, colors);
            //canvas.DrawPieChart(chart, 110, 300, 100, font);
            //canvas.DrawPieChart(chart, 410, 300, 60);

            string[] captions4 = { "CAPTION 1", "CAPTION 2", "CAPTION 3", "CAPTION 4", "CAPTION 5" };
            double[] values4 = { 100, 80, 60, 50, 50 };
            BaseColor pantome376 = new BaseColor(130, 197, 91);
            BaseColor pantone369 = new BaseColor(95, 182, 85);
            BaseColor pantonecool = new BaseColor(88, 89, 91);
            BaseColor pantone447 = new BaseColor(67, 66, 61);
            BaseColor pantonecoolLight = new BaseColor(173, 216, 230);
            BaseColor[] colors4 = { pantome376, pantone369, pantonecool, pantone447, pantonecoolLight };
            chart = new PieChart(values4, captions4, colors4);
            canvas.DrawPieChart(chart, 110, 300, 100, font);
            canvas.DrawPieChart(chart, 410, 300, 60);

            document.Close();
            return true;
        }


    }

    class BarChartGenerator
    {
        public static bool Generate(string filename)
        {
            Document document = new Document(PageSize.A4, 0f, 0f, 0f, 0f);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filename, FileMode.Create));
            document.Open();

            PdfContentByte canvas = writer.DirectContent;

            Font font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);


            //float[,] val = { 2000, 3000, 4000 };
            //string[,] captions = { "", "", "" };
            //BarChart chart = new BarChart(val, captions);

            //canvas.DrawBarChart(chart, 40, 500, 300, 200);

            document.Close();
            return true;
        }
    }
}
