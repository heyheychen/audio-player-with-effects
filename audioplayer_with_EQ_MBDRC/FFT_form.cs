using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Threading;
using ZedGraph;

namespace audioplayer_with_EQ_MBDRC
{
    public partial class FFT_form : Form
    {
        FFT_class fft_temp;
        GraphPane fft_form_frequency_response;
       // BackgroundWorker bgw_fft_enabled;


        public bool fft_enabled = false;

        private int fft_form_sampling_rate;
        private int fft_form_num_of_sample;
        private double[] fft_x_data ;
        private double[] fft_y_data;

        public FFT_form(FFT_class _real_time_fft)
        {
            InitializeComponent();
            fft_temp = _real_time_fft;
            fft_form_sampling_rate = fft_temp.sampling_rate;
            fft_form_num_of_sample = fft_temp.fft_num_of_sample;
          

            fft_x_data = new double[fft_form_num_of_sample];
            fft_y_data = new double[fft_form_num_of_sample];

        }

        private void FFT_form_Load(object sender, EventArgs e)
        {
            zedgraph_ini(zedGraphControl1);

            //to make new thread 
            Form.CheckForIllegalCrossThreadCalls = false;
            form_window_type.Text = fft_temp.window_type_selection.ToString();

        }


        private void zedgraph_ini(ZedGraphControl _zgc)
        {
            fft_form_frequency_response = _zgc.GraphPane;

            fft_form_frequency_response.Title.Text = "FFT"; //圖表的表頭
            fft_form_frequency_response.Title.FontSpec.Size = 20;
            fft_form_frequency_response.XAxis.Title.Text = "frequency (Hz)"; //X軸的名稱
            fft_form_frequency_response.XAxis.Title.FontSpec.Size = 16;
            fft_form_frequency_response.XAxis.Type = ZedGraph.AxisType.Log;


            fft_form_frequency_response.YAxis.Title.Text = "Amplitude (dB)"; //Y軸的名稱
            fft_form_frequency_response.YAxis.Title.FontSpec.Size = 16;
            fft_form_frequency_response.YAxis.Scale.MajorStep = 5;

            fft_form_frequency_response.AxisChange();

            fft_form_frequency_response.XAxis.Scale.Min = 20;
            fft_form_frequency_response.XAxis.Scale.Max = 20000;
            fft_form_frequency_response.YAxis.Scale.Min = -60;
            fft_form_frequency_response.YAxis.Scale.Max = 100;
        }
        private void gen_plot_data()
        {
           
            for (int i = 0; i < fft_form_num_of_sample; i++)
            {
                fft_x_data[i] = (double)fft_form_sampling_rate / (double)fft_form_num_of_sample * i;
                fft_y_data[i] = fft_temp.fft_mag_out[i];
            }
        }

        public void fft_plot()
        {      
            gen_plot_data();
         

            fft_form_frequency_response.CurveList.Clear();
            fft_form_frequency_response.GraphObjList.Clear();

            double x, y;
            PointPairList list1 = new PointPairList();
            for (int i = 0; i < fft_x_data.Length; i++)
            {
                x = fft_x_data[i];
                y = fft_y_data[i];

                list1.Add(x, y);
            }
        
            //frequency_response.LineType.;
            LineItem myCurve = fft_form_frequency_response.AddCurve(null, list1, Color.Blue, SymbolType.None);
           
            this.Refresh();
        
        }

            private void button1_Click(object sender, EventArgs e)
        {
            if (fft_enabled == true)
            {
                fft_enabled = false;
            }
            else
            {
                fft_enabled = true;
            }


            new Thread(() =>
            {
                for (int i = 0; i < 100; i++)
                {

                    while (fft_enabled == true)
                    {
                        //when fft sample are full -> plot
                        if (fft_temp.plot_flag == true)
                        { 
                            fft_plot();
                            Thread.Sleep(1);
                        }
                    }
                }
            }).Start();


        }

        private void form_window_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            fft_temp.window_type_selection = (FFT_class.fft_window_type)Enum.Parse(typeof(FFT_class.fft_window_type), form_window_type.SelectedIndex.ToString());
            
        }

   
    }
}
