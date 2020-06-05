using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace ChartTest1
{
    class SumOverTime
    {
        public String Sum { get; set; } = String.Empty;
        public String Time { get; set; } = String.Empty;
        public SumOverTime(String time, String sum)
        {
            Sum = sum;
            Time = time;
        }
    }
    class ChartForm
    {
        public String formTitle = String.Empty;
        public List<ChartSeries> series = new List<ChartSeries>();
    }
    class ChartSeries
    {
        public List<SumOverTime> sumsOverTime { get; set; } = new List<SumOverTime>();
        public String Name { get; set; } = String.Empty;
        public System.Drawing.Color color { get; set; } = System.Drawing.Color.Black;
        public SeriesChartType chartType { get; set; } = SeriesChartType.Line;
        public Series makeSeries()
        {
            var series = new Series
            {
                Name = Name,
                Color = color,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = chartType
            };
            foreach(SumOverTime sum in sumsOverTime)
            {
                series.Points.AddXY(sum.Time, sum.Sum);
            }
            return series;
        }
    }
    class Program
    {
        public class FakeChartForm1 : Form
        {
            public String text { get; set; } = String.Empty;
            List<ChartSeries> chartSeries { get; set; } = new List<ChartSeries>();
            private System.ComponentModel.IContainer components = null;
            System.Windows.Forms.DataVisualization.Charting.Chart chart1;

            public FakeChartForm1(String t1)
            {
                text = t1;
                InitializeComponent();
            }

            private void Form1_Load(object sender, EventArgs e)
            {
                chart1.Series.Clear();
                foreach(ChartSeries series in chartSeries)
                {
                    this.chart1.Series.Add(series.makeSeries());
                }
                chart1.Invalidate();
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }

            private void InitializeComponent()
            {
                this.components = new System.ComponentModel.Container();
                System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
                System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
                this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
                ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
                this.SuspendLayout();
                //
                // chart1
                //
                chartArea1.Name = "ChartArea1";
                this.chart1.ChartAreas.Add(chartArea1);
                this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
                legend1.Name = "Legend1";
                this.chart1.Legends.Add(legend1);
                this.chart1.Location = new System.Drawing.Point(0, 50);
                this.chart1.Name = "chart1";
                // this.chart1.Size = new System.Drawing.Size(284, 212);
                this.chart1.TabIndex = 0;
                this.chart1.Text = "chart1";
                //
                // Form1
                //
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(284, 262);
                this.Controls.Add(this.chart1);
                this.Name = "Form1";
                //this.Text = "FakeChart";
                this.Text = text;
                this.Load += new System.EventHandler(this.Form1_Load);
                ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
                this.ResumeLayout(false);
            }
            public enum Colors
            {
                Black = 0,
                Gray,
                Blue,
                Red,
                Green,
                Violet,
                Yellow, 
                Orange,
                Magenta,
                Cyan
            }
            public enum SeriesChartTypes
            {
                Line = 0,
                Spline,
                FastLine,
                Bar,
                StackedBar,
                Column,
                StackedColumn,
                Area,
                SplineArea,
                StackedArea,
                Pie,
                Radar,
                ThreeLineBreak
            }
            public static SeriesChartType getChartTypeOfChartTypes(SeriesChartTypes seriesChartTypes)
            {
                switch (seriesChartTypes)
                {
                    case SeriesChartTypes.Area:
                        return SeriesChartType.Area;
                    case SeriesChartTypes.Bar:
                        return SeriesChartType.Bar;
                    case SeriesChartTypes.Column:
                        return SeriesChartType.Column;
                    case SeriesChartTypes.FastLine:
                        return SeriesChartType.FastLine;
                    case SeriesChartTypes.Line:
                        return SeriesChartType.Line;
                    case SeriesChartTypes.Pie:
                        return SeriesChartType.Pie;
                    case SeriesChartTypes.Radar:
                        return SeriesChartType.Radar;
                    case SeriesChartTypes.Spline:
                        return SeriesChartType.Spline;
                    case SeriesChartTypes.SplineArea:
                        return SeriesChartType.SplineArea;
                    case SeriesChartTypes.StackedArea:
                        return SeriesChartType.StackedArea;
                    case SeriesChartTypes.StackedBar:
                        return SeriesChartType.StackedBar;
                    case SeriesChartTypes.StackedColumn:
                        return SeriesChartType.StackedColumn;
                    case SeriesChartTypes.ThreeLineBreak:
                        return SeriesChartType.ThreeLineBreak;
                    default:
                        return SeriesChartType.Spline;
                }
            }
            public static System.Drawing.Color getColorOfColors(Colors color)
            {
                switch (color)
                {
                    case Colors.Black:
                        return System.Drawing.Color.Black;
                    case Colors.Blue:
                        return System.Drawing.Color.Blue;
                    case Colors.Cyan:
                        return System.Drawing.Color.Cyan;
                    case Colors.Green:
                        return System.Drawing.Color.Green;
                    case Colors.Gray:
                        return System.Drawing.Color.Gray;
                    case Colors.Magenta:
                        return System.Drawing.Color.Magenta;
                    case Colors.Orange:
                        return System.Drawing.Color.Orange;
                    case Colors.Red:
                        return System.Drawing.Color.Red;
                    case Colors.Violet:
                        return System.Drawing.Color.Violet;
                    case Colors.Yellow:
                        return System.Drawing.Color.Yellow;
                    default:
                        return System.Drawing.Color.Black;
                }
            }
            /// <summary>
            /// The main entry point for the application.
            /// </summary>
            [STAThread]
            static void Main(string[] args)
            {
                String regexTest = "titleValue, <[x1;y1,x2;y2,x3;y3,x4;y4],SeriesName1,0,1>,<[x1;y1,x2;y2],SeriesName2,1,1>,<[x1;y1,x2;y2,x3;y3],SeriesName3,2,1>,<[x1;y1,x2;y2,x3;y3,x4;y4,x5;y5,x6;y6],SeriesName4,3,1>";
                var m2 = regexTest.Split('<');
                List<String> elements = new List<String>();

                ChartForm chartForm = new ChartForm();
                foreach(var element in m2)
                {
                    if (element.Contains(">")) { String newelement = element.Replace('>', ' ').Remove(element.Length - 1).Trim(); elements.Add(newelement); }
                    else { String newelement = element.Replace(',', ' ').Trim(); chartForm.formTitle = newelement; }
                }

                foreach(var element in elements)
                {
                    ChartSeries cs = new ChartSeries();
                    String regexExpresion = @"\[.*\]";
                    var match = Regex.Match(element, regexExpresion);
                    string d = "";
                    var settings = element.Replace(match.ToString(), "").Substring(1).Split(',');
                    cs.Name = settings[0];
                    cs.color = getColorOfColors((Colors)Convert.ToInt32(settings[1]));
                    string q = "";
                }                

                string p = "";
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                #region Test1
                /*
                List<SumOverTime> sums = new List<SumOverTime>();
                for(double i = 0; i <= 1; i = i + 0.01)
                {
                    sums.Add(new SumOverTime(i, Math.Sin(i)));
                }*/
                List<SumOverTime> sums = readSeries();
                ChartSeries chartSeries1 = new ChartSeries();
                chartSeries1.chartType = SeriesChartType.PointAndFigure;
                chartSeries1.Name = "704";
                chartSeries1.color = System.Drawing.Color.Red;
                chartSeries1.sumsOverTime = sums;
                /*
                form1.chartSeries.Add(chartSeries1);
                #endregion
                #region Test2
                List<SumOverTime> sums1 = new List<SumOverTime>();
                for (double i = 0; i <= 1; i = i + 0.01)
                {
                    sums1.Add(new SumOverTime(i, Math.Cos(i)));
                }
                ChartSeries chartSeries2 = new ChartSeries();
                chartSeries2.chartType = SeriesChartType.PointAndFigure;
                chartSeries2.Name = "Cosinus";
                chartSeries2.color = System.Drawing.Color.Red;
                chartSeries2.sumsOverTime = sums1;
                //form1.chartSeries.Add(chartSeries2);*/
                foreach(var x in (SeriesChartType[])Enum.GetValues(typeof(SeriesChartType)))
                {
                    
                    var form1 = new FakeChartForm1(x.ToString());
                    chartSeries1.chartType = x;
                    form1.chartSeries.Add(chartSeries1);
                    Application.Run(form1);
                }
                #endregion
            }
            public static List<SumOverTime> readSeries()
            {
                List<SumOverTime> series1 = new List<SumOverTime>();
                foreach (String line in System.IO.File.ReadAllLines("D:\\Charts\\Serie.txt").ToList())
                {
                    String[] array = line.Replace('[', ' ').Replace(']', ' ').Trim().Split(';');
                    series1.Add(new SumOverTime(array[1], array[0]));
                }
                return series1;
            }
        }
    }
}
