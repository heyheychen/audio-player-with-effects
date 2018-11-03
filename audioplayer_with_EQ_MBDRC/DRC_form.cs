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
    public partial class DRC_form : Form
    {
        //for comprssor plot
        private double[][] x_data = new double [3][];
        private double[][] y_data = new double [3][];
        private double[][] x_threshold = new double[3][];
        private double[][] y_threshold = new double[3][];

        Effect_3band_DRC three_band_drc_temp;
        GraphPane compressor_LB, compressor_MB, compressor_HB;


        public DRC_form(Effect_3band_DRC _three_band_drc)
        {
            InitializeComponent();
            three_band_drc_temp = _three_band_drc;
        }

        private void DRC_form_Load(object sender, EventArgs e)
        {

            zedgraph_ini(zedGraphControl1, zedGraphControl2, zedGraphControl3);

           

            //drc enable
            checkbox_DRC_enable.Checked = three_band_drc_temp.DRC_enable;
            //peak rms mode
            if (three_band_drc_temp.peak_mode[0] == 1)
            {
                radioButton_LB_peakmode.Checked = true;
            }
            else
            {
                radioButton_LB_peakmode.Checked = false;
            }

            if (three_band_drc_temp.peak_mode[1] == 1)
            {
                radioButton_MB_peakmode.Checked = true;
            }
            else
            {
                radioButton_MB_peakmode.Checked = false;
            }

            if (three_band_drc_temp.peak_mode[2] == 1)
            {
                radioButton_HB_peakmode.Checked = true;
            }
            else
            {
                radioButton_HB_peakmode.Checked = false;
            }
            //threshold
            textBox_LB_threshold.Text = (three_band_drc_temp.threshold[0] * 20 / Math.Log(10 ,2)).ToString();
            textBox_MB_threshold.Text = (three_band_drc_temp.threshold[1] * 20 / Math.Log(10, 2)).ToString();
            textBox_HB_threshold.Text = (three_band_drc_temp.threshold[2] * 20 / Math.Log(10, 2)).ToString();
            //ratio
            textBox_LB_ratio.Text = (three_band_drc_temp.ratio[0] ).ToString();
            textBox_MB_ratio.Text = (three_band_drc_temp.ratio[1] ).ToString();
            textBox_HB_ratio.Text = (three_band_drc_temp.ratio[2] ).ToString();
            //alpha
            textBox_LB_alpha.Text = Math.Round(1 / (Math.Log(1 - three_band_drc_temp.alpha[0]) * 0.001 * three_band_drc_temp.sampling_rate / -2.2),1).ToString();
            textBox_MB_alpha.Text = Math.Round(1 / (Math.Log(1 - three_band_drc_temp.alpha[1]) * 0.001 * three_band_drc_temp.sampling_rate / -2.2),1).ToString();
            textBox_HB_alpha.Text = Math.Round(1 / (Math.Log(1 - three_band_drc_temp.alpha[2]) * 0.001 * three_band_drc_temp.sampling_rate / -2.2),1).ToString();
            //omega
            textBox_LB_omega.Text = Math.Round(1 / (Math.Log(1 - three_band_drc_temp.omega[0]) * 0.001 * three_band_drc_temp.sampling_rate / -2.2), 1).ToString();
            textBox_MB_omega.Text = Math.Round(1 / (Math.Log(1 - three_band_drc_temp.omega[1]) * 0.001 * three_band_drc_temp.sampling_rate / -2.2), 1).ToString();
            textBox_HB_omega.Text = Math.Round(1 / (Math.Log(1 - three_band_drc_temp.omega[2]) * 0.001 * three_band_drc_temp.sampling_rate / -2.2), 1).ToString();
            //attack
            textBox_LB_attack.Text = Math.Round(1 / (Math.Log(1 - three_band_drc_temp.attack[0]) * 0.001 * three_band_drc_temp.sampling_rate / -2.2), 1).ToString();
            textBox_MB_attack.Text = Math.Round(1 / (Math.Log(1 - three_band_drc_temp.attack[1]) * 0.001 * three_band_drc_temp.sampling_rate / -2.2), 1).ToString();
            textBox_HB_attack.Text = Math.Round(1 / (Math.Log(1 - three_band_drc_temp.attack[2]) * 0.001 * three_band_drc_temp.sampling_rate / -2.2), 1).ToString();
            //release
            textBox_LB_release.Text = Math.Round(1 / (Math.Log(1 - three_band_drc_temp.release[0]) * 0.001 * three_band_drc_temp.sampling_rate / -2.2), 1).ToString();
            textBox_MB_release.Text = Math.Round(1 / (Math.Log(1 - three_band_drc_temp.release[1]) * 0.001 * three_band_drc_temp.sampling_rate / -2.2), 1).ToString();
            textBox_HB_release.Text = Math.Round(1 / (Math.Log(1 - three_band_drc_temp.release[2]) * 0.001 * three_band_drc_temp.sampling_rate / -2.2), 1).ToString();
            //makeup
            textBox_LB_makeup.Text = (three_band_drc_temp.makeup[0] * 20 / Math.Log(10, 2)).ToString();
            textBox_MB_makeup.Text = (three_band_drc_temp.makeup[1] * 20 / Math.Log(10, 2)).ToString();
            textBox_HB_makeup.Text = (three_band_drc_temp.makeup[2] * 20 / Math.Log(10, 2)).ToString();
            //Split frequency
            TextBox_split_f1.Text = (three_band_drc_temp.splipt_frequency[0] ).ToString();
            TextBox_split_f2.Text = (three_band_drc_temp.splipt_frequency[1]).ToString();

            //plot compressor
            gen_graph_datas_and_plot();
 
     

        }


        //drc enable
        private void checkbox_DRC_enable_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbox_DRC_enable.Checked == true)
            {
                three_band_drc_temp.DRC_enable = true;
            }
            else
            {
                three_band_drc_temp.DRC_enable = false;
            }
        }

        
        ////////////peak rms mode start ////////////////
        private void radioButton_LB_peakmode_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_LB_peakmode.Checked == true)
            {
                three_band_drc_temp.peak_mode[0] = 1;
                textBox_LB_omega.Enabled = true;
            }
            else
            {
                three_band_drc_temp.peak_mode[0] = 0;
            }
        }
        private void radioButton_LB_rmsmode_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_LB_rmsmode.Checked == true)
            {
                three_band_drc_temp.peak_mode[0] = 0;
                textBox_LB_omega.Enabled = false;
            }
            else
            {
                three_band_drc_temp.peak_mode[0] = 1;
            }
        }
        private void radioButton_MB_peakmode_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_MB_peakmode.Checked == true)
            {
                three_band_drc_temp.peak_mode[1] = 1;
                textBox_MB_omega.Enabled = true;
            }
            else
            {
                three_band_drc_temp.peak_mode[1] = 0;
            }

        }
        private void radioButton_MB_rmsmode_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_MB_rmsmode.Checked == true)
            {
                three_band_drc_temp.peak_mode[1] = 0;
                textBox_MB_omega.Enabled = false;
            }
            else
            {
                three_band_drc_temp.peak_mode[1] = 1;
            }

        }
        private void radioButton_HB_peakmode_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_HB_peakmode.Checked == true)
            {
                three_band_drc_temp.peak_mode[2] = 1;
                textBox_HB_omega.Enabled = true;
            }
            else
            {
                three_band_drc_temp.peak_mode[2] = 0;
            }
        }
        private void radioButton_HB_rmsmode_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton_HB_rmsmode.Checked == true)
            {
                three_band_drc_temp.peak_mode[2] = 0;
                textBox_HB_omega.Enabled = false;
            }
            else
            {
                three_band_drc_temp.peak_mode[2] = 1;
            }
        }
        ////////////peak rms mode end ////////////////

        ////////////threshold start ////////////////
        private void textBox_LB_threshold_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_LB_threshold.Text) <= 0 & double.Parse(textBox_LB_threshold.Text) >= -100)
                {
                    three_band_drc_temp.threshold[0] = double.Parse(textBox_LB_threshold.Text) * Math.Log(10, 2) / 20;
                    gen_graph_datas_and_plot();
                }
                else
                {
                    MessageBox.Show("out of range (0 ~ -100 dB)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }

        }

        private void textBox_LB_threshold_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (double.Parse(textBox_LB_threshold.Text) <= 0 & double.Parse(textBox_LB_threshold.Text) >= -100)
                    {
                        three_band_drc_temp.threshold[0] = double.Parse(textBox_LB_threshold.Text) * Math.Log(10, 2) / 20;
                        gen_graph_datas_and_plot();
                    }
                    else
                    {
                        MessageBox.Show("out of range (0 ~ -100 dB)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }

        }
        private void textBox_MB_threshold_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                    try
                    {
                        if (double.Parse(textBox_MB_threshold.Text) <= 0 & double.Parse(textBox_MB_threshold.Text) >= -100)
                        {
                            three_band_drc_temp.threshold[1] = double.Parse(textBox_MB_threshold.Text) * Math.Log(10, 2) / 20;
                            gen_graph_datas_and_plot();
                        }
                        else
                        {
                            MessageBox.Show("out of range (0 ~ -100 dB)");
                        }
                    }
                    catch (Exception )
                    {
                        MessageBox.Show("invalid number");
                    }
            }
        }

        private void textBox_MB_threshold_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_MB_threshold.Text) <= 0 & double.Parse(textBox_MB_threshold.Text) >= -100)
                {
                    three_band_drc_temp.threshold[1] = double.Parse(textBox_MB_threshold.Text) * Math.Log(10, 2) / 20;
                    gen_graph_datas_and_plot();
                }
                else
                {
                    MessageBox.Show("out of range (0 ~ -100 dB)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }         
        }
        private void textBox_HB_threshold_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_HB_threshold.Text) <= 0 & double.Parse(textBox_HB_threshold.Text) >= -100)
                {
                    three_band_drc_temp.threshold[2] = double.Parse(textBox_HB_threshold.Text) * Math.Log(10, 2) / 20;
                    gen_graph_datas_and_plot();
                }
                else
                {
                    MessageBox.Show("out of range (0 ~ -100 dB)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }
 
        }
        private void textBox_HB_threshold_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Return)
            {
                    try
                    {
                        if (double.Parse(textBox_HB_threshold.Text) <= 0 & double.Parse(textBox_HB_threshold.Text) >= -100)
                        {
                            three_band_drc_temp.threshold[2] = double.Parse(textBox_HB_threshold.Text) * Math.Log(10, 2) / 20;
                            gen_graph_datas_and_plot();
                        }
                        else
                        {
                            MessageBox.Show(" (0 ~ -100 dB)");
                        }
                    }
                    catch (Exception )
                    {
                        MessageBox.Show("invalid number");
                    }
            }

        }
        ////////////threshold end ////////////////


        ////////////ratio start ////////////////

        private void textBox_LB_ratio_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_LB_ratio.Text) >= 0 & double.Parse(textBox_LB_ratio.Text) <= 100)
                {
                    three_band_drc_temp.ratio[0] = double.Parse(textBox_LB_ratio.Text);
                    gen_graph_datas_and_plot();
                }
                else
                {
                    MessageBox.Show("out of range (0 ~ 100)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }        
        }
        private void textBox_LB_ratio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (double.Parse(textBox_LB_ratio.Text) >= 0 & double.Parse(textBox_LB_ratio.Text) <= 100)
                    {
                        three_band_drc_temp.ratio[0] = double.Parse(textBox_LB_ratio.Text);
                        gen_graph_datas_and_plot();
                    }
                    else
                    {
                        MessageBox.Show("out of range (0 ~ 100)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }
        }
        private void textBox_MB_ratio_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_MB_ratio.Text) >= 0 & double.Parse(textBox_MB_ratio.Text) <= 100)
                {
                    three_band_drc_temp.ratio[1] = double.Parse(textBox_MB_ratio.Text);
                    gen_graph_datas_and_plot();
                }
                else
                {
                    MessageBox.Show("out of range (0 ~ 100)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }
  
        }
        
        private void textBox_MB_ratio_KeyDown(object sender, KeyEventArgs e)
        {
             if (e.KeyCode == Keys.Return)
             {
                try
                {
                    if (double.Parse(textBox_MB_ratio.Text) >= 0 & double.Parse(textBox_MB_ratio.Text) <= 100)
                    {
                        three_band_drc_temp.ratio[1] = double.Parse(textBox_MB_ratio.Text);
                        gen_graph_datas_and_plot();
                    }
                    else
                    {
                        MessageBox.Show("out of range (0 ~ 100)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }
        }

        private void textBox_HB_ratio_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_HB_ratio.Text) >= 0 & double.Parse(textBox_HB_ratio.Text) <= 100)
                {
                    three_band_drc_temp.ratio[2] = double.Parse(textBox_HB_ratio.Text);
                    gen_graph_datas_and_plot();
                }
                else
                {
                    MessageBox.Show("out of range (0 ~ 100)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }      
        }

        private void textBox_HB_ratio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (double.Parse(textBox_HB_ratio.Text) >= 0 & double.Parse(textBox_HB_ratio.Text) <= 100)
                    {
                        three_band_drc_temp.ratio[2] = double.Parse(textBox_HB_ratio.Text);
                        gen_graph_datas_and_plot();
                    }
                    else
                    {
                        MessageBox.Show("out of range (0 ~ 100)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }

        }

        ////////////ratio end ////////////////


        ////////////alpha start ////////////////

        private void textBox_LB_alpha_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_LB_alpha.Text) >= 0 & double.Parse(textBox_LB_alpha.Text) <= 5000)
                {
                    three_band_drc_temp.alpha[0] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_LB_alpha.Text) * 0.001));
                }
                else
                {
                    MessageBox.Show("out of range (0 ~ 5000)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }
        }

        private void textBox_LB_alpha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (double.Parse(textBox_LB_alpha.Text) >= 0 & double.Parse(textBox_LB_alpha.Text) <= 5000)
                    {
                        three_band_drc_temp.alpha[0] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_LB_alpha.Text) * 0.001));
                    }
                    else
                    {
                        MessageBox.Show("out of range (0 ~ 5000)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }
        }
        private void textBox_MB_alpha_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_MB_alpha.Text) >= 0 & double.Parse(textBox_MB_alpha.Text) <= 5000)
                {
                    three_band_drc_temp.alpha[1] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_MB_alpha.Text) * 0.001));
                }
                else
                {
                    MessageBox.Show("out of range (0 ~ 5000)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }           
        }
        private void textBox_MB_alpha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (double.Parse(textBox_MB_alpha.Text) >= 0 & double.Parse(textBox_MB_alpha.Text) <= 5000)
                    {
                        three_band_drc_temp.alpha[1] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_MB_alpha.Text) * 0.001));
                    }
                    else
                    {
                        MessageBox.Show("out of range (0 ~ 5000)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }
        }
        private void textBox_HB_alpha_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_HB_alpha.Text) >= 0 & double.Parse(textBox_HB_alpha.Text) <= 5000)
                {
                    three_band_drc_temp.alpha[2] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_HB_alpha.Text) * 0.001));
                }
                else
                {
                    MessageBox.Show("out of range (0 ~ 5000)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }
        }
        private void textBox_HB_alpha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (double.Parse(textBox_HB_alpha.Text) >= 0 & double.Parse(textBox_HB_alpha.Text) <= 5000)
                    {
                        three_band_drc_temp.alpha[2] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_HB_alpha.Text) * 0.001));
                    }
                    else
                    {
                        MessageBox.Show("out of range (0 ~ 5000)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }
        }
        ////////////alpha end ////////////////

        ////////////omega start ////////////////

        private void textBox_LB_omega_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_LB_omega.Text) >= 0 & double.Parse(textBox_LB_omega.Text) <= 5000)
                {
                    three_band_drc_temp.omega[0] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_LB_omega.Text) * 0.001));
                }
                else
                {
                    MessageBox.Show("out of range (0 ~ 5000)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }

        }

        private void textBox_LB_omega_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (double.Parse(textBox_LB_omega.Text) >= 0 & double.Parse(textBox_LB_omega.Text) <= 5000)
                    {
                        three_band_drc_temp.omega[0] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_LB_omega.Text) * 0.001));
                    }
                    else
                    {
                        MessageBox.Show("out of range (0 ~ 5000)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }
        }

        private void textBox_MB_omega_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_MB_omega.Text) >= 0 & double.Parse(textBox_MB_omega.Text) <= 5000)
                {
                    three_band_drc_temp.omega[1] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_MB_omega.Text) * 0.001));
                }
                else
                {
                    MessageBox.Show("out of range (0 ~ 5000)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }
        }

        private void textBox_MB_omega_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (double.Parse(textBox_MB_omega.Text) >= 0 & double.Parse(textBox_MB_omega.Text) <= 5000)
                    {
                        three_band_drc_temp.omega[1] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_MB_omega.Text) * 0.001));
                    }
                    else
                    {
                        MessageBox.Show("out of range (0 ~ 5000)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }
        }

        private void textBox_HB_omega_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_HB_omega.Text) >= 0 & double.Parse(textBox_HB_omega.Text) <= 5000)
                {
                    three_band_drc_temp.omega[2] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_HB_omega.Text) * 0.001));
                }
                else
                {
                    MessageBox.Show("out of range (0 ~ 5000)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }      
        }
    

        private void textBox_HB_omega_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (double.Parse(textBox_HB_omega.Text) >= 0 & double.Parse(textBox_HB_omega.Text) <= 5000)
                    {
                        three_band_drc_temp.omega[2] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_HB_omega.Text) * 0.001));
                    }
                    else
                    {
                        MessageBox.Show("out of range (0 ~ 5000)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }
        }
        ////////////omega end ////////////////

        ////////////attack start ////////////////
        private void textBox_LB_attack_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_LB_attack.Text) >= 0 & double.Parse(textBox_LB_attack.Text) <= 5000)
                {
                    three_band_drc_temp.attack[0] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_LB_attack.Text) * 0.001));
                }
                else
                {
                    MessageBox.Show("out of range (0 ~ 5000)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }
        }

        private void textBox_LB_attack_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (double.Parse(textBox_LB_attack.Text) >= 0 & double.Parse(textBox_LB_attack.Text) <= 5000)
                    {
                        three_band_drc_temp.attack[0] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_LB_attack.Text) * 0.001));
                    }
                    else
                    {
                        MessageBox.Show("out of range (0 ~ 5000)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }
        }

        private void textBox_MB_attack_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_MB_attack.Text) >= 0 & double.Parse(textBox_MB_attack.Text) <= 5000)
                {
                    three_band_drc_temp.attack[1] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_MB_attack.Text) * 0.001));
                }
                else
                {
                    MessageBox.Show("out of range (0 ~ 5000)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            } 
        }

        private void textBox_MB_attack_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (double.Parse(textBox_MB_attack.Text) >= 0 & double.Parse(textBox_MB_attack.Text) <= 5000)
                    {
                        three_band_drc_temp.attack[1] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_MB_attack.Text) * 0.001));
                    }
                    else
                    {
                        MessageBox.Show("out of range (0 ~ 5000)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }
        }

        private void textBox_HB_attack_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_HB_attack.Text) >= 0 & double.Parse(textBox_HB_attack.Text) <= 5000)
                {
                    three_band_drc_temp.attack[2] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_HB_attack.Text) * 0.001));
                }
                else
                {
                    MessageBox.Show("out of range (0 ~ 5000)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }        
        }

        private void textBox_HB_attack_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (double.Parse(textBox_HB_attack.Text) >= 0 & double.Parse(textBox_HB_attack.Text) <= 5000)
                    {
                        three_band_drc_temp.attack[2] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_HB_attack.Text) * 0.001));
                    }
                    else
                    {
                        MessageBox.Show("out of range (0 ~ 5000)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }
        }
        ////////////attack end ////////////////


        ////////////release start ////////////////

        private void textBox_LB_release_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_LB_release.Text) >= 0 & double.Parse(textBox_LB_release.Text) <= 5000)
                {
                    three_band_drc_temp.release[0] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_LB_release.Text) * 0.001));
                }
                else
                {
                    MessageBox.Show("out of range (0 ~ 5000)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }

        }

        private void textBox_LB_release_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (double.Parse(textBox_LB_release.Text) >= 0 & double.Parse(textBox_LB_release.Text) <= 5000)
                    {
                        three_band_drc_temp.release[0] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_LB_release.Text) * 0.001));
                     
                    }
                    else
                    {
                        MessageBox.Show("out of range (0 ~ 5000)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }
        }

        private void textBox_MB_release_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_MB_release.Text) >= 0 & double.Parse(textBox_MB_release.Text) <= 5000)
                {
                    three_band_drc_temp.release[1] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_MB_release.Text) * 0.001));
                }
                else
                {
                    MessageBox.Show("out of range (0 ~ 5000)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }            
        }

        private void textBox_MB_release_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (double.Parse(textBox_MB_release.Text) >= 0 & double.Parse(textBox_MB_release.Text) <= 5000)
                    {
                        three_band_drc_temp.release[1] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_MB_release.Text) * 0.001));
                    }
                    else
                    {
                        MessageBox.Show("out of range (0 ~ 5000)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }
        }

        private void textBox_HB_release_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_HB_release.Text) >= 0 & double.Parse(textBox_HB_release.Text) <= 5000)
                {
                    three_band_drc_temp.release[2] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_HB_release.Text) * 0.001));
                }
                else
                {
                    MessageBox.Show("out of range (0 ~ 5000)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }         
        }

        private void textBox_HB_release_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (double.Parse(textBox_HB_release.Text) >= 0 & double.Parse(textBox_HB_release.Text) <= 5000)
                    {
                        three_band_drc_temp.release[2] = 1 - Math.Exp(-2.2 / three_band_drc_temp.sampling_rate / (double.Parse(textBox_HB_release.Text) * 0.001));
                    }
                    else
                    {
                        MessageBox.Show("out of range (0 ~ 5000)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }
        }

        ////////////release end ////////////////


        ////////////makeup start ////////////////
        private void textBox_LB_makeup_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_LB_makeup.Text) >= -30 & double.Parse(textBox_LB_makeup.Text) <= 30)
                {
                    three_band_drc_temp.makeup[0] = double.Parse(textBox_LB_makeup.Text) * Math.Log(10, 2) / 20;
                    gen_graph_datas_and_plot();
                }
                else
                {
                    MessageBox.Show("out of range (-30 ~ 30)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }     
        }

        private void textBox_LB_makeup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (double.Parse(textBox_LB_makeup.Text) >= -30 & double.Parse(textBox_LB_makeup.Text) <= 30)
                    {
                        three_band_drc_temp.makeup[0] = double.Parse(textBox_LB_makeup.Text) * Math.Log(10, 2) / 20;
                        gen_graph_datas_and_plot();
                    }
                    else
                    {
                        MessageBox.Show("out of range (-30 ~ 30)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }
        }

        private void textBox_MB_makeup_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_MB_makeup.Text) >= -30 & double.Parse(textBox_MB_makeup.Text) <= 30)
                {
                    three_band_drc_temp.makeup[1] = double.Parse(textBox_MB_makeup.Text) * Math.Log(10, 2) / 20;
                    gen_graph_datas_and_plot();
                }
                else
                {
                    MessageBox.Show("out of range (-30 ~ 30)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }        
        }

        private void textBox_MB_makeup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (double.Parse(textBox_MB_makeup.Text) >= -30 & double.Parse(textBox_MB_makeup.Text) <= 30)
                    {
                        three_band_drc_temp.makeup[1] = double.Parse(textBox_MB_makeup.Text) * Math.Log(10, 2) / 20;
                        gen_graph_datas_and_plot();
                    }
                    else
                    {
                        MessageBox.Show("out of range (-30 ~ 30)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }
        }

        private void textBox_HB_makeup_Leave(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(textBox_HB_makeup.Text) >= -30 & double.Parse(textBox_HB_makeup.Text) <= 30)
                {
                    three_band_drc_temp.makeup[2] = double.Parse(textBox_HB_makeup.Text) * Math.Log(10, 2) / 20;
                    gen_graph_datas_and_plot();
                }
                else
                {
                    MessageBox.Show("out of range (-30 ~ 30)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }           
        }

        private void textBox_HB_makeup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (double.Parse(textBox_HB_makeup.Text) >= -30 & double.Parse(textBox_HB_makeup.Text) <= 30)
                    {
                        three_band_drc_temp.makeup[2] = double.Parse(textBox_HB_makeup.Text) * Math.Log(10, 2) / 20;
                        gen_graph_datas_and_plot();
                    }
                    else
                    {
                        MessageBox.Show("out of range (-30 ~ 30)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }
        }
        ////////////makeup end ////////////////

        ////////////split f start ////////////////
        private void TextBox_split_f1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(TextBox_split_f1.Text) >= 20 & int.Parse(TextBox_split_f1.Text) <= 20000 & int.Parse(TextBox_split_f1.Text)<= three_band_drc_temp.splipt_frequency[1])
                {
                    three_band_drc_temp.splipt_frequency[0] = int.Parse(TextBox_split_f1.Text);
                    three_band_drc_temp.gen_LR_filter_coefficient();
                }
                else
                {
                    MessageBox.Show("out of range (20 ~ 20000, smaller than f2)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }
 
        }

        private void TextBox_split_f1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (int.Parse(TextBox_split_f1.Text) >= 20 & int.Parse(TextBox_split_f1.Text) <= 20000 & int.Parse(TextBox_split_f1.Text) <= three_band_drc_temp.splipt_frequency[1])
                    {
                        three_band_drc_temp.splipt_frequency[0] = int.Parse(TextBox_split_f1.Text);
                        three_band_drc_temp.gen_LR_filter_coefficient();
                    }
                    else
                    {
                        MessageBox.Show("out of range (20 ~ 20000, smaller than f2)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }

            }
        }

        private void TextBox_split_f2_Leave(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(TextBox_split_f2.Text) >= 20 & int.Parse(TextBox_split_f2.Text) <= 20000 & int.Parse(TextBox_split_f2.Text) >= three_band_drc_temp.splipt_frequency[0])
                {
                    three_band_drc_temp.splipt_frequency[1] = int.Parse(TextBox_split_f2.Text);
                    three_band_drc_temp.gen_LR_filter_coefficient();
                }
                else
                {
                    MessageBox.Show("out of range (20 ~ 20000, larger than f1)");
                }
            }
            catch (Exception )
            {
                MessageBox.Show("invalid number");
            }

        }

        private void TextBox_split_f2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                try
                {
                    if (int.Parse(TextBox_split_f2.Text) >= 20 & int.Parse(TextBox_split_f2.Text) <= 20000 & int.Parse(TextBox_split_f2.Text) >= three_band_drc_temp.splipt_frequency[0])
                    {
                        three_band_drc_temp.splipt_frequency[1] = int.Parse(TextBox_split_f2.Text);
                        three_band_drc_temp.gen_LR_filter_coefficient();
                    }
                    else
                    {
                        MessageBox.Show("out of range (20 ~ 20000, larger than f1)");
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("invalid number");
                }
            }
        }
        ////////////split f end ////////////////


        private void zedgraph_ini(ZedGraphControl _zgc_LB, ZedGraphControl _zgc_MB, ZedGraphControl _zgc_HB)
        {
            compressor_LB = _zgc_LB.GraphPane;
            compressor_MB = _zgc_MB.GraphPane;
            compressor_HB = _zgc_HB.GraphPane;


            compressor_LB.Title.Text = "Compressor_LB"; //圖表的表頭
            compressor_LB.Title.FontSpec.Size = 20;
            compressor_MB.Title.Text = "Compressor_MB"; //圖表的表頭
            compressor_MB.Title.FontSpec.Size = 20;
            compressor_HB.Title.Text = "Compressor_HB"; //圖表的表頭
            compressor_HB.Title.FontSpec.Size = 20;

            compressor_LB.XAxis.Title.Text = "Input (dB)"; //X軸的名稱
            compressor_LB.XAxis.Title.FontSpec.Size = 16;
            compressor_LB.XAxis.Scale.MajorStep = 10;
            compressor_LB.XAxis.Type = ZedGraph.AxisType.Linear;
            compressor_MB.XAxis.Title.Text = "Input (dB)"; //X軸的名稱
            compressor_MB.XAxis.Title.FontSpec.Size = 16;
            compressor_MB.XAxis.Scale.MajorStep = 10;
            compressor_MB.XAxis.Type = ZedGraph.AxisType.Linear;
            compressor_HB.XAxis.Title.Text = "Input (dB)"; //X軸的名稱
            compressor_HB.XAxis.Title.FontSpec.Size = 16;
            compressor_HB.XAxis.Scale.MajorStep = 10;
            compressor_HB.XAxis.Type = ZedGraph.AxisType.Linear;


            compressor_LB.YAxis.Title.Text = "Out put (dB)"; //Y軸的名稱
            compressor_LB.YAxis.Title.FontSpec.Size = 16;
            compressor_LB.YAxis.Scale.MajorStep = 10;
            compressor_LB.YAxis.MajorGrid.IsZeroLine = false;
            compressor_MB.YAxis.Title.Text = "Out put (dB)"; //Y軸的名稱
            compressor_MB.YAxis.Title.FontSpec.Size = 16;
            compressor_MB.YAxis.Scale.MajorStep = 10;
            compressor_MB.YAxis.MajorGrid.IsZeroLine = false;
            compressor_HB.YAxis.Title.Text = "Out put (dB)"; //Y軸的名稱
            compressor_HB.YAxis.Title.FontSpec.Size = 16;
            compressor_HB.YAxis.Scale.MajorStep = 10;
            compressor_HB.YAxis.MajorGrid.IsZeroLine = false;
            

            compressor_LB.AxisChange();
            compressor_MB.AxisChange();
            compressor_HB.AxisChange();

            compressor_LB.XAxis.Scale.Min = -60;
            compressor_LB.XAxis.Scale.Max = 10;
            compressor_LB.YAxis.Scale.Min = -60;
            compressor_LB.YAxis.Scale.Max = 10;
            compressor_MB.XAxis.Scale.Min = -60;
            compressor_MB.XAxis.Scale.Max = 10;
            compressor_MB.YAxis.Scale.Min = -60;
            compressor_MB.YAxis.Scale.Max = 10;
            compressor_HB.XAxis.Scale.Min = -60;
            compressor_HB.XAxis.Scale.Max = 10;
            compressor_HB.YAxis.Scale.Min = -60;
            compressor_HB.YAxis.Scale.Max = 10;

        }

        private void gen_graph_datas_and_plot()
        {
           
            for (int i = 0; i < 3; i++)
            {
                x_data[i] = new double[81];
                y_data[i] = new double[81];
                x_threshold[i] = new double[20];
                y_threshold[i] = new double[20];
            }

            //compress line
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 81; j++)

                {
                    

                    x_data[i][j] = j - 60;
                    if (x_data[i][j] <= (three_band_drc_temp.threshold[i] * 20 / Math.Log(10, 2)))
                    {
                        y_data[i][j] = x_data[i][j] + (three_band_drc_temp.makeup[i] * 20 / Math.Log(10, 2));
                    }
                    else
                    {
                        y_data[i][j] = (three_band_drc_temp.threshold[i] * 20 / Math.Log(10, 2)) + (x_data[i][j] - (three_band_drc_temp.threshold[i] * 20 / Math.Log(10, 2))) * three_band_drc_temp.ratio[i] + (three_band_drc_temp.makeup[i] * 20 / Math.Log(10, 2));
                    }
                }
            }

            //threshold
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)

                {
                    x_threshold[i][j] = (three_band_drc_temp.threshold[i] * 20 / Math.Log(10, 2));
                    y_threshold[i][j] = 8 * j - 60;
                }
            }



            compressor_LB.CurveList.Clear();
            compressor_LB.GraphObjList.Clear();
            compressor_MB.CurveList.Clear();
            compressor_MB.GraphObjList.Clear();
            compressor_HB.CurveList.Clear();
            compressor_HB.GraphObjList.Clear();

            PointPairList list_LB = new PointPairList();
            PointPairList list_MB = new PointPairList();
            PointPairList list_HB = new PointPairList();
            PointPairList threshold_LB = new PointPairList();
            PointPairList threshold_MB = new PointPairList();
            PointPairList threshold_HB = new PointPairList();

            for (int j = 0; j < 81; j++)
                {
                    list_LB.Add(x_data[0][j], y_data[0][j]);
                    list_MB.Add(x_data[1][j], y_data[1][j]);
                    list_HB.Add(x_data[2][j], y_data[2][j]);
                    
            }
            for (int j = 0; j < 10; j++)
            {
                threshold_LB.Add(x_threshold[0][j], y_threshold[0][j]);
                threshold_MB.Add(x_threshold[1][j], y_threshold[1][j]);
                threshold_HB.Add(x_threshold[2][j], y_threshold[2][j]);
            }
                //frequency_response.LineType.;
                LineItem comp_LB_curve = compressor_LB.AddCurve(null, list_LB, Color.Blue, SymbolType.None);
            LineItem comp_LB_threshold = compressor_LB.AddCurve(null, threshold_LB, Color.LightGray, SymbolType.None);
            this.Refresh();
            LineItem comp_MB_curve = compressor_MB.AddCurve(null, list_MB, Color.Blue, SymbolType.None);
            LineItem comp_MB_threshold = compressor_MB.AddCurve(null, threshold_MB, Color.LightGray, SymbolType.None);
            this.Refresh();
            LineItem comp_HB_curve = compressor_HB.AddCurve(null, list_HB, Color.Blue, SymbolType.None);
            LineItem comp_HB_threshold = compressor_HB.AddCurve(null, threshold_HB, Color.LightGray, SymbolType.None);
            this.Refresh();
        }


    }
}
