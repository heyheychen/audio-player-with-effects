using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace audioplayer_with_EQ_MBDRC
{
    public class effect_EQ
    {
        public enum Filter_type
        {
            peaking,
            high_shelf,
            low_shelf,
            notch,
            lpf,
            hpf,
            first_lpf,
            first_hpf,
            bypass
        };

        public int EQ_enable;

        public int sampling_rate;
        public int samples_per_frame;
        public int audio_channels;

        //for GUI update
        public int[] EQ_channel = new int[10];
        public int[] fc = new int[10];
        public double[] Q_factor = new double[10];
        public double[] gain = new double[10];
        public Filter_type[] filter_selection = new Filter_type[10];


        public double[][] coef_eq = new double[10][];
        public double[][] filter_out;



        //memory filter temp
        public double[][] num_out = new double[2][];// channels * sample per frame
        public double[][][] x_y_delay_temp = new double[2][][]; //channels* 10 band* x[n-1], x[n-2],  y[n-1], y[n-2] 2*10*4

        public effect_EQ(int _sampling_rate, int _samples_per_frame, int _audio_channels)
        {

            sampling_rate = _sampling_rate;
            samples_per_frame = _samples_per_frame;
            audio_channels = _audio_channels;

            for (int i = 0; i < 10; i++)
            {
                EQ_enable = 1;

                coef_eq[i] = new double[6];
                EQ_channel[i] = i;
                fc[i] = 1000;
                Q_factor[i] = 0.707;
                gain[i] = 0;
                filter_selection[i] = Filter_type.bypass;
            }
  
            //ini coef
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if ((j % 3) == 0)
                        coef_eq[i][j] = 1;
                    else
                        coef_eq[i][j] = 0;
                }
            }
        }



        public void update_coef()
        {
            for (int i = 0; i < 10; i++)
            {
                coef_gen(EQ_channel[i], filter_selection[i], fc[i], gain[i], Q_factor[i]);
            }
        }


        public void init_memory()
        {
            //init memory

            for (int i = 0; i < audio_channels; i++)
            {

                num_out[i] = new double[samples_per_frame];
                x_y_delay_temp[i] = new double[10][];
                for (int j = 0; j < 10; j++)
                {
                    x_y_delay_temp[i][j] = new double[4];

                }
            }
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        x_y_delay_temp[i][j][k] = 0;
                    }
                }
            }
        }

        public void coef_gen(int _EQ_channel_num, Filter_type _filter_type_select, int _fc, double _gain, double _Q_factor)
        {
            double w_0, w_1, alpha, A, b0, b1, b2, a0, a1, a2;

            w_0 = 2 * Math.PI * (double)_fc / sampling_rate;
            A = Math.Pow(10, _gain / 40);
            alpha = Math.Sin(w_0) / (2 * _Q_factor);
            w_1 = Math.Tan(Math.PI * (double)_fc / sampling_rate);

            switch (_filter_type_select)
            {

                case Filter_type.peaking:

                    b0 = 1 + alpha * A;
                    b1 = -2 * Math.Cos(w_0);
                    b2 = 1 - alpha * A;
                    a0 = 1 + alpha / A;
                    a1 = -2 * Math.Cos(w_0);
                    a2 = 1 - alpha / A;

                    coef_eq[_EQ_channel_num][0] = b0 / a0;
                    coef_eq[_EQ_channel_num][1] = b1 / a0;
                    coef_eq[_EQ_channel_num][2] = b2 / a0;
                    coef_eq[_EQ_channel_num][3] = 1;
                    coef_eq[_EQ_channel_num][4] = a1 / a0;
                    coef_eq[_EQ_channel_num][5] = a2 / a0;


                    break;

                case Filter_type.high_shelf:

                    b0 = A * ((A + 1) + (A - 1) * Math.Cos(w_0) + 2 * Math.Sqrt(A) * alpha);
                    b1 = -2 * A * ((A - 1) + (A + 1) * Math.Cos(w_0));
                    b2 = A * ((A + 1) + (A - 1) * Math.Cos(w_0) - 2 * Math.Sqrt(A) * alpha);
                    a0 = A + 1 - (A - 1) * Math.Cos(w_0) + 2 * Math.Sqrt(A) * alpha;
                    a1 = 2 * ((A - 1) - (A + 1) * Math.Cos(w_0));
                    a2 = A + 1 - (A - 1) * Math.Cos(w_0) - 2 * Math.Sqrt(A) * alpha;

                    coef_eq[_EQ_channel_num][0] = b0 / a0;
                    coef_eq[_EQ_channel_num][1] = b1 / a0;
                    coef_eq[_EQ_channel_num][2] = b2 / a0;
                    coef_eq[_EQ_channel_num][3] = 1;
                    coef_eq[_EQ_channel_num][4] = a1 / a0;
                    coef_eq[_EQ_channel_num][5] = a2 / a0;

                    break;

                case Filter_type.low_shelf:
                    w_0 = 2 * Math.PI * _fc / sampling_rate;
                    A = Math.Pow(10, _gain / 40);
                    alpha = Math.Sin(w_0) / (2 * _Q_factor);

                    b0 = A * ((A + 1) - (A - 1) * Math.Cos(w_0) + 2 * Math.Sqrt(A) * alpha);
                    b1 = 2 * A * ((A - 1) - (A + 1) * Math.Cos(w_0));
                    b2 = A * ((A + 1) - (A - 1) * Math.Cos(w_0) - 2 * Math.Sqrt(A) * alpha);
                    a0 = A + 1 + (A - 1) * Math.Cos(w_0) + 2 * Math.Sqrt(A) * alpha;
                    a1 = -2 * ((A - 1) + (A + 1) * Math.Cos(w_0));
                    a2 = A + 1 + (A - 1) * Math.Cos(w_0) - 2 * Math.Sqrt(A) * alpha;

                    coef_eq[_EQ_channel_num][0] = b0 / a0;
                    coef_eq[_EQ_channel_num][1] = b1 / a0;
                    coef_eq[_EQ_channel_num][2] = b2 / a0;
                    coef_eq[_EQ_channel_num][3] = 1;
                    coef_eq[_EQ_channel_num][4] = a1 / a0;
                    coef_eq[_EQ_channel_num][5] = a2 / a0;

                    break;

                case Filter_type.notch:
                    b0 = 1;
                    b1 = -2 * Math.Cos(w_0);
                    b2 = 1;
                    a0 = 1 + alpha;
                    a1 = -2 * Math.Cos(w_0);
                    a2 = 1 - alpha;

                    coef_eq[_EQ_channel_num][0] = b0 / a0;
                    coef_eq[_EQ_channel_num][1] = b1 / a0;
                    coef_eq[_EQ_channel_num][2] = b2 / a0;
                    coef_eq[_EQ_channel_num][3] = 1;
                    coef_eq[_EQ_channel_num][4] = a1 / a0;
                    coef_eq[_EQ_channel_num][5] = a2 / a0;

                    break;

                case Filter_type.lpf:
                    b0 = (1 - Math.Cos(w_0)) / 2;
                    b1 = 1 - Math.Cos(w_0);
                    b2 = (1 - Math.Cos(w_0)) / 2;
                    a0 = 1 + alpha;
                    a1 = -2 * Math.Cos(w_0);
                    a2 = 1 - alpha;

                    coef_eq[_EQ_channel_num][0] = b0 / a0;
                    coef_eq[_EQ_channel_num][1] = b1 / a0;
                    coef_eq[_EQ_channel_num][2] = b2 / a0;
                    coef_eq[_EQ_channel_num][3] = 1;
                    coef_eq[_EQ_channel_num][4] = a1 / a0;
                    coef_eq[_EQ_channel_num][5] = a2 / a0;

                    break;

                case Filter_type.hpf:
                    b0 = (1 + Math.Cos(w_0)) / 2;
                    b1 = -(1 + Math.Cos(w_0));
                    b2 = (1 + Math.Cos(w_0)) / 2;
                    a0 = 1 + alpha;
                    a1 = -2 * Math.Cos(w_0);
                    a2 = 1 - alpha;

                    coef_eq[_EQ_channel_num][0] = b0 / a0;
                    coef_eq[_EQ_channel_num][1] = b1 / a0;
                    coef_eq[_EQ_channel_num][2] = b2 / a0;
                    coef_eq[_EQ_channel_num][3] = 1;
                    coef_eq[_EQ_channel_num][4] = a1 / a0;
                    coef_eq[_EQ_channel_num][5] = a2 / a0;

                    break;

                case Filter_type.first_lpf:
                    b0 = w_1 / (1 + w_1);
                    b1 = b0;
                    b2 = 0;
                    a0 = 1;
                    a1 = (w_1 - 1) / (w_1 + 1);
                    a2 = 0;

                    coef_eq[_EQ_channel_num][0] = b0 / a0;
                    coef_eq[_EQ_channel_num][1] = b1 / a0;
                    coef_eq[_EQ_channel_num][2] = b2 / a0;
                    coef_eq[_EQ_channel_num][3] = 1;
                    coef_eq[_EQ_channel_num][4] = a1 / a0;
                    coef_eq[_EQ_channel_num][5] = a2 / a0;

                    break;

                case Filter_type.first_hpf:
                    b0 = (1 + w_1);
                    b1 = -b0;
                    b2 = 0;
                    a0 = 1;
                    a1 = (w_1 - 1) / (w_1 + 1);
                    a2 = 0;

                    coef_eq[_EQ_channel_num][0] = b0 / a0;
                    coef_eq[_EQ_channel_num][1] = b1 / a0;
                    coef_eq[_EQ_channel_num][2] = b2 / a0;
                    coef_eq[_EQ_channel_num][3] = 1;
                    coef_eq[_EQ_channel_num][4] = a1 / a0;
                    coef_eq[_EQ_channel_num][5] = a2 / a0;

                    break;

                default:

                    coef_eq[_EQ_channel_num][0] = 1;
                    coef_eq[_EQ_channel_num][1] = 0;
                    coef_eq[_EQ_channel_num][2] = 0;
                    coef_eq[_EQ_channel_num][3] = 1;
                    coef_eq[_EQ_channel_num][4] = 0;
                    coef_eq[_EQ_channel_num][5] = 0;


                    break;
            }

        }


        private void iir_filtering(double[] _xin, double _coef_b0, double _coef_b1, double _coef_b2, double _coef_a0, double _coef_a1, double _coef_a2, ref double _x_n_1, ref double _x_n_2, ref double _y_n_1, ref double _y_n_2, double[] _yout)
        {
            double[] xin_temp = new double[samples_per_frame];
            double[] yout_temp = new double[samples_per_frame];

            for (int i = 0; i < samples_per_frame; i++)
            {
                xin_temp[i] = _xin[i];
                yout_temp[i] = 0;
            }
            for (int i = 0; i < samples_per_frame; i++)
            {

                yout_temp[i] = (xin_temp[i] * _coef_b0) + (_x_n_1 * _coef_b1) + (_x_n_2 * _coef_b2)
                                          - (_y_n_1 * _coef_a1) - (_y_n_2 * _coef_a2);

                /*
                if (yout_temp[i] > 1)
                {
                    yout_temp[i] = 1;
                }
                else if (yout_temp[i] < -1)
                {
                    yout_temp[i] = -1;
                }
                */
                _x_n_2 = _x_n_1;
                _x_n_1 = xin_temp[i];
                _y_n_2 = _y_n_1;
                _y_n_1 = yout_temp[i];

            }
            for (int i = 0; i < samples_per_frame; i++)
            {
                _yout[i] = yout_temp[i];
            }
        }

        public void eq_filtering(double[][] _xin)
        {

            //buffer new
            filter_out = new double[audio_channels][];
            double[][] filter_out_temp = new double[audio_channels][];

            for (int ch = 0; ch < audio_channels; ch++)
            {
                filter_out[ch] = new double[samples_per_frame];
                filter_out_temp[ch] = new double[samples_per_frame];
            }

            ////filtering
            for (int i = 0; i < audio_channels; i++)
            {
                for (int j = 0; j < 10; j++) ///ten band eq
                {
                    iir_filtering(_xin[i], coef_eq[j][0], coef_eq[j][1], coef_eq[j][2], coef_eq[j][3], coef_eq[j][4], coef_eq[j][5], ref x_y_delay_temp[i][j][0], ref x_y_delay_temp[i][j][1], ref x_y_delay_temp[i][j][2], ref x_y_delay_temp[i][j][3], filter_out_temp[i]);
                    for (int k = 0; k < samples_per_frame; k++) ///temp
                    {
                        _xin[i][k] = filter_out_temp[i][k];
                    }
                }
             }

            //let temp data to ouput
            for (int i = 0; i < audio_channels; i++)
            {
                for (int j = 0; j < samples_per_frame; j++) 
                {
                    filter_out[i][j] = _xin[i][j];
                }
            }

        }
    }
}


             
  




