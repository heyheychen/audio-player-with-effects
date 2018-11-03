using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace audioplayer_with_EQ_MBDRC
{
    class signalconverter 
    {
            int AudioChannelNum;
            int AudioBytePerSample;
            private
            int MaxBytePerSample = 4;
            int MaxBitPerSample = 32;
            int RedundantByte;
            public signalconverter(int _channelnum, int _bytepersample)
            {
                AudioChannelNum = _channelnum;
                AudioBytePerSample = _bytepersample;
                RedundantByte = MaxBytePerSample - AudioBytePerSample;
            }
            public void ByteToDouble(byte[] _BufferByte, double[][] _BufferDouble)
            {
                for (int i = 0; i < _BufferDouble[0].Length; i++)
                {
                    for (int ch = 0; ch < AudioChannelNum; ch++)
                    {
                        byte[] temp = new byte[MaxBytePerSample];
                        for (int z = 0; z < (RedundantByte); z++)
                            temp[z] = 0;
                        for (int k = 0; k < AudioBytePerSample; k++)
                            temp[k + (RedundantByte)] = _BufferByte[AudioChannelNum * AudioBytePerSample * i + ch * AudioBytePerSample + k];
                        _BufferDouble[ch][i] = (double)BitConverter.ToInt32(temp, 0) / Math.Pow(2, MaxBitPerSample - 1);
                        temp = null;
                    }
                }
                _BufferByte = null;
            }
            public void ScalarBack(double[][] bufferDouble)
            {
                double scalar = Math.Pow(2, MaxBitPerSample - 1);
                for (int i = 0; i < bufferDouble.Length; i++)
                {
                    for (int j = 0; j < bufferDouble[i].Length; j++)
                        bufferDouble[i][j] *= scalar;
                }
            }
            public void DoubleToByte(byte[] _buffer, double[][] _bufferDouble)
            {
                double Max = Math.Pow(2, MaxBitPerSample - 1) - 1;
                double Min = -Math.Pow(2, MaxBitPerSample - 1);
                int OutQuantBytes = AudioBytePerSample / 8;
                for (int ch = 0; ch < AudioChannelNum; ch++)
                {
                    for (int i = 0; i < _bufferDouble[0].Length; i++)
                    {
                        if (_bufferDouble[ch][i] > Max)
                            _bufferDouble[ch][i] = Max;
                        else if (_bufferDouble[ch][i] < Min)
                            _bufferDouble[ch][i] = Min;

                        int result = Convert.ToInt32(_bufferDouble[ch][i]);
                        byte[] byteArray = BitConverter.GetBytes(result);
                        for (int n = 0; n < AudioBytePerSample; n++)
                            _buffer[AudioChannelNum * AudioBytePerSample * i + ch * AudioBytePerSample + n] = byteArray[n + 4 - AudioBytePerSample];
                        byteArray = null;
                    }
                }
            }
        
    }
}
