
using NAudio.CoreAudioApi;
using NAudio.Wave;
using Vosk;

Model model = new Model(@"path\to\model");

using (var waveIn = new WaveInEvent())
{
    waveIn.WaveFormat = new WaveFormat(44100, 1);

    var rec = new VoskRecognizer(model, waveIn.WaveFormat.SampleRate);
    rec.SetMaxAlternatives(0);
    rec.SetWords(true);

    waveIn.DataAvailable += (_, e) =>
    {
        
        if (rec.AcceptWaveform(e.Buffer, e.BytesRecorded))
        {
            Console.WriteLine(rec.Result());
        }
        else
        {
            Console.WriteLine(rec.PartialResult());
        }

    };
    waveIn.StartRecording();
    Console.WriteLine("Press ENTER to quit...");
    Console.ReadLine();
    waveIn.StopRecording();
}

Console.WriteLine("Program ended successfully.");