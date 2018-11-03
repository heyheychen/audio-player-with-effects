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
    public class Effect_3band_DRC
    {
        public bool DRC_enable;

        public int sampling_rate;
        public int sample_per_frame;
        public int num_of_channel;

        //DRC parameter
        public int Num_of_band;
        public int[] splipt_frequency;
        public int[] peak_mode;
        public double[] alpha; // 1-exp(-2.2/fs/(10(ms)*0.001)  
        public double[] omega;
        public double[] attack;
        public double[] release;
        public double[] threshold;
        public double[] ratio;
        public double[] makeup;

        private double[] LR_filter_coeficient_f1_LP;
        private double[] LR_filter_coeficient_f1_HP;
        private double[] LR_filter_coeficient_f2_LP;
        private double[] LR_filter_coeficient_f2_HP;

        //memory
        public double[][][] data_buffer;  //  5 band(xin, 3band, output) * channel * sample per frame
        public double[][][][] x_y_delay_temp; //3 band() * 4 (iir * 2* high, low band)  * 2 channel* 4 (x(n-1),x(n-2),y(n-1),y(n-2)) 
        private double[][] prev_xin; // 3 band * 2 channel
        private double[][] prev_gain; // 3 band * 2 channel
        public double[][][] gain_buffer; // 3 band * 2 channel * sample per framde


        //debug temp

        public double[][][] current_xin_level = new double[3][][];
       
        ///constructor
        public Effect_3band_DRC()
        {
         sampling_rate = 44100;
         sample_per_frame = 3969;
         num_of_channel = 2;
         ini_parameter();
         ini_memory();
        }
       
        public void ini_parameter()
        {
            peak_mode = new int[3] { 1,1,1};
            Num_of_band = 3;
            DRC_enable = false;

            alpha = new double[3] { 1 - Math.Exp(-2.2 / sampling_rate / (20 * 0.001)), 1 - Math.Exp(-2.2 / sampling_rate / (10 * 0.001)), 1 - Math.Exp(-2.2 / sampling_rate / (10* 0.001)) };
            omega = new double[3] { 1 - Math.Exp(-2.2 / sampling_rate / (100 * 0.001)), 1 - Math.Exp(-2.2 / sampling_rate / (100 * 0.001)), 1 - Math.Exp(-2.2 / sampling_rate / (100 * 0.001)) };
            attack = new double[3] { 1 - Math.Exp(-2.2 / sampling_rate / (20 * 0.001)), 1 - Math.Exp(-2.2 / sampling_rate / (10 * 0.001)), 1 - Math.Exp(-2.2 / sampling_rate / (10 * 0.001)) };
            release = new double[3] { 1 - Math.Exp(-2.2 / sampling_rate / (1000 * 0.001)), 1 - Math.Exp(-2.2 / sampling_rate / (1000 * 0.001)), 1 - Math.Exp(-2.2 / sampling_rate / (1000 * 0.001)) };
            threshold = new double[3] { -20* Math.Log(10, 2) / 20, -15* Math.Log(10, 2) / 20, -10* Math.Log(10, 2) / 20 };//dB = -10,  -10*log2(10)/20 
                                                                                                                           
            ratio = new double[3] {0, 0.25, 0.5 };// slope;
            makeup = new double[3] { 6 * Math.Log(10, 2) / 20, 3* Math.Log(10, 2) / 20, 3 * Math.Log(10, 2) / 20 };//dB = 6,  6*log2(10)/20


            splipt_frequency = new int[2] { 300, 3000 };
            LR_filter_coeficient_f1_LP = new double[6];
            LR_filter_coeficient_f1_HP = new double[6];
            LR_filter_coeficient_f2_LP = new double[6];
            LR_filter_coeficient_f2_HP = new double[6];
            gen_LR_filter_coefficient();
        }


        public void ini_memory()
        {
            for (int i = 0; i < 3; i++)
            {
                current_xin_level[i] = new double[2][];
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    current_xin_level[i][j] = new double[3969];
                }
            }

            //data_buffer
            data_buffer = new double[5][][];
            for (int i = 0; i < 5; i++)
            {
                data_buffer[i] =  new double[num_of_channel][];;
  
                for (int j = 0; j < num_of_channel; j++)
                {
                    data_buffer[i][j] = new double[sample_per_frame];

                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < num_of_channel; j++)
                {
                    for (int k = 0; k < sample_per_frame; k++)
                    {
                        data_buffer[i][j][k] = 0;
                    }
                }
            }


            //x_y_delay_temp
            x_y_delay_temp = new double[3][][][];
            for (int i = 0; i < 3; i++)
            {
                x_y_delay_temp[i] = new double[4][][]; ;

                for (int j = 0; j < 4; j++)
                {
                    x_y_delay_temp[i][j] = new double[num_of_channel][];
                        for (int k = 0; k < num_of_channel; k++)
                        {
                        x_y_delay_temp[i][j][k] = new double[4];
                        }
                 }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < num_of_channel; k++)
                    {
                        for (int l = 0; l < 4; l++)
                        {
                            x_y_delay_temp[i][j][k][l] = 0;
                        }
                    }
                }
            }


            //prev_xin
            prev_xin = new double[3][];
            for (int i = 0; i < 3; i++)
            {
                prev_xin[i] = new double[num_of_channel]; ;

            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < num_of_channel; j++)
                {
                    prev_xin[i][j] = 0;
                }
            }


            //prev_gain
            prev_gain = new double[3][];
            for (int i = 0; i < 3; i++)
            {
                prev_gain[i] = new double[num_of_channel]; ;

            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < num_of_channel; j++)
                {
                    prev_gain[i][j] = 0;
                }
            }

            //gain buffer
            gain_buffer = new double[3][][];
            for (int i = 0; i < 3; i++)
            {
                gain_buffer[i] = new double[num_of_channel][];
                    for (int j = 0; j < num_of_channel; j++)
                    {
                        gain_buffer[i][j] = new double[sample_per_frame];
                    }

                }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < num_of_channel; j++)
                {
                    for (int k = 0; k < sample_per_frame; k++)
                    {
                        gain_buffer[i][j][k] = 1;
                    }
                }
            }
        }


        public void update_music_information(int _sampling_rate, int _sample_per_frame, int _num_of_channel)
        {


            double[] alpha_temp = new double[3];
            double[] omega_temp = new double[3];
            double[] attack_temp = new double[3];
            double[] release_temp = new double[3];
            ///record prev parameters
            for (int i = 0; i < 3; i++)
            {
                alpha_temp[i] = Math.Round(1 / (Math.Log(1 - alpha[i]) * 0.001 * sampling_rate / -2.2), 1);
                omega_temp[i] = Math.Round(1 / (Math.Log(1 - omega[i]) * 0.001 * sampling_rate / -2.2), 1);
                attack_temp[i] = Math.Round(1 / (Math.Log(1 - attack[i]) * 0.001 * sampling_rate / -2.2), 1);
                release_temp[i] = Math.Round(1 / (Math.Log(1 - release[i]) * 0.001 * sampling_rate / -2.2), 1);

             }
            sampling_rate = _sampling_rate;
            sample_per_frame = _sample_per_frame;
            num_of_channel = _num_of_channel;
            //update parameters
            for (int i = 0; i < 3; i++)
            {
                alpha[i] = 1 - Math.Exp(-2.2 / sampling_rate / (alpha_temp[i] * 0.001));
                omega[i] = 1 - Math.Exp(-2.2 / sampling_rate / (omega_temp[i] * 0.001));
                attack[i] = 1 - Math.Exp(-2.2 / sampling_rate / (attack_temp[i] * 0.001));
                release[i] = 1 - Math.Exp(-2.2 / sampling_rate / (release_temp[i] * 0.001));
            }


            gen_LR_filter_coefficient();

        }


        public void gen_LR_filter_coefficient()
        {
            double omegac_d_f1, omegac_a_f1, omegac_d_f2, omegac_a_f2;

            omegac_d_f1 = (double)splipt_frequency[0] / (double)sampling_rate * 2 * Math.PI;
            omegac_a_f1 = Math.Tan(omegac_d_f1 / 2);
            double den_f1_temp = (1 + (Math.Sqrt(2) * omegac_a_f1) + Math.Pow(omegac_a_f1, 2));

            LR_filter_coeficient_f1_LP[0] = Math.Pow(omegac_a_f1, 2) / den_f1_temp;
            LR_filter_coeficient_f1_LP[1] = 2 * Math.Pow(omegac_a_f1, 2) / den_f1_temp;
            LR_filter_coeficient_f1_LP[2] = Math.Pow(omegac_a_f1, 2) / den_f1_temp;
            LR_filter_coeficient_f1_LP[3] = 1;
            LR_filter_coeficient_f1_LP[4] = (-2 + 2 * Math.Pow(omegac_a_f1, 2)) / den_f1_temp;
            LR_filter_coeficient_f1_LP[5] = (1 - Math.Sqrt(2) * omegac_a_f1 + Math.Pow(omegac_a_f1, 2)) / den_f1_temp;

            LR_filter_coeficient_f1_HP[0] = 1 / den_f1_temp;
            LR_filter_coeficient_f1_HP[1] = -2 / den_f1_temp;
            LR_filter_coeficient_f1_HP[2] = 1 / den_f1_temp;
            LR_filter_coeficient_f1_HP[3] = 1;
            LR_filter_coeficient_f1_HP[4] = (-2 + 2 * Math.Pow(omegac_a_f1, 2)) / den_f1_temp;
            LR_filter_coeficient_f1_HP[5] = (1 - Math.Sqrt(2) * omegac_a_f1 + Math.Pow(omegac_a_f1, 2)) / den_f1_temp;



            omegac_d_f2 = (double)splipt_frequency[1] / (double)sampling_rate * 2 * Math.PI;
            omegac_a_f2 = Math.Tan(omegac_d_f2 / 2);
            double den_f2_temp = (1 + (Math.Sqrt(2) * omegac_a_f2) + Math.Pow(omegac_a_f2, 2));


            LR_filter_coeficient_f2_LP[0] = Math.Pow(omegac_a_f2, 2) / den_f2_temp;
            LR_filter_coeficient_f2_LP[1] = 2 * Math.Pow(omegac_a_f2, 2) / den_f2_temp;
            LR_filter_coeficient_f2_LP[2] = Math.Pow(omegac_a_f2, 2) / den_f2_temp;
            LR_filter_coeficient_f2_LP[3] = 1;
            LR_filter_coeficient_f2_LP[4] = (-2 + 2 * Math.Pow(omegac_a_f2, 2)) / den_f2_temp;
            LR_filter_coeficient_f2_LP[5] = (1 - Math.Sqrt(2) * omegac_a_f2 + Math.Pow(omegac_a_f2, 2)) / den_f2_temp;

            LR_filter_coeficient_f2_HP[0] = 1 / den_f2_temp;
            LR_filter_coeficient_f2_HP[1] = -2 / den_f2_temp;
            LR_filter_coeficient_f2_HP[2] = 1 / den_f2_temp;
            LR_filter_coeficient_f2_HP[3] = 1;
            LR_filter_coeficient_f2_HP[4] = (-2 + 2 * Math.Pow(omegac_a_f2, 2)) / den_f2_temp;
            LR_filter_coeficient_f2_HP[5] = (1 - Math.Sqrt(2) * omegac_a_f2 + Math.Pow(omegac_a_f2, 2)) / den_f2_temp;

        }

        public void xin_band_split(double[][] _xin)
        {

            //signal input
            for (int i = 0; i < num_of_channel; i++)
            {
                for (int j = 0; j < sample_per_frame; j++)
                {
                    data_buffer[0][i][j] = _xin[i][j];
                }
            }

            //low band
                for (int i = 0; i < num_of_channel; i++)
                {
                    iir_filtering(data_buffer[0][i], 
                        LR_filter_coeficient_f1_LP[0], LR_filter_coeficient_f1_LP[1], LR_filter_coeficient_f1_LP[2], LR_filter_coeficient_f1_LP[3], LR_filter_coeficient_f1_LP[4], LR_filter_coeficient_f1_LP[5],
                       ref x_y_delay_temp[0][0][i][0], ref x_y_delay_temp[0][0][i][1], ref x_y_delay_temp[0][0][i][2], ref x_y_delay_temp[0][0][i][3], 
                        data_buffer[1][i]);

                    iir_filtering(data_buffer[1][i],
                        LR_filter_coeficient_f1_LP[0], LR_filter_coeficient_f1_LP[1], LR_filter_coeficient_f1_LP[2], LR_filter_coeficient_f1_LP[3], LR_filter_coeficient_f1_LP[4], LR_filter_coeficient_f1_LP[5],
                       ref x_y_delay_temp[0][1][i][0], ref x_y_delay_temp[0][1][i][1], ref x_y_delay_temp[0][1][i][2], ref x_y_delay_temp[0][1][i][3],
                        data_buffer[1][i]);

                    iir_filtering(data_buffer[1][i],
                            LR_filter_coeficient_f2_LP[0], LR_filter_coeficient_f2_LP[1], LR_filter_coeficient_f2_LP[2], LR_filter_coeficient_f2_LP[3], LR_filter_coeficient_f2_LP[4], LR_filter_coeficient_f2_LP[5],
                           ref x_y_delay_temp[0][2][i][0], ref x_y_delay_temp[0][2][i][1], ref x_y_delay_temp[0][2][i][2], ref x_y_delay_temp[0][2][i][3],
                            data_buffer[1][i]);

                    iir_filtering(data_buffer[1][i],
                        LR_filter_coeficient_f2_LP[0], LR_filter_coeficient_f2_LP[1], LR_filter_coeficient_f2_LP[2], LR_filter_coeficient_f2_LP[3], LR_filter_coeficient_f2_LP[4], LR_filter_coeficient_f2_LP[5],
                       ref x_y_delay_temp[0][3][i][0], ref x_y_delay_temp[0][3][i][1], ref x_y_delay_temp[0][3][i][2], ref x_y_delay_temp[0][3][i][3],
                        data_buffer[1][i]);
                        
                }
            //mid band
            for (int i = 0; i < num_of_channel; i++)
            {
                iir_filtering(data_buffer[0][i],
                    LR_filter_coeficient_f1_HP[0], LR_filter_coeficient_f1_HP[1], LR_filter_coeficient_f1_HP[2], LR_filter_coeficient_f1_HP[3], LR_filter_coeficient_f1_HP[4], LR_filter_coeficient_f1_HP[5],
                    ref x_y_delay_temp[1][0][i][0], ref x_y_delay_temp[1][0][i][1], ref x_y_delay_temp[1][0][i][2], ref x_y_delay_temp[1][0][i][3],
                    data_buffer[2][i]);
                
                iir_filtering(data_buffer[2][i],
                    LR_filter_coeficient_f1_HP[0], LR_filter_coeficient_f1_HP[1], LR_filter_coeficient_f1_HP[2], LR_filter_coeficient_f1_HP[3], LR_filter_coeficient_f1_HP[4], LR_filter_coeficient_f1_HP[5],
                    ref x_y_delay_temp[1][1][i][0], ref x_y_delay_temp[1][1][i][1], ref x_y_delay_temp[1][1][i][2], ref x_y_delay_temp[1][1][i][3],
                    data_buffer[2][i]);
                
                iir_filtering(data_buffer[2][i],
                     LR_filter_coeficient_f2_LP[0], LR_filter_coeficient_f2_LP[1], LR_filter_coeficient_f2_LP[2], LR_filter_coeficient_f2_LP[3], LR_filter_coeficient_f2_LP[4], LR_filter_coeficient_f2_LP[5],
                    ref x_y_delay_temp[1][2][i][0], ref x_y_delay_temp[1][2][i][1], ref x_y_delay_temp[1][2][i][2], ref x_y_delay_temp[1][2][i][3],
                     data_buffer[2][i]);

                iir_filtering(data_buffer[2][i],
                    LR_filter_coeficient_f2_LP[0], LR_filter_coeficient_f2_LP[1], LR_filter_coeficient_f2_LP[2], LR_filter_coeficient_f2_LP[3], LR_filter_coeficient_f2_LP[4], LR_filter_coeficient_f2_LP[5],
                   ref x_y_delay_temp[1][3][i][0], ref x_y_delay_temp[1][3][i][1], ref x_y_delay_temp[1][3][i][2], ref x_y_delay_temp[1][3][i][3],
                    data_buffer[2][i]);
                   
            }
            
            //high band
            for (int i = 0; i < num_of_channel; i++)
            {
                
                iir_filtering(data_buffer[0][i],
                    LR_filter_coeficient_f1_HP[0], LR_filter_coeficient_f1_HP[1], LR_filter_coeficient_f1_HP[2], LR_filter_coeficient_f1_HP[3], LR_filter_coeficient_f1_HP[4], LR_filter_coeficient_f1_HP[5],
                   ref x_y_delay_temp[2][0][i][0], ref x_y_delay_temp[2][0][i][1], ref x_y_delay_temp[2][0][i][2], ref x_y_delay_temp[2][0][i][3],
                    data_buffer[3][i]);

                iir_filtering(data_buffer[3][i],
                    LR_filter_coeficient_f1_HP[0], LR_filter_coeficient_f1_HP[1], LR_filter_coeficient_f1_HP[2], LR_filter_coeficient_f1_HP[3], LR_filter_coeficient_f1_HP[4], LR_filter_coeficient_f1_HP[5],
                   ref x_y_delay_temp[2][1][i][0], ref x_y_delay_temp[2][1][i][1], ref x_y_delay_temp[2][1][i][2], ref x_y_delay_temp[2][1][i][3],
                    data_buffer[3][i]);
                
                
                iir_filtering(data_buffer[3][i],
                    LR_filter_coeficient_f2_HP[0], LR_filter_coeficient_f2_HP[1], LR_filter_coeficient_f2_HP[2], LR_filter_coeficient_f2_HP[3], LR_filter_coeficient_f2_HP[4], LR_filter_coeficient_f2_HP[5],
                      ref x_y_delay_temp[2][2][i][0], ref x_y_delay_temp[2][2][i][1], ref x_y_delay_temp[2][2][i][2], ref x_y_delay_temp[2][2][i][3],
                        data_buffer[3][i]);

                iir_filtering(data_buffer[3][i],
                    LR_filter_coeficient_f2_HP[0], LR_filter_coeficient_f2_HP[1], LR_filter_coeficient_f2_HP[2], LR_filter_coeficient_f2_HP[3], LR_filter_coeficient_f2_HP[4], LR_filter_coeficient_f2_HP[5],
                  ref x_y_delay_temp[2][3][i][0], ref x_y_delay_temp[2][3][i][1], ref x_y_delay_temp[2][3][i][2], ref x_y_delay_temp[2][3][i][3],
                    data_buffer[3][i]);    
                    
            }

        }

        private void iir_filtering(double[] _xin ,double _coef_b0, double _coef_b1, double _coef_b2, double _coef_a0, double _coef_a1, double _coef_a2 ,ref double _x_n_1, ref double _x_n_2, ref double _y_n_1, ref double _y_n_2,double[] _yout)
        {
            double[] xin_temp = new double[sample_per_frame];
            double[] yout_temp = new double[sample_per_frame];

            for (int i = 0; i < sample_per_frame; i++)
            {
                xin_temp[i] = _xin[i];
                yout_temp[i] = 0;
            }
                for (int i = 0; i < sample_per_frame; i++)
            {

                yout_temp[i] = (xin_temp[i] * _coef_b0) + (_x_n_1 * _coef_b1) + (_x_n_2 * _coef_b2)
                                          - (_y_n_1 * _coef_a1) - (_y_n_2 * _coef_a2);


                if (yout_temp[i] > 1)
                {
                    yout_temp[i] = 1;
                }
                else if (yout_temp[i] < -1)
                {
                    yout_temp[i] = -1;
                }

                _x_n_2 = _x_n_1;
                _x_n_1 = xin_temp[i];
                _y_n_2 = _y_n_1;
                _y_n_1 = yout_temp[i];
                
            }
            for (int i = 0; i < sample_per_frame; i++)
            {
                _yout[i] = yout_temp[i];
            }
        }

        public void drc_filtering(double[][] _xin)
        {
           
            double log_gain; 
            double log_input_level ; 
            double current_gain;
           // double current_xin_level;



            //signal band split
         
            xin_band_split(_xin);
           
            //drc filtering

            for (int i = 0; i < Num_of_band; i++)
            {
                ////peak mode
                if (peak_mode[i] == 1)
                {
                    for (int j = 0; j < num_of_channel; j++)
                    {
                        for (int k = 0; k < sample_per_frame; k++)
                        {
                            //////level measurement
                            if (Math.Abs(data_buffer[i + 1][j][k]) > prev_xin[i][j])
                            {
                                current_xin_level[i][j][k] = (1 - alpha[i]) * prev_xin[i][j] + alpha[i] * Math.Abs(data_buffer[i + 1][j][k]);
                            }
                            else
                            {
                                current_xin_level[i][j][k] = (1 - omega[i]) * prev_xin[i][j];
                            }

                            log_input_level = Math.Log(current_xin_level[i][j][k], 2);

                            /////compress
                            if (log_input_level > threshold[i])
                            {
                                log_gain = (log_input_level - threshold[i])*(ratio[i]-1);
                            }
                            else
                            {
                                log_gain = 0;
                            }
                            current_gain = Math.Pow(2, (log_gain + makeup[i]));

                            //attack release
                            if (current_gain < prev_gain[i][j])
                            {
                                gain_buffer[i][j][k] = (1 - alpha[i]) * prev_gain[i][j] + attack[i] * current_gain;
                            }
                            else
                            {
                                gain_buffer[i][j][k] = (1 - omega[i]) * prev_gain[i][j] + release[i] * current_gain;
                            }
                            prev_gain[i][j] = current_gain;
                            prev_xin[i][j] = current_xin_level[i][j][k];
                        }
                    }
                }

                else //rms mode
                {
                    for (int j = 0; j < num_of_channel; j++)
                    {
                        for (int k = 0; k < sample_per_frame; k++)
                        {
                            //////level measurement
                            current_xin_level[i][j][k] = (1 - alpha[i]) * Math.Pow(prev_xin[i][j], 2) + alpha[i] * Math.Pow(data_buffer[i + 1][j][k], 2);

                            log_input_level = Math.Log(prev_xin[i][j], 2) / 2;

                            /////compress
                            if (log_input_level > threshold[i])
                            {
                                log_gain = log_input_level - threshold[i];
                            }
                            else
                            {
                                log_gain = 0;
                            }
                            current_gain = Math.Pow(2, (log_gain + makeup[i]));

                            //attack release
                            if (current_gain < prev_gain[i][j])
                            {
                                gain_buffer[i][j][k] = (1 - alpha[i]) * prev_gain[i][j] + alpha[i] * current_gain;
                            }
                            else
                            {
                                gain_buffer[i][j][k] = (1 - omega[i]) * prev_gain[i][j] + omega[i] * current_gain;
                            }
                            prev_gain[i][j] = current_gain;
                            prev_xin[i][j] = current_xin_level[i][j][k];
                        }

                    }
                }

             }
             
                //data * gain and mixing
                for (int i = 0; i < num_of_channel; i++)
            {
                for (int j = 0; j < sample_per_frame; j++)
                {
                    data_buffer[4][i][j] = data_buffer[1][i][j] * gain_buffer[0][i][j] + data_buffer[2][i][j] * gain_buffer[1][i][j] + data_buffer[3][i][j] * gain_buffer[2][i][j];
                   
                    
                    if (data_buffer[4][i][j] > 1)
                    {
                        data_buffer[4][i][j] = 1;
                    }
                    else if (data_buffer[4][i][j] < -1)
                    {
                        data_buffer[4][i][j] = -1;
                    }
                    
                }
            }
        }
    }
}
