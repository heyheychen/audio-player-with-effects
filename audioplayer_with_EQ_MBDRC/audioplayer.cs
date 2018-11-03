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
using System.Numerics;
using NAudio.Wave;



namespace audioplayer_with_EQ_MBDRC
{

    public partial class audioplayer_UI_form : Form
    {   


        //////////initial parameters and objects//////////////////
        MediaFoundationReader Audio_reader;
        BackgroundWorker bgw_play;
        audio_player_with_effect_class audio_player;
        //new a audio reader (open)
        OpenFileDialog open_temp;
        // trackbar_no_focus_cue trackbar_playing_time;

        EQ_form eq_form;
        effect_EQ equalizer;

        DRC_form drc_form;
        Effect_3band_DRC three_band_drc;

        FFT_class real_time_fft;
        FFT_form fft_form;

        string Audio_file_name;

        double frame_size = 56; //millisecond user defined, it will affect FFT sample num
        
        int Audio_sample_number;
        int Samples_per_frame;
        int In_total_byte_per_frame;
        int Out_total_byte_per_frame;
        double Music_total_time;
        public int track_bar_time;
        int sampling_rate;
        int audio_channel;
        public int volume_UI;
       
        

        public audioplayer_UI_form()
        {
            InitializeComponent();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

            equalizer = new effect_EQ(sampling_rate, Samples_per_frame, audio_channel);
            three_band_drc = new Effect_3band_DRC();
            real_time_fft = new FFT_class();





            //this.trackbar_playing_time = new trackbar_no_focus_cue();
            textBox1.Text = (string)"please select a audio file";
            Label_total_time.Text = (string)"00:00";
            Label_current_time.Text = (string)"00:00";
            trackbar_playing_time.Value = 0;
            start_playing_button.Enabled = false;
            Pause_button.Enabled = false;
            stop_button.Enabled = false;
            volume.Enabled = false;
               

        }


        public void openAudioFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //for second load music


            OpenFileDialog open = new OpenFileDialog();
            open_temp = open;

            open.Filter = "(*.wav;*.mp3)| *.wav; *.mp3";

            if (open.ShowDialog() != DialogResult.OK) return;

            if (audio_player != null && audio_player.stop_flag == false)
            {
                audio_player.StopPlaying();
            }

            Get_audio_file_information_and_refresh_reader();

          

            sampling_rate = Audio_reader.WaveFormat.SampleRate;
            int bits_per_sample = Audio_reader.WaveFormat.BitsPerSample;
            audio_channel = Audio_reader.WaveFormat.Channels;
            int buflen = (int)(Math.Floor((double)Audio_reader.Length));

            

            //refresh effect parameter
            
            equalizer.sampling_rate = sampling_rate;
            equalizer.samples_per_frame = Samples_per_frame;
            equalizer.audio_channels = audio_channel;
            equalizer.update_coef();
            equalizer.init_memory();
            three_band_drc.update_music_information(sampling_rate, Samples_per_frame, audio_channel);
            real_time_fft.ini_parameter(sampling_rate, Samples_per_frame, audio_channel);
            //



            bgw_play = new BackgroundWorker();
            
            audio_player = new audio_player_with_effect_class(real_time_fft, three_band_drc, equalizer, bgw_play, Audio_reader, buflen, In_total_byte_per_frame, Samples_per_frame);
            bgw_play.DoWork += new DoWorkEventHandler(bgw_start_playing);
            bgw_play.ProgressChanged += new ProgressChangedEventHandler(bgw_get_current_time);
            bgw_play.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_run_worker_complete);
            bgw_play.WorkerReportsProgress = true;
            bgw_play.WorkerSupportsCancellation = true;


            Label_current_time.Text = (string)"00:00";
            trackbar_playing_time.Value = 0;
            start_playing_button.Enabled = true;
            Pause_button.Enabled = false;
            stop_button.Enabled = false;
            volume.Enabled = true;
            volume_UI = volume.Value;
            audio_player.volume = (float)(((double)volume_UI) / 100);
            peak_meter.Value = 0;


        }

        private void start_playing_Click(object sender, EventArgs e)
        {
            start_playing_button.Enabled = false;
            Pause_button.Enabled = true;
            stop_button.Enabled = true;

           

            if (audio_player.pause_flag)
            {
   
                audio_player.StartPlaying();
                audio_player.stop_flag = false;
                audio_player.pause_flag = false;
                
            }
            else
            {

                bgw_play.RunWorkerAsync();
                
            }
            
        }


        public void bgw_start_playing(object sender, DoWorkEventArgs e)
        {
            audio_player.stop_flag = false;
            audio_player.pause_flag = false;
            audio_player.StartPlaying();
  
        }

        private void Pause_Click(object sender, EventArgs e)
        {

            audio_player.pause_flag = true;
            audio_player.PausePlayer();
            start_playing_button.Enabled = true;
            Pause_button.Enabled = false;

            peak_meter.Value = 0;
        }


        private void stop_Click(object sender, EventArgs e)
        {
            audio_player.StopPlaying();

            audio_player.pause_flag = false;
            audio_player.stop_flag = true;

            //delay for background playing loop to finish the break event
            Thread.Sleep(30);
            start_playing_button.Enabled = true;
            Pause_button.Enabled = false;
            stop_button.Enabled = false;

            Label_current_time.Text = (string)"00:00";
            trackbar_playing_time.Value = 0;
            audio_player.update_trackbar_position(0);
            audio_player.frame_counter = 0;
            audio_player.tempPos = 0;
            audio_player.BufferClear();
            audio_player.play_time_changed = false;

            peak_meter.Value = 0;

        }
    


        public void bgw_run_worker_complete(object sender, RunWorkerCompletedEventArgs e)
        {
     
            Label_current_time.Text = (string)"00:00";
            trackbar_playing_time.Value = 0;
            audio_player.frame_counter = 0;
            audio_player.tempPos = 0;
            audio_player.BufferClear();
            audio_player.update_trackbar_position(0);
            audio_player.play_time_changed = false;
            start_playing_button.Enabled = true;
            Pause_button.Enabled = false;
            stop_button.Enabled = false;
            
            
        }

        public void bgw_get_current_time(object sender, ProgressChangedEventArgs e)
        {
            //abel_current_time.Text = e.ProgressPercentage.ToString();
            Thread.Sleep(1);
            trackbar_playing_time.Value = (int)Math.Floor ( (double)audio_player.play_current_time / (double)Music_total_time*(double) trackbar_playing_time.Maximum);
            Label_current_time.Text = (audio_player.play_current_time / 60).ToString("00") + ":" + (audio_player.play_current_time % 60).ToString("00");
            peak_meter.Value = audio_player.peak;
            
            //debug
            //label2.Text = trackbar_playing_time.Maximum.ToString();
            //label5.Text = Music_total_time.ToString();
            //label7.Text = audio_player.play_current_time.ToString();
            //label9.Text = trackbar_playing_time.Value.ToString();
            //label8.Text =audio_player.initPos.ToString();
            //label10.Text = audio_player.tempPos.ToString();
            //label3.Text = (audio_player.bufferlen - audio_player.initPos).ToString();
            //label4.Text = (audio_player.wo_position_record_value).ToString();
            //label6.Text = (audio_player.play_time_changed).ToString();
            //label11.Text = (audio_player.pause_flag).ToString();
            //label12.Text = (audio_player.stop_flag).ToString();
            //label13.Text = (audio_player.frame_counter).ToString();
            //label14.Text = (track_bar_time).ToString();
            //label15.Text = (audio_player.bufferlen - audio_player.initPos).ToString();
            //label17.Text = (Samples_per_frame).ToString();
            //label18.Text = (audio_channel).ToString();
            
  
        }




        private void Get_audio_file_information_and_refresh_reader()
        {
            Audio_reader = new NAudio.Wave.MediaFoundationReader(open_temp.FileName);
            Audio_file_name = open_temp.FileName.ToString();
            textBox1.Text = Path.GetFileNameWithoutExtension(Audio_file_name);

            sampling_rate = Audio_reader.WaveFormat.SampleRate;
            Audio_sample_number = (int)Audio_reader.Length / (Audio_reader.WaveFormat.BitsPerSample / 8 * Audio_reader.WaveFormat.Channels);
            //Initial Frame parameters 
            Samples_per_frame = (int)((double)Audio_reader.WaveFormat.SampleRate * (double)frame_size / (double)1000);
            In_total_byte_per_frame = Samples_per_frame * Audio_reader.WaveFormat.BitsPerSample / 8 * Audio_reader.WaveFormat.Channels;
            Out_total_byte_per_frame = Samples_per_frame * Audio_reader.WaveFormat.BitsPerSample / 8 * Audio_reader.WaveFormat.Channels;
            Music_total_time = Audio_reader.Length / (double)Audio_reader.WaveFormat.Channels / (double)Audio_reader.WaveFormat.SampleRate / ((double)Audio_reader.WaveFormat.BitsPerSample / 8);
            Label_total_time.Text = (Math.Floor(Music_total_time / 60)).ToString("00") + ":" + (Music_total_time % 60).ToString("00");

        }


        public int Set_label(int current_time_input)
        {
            Label_current_time.Text = (current_time_input / 60).ToString("00") + ":" + (current_time_input % 60).ToString("00");
            return current_time_input;
        }

        private void refesh_status_Click(object sender, EventArgs e)
        {
            /*
            label2.Text = trackbar_playing_time.Maximum.ToString();
            label5.Text = Music_total_time.ToString();
            label7.Text = audio_player.play_current_time.ToString();
            label9.Text = trackbar_playing_time.Value.ToString();
            label8.Text = audio_player.initPos.ToString();
            label10.Text = audio_player.tempPos.ToString();
            //label3.Text = (audio_player.bufferlen - audio_player.initPos).ToString();
            label3.Text = (audio_player.bufferlen ).ToString();
           // label4.Text = (audio_player.wo_position_record_value).ToString();
            label6.Text = (audio_player.play_time_changed).ToString();
            label11.Text = (audio_player.pause_flag).ToString();
            label12.Text = (audio_player.stop_flag).ToString();
            label13.Text = (audio_player.frame_counter).ToString();
            label14.Text = (track_bar_time).ToString();
            label15.Text = (audio_player.bufferlen - audio_player.initPos).ToString();
            label16.Text = (equalizer.coef_eq[0][0]).ToString("0.000");
            label17.Text = (equalizer.coef_eq[0][1]).ToString("0.000");
            label18.Text = (equalizer.coef_eq[0][2]).ToString("0.000");
            label19.Text = (equalizer.coef_eq[0][3]).ToString("0.000");
            label20.Text = (equalizer.coef_eq[0][4]).ToString("0.000");
            label21.Text = (equalizer.coef_eq[0][5]).ToString("0.000");
            
            */
        }


        private void trackbar_playing_time_Scroll(object sender, EventArgs e)
        {


            if (audio_player != null)
            {
                audio_player.tempPos = 0;

                if (audio_player.stop_flag == false && audio_player.pause_flag == false)  // if music is playing , pause music
                {

                    audio_player.play_time_changed = true;

                    audio_player.PausePlayer();

                    start_playing_button.Enabled = true;
                    Pause_button.Enabled = false;


                    Thread.Sleep(1); //let player and buffer clear
                }
                else if (audio_player.stop_flag == true && audio_player.pause_flag == false)
                {
                    audio_player.play_time_changed = true;

                }
                else if (audio_player.stop_flag == false && audio_player.pause_flag == true)
                {
                    audio_player.play_time_changed = true;

                }

                track_bar_time = (int)Math.Floor((double)Music_total_time * (double)trackbar_playing_time.Value / (double)(trackbar_playing_time.Maximum));

                Set_label(track_bar_time);

                peak_meter.Value = 0;

            }
        }

        private void trackbar_playing_time_KeyDown(object sender, KeyEventArgs e)
        {


            if (audio_player != null)
            {
                audio_player.tempPos = 0;

                if (audio_player.stop_flag == false && audio_player.pause_flag == false)  // if music is playing , pause music
                {

                    audio_player.play_time_changed = true;

                    audio_player.PausePlayer();

                    start_playing_button.Enabled = true;
                    Pause_button.Enabled = false;


                    Thread.Sleep(1); //let player and buffer clear
                }
                else if (audio_player.stop_flag == true && audio_player.pause_flag == false)
                {
                    audio_player.play_time_changed = true;

                }
                else if (audio_player.stop_flag == false && audio_player.pause_flag == true)
                {
                    audio_player.play_time_changed = true;

                }

                track_bar_time = (int)Math.Floor((double)Music_total_time * (double)trackbar_playing_time.Value / (double)(trackbar_playing_time.Maximum));

                Set_label(track_bar_time);

            }
        }

        private void trackbar_playing_time_KeyUp(object sender, KeyEventArgs e)
        {


            if (audio_player != null)
            {

                audio_player.update_trackbar_position(track_bar_time);

                if (audio_player.stop_flag == false && audio_player.pause_flag == false)  // if music is playing , pause music
                {
                    audio_player.stop_flag = false;
                    audio_player.pause_flag = true;
                }
                else if (audio_player.stop_flag == true && audio_player.pause_flag == false)
                {

                    audio_player.stop_flag = true;
                    audio_player.pause_flag = false;
                }
                else if (audio_player.stop_flag == false && audio_player.pause_flag == true)
                {

                    audio_player.stop_flag = false;
                    audio_player.pause_flag = true;
                }


            }
        }

        private void trackbar_playing_time_MouseDown(object sender, MouseEventArgs e)
        {


            if (audio_player != null)
            {
                audio_player.tempPos = 0;

                if (audio_player.stop_flag == false && audio_player.pause_flag == false)  // if music is playing , pause music
                {

                    audio_player.play_time_changed = true;

                    audio_player.PausePlayer();

                    start_playing_button.Enabled = true;
                    Pause_button.Enabled = false;


                    Thread.Sleep(1); //let player and buffer clear
                }
                else if (audio_player.stop_flag == true && audio_player.pause_flag == false)
                {
                    audio_player.play_time_changed = true;

                }
                else if (audio_player.stop_flag == false && audio_player.pause_flag == true)
                {
                    audio_player.play_time_changed = true;

                }

                track_bar_time = (int)Math.Floor((double)Music_total_time * (double)trackbar_playing_time.Value / (double)(trackbar_playing_time.Maximum));

                Set_label(track_bar_time);

            }
        }

        private void trackbar_playing_time_MouseUp(object sender, MouseEventArgs e)
        {


            if (audio_player != null)
            {

                audio_player.update_trackbar_position(track_bar_time);

                if (audio_player.stop_flag == false && audio_player.pause_flag == false)  // if music is playing , pause music
                {
                    audio_player.stop_flag = false;
                    audio_player.pause_flag = true;
                }
                else if (audio_player.stop_flag == true && audio_player.pause_flag == false)
                {

                    audio_player.stop_flag = true;
                    audio_player.pause_flag = false;
                }
                else if (audio_player.stop_flag == false && audio_player.pause_flag == true)
                {

                    audio_player.stop_flag = false;
                    audio_player.pause_flag = true;
                }


            }
        }
 
        private void volume_Scroll(object sender, EventArgs e)
        {
            volume_UI = volume.Value;
            audio_player.volume =(float) (((double)volume_UI) / 100);
        }


        private void EQ_parameter_Click(object sender, EventArgs e)
        {
            if (eq_form == null)
            {
                eq_form = new EQ_form(equalizer);
            }
            eq_form.ShowDialog();
        }

        private void MBDRC_Click(object sender, EventArgs e)
        {

            if (drc_form == null)
            {
                drc_form = new DRC_form(three_band_drc);
            }
            drc_form.ShowDialog();
        
/*
            double[][] xin = new double[2][];

            
                for (int i = 0; i < 2; i++)
            {
                xin[i] = new double[3969]
                {          };
            }
           
            three_band_drc.drc_filtering(xin);
            
            double[] low_output, mid_output, high_output,mix_out, mid_gain, mid_level;
            string[] low_output_s, mid_output_s, high_output_s, mix_out_s, mid_gain_s, mid_level_s;
            low_output_s = new string[3969];
            mid_output_s = new string[3969];
            high_output_s = new string[3969];
            mix_out_s = new string[3969];
            mid_gain_s = new string[3969];
            mid_level_s = new string[3969];

            low_output = new double[3969];
            mid_output = new double[3969];
            high_output = new double[3969];
            mix_out = new double[3969];
            mid_gain = new double[3969];
            mid_level = new double[3969];

            for (int i = 0; i < low_output.Length; i++)
            {
                low_output[i] = three_band_drc.data_buffer[1][1][i];
            mid_output[i] = three_band_drc.data_buffer[2][1][i];
            high_output[i] = three_band_drc.data_buffer[3][1][i];
            mix_out[i] = three_band_drc.data_buffer[4][1][i];
                mid_gain[i] = three_band_drc.gain_buffer[1][0][i];
                mid_level[i] = three_band_drc.current_xin_level[1][0][i];

                    low_output_s[i] = low_output[i].ToString();
                mid_output_s[i] = mid_output[i].ToString();
                high_output_s[i] = high_output[i].ToString();
                mix_out_s[i] = mix_out[i].ToString();
                mid_gain_s[i] = mid_gain[i].ToString();
                mid_level_s[i] = mid_level[i].ToString();

            }
            System.IO.File.WriteAllLines(@"\\Mac\Home\Desktop\work\matlab\mbdrc\lowband.txt", low_output_s);
            System.IO.File.WriteAllLines(@"\\Mac\Home\Desktop\work\matlab\mbdrc\midband.txt", mid_output_s);
            System.IO.File.WriteAllLines(@"\\Mac\Home\Desktop\work\matlab\mbdrc\highband.txt", high_output_s);
            System.IO.File.WriteAllLines(@"\\Mac\Home\Desktop\work\matlab\mbdrc\mixband.txt", mix_out_s);
            System.IO.File.WriteAllLines(@"\\Mac\Home\Desktop\work\matlab\mbdrc\midbandgain.txt", mid_gain_s);
            System.IO.File.WriteAllLines(@"\\Mac\Home\Desktop\work\matlab\mbdrc\midbandlevel.txt", mid_level_s);
            */
        }

        private void FFT_Click(object sender, EventArgs e)
        {
            if (fft_form == null)
            {
                fft_form = new FFT_form(real_time_fft);
            }
            
            fft_form.ShowDialog();



            /*
            double[][] xin_test1 = new double[2][];
            double[][] xin_test2 = new double[2][];
            double[][] xin_test3 = new double[2][];
            double[][] xin_test4 = new double[2][];
            double[][] xin_test5 = new double[2][];
            string[] xin_remap_string = new string[16];

            for (int i = 0; i < 2; i++)
            {
                xin_test1 [i]= new double[4] { 0, 1, 2 ,3};
                 xin_test2[i] = new double[4] { 4, 5 ,6,7};
                 xin_test3[i] = new double[4] { 8, 9, 10, 11 };
                xin_test4[i] = new double[4] { 12,13,14,15};             
            }

            real_time_fft.fft(xin_test1);
            real_time_fft.fft(xin_test2);
            real_time_fft.fft(xin_test3);
            real_time_fft.fft(xin_test4);
  
            
            for (int i = 0; i < 16; i++)
            {
                //xin_remap_string[i] = real_time_fft.xin[i].ToString();
                //MessageBox.Show(xin_remap_string[i]);
                 xin_remap_string[i] = (real_time_fft.fft_out[i].Real.ToString()+" " + real_time_fft.fft_out[i].Imaginary.ToString()+"i") ;
               MessageBox.Show(real_time_fft.fft_mag_out[i].ToString());
            }
            
            //System.IO.File.WriteAllLines(@"\\Mac\Home\Desktop\work\FFT\bit_reversal.txt", xin_remap_string);
            */
        }
    }
}
