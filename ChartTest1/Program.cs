using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace ChartTest1
{
    class Program
    {
        public class FakeChartForm1 : Form
        {
            private System.ComponentModel.IContainer components = null;
            System.Windows.Forms.DataVisualization.Charting.Chart chart1;

            public FakeChartForm1()
            {
                InitializeComponent();
            }

            private double f(int i)
            {
                var f1 = 59894 - (8128 * i) + (262 * i * i) - (1.6 * i * i * i);
                return f1;
            }
            class SumOverTime
            {
                public Double Sin { get; set; } = new Double();
                public Double Cos { get; set; } = new Double();
                public Double Tan { get; set; } = new Double();
                public Double Time { get; set; } = new Double();
                public SumOverTime(Double sum, Double cos, Double tan, Double time)
                {
                    Sin = sum;
                    Cos = cos;
                    Tan = tan;
                    Time = time;
                }
            }

            List<SumOverTime> sumOverTimes = new List<SumOverTime>();
            private void fillSums()
            {
                for (double i = 1; i <= 100; i=i+0.01)
                {
                    sumOverTimes.Add(new SumOverTime(Math.Sin(i), Math.Cos(i),Math.Tan(i), i));
                }
            }
            private void Form1_Load(object sender, EventArgs e)
            {
                chart1.Series.Clear();
                var series1 = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Series1",
                    Color = System.Drawing.Color.Green,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line
                };
                var series2 = new Series
                {
                    Name = "Series2",
                    Color = System.Drawing.Color.Blue,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line
                };
                var series3 = new Series
                {
                    Name = "Series3",
                    Color = System.Drawing.Color.Red,
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line
                };

                this.chart1.Series.Add(series1);
                this.chart1.Series.Add(series2); 
                //this.chart1.Series.Add(series3);
                fillSums();
                foreach(SumOverTime sum in sumOverTimes)
                {
                    series1.Points.AddXY(sum.Time,sum.Cos);
                    series2.Points.AddXY(sum.Time, sum.Sin);
                    //series3.Points.AddXY(sum.Time, sum.Tan);
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
                this.Text = "FakeChart";
                this.Load += new System.EventHandler(this.Form1_Load);
                ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
                this.ResumeLayout(false);
            }

            /// <summary>
            /// The main entry point for the application.
            /// </summary>
            [STAThread]
            static void Main(string[] args)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FakeChartForm1());
            }
        }
    }
}
