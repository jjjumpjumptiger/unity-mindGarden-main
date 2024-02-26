using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using UnityEngine.UI;

public class RecordingManager : MonoBehaviour
{
    private WebCamTexture webcamTexture;
    private AudioClip audioClip;


    //This example captures frames from webcamTexture and audio from audioClip and stores them in a list. 
    private List<float> audioSamples;
    private List<Texture2D> videoFrames;

    // UI button for stopping recording in the End Scene
    public Button stopRecordingButton;
    private bool isRecording = false;

    // Singleton pattern to ensure only one instance exists
    private static RecordingManager instance;

    public static RecordingManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<RecordingManager>();
                if (instance == null)
                {
                    GameObject recordingManagerObj = new GameObject("RecordingManager");
                    instance = recordingManagerObj.AddComponent<RecordingManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        // Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeCameraAndMicrophone();
        
    }

    void InitializeCameraAndMicrophone()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length > 0)
        {
            webcamTexture = new WebCamTexture(devices[0].name, Screen.width, Screen.height, 30);
            webcamTexture.Play();
        }
        else
        {
            UnityEngine.Debug.LogWarning("No available webcams found.");
        }

        // Adjust as needed for audio settings
        audioClip = Microphone.Start(null, true, 10, AudioSettings.outputSampleRate);
    }

    public void StartRecording()
    {
        if (!isRecording)
        {
            StartRecordingLogic();
            isRecording = true;
        }
    }

    public void StopRecording()
    {
        if (isRecording)
        {
            StopRecordingLogic();
            isRecording = false;
        }
    }

    void StartRecordingLogic()
    {
        videoFrames = new List<Texture2D>();
        audioSamples = new List<float>();
        isRecording = true;
        // Start recording logic
        // Example: Capture frames and audio samples in update
        InvokeRepeating("CaptureFrameAndAudio", 0f, 0.0333f); // Assuming 30 frames per second
    }

    void StopRecordingLogic()
    {
        // Stop recording logic
        isRecording = false;
        // Example: Stop capturing frames and audio samples in update
        CancelInvoke("CaptureFrameAndAudio");

        // Example: Save recorded frames and audio to file
        SaveRecording();
    }

    void CaptureFrameAndAudio()
    {
        // Capture frame
        Texture2D frame = new Texture2D(webcamTexture.width, webcamTexture.height);
        frame.SetPixels(webcamTexture.GetPixels());
        frame.Apply();
        videoFrames.Add(frame);

        // Capture audio sample
        float[] samples = new float[audioClip.samples];
        audioClip.GetData(samples, 0);
        audioSamples.AddRange(samples);
    }

    void SaveRecording()
    {
        // Combine frames and audio samples into a video file (e.g., using FFmpeg)
        CombineFramesAndAudio(videoFrames, audioSamples);

    }

    void CombineFramesAndAudio(List<Texture2D> frames, List<float> audioSamples)
    {
        // Combine frames and audio using FFmpeg
        // Save frames as PNG images
        for (int i = 0; i < frames.Count; i++)
        {
            byte[] bytes = frames[i].EncodeToPNG();
            File.WriteAllBytes($"frame_{i}.png", bytes);
        }

        // Save audio samples as WAV file
        WavUtility.FromAudioClip("audio.wav", audioClip, 0, audioClip.samples);

        // Combine frames and audio using FFmpeg
        string ffmpegPath = Path.Combine(Application.streamingAssetsPath, "Assets/ExternalTools/ffmpeg");
        string command = $"-framerate 30 -i frame_%d.png -i audio.wav -c:v libx264 -c:a aac -strict experimental -b:a 192k output.mp4";

        Process process = new Process();
        process.StartInfo.FileName = "ffmpeg";
        process.StartInfo.Arguments = command;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.CreateNoWindow = true;

        process.Start();
        process.WaitForExit();

        //Optional: Delete temporary files
        for (int i = 0; i < frames.Count; i++)
        {
            File.Delete($"frame_{i}.png");
        }
        File.Delete("audio.wav");

        // Same as before
    }

    // Add FFmpeg integration here
    public void StopRecordingOnClick()
    {
        StopRecordingLogic();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRecording)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "EndScene")
            {
                // Enable the UI button for stopping recording in the End Scene
                stopRecordingButton.gameObject.SetActive(true);

                // Check if the "TemporaryStopButton" is clicked in the "EndScene"
                // Check if the "TemporaryStopButton" is clicked in the "EndScene"
            GameObject temporaryStopButtonObj = GameObject.Find("TemporaryStopButton");

            if (temporaryStopButtonObj != null)
            {
                Button temporaryStopButton = temporaryStopButtonObj.GetComponent<Button>();

                if (temporaryStopButton != null)
                {
                    temporaryStopButton.onClick.AddListener(() =>
                    {
                        // Handle stopping logic here
                        StopRecordingLogic();
                    });
                }
            }

            }
            else
            {
                // Disable the UI button for stopping recording in other scenes
                stopRecordingButton.gameObject.SetActive(false);
            }
        }
    }

}
