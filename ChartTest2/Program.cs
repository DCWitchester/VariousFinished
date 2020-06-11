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
    /// <summary>
    /// the main SumOverTime containing the X and Y points for a series
    /// </summary>
    class SumOverTime
    {
        /// <summary>
        /// the sum or Y value
        /// </summary>
        public String Sum { get; set; } = String.Empty;
        /// <summary>
        /// the time or X value
        /// </summary>
        public String Time { get; set; } = String.Empty;
        /// <summary>
        /// the main constructor for the class
        /// </summary>
        /// <param name="time">the time or X value</param>
        /// <param name="sum">the </param>
        public SumOverTime(String time, String sum)
        {
            Sum = sum;
            Time = time;
        }
    }
    /// <summary>
    /// the main chartForm class used for the chart form initialization and display
    /// </summary>
    class ChartForm
    {
        /// <summary>
        /// the title for the form
        /// </summary>
        public String formTitle = String.Empty;
        /// <summary>
        /// the chart series list
        /// </summary>
        public List<ChartSeries> series = new List<ChartSeries>();
    }
    /// <summary>
    /// the main CharSeries structure
    /// </summary>
    class ChartSeries
    {
        /// <summary>
        /// the list containing the point structures
        /// </summary>
        public List<SumOverTime> sumsOverTime { get; set; } = new List<SumOverTime>();
        /// <summary>
        /// the series name used to display the index
        /// </summary>
        public String Name { get; set; } = String.Empty;
        /// <summary>
        /// the color that will be displayed for the series
        /// </summary>
        public System.Drawing.Color color { get; set; } = System.Drawing.Color.Black;
        /// <summary>
        /// the chartType for displaying the series
        /// </summary>
        public SeriesChartType chartType { get; set; } = SeriesChartType.Line;
        /// <summary>
        /// this function will create a System.Windows.Forms.DataVisualization.Charting.Series series from the current chart ChartSeries object
        /// </summary>
        /// <returns></returns>
        public Series makeSeries()
        {
            //we initialize a new series with all the settings
            var series = new Series
            {
                Name = Name,
                Color = color,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = chartType
            };
            //then add all the points
            foreach(SumOverTime sum in sumsOverTime)
            {
                series.Points.AddXY(sum.Time, sum.Sum);
            }
            //and return the series
            return series;
        }
    }
    /// <summary>
    /// the main program class for the display
    /// </summary>
    class Program
    {
        /// <summary>
        /// we extend a form class with what is needed
        /// </summary>
        public class FakeChartForm1 : Form
        {
            #region Form Properties
            public String text { get; set; } = String.Empty;
            List<ChartSeries> chartSeries { get; set; } = new List<ChartSeries>();
            private System.ComponentModel.IContainer components = null;
            System.Windows.Forms.DataVisualization.Charting.Chart chart1;
            #endregion
            /// <summary>
            /// the main constructor for the class
            /// </summary>
            /// <param name="t1">the title text</param>
            public FakeChartForm1(String t1)
            {
                text = t1;
                InitializeComponent();
            }

            /// <summary>
            /// the load event for the form
            /// </summary>
            /// <param name="sender">the form</param>
            /// <param name="e">the Load Event</param>
            private void Form1_Load(object sender, EventArgs e)
            {
                //we clear the existing series 
                chart1.Series.Clear();
                //then we reinitialize it on the add
                foreach(ChartSeries series in chartSeries)
                {
                    this.chart1.Series.Add(series.makeSeries());
                }
                //and invalidate the chart
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

            //and the normal initialization of the components
            private void InitializeComponent()
            {
                #region Object initialization
                this.components = new System.ComponentModel.Container();
                System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
                System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
                this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
                #endregion
                
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
                this.ClientSize = new System.Drawing.Size(1366, 768);
                this.Controls.Add(this.chart1);
                this.Name = "Form1";
                //this.Text = "FakeChart";
                this.Text = text;
                this.Load += new System.EventHandler(this.Form1_Load);
                ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
                this.ResumeLayout(false);
            }
            /// <summary>
            /// the main colors enumerator
            /// </summary>
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
            /// <summary>
            /// the main seriesCharType enumerator
            /// </summary>
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
            /// <summary>
            /// this function will return a System.Windows.Forms.DataVisualization.Charting.SeriesChartType from our enumerator
            /// </summary>
            /// <param name="seriesChartTypes">the enum value</param>
            /// <returns>the System.Windows.Forms.DataVisualization.Charting.SeriesChartType value</returns>
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
            /// <summary>
            /// this function will return a System.Drawing.Color from our colors enumerator
            /// </summary>
            /// <param name="color">the enumerator value</param>
            /// <returns>the System.Drawing.Color</returns>
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
                //if we don't receive any arguments we close the program
                if (args.Count() < 1) return;
                //if we receive an argument but is null we close the program
                if (String.IsNullOrWhiteSpace(args[0])) return;
                //we set the argument to a String
                String filePath = args[0];
                //if the parameter is not a valid path we close the program
                if (!System.IO.File.Exists(filePath)) return;
                //we enable the visual styles and the text rendering
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                //then we read the text file and for each line we try to draw the chart
                foreach (String line in System.IO.File.ReadAllLines(filePath))
                {
                    //if the line is empty we return
                    if (String.IsNullOrWhiteSpace(line)) return;
                    //else we split the line on '<' to obtain the title
                    String regexTest = line;
                    var m2 = regexTest.Split('<');
                    //we initialize an elements list as a string
                    List<String> elements = new List<String>();
                    //then initialize a new chart form
                    ChartForm chartForm = new ChartForm();
                    //and foreach element in m2
                    foreach (var element in m2)
                    {
                        //if the element contains a ">" then it is a new element list
                        if (element.Contains(">")) { String newelement = element.Replace('>', ' ').Remove(element.Length - 1).Trim(); elements.Add(newelement); }
                        //else it is the title element for the chartForm
                        else { String newelement = element.Replace(',', ' ').Trim(); chartForm.formTitle = newelement; }
                    }
                    //then foreach element in the string list
                    foreach (var element in elements)
                    {
                        //we initialize a chartSeries
                        ChartSeries cs = new ChartSeries();
                        //retrieve the elements between []
                        String regexExpresion = @"\[.*\]";
                        var match = Regex.Match(element, regexExpresion).ToString();
                        //and break the element into an array
                        var settings = element.Replace(match.ToString(), "").Substring(1).Split(',');
                        //set the name, color and chartType
                        cs.Name = settings[0];
                        cs.color = getColorOfColors((Colors)Convert.ToInt32(settings[1]));
                        cs.chartType = getChartTypeOfChartTypes((SeriesChartTypes)Convert.ToInt32(settings[2]));
                        //and finaly we take the points and iterate them
                        foreach (var point in match.Replace('[', ' ').Replace(']', ' ').Trim().Split(','))
                        {
                            cs.sumsOverTime.Add(new SumOverTime(point.Split(';')[1].Trim(), point.Split(';')[0].Trim()));
                        }
                        //and add the series to the form
                        chartForm.series.Add(cs);
                    }
                    //and finaly we display all the chartSeries
                    var form1 = new FakeChartForm1(chartForm.formTitle);
                    foreach (var chartSeries in chartForm.series) form1.chartSeries.Add(chartSeries);
                    Application.Run(form1);
                }
            }
            //Deprecated *.*
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
