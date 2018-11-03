# audio player with effects

## Introduction
This is a mp3/wav player with effects that are including:
- 10 bands EQ
- three band DRC
- FFT

Player GUI:    
![Alt text](https://github.com/heyheychen/audio-player-with-effects/blob/master/pic/audioplayer_marked.png?raw=true)    
We can select a mp3 or wav file, text box will show the title of the song and total music time will be updated as well.   
It also provide volume control, peak meter and scroll bar for adjusting the playing current time of the song.   

10band EQ GUI:    
![Alt text](https://github.com/heyheychen/audio-player-with-effects/blob/master/pic/EQ.png?raw=true)  
Select a filter type and fill out desired values of gain, frequency, Q factor, the frequency response will update automatically, click apply button to apply the EQ effect to the music.   
filter types are:   
- peaking
- high shelf
- low shelf
- 1st order high pass
- 1st order low pass
- 2nd order high pass
- 2nd order low pass
- notch
- bypass    

Three band DRC GUI:   
![Alt text](https://github.com/heyheychen/audio-player-with-effects/blob/master/pic/MBDRC.png?raw=true)  
It is three band dynamic range control, we can seperate music frequency into low-band, mid-band, and high-band by setting split frequencys. Each bands provides RMS and peak mode and a check box for enable or disable DRC effect.

FFT GUI:    
![Alt text](https://github.com/heyheychen/audio-player-with-effects/blob/master/pic/FFT.png?raw=true)  
It is a real time FFT, 4 types of FFT window are provided:   
- rectangular
- hamming
- hann
- blackman

## Software flow
![Alt text](https://github.com/heyheychen/audio-player-with-effects/blob/master/pic/audio%20player%20flow.png?raw=true)  


## Reference
1. NAudio library
2. Zedgraph library
