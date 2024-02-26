using UnityEngine;
using System.IO;

public static class WavUtility
{
    public static void FromAudioClip(string filePath, AudioClip audioClip, int offsetSamples = 0, int lengthSamples = -1)
    {
        if (lengthSamples == -1)
            lengthSamples = audioClip.samples;

        float[] data = new float[lengthSamples];
        audioClip.GetData(data, offsetSamples);

        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
        {
            WriteWavHeader(writer, audioClip.channels, audioClip.frequency, audioClip.samples);
            ConvertAndWrite(writer, data);
        }
    }

    private static void ConvertAndWrite(BinaryWriter writer, float[] samples)
    {
        int maxValue = 32767; // Max value for a 16-bit PCM WAV file

        for (int i = 0; i < samples.Length; i++)
        {
            writer.Write((short)(samples[i] * maxValue));
        }
    }

    private static void WriteWavHeader(BinaryWriter writer, int channels, int frequency, int sampleCount)
    {
        writer.Write(new char[4] { 'R', 'I', 'F', 'F' });
        writer.Write(36 + sampleCount * channels * 2); // 36 + sampleCount * channels * 2
        writer.Write(new char[4] { 'W', 'A', 'V', 'E' });
        writer.Write(new char[4] { 'f', 'm', 't', ' ' });
        writer.Write(16); // 16 for PCM
        writer.Write((short)1); // 1 for PCM
        writer.Write((short)channels);
        writer.Write(frequency);
        writer.Write(frequency * channels * 2); // Bytes per second
        writer.Write((short)(channels * 2)); // Block align
        writer.Write((short)16); // Bits per sample
        writer.Write(new char[4] { 'd', 'a', 't', 'a' });
        writer.Write(sampleCount * channels * 2); // Size of the data section
    }
}
