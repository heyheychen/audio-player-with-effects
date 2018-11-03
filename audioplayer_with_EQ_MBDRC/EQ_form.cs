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
using ZedGraph;



namespace audioplayer_with_EQ_MBDRC
{   

    public partial class EQ_form : Form
    {
        effect_EQ equalizer_temp;
        GraphPane frequency_response;

        //for plot
        private int EQ_form_sampling_rate;
        private int EQ_form_NFFT_length;
        private double[] x_data;
        private double[] y_data;
        private double poly_abs_value;
        private Complex poly_num, poly_den, poly_total;
        private int[] UI_EQ_channel = new int[10];
        private int[] UI_fc = new int[10];
        private double[] UI_Q_factor = new double[10];
        private double[] UI_gain = new double[10];
        private effect_EQ.Filter_type[] UI_filter_selection = new effect_EQ.Filter_type[10];
        private double[][] UI_coef_eq = new double[10][];



        public EQ_form(effect_EQ _equalizer)
        { 
            InitializeComponent();
            equalizer_temp = _equalizer;
            EQ_form_NFFT_length = 65536;
            EQ_form_sampling_rate = 48000;   
        }

        

        private void EQ_form_Load(object sender, EventArgs e)
        {

        
            zedgraph_ini(zedGraphControl1);
            

            filter_selection1.Text = equalizer_temp.filter_selection[0].ToString();
            filter_selection2.Text = equalizer_temp.filter_selection[1].ToString();
            filter_selection3.Text = equalizer_temp.filter_selection[2].ToString();
            filter_selection4.Text = equalizer_temp.filter_selection[3].ToString();
            filter_selection5.Text = equalizer_temp.filter_selection[4].ToString();
            filter_selection6.Text = equalizer_temp.filter_selection[5].ToString();
            filter_selection7.Text = equalizer_temp.filter_selection[6].ToString();
            filter_selection8.Text = equalizer_temp.filter_selection[7].ToString();
            filter_selection9.Text = equalizer_temp.filter_selection[8].ToString();
            filter_selection10.Text = equalizer_temp.filter_selection[9].ToString();

            fc1.Text = equalizer_temp.fc[0].ToString();
            fc2.Text = equalizer_temp.fc[1].ToString();
            fc3.Text = equalizer_temp.fc[2].ToString();
            fc4.Text = equalizer_temp.fc[3].ToString();
            fc5.Text = equalizer_temp.fc[4].ToString();
            fc6.Text = equalizer_temp.fc[5].ToString();
            fc7.Text = equalizer_temp.fc[6].ToString();
            fc8.Text = equalizer_temp.fc[7].ToString();
            fc9.Text = equalizer_temp.fc[8].ToString();
            fc10.Text = equalizer_temp.fc[9].ToString();

            gain1.Text = equalizer_temp.gain[0].ToString();
            gain2.Text = equalizer_temp.gain[1].ToString();
            gain3.Text = equalizer_temp.gain[2].ToString();
            gain4.Text = equalizer_temp.gain[3].ToString();
            gain5.Text = equalizer_temp.gain[4].ToString();
            gain6.Text = equalizer_temp.gain[5].ToString();
            gain7.Text = equalizer_temp.gain[6].ToString();
            gain8.Text = equalizer_temp.gain[7].ToString();
            gain9.Text = equalizer_temp.gain[8].ToString();
            gain10.Text = equalizer_temp.gain[9].ToString();


            Q_factor1.Text = equalizer_temp.Q_factor[0].ToString();
            Q_factor2.Text = equalizer_temp.Q_factor[1].ToString();
            Q_factor3.Text = equalizer_temp.Q_factor[2].ToString();
            Q_factor4.Text = equalizer_temp.Q_factor[3].ToString();
            Q_factor5.Text = equalizer_temp.Q_factor[4].ToString();
            Q_factor6.Text = equalizer_temp.Q_factor[5].ToString();
            Q_factor7.Text = equalizer_temp.Q_factor[6].ToString();
            Q_factor8.Text = equalizer_temp.Q_factor[7].ToString();
            Q_factor9.Text = equalizer_temp.Q_factor[8].ToString();
            Q_factor10.Text = equalizer_temp.Q_factor[9].ToString();

            frequency_response_plot();
        }

        private void EQ_coef_apply_Click(object sender, EventArgs e)
        {

            equalizer_temp.filter_selection[0] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection1.SelectedIndex.ToString());
            equalizer_temp.filter_selection[1] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection2.SelectedIndex.ToString());
            equalizer_temp.filter_selection[2] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection3.SelectedIndex.ToString());
            equalizer_temp.filter_selection[3] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection4.SelectedIndex.ToString());
            equalizer_temp.filter_selection[4] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection5.SelectedIndex.ToString());
            equalizer_temp.filter_selection[5] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection6.SelectedIndex.ToString());
            equalizer_temp.filter_selection[6] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection7.SelectedIndex.ToString());
            equalizer_temp.filter_selection[7] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection8.SelectedIndex.ToString());
            equalizer_temp.filter_selection[8] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection9.SelectedIndex.ToString());
            equalizer_temp.filter_selection[9] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection10.SelectedIndex.ToString());

            

            equalizer_temp.gain[0] = double.Parse(gain1.Text);
            equalizer_temp.gain[1] = double.Parse(gain2.Text);
            equalizer_temp.gain[2] = double.Parse(gain3.Text);
            equalizer_temp.gain[3] = double.Parse(gain4.Text);
            equalizer_temp.gain[4] = double.Parse(gain5.Text);
            equalizer_temp.gain[5] = double.Parse(gain6.Text);
            equalizer_temp.gain[6] = double.Parse(gain7.Text);
            equalizer_temp.gain[7] = double.Parse(gain8.Text);
            equalizer_temp.gain[8] = double.Parse(gain9.Text);
            equalizer_temp.gain[9] = double.Parse(gain10.Text);

      

            equalizer_temp.fc[0] = int.Parse(fc1.Text);
            equalizer_temp.fc[1] = int.Parse(fc2.Text);
            equalizer_temp.fc[2] = int.Parse(fc3.Text);
            equalizer_temp.fc[3] = int.Parse(fc4.Text);
            equalizer_temp.fc[4] = int.Parse(fc5.Text);
            equalizer_temp.fc[5] = int.Parse(fc6.Text);
            equalizer_temp.fc[6] = int.Parse(fc7.Text);
            equalizer_temp.fc[7] = int.Parse(fc8.Text);
            equalizer_temp.fc[8] = int.Parse(fc9.Text);
            equalizer_temp.fc[9] = int.Parse(fc10.Text);

     

            equalizer_temp.Q_factor[0] = double.Parse(Q_factor1.Text);
            equalizer_temp.Q_factor[1] = double.Parse(Q_factor2.Text);
            equalizer_temp.Q_factor[2] = double.Parse(Q_factor3.Text);
            equalizer_temp.Q_factor[3] = double.Parse(Q_factor4.Text);
            equalizer_temp.Q_factor[4] = double.Parse(Q_factor5.Text);
            equalizer_temp.Q_factor[5] = double.Parse(Q_factor6.Text);
            equalizer_temp.Q_factor[6] = double.Parse(Q_factor7.Text);
            equalizer_temp.Q_factor[7] = double.Parse(Q_factor8.Text);
            equalizer_temp.Q_factor[8] = double.Parse(Q_factor9.Text);
            equalizer_temp.Q_factor[9] = double.Parse(Q_factor10.Text);

            //gen_UI_EQ_coef();
            equalizer_temp.update_coef();

        }
        


        private void zedgraph_ini(ZedGraphControl _zgc)
        {
            frequency_response = _zgc.GraphPane;
            
            frequency_response.Title.Text = "frequency response"; //圖表的表頭
            frequency_response.Title.FontSpec.Size = 20;
            frequency_response.XAxis.Title.Text = "frequency (Hz)"; //X軸的名稱
            frequency_response.XAxis.Title.FontSpec.Size = 16;
            frequency_response.XAxis.Type = ZedGraph.AxisType.Log;
    

            frequency_response.YAxis.Title.Text = "Amplitude (dB)"; //Y軸的名稱
            frequency_response.YAxis.Title.FontSpec.Size = 16;   
            frequency_response.YAxis.Scale.MajorStep = 5;
         
            frequency_response.AxisChange();

            frequency_response.XAxis.Scale.Min = 20;
            frequency_response.XAxis.Scale.Max = 20000;
            frequency_response.YAxis.Scale.Min = -15;
            frequency_response.YAxis.Scale.Max = 15;
  
        }


        private void gen_UI_EQ_coef()
        {
            UI_filter_selection[0] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection1.SelectedIndex.ToString());
            UI_filter_selection[1] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection2.SelectedIndex.ToString());
            UI_filter_selection[2] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection3.SelectedIndex.ToString());
            UI_filter_selection[3] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection4.SelectedIndex.ToString());
            UI_filter_selection[4] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection5.SelectedIndex.ToString());
            UI_filter_selection[5] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection6.SelectedIndex.ToString());
            UI_filter_selection[6] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection7.SelectedIndex.ToString());
            UI_filter_selection[7] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection8.SelectedIndex.ToString());
            UI_filter_selection[8] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection9.SelectedIndex.ToString());
            UI_filter_selection[9] = (effect_EQ.Filter_type)Enum.Parse(typeof(effect_EQ.Filter_type), filter_selection10.SelectedIndex.ToString());



            UI_gain[0] = double.Parse(gain1.Text);
            UI_gain[1] = double.Parse(gain2.Text);
            UI_gain[2] = double.Parse(gain3.Text);
            UI_gain[3] = double.Parse(gain4.Text);
            UI_gain[4] = double.Parse(gain5.Text);
            UI_gain[5] = double.Parse(gain6.Text);
            UI_gain[6] = double.Parse(gain7.Text);
            UI_gain[7] = double.Parse(gain8.Text);
            UI_gain[8] = double.Parse(gain9.Text);
            UI_gain[9] = double.Parse(gain10.Text);



            UI_fc[0] = int.Parse(fc1.Text);
            UI_fc[1] = int.Parse(fc2.Text);
            UI_fc[2] = int.Parse(fc3.Text);
            UI_fc[3] = int.Parse(fc4.Text);
            UI_fc[4] = int.Parse(fc5.Text);
            UI_fc[5] = int.Parse(fc6.Text);
            UI_fc[6] = int.Parse(fc7.Text);
            UI_fc[7] = int.Parse(fc8.Text);
            UI_fc[8] = int.Parse(fc9.Text);
            UI_fc[9] = int.Parse(fc10.Text);



            UI_Q_factor[0] = double.Parse(Q_factor1.Text);
            UI_Q_factor[1] = double.Parse(Q_factor2.Text);
            UI_Q_factor[2] = double.Parse(Q_factor3.Text);
            UI_Q_factor[3] = double.Parse(Q_factor4.Text);
            UI_Q_factor[4] = double.Parse(Q_factor5.Text);
            UI_Q_factor[5] = double.Parse(Q_factor6.Text);
            UI_Q_factor[6] = double.Parse(Q_factor7.Text);
            UI_Q_factor[7] = double.Parse(Q_factor8.Text);
            UI_Q_factor[8] = double.Parse(Q_factor9.Text);
            UI_Q_factor[9] = double.Parse(Q_factor10.Text);

            for (int i = 0; i < 10; i++)
            {
                UI_coef_eq[i] = new double[6];
                coef_gen(i, UI_filter_selection[i], UI_fc[i],UI_gain[i],UI_Q_factor[i]);
                //MessageBox.Show("i=" + i.ToString() + "   "+ UI_coef_eq[i][0].ToString("0.00") + "   " + UI_coef_eq[i][1].ToString("0.00") + "   " + UI_coef_eq[i][2].ToString("0.00") + "   " + UI_coef_eq[i][3].ToString("0.00") + "   " + UI_coef_eq[i][4].ToString("0.00") + "   " + UI_coef_eq[i][5].ToString("0.00") );
            }
            
        }
        
        private void frequency_response_plot()
        {
            gen_UI_EQ_coef();
            UI_plot_data_gen();

            frequency_response.CurveList.Clear();
            frequency_response.GraphObjList.Clear();

            double x, y;
            PointPairList list1 = new PointPairList();
            for (int i = 0; i < x_data.Length; i++)
            {
                x = x_data[i];
                y = y_data[i];

                list1.Add(x, y);
            }
            //frequency_response.LineType.;
            LineItem myCurve = frequency_response.AddCurve(null, list1, Color.Blue, SymbolType.None);

            this.Refresh();
        }

        private void filter_selection1_SelectedIndexChanged(object sender, EventArgs e)
        {
            frequency_response_plot();
        }

        private void filter_selection2_SelectedIndexChanged(object sender, EventArgs e)
        {
            frequency_response_plot();
        }

        private void filter_selection3_SelectedIndexChanged(object sender, EventArgs e)
        {
            frequency_response_plot();
        }

        private void filter_selection4_SelectedIndexChanged(object sender, EventArgs e)
        {
            frequency_response_plot();
        }

        private void filter_selection5_SelectedIndexChanged(object sender, EventArgs e)
        {
            frequency_response_plot();
        }

        private void filter_selection6_SelectedIndexChanged(object sender, EventArgs e)
        {
            frequency_response_plot();
        }

        private void filter_selection7_SelectedIndexChanged(object sender, EventArgs e)
        {
            frequency_response_plot();
        }

        private void filter_selection8_SelectedIndexChanged(object sender, EventArgs e)
        {
            frequency_response_plot();
        }

        private void filter_selection9_SelectedIndexChanged(object sender, EventArgs e)
        {
            frequency_response_plot();
        }

        private void filter_selection10_SelectedIndexChanged(object sender, EventArgs e)
        {
            frequency_response_plot();
        }

        private void all_parameter_key_down(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                frequency_response_plot();
            }
        }

        private void all_button_leave_focus(object sender, EventArgs e)
        {
            frequency_response_plot();
        }

       

        private void coef_gen(int _EQ_channel_num, effect_EQ.Filter_type _filter_type_select, int _fc, double _gain, double _Q_factor)
        {
            double w_0, w_1, alpha, A, b0, b1, b2, a0, a1, a2;

            w_0 = 2 * Math.PI * (double)_fc / EQ_form_sampling_rate;
            A = Math.Pow(10, _gain / 40);
            alpha = Math.Sin(w_0) / (2 * _Q_factor);
            w_1 = Math.Tan(Math.PI * (double)_fc / EQ_form_sampling_rate);

            switch (_filter_type_select)
            {

                case effect_EQ.Filter_type.peaking:

                    b0 = 1 + alpha * A;
                    b1 = -2 * Math.Cos(w_0);
                    b2 = 1 - alpha * A;
                    a0 = 1 + alpha / A;
                    a1 = -2 * Math.Cos(w_0);
                    a2 = 1 - alpha / A;

                    UI_coef_eq[_EQ_channel_num][0] = b0 / a0;
                    UI_coef_eq[_EQ_channel_num][1] = b1 / a0;
                    UI_coef_eq[_EQ_channel_num][2] = b2 / a0;
                    UI_coef_eq[_EQ_channel_num][3] = 1;
                    UI_coef_eq[_EQ_channel_num][4] = a1 / a0;
                    UI_coef_eq[_EQ_channel_num][5] = a2 / a0;


                    break;

                case effect_EQ.Filter_type.high_shelf:

                    b0 = A * ((A + 1) + (A - 1) * Math.Cos(w_0) + 2 * Math.Sqrt(A) * alpha);
                    b1 = -2 * A * ((A - 1) + (A + 1) * Math.Cos(w_0));
                    b2 = A * ((A + 1) + (A - 1) * Math.Cos(w_0) - 2 * Math.Sqrt(A) * alpha);
                    a0 = A + 1 - (A - 1) * Math.Cos(w_0) + 2 * Math.Sqrt(A) * alpha;
                    a1 = 2 * ((A - 1) - (A + 1) * Math.Cos(w_0));
                    a2 = A + 1 - (A - 1) * Math.Cos(w_0) - 2 * Math.Sqrt(A) * alpha;

                    UI_coef_eq[_EQ_channel_num][0] = b0 / a0;
                    UI_coef_eq[_EQ_channel_num][1] = b1 / a0;
                    UI_coef_eq[_EQ_channel_num][2] = b2 / a0;
                    UI_coef_eq[_EQ_channel_num][3] = 1;
                    UI_coef_eq[_EQ_channel_num][4] = a1 / a0;
                    UI_coef_eq[_EQ_channel_num][5] = a2 / a0;

                    break;

                case effect_EQ.Filter_type.low_shelf:
                    w_0 = 2 * Math.PI * _fc / EQ_form_sampling_rate;
                    A = Math.Pow(10, _gain / 40);
                    alpha = Math.Sin(w_0) / (2 * _Q_factor);

                    b0 = A * ((A + 1) - (A - 1) * Math.Cos(w_0) + 2 * Math.Sqrt(A) * alpha);
                    b1 = 2 * A * ((A - 1) - (A + 1) * Math.Cos(w_0));
                    b2 = A * ((A + 1) - (A - 1) * Math.Cos(w_0) - 2 * Math.Sqrt(A) * alpha);
                    a0 = A + 1 + (A - 1) * Math.Cos(w_0) + 2 * Math.Sqrt(A) * alpha;
                    a1 = -2 * ((A - 1) + (A + 1) * Math.Cos(w_0));
                    a2 = A + 1 + (A - 1) * Math.Cos(w_0) - 2 * Math.Sqrt(A) * alpha;

                    UI_coef_eq[_EQ_channel_num][0] = b0 / a0;
                    UI_coef_eq[_EQ_channel_num][1] = b1 / a0;
                    UI_coef_eq[_EQ_channel_num][2] = b2 / a0;
                    UI_coef_eq[_EQ_channel_num][3] = 1;
                    UI_coef_eq[_EQ_channel_num][4] = a1 / a0;
                    UI_coef_eq[_EQ_channel_num][5] = a2 / a0;

                    break;

                case effect_EQ.Filter_type.notch:
                    b0 = 1;
                    b1 = -2 * Math.Cos(w_0);
                    b2 = 1;
                    a0 = 1 + alpha;
                    a1 = -2 * Math.Cos(w_0);
                    a2 = 1 - alpha;

                    UI_coef_eq[_EQ_channel_num][0] = b0 / a0;
                    UI_coef_eq[_EQ_channel_num][1] = b1 / a0;
                    UI_coef_eq[_EQ_channel_num][2] = b2 / a0;
                    UI_coef_eq[_EQ_channel_num][3] = 1;
                    UI_coef_eq[_EQ_channel_num][4] = a1 / a0;
                    UI_coef_eq[_EQ_channel_num][5] = a2 / a0;

                    break;

                case effect_EQ.Filter_type.lpf:
                    b0 = (1 - Math.Cos(w_0)) / 2;
                    b1 = 1 - Math.Cos(w_0);
                    b2 = (1 - Math.Cos(w_0)) / 2;
                    a0 = 1 + alpha;
                    a1 = -2 * Math.Cos(w_0);
                    a2 = 1 - alpha;

                    UI_coef_eq[_EQ_channel_num][0] = b0 / a0;
                    UI_coef_eq[_EQ_channel_num][1] = b1 / a0;
                    UI_coef_eq[_EQ_channel_num][2] = b2 / a0;
                    UI_coef_eq[_EQ_channel_num][3] = 1;
                    UI_coef_eq[_EQ_channel_num][4] = a1 / a0;
                    UI_coef_eq[_EQ_channel_num][5] = a2 / a0;

                    break;

                case effect_EQ.Filter_type.hpf:
                    b0 = (1 + Math.Cos(w_0)) / 2;
                    b1 = -(1 + Math.Cos(w_0));
                    b2 = (1 + Math.Cos(w_0)) / 2;
                    a0 = 1 + alpha;
                    a1 = -2 * Math.Cos(w_0);
                    a2 = 1 - alpha;

                    UI_coef_eq[_EQ_channel_num][0] = b0 / a0;
                    UI_coef_eq[_EQ_channel_num][1] = b1 / a0;
                    UI_coef_eq[_EQ_channel_num][2] = b2 / a0;
                    UI_coef_eq[_EQ_channel_num][3] = 1;
                    UI_coef_eq[_EQ_channel_num][4] = a1 / a0;
                    UI_coef_eq[_EQ_channel_num][5] = a2 / a0;

                    break;

                case effect_EQ.Filter_type.first_lpf:
                    b0 = w_1 / (1 + w_1);
                    b1 = b0;
                    b2 = 0;
                    a0 = 1;
                    a1 = (w_1 - 1) / (w_1 + 1);
                    a2 = 0;

                    UI_coef_eq[_EQ_channel_num][0] = b0 / a0;
                    UI_coef_eq[_EQ_channel_num][1] = b1 / a0;
                    UI_coef_eq[_EQ_channel_num][2] = b2 / a0;
                    UI_coef_eq[_EQ_channel_num][3] = 1;
                    UI_coef_eq[_EQ_channel_num][4] = a1 / a0;
                    UI_coef_eq[_EQ_channel_num][5] = a2 / a0;

                    break;

                case effect_EQ.Filter_type.first_hpf:
                    b0 = (1 + w_1);
                    b1 = -b0;
                    b2 = 0;
                    a0 = 1;
                    a1 = (w_1 - 1) / (w_1 + 1);
                    a2 = 0;

                    UI_coef_eq[_EQ_channel_num][0] = b0 / a0;
                    UI_coef_eq[_EQ_channel_num][1] = b1 / a0;
                    UI_coef_eq[_EQ_channel_num][2] = b2 / a0;
                    UI_coef_eq[_EQ_channel_num][3] = 1;
                    UI_coef_eq[_EQ_channel_num][4] = a1 / a0;
                    UI_coef_eq[_EQ_channel_num][5] = a2 / a0;

                    break;

                default:

                    UI_coef_eq[_EQ_channel_num][0] = 1;
                    UI_coef_eq[_EQ_channel_num][1] = 0;
                    UI_coef_eq[_EQ_channel_num][2] = 0;
                    UI_coef_eq[_EQ_channel_num][3] = 1;
                    UI_coef_eq[_EQ_channel_num][4] = 0;
                    UI_coef_eq[_EQ_channel_num][5] = 0;


                    break;
            }

        }

        private void UI_plot_data_gen()
        {
            x_data = new double[EQ_form_NFFT_length];
            y_data = new double[EQ_form_NFFT_length];
            double[] w = new double[EQ_form_NFFT_length];
            Complex[] z = new Complex[EQ_form_NFFT_length];

            for (int i = 0; i < EQ_form_NFFT_length; i++)
            {
                w[i] = 2 * Math.PI / (EQ_form_NFFT_length) * i;
                z[i] = new Complex(Math.Cos(w[i]), -Math.Sin(w[i]));

                double temp = 1;
                for (int j = 0; j < 10; j++)
                {

                    poly_num = UI_coef_eq[j][0] + UI_coef_eq[j][1] * z[i] + UI_coef_eq[j][2] * z[i] * z[i];
                    poly_den = UI_coef_eq[j][3] + UI_coef_eq[j][4] * z[i] + UI_coef_eq[j][5] * z[i] * z[i];
                    poly_total = poly_num / poly_den;
                    poly_abs_value = Complex.Abs(poly_total);
                    temp *= poly_abs_value;

                }
                y_data[i] = 20 * Math.Log10(temp);
                x_data[i] = w[i] * EQ_form_sampling_rate / (2 * Math.PI);

            }
        }
    }
}
