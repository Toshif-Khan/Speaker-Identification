# Speaker-Identification
It is a WPF C# project through this we can enroll a speaker and identify the speaker voice using Microsoft Cognitive Services.


In this project we are trying to identify the voice. To do this first we need to start recording event to record our voice by using a microphone. But one thing is to remember is that, Speaker recognition API accept only .wav file audio with the following parameters

Container	    : WAV
Encoding	    : PCM
Rate	        : 16K
Sample Format	: 16 bit
Channels	    : Mono

So I set this parameter before strating the recording in my code you can check it. 

First of all create a profile using speaker recognition API call and create a profile in your local (I created in en-us). Then after start recording and record your voice try to record a noiceless clear voice of atleast more than 5 second recommanded by me. Now your voice is registered. Now again record your voice to identify the speaker. Then we will send a request to speaker recognition API to identify our recorded voice, then API send response identified speaker id only. So you can check your speaker id against your enroll id which was created at the enrollment time.  
