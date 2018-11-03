using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using NAudio.Wave;
using NAudio.Utils;
using System.Threading;


namespace audioplayer_with_EQ_MBDRC
{
    public class audio_player_with_effect_class
    {
        BufferedWaveProvider buffprovider; // buffer after signal processing
        WaveFormat waveFormatOut;
        WaveOutEvent wo;            // player for playing the song
        //DirectSoundOut wo;
        MediaFoundationReader audioreader_temp;
        signalconverter sig_conv_temp;
        BackgroundWorker bgw_temp;
   

        effect_EQ equalizer_temp;
        Effect_3band_DRC three_band_drc_temp;
        FFT_class real_time_fft_temp;


        public int bufferlen;
        public int frame_counter = 0;
        public long initPos = 0;
        public long tempPos = 0;
        public bool pause_flag = false;
        public bool stop_flag = true;
        //for judgeing which wo is use 
        public bool wo_flag = true;
        public int play_current_time;
        //for music playing and pausing scroll 
        public bool play_time_changed = false;


        //signal processing input 
        public int in_total_byte_per_frame;
        public int out_total_byte_per_frame;
        public int Samples_per_frame;
        public int peak;
        public float volume=1;
    

        //debug
        public long wo_position_record_value;

        public audio_player_with_effect_class(FFT_class _real_time_fft,Effect_3band_DRC _three_band_drc,effect_EQ _equalizer, BackgroundWorker _bgw_temp, MediaFoundationReader audioreader,int _AudioLength, int _in_total_byte_per_frame, int _Samples_per_frame)
        {
            // MessageBox.Show("new audio class");

            audioreader_temp = audioreader;
            initPos = audioreader_temp.Position;
            //MessageBox.Show(initPos.ToString());
            bgw_temp = _bgw_temp;

            sig_conv_temp = new signalconverter(audioreader_temp.WaveFormat.Channels, audioreader_temp.WaveFormat.BitsPerSample / 8);
            waveFormatOut = new WaveFormat(audioreader_temp.WaveFormat.SampleRate, audioreader_temp.WaveFormat.BitsPerSample, audioreader_temp.WaveFormat.Channels);

            
        

            in_total_byte_per_frame = _in_total_byte_per_frame;
            out_total_byte_per_frame = in_total_byte_per_frame;
            Samples_per_frame = _Samples_per_frame;
            bufferlen = _AudioLength;

            ///effect
            ///
            equalizer_temp = _equalizer;
            three_band_drc_temp = _three_band_drc;
            real_time_fft_temp = _real_time_fft;


            // equalizer = new effect_EQ(audioreader_temp.WaveFormat.SampleRate, Samples_per_frame, audioreader_temp.WaveFormat.Channels);


        }
        public void StartPlaying()
        {


            if (pause_flag == true && stop_flag == false ) //for pause resume
            {
                ////////for clear already existing frame that processed
                if (play_time_changed == true)
                {

                        BufferClear();
                        waveFormatOut = new WaveFormat(audioreader_temp.WaveFormat.SampleRate, audioreader_temp.WaveFormat.BitsPerSample, audioreader_temp.WaveFormat.Channels);
                        buffprovider = new BufferedWaveProvider(waveFormatOut);
                        buffprovider.BufferLength = (int)Math.Floor((double)audioreader_temp.Length);
                        bufferlen = buffprovider.BufferLength;
                        wo = new WaveOutEvent();
                        wo.DesiredLatency = 100;
                        wo.Init(buffprovider);
                        frame_counter = 0;
                        tempPos = 0;
                        Thread.Sleep(20);

                }
                play_time_changed = false;
                wo.Play();
 
               
            }

            else
            {

                
                buffprovider = new BufferedWaveProvider(waveFormatOut);
                buffprovider.BufferLength = (int)Math.Floor((double)audioreader_temp.Length);
                bufferlen = buffprovider.BufferLength;
                wo = new WaveOutEvent();
                wo.DesiredLatency = 100;
                wo.Init(buffprovider);

                Thread.Sleep(20);

                signal_processing();
                play_time_changed = false;

                Thread.Sleep(20);

                wo.Play();
                
                tempPos = GetPlayerPos();

                while (tempPos <= bufferlen - initPos && stop_flag != true)
                {
                    
                    if (tempPos > frame_counter )
                    {

                        //test
                        play_current_time = ((int)(tempPos + initPos ) / (int)audioreader_temp.WaveFormat.Channels / (int)audioreader_temp.WaveFormat.SampleRate / ((int)audioreader_temp.WaveFormat.BitsPerSample / 8));
                        //play_current_time = (int) tempPos;

                       
                        bgw_temp.ReportProgress(play_current_time);   

                        signal_processing();

                        frame_counter = frame_counter + in_total_byte_per_frame;
                        tempPos = GetPlayerPos();
                        wo.Volume = volume;

                    }
                    else
                    {
                          tempPos = GetPlayerPos();
                        Thread.Sleep(1);
                      
                    }
                    
                    if (pause_flag == true)
                    {
                     
                        tempPos = 0;
                        frame_counter = 0;
                     }
                    
                }

                

                StopPlaying();
                stop_flag = true;
                pause_flag = false;
                
                frame_counter = 0;
                tempPos = 0;
                play_time_changed = false;


            }
        }
        public void StopPlaying()
        {
                wo.Stop();
        }
        public void PausePlayer()
        {
                wo.Pause();
        }

        public void BufferAddSample(byte[] _outbuffer)
        {
            buffprovider.AddSamples(_outbuffer, 0, _outbuffer.Length);
             
        }

        public long GetPlayerPos()
        {
                return wo.GetPosition();
            
        }
        public void PlayerClear()
        {
            if (wo != null)
            {
                wo.Stop();
                wo.Dispose();
                wo = null;
                //MessageBox.Show("dispose");
            }
        }
        public void BufferClear()
        {
            if (buffprovider != null)
            {
                buffprovider.ClearBuffer();
                buffprovider = null;
               
            }
        }


        //input buffer data and effect processing
        public void signal_processing()
        {
            double max = 0;
            double[][] InProcessbuffer0 = new double[audioreader_temp.WaveFormat.Channels][];
            double[][] OutProcessbuffer = new double[audioreader_temp.WaveFormat.Channels][];
            double[][] temp_buffer = new double[audioreader_temp.WaveFormat.Channels][];
            byte[] InBuffer = new byte[in_total_byte_per_frame];
            byte[] OutBuffer = new byte[out_total_byte_per_frame];  // buffer for loading the raw data

            for (int ch = 0; ch < audioreader_temp.WaveFormat.Channels; ch++)
            {
                InProcessbuffer0[ch] = new double[Samples_per_frame];
                OutProcessbuffer[ch] = new double[Samples_per_frame];
                temp_buffer[ch] = new double[Samples_per_frame]; 
            }
         
                audioreader_temp.Read(InBuffer, 0, (int)in_total_byte_per_frame);
           
            sig_conv_temp.ByteToDouble(InBuffer, InProcessbuffer0);

            //find peak
            
            for (int k = 0; k < OutProcessbuffer[0].Length; k++)
            { 
                if (Math.Abs(InProcessbuffer0[0][k]) > max)
                {
                    max = Math.Abs(InProcessbuffer0[0][k]);
                }
            }

            
            peak =(int) (max* volume *120);
            if (peak >= 100)
            {
                peak = 100;
            }
            
            ////effect
            equalizer_temp.eq_filtering(InProcessbuffer0);
            temp_buffer = equalizer_temp.filter_out;
            //DRC
            if (three_band_drc_temp.DRC_enable == true)
            {
                three_band_drc_temp.drc_filtering(temp_buffer);
                for (int i = 0; i < OutProcessbuffer[0].Length; i++)
                {
                    for (int k = 0; k < audioreader_temp.WaveFormat.Channels; k++)
                    {
                        temp_buffer[k][i] = three_band_drc_temp.data_buffer[4][k][i];
                    }
                }
            }
            //FFT
            real_time_fft_temp.fft(temp_buffer);

            for (int i = 0; i < OutProcessbuffer[0].Length; i++)
            {
                for (int k = 0; k < audioreader_temp.WaveFormat.Channels; k++)
                {
                    OutProcessbuffer[k][i] = temp_buffer[k][i];
                }    
            }
            sig_conv_temp.ScalarBack(OutProcessbuffer);
            sig_conv_temp.DoubleToByte(OutBuffer, OutProcessbuffer);
            BufferAddSample(OutBuffer);

            InProcessbuffer0 = null;
            OutProcessbuffer = null;
            InBuffer = null;
            OutBuffer = null;
            

            

        }
        
        public void update_trackbar_position(int _scroll_time)
        {
            
            //temp_value = (long)((_scroll_time * (int)audioreader_temp.WaveFormat.Channels * (int)audioreader_temp.WaveFormat.SampleRate * ((int)audioreader_temp.WaveFormat.BitsPerSample / 8)));
            audioreader_temp.Position = (long)((_scroll_time * (int)audioreader_temp.WaveFormat.Channels * (int)audioreader_temp.WaveFormat.SampleRate * ((int)audioreader_temp.WaveFormat.BitsPerSample / 8)));
            //MessageBox.Show(temp_value.ToString());
            initPos = audioreader_temp.Position;

        }
        
    }
}
