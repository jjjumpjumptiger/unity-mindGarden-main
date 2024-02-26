using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMicrophoneManager : MonoBehaviour
{
    //Set these values in the Unity Editor
    [SerializeField] private RawImage cameraDisplay;
    public static CameraMicrophoneManager Instance;

    private WebCamTexture webcamTexture;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy the GameObject when loading a new scene
            RequestCameraAndMicrophonePermission();
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    private void RequestCameraAndMicrophonePermission()
    {
        // Request camera permission
        Application.RequestUserAuthorization(UserAuthorization.WebCam);

        // Request microphone permission
        Application.RequestUserAuthorization(UserAuthorization.Microphone);

        // Check if the user granted permission
        if (Application.HasUserAuthorization(UserAuthorization.WebCam) && Application.HasUserAuthorization(UserAuthorization.Microphone))
        {
            StartCamera();
            StartMicrophone();
        }
        else
        {
            Debug.LogWarning("Camera and/or microphone access not granted by the user.");
        }
    }

    private void StartCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length > 0)
        {
            webcamTexture = new WebCamTexture(devices[0].name, Screen.width, Screen.height, 30);
            webcamTexture.Play();

            if (cameraDisplay != null)
            {
                cameraDisplay.texture = webcamTexture;
            }
        }
        else
        {
            Debug.LogWarning("No available webcams found.");
        }
    }

    private void StartMicrophone()
    {
        // Start recording from the default microphone
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = Microphone.Start(null, true, 10, AudioSettings.outputSampleRate);
        audioSource.loop = true;

        // Wait for microphone to start recording
        while (!(Microphone.GetPosition(null) > 0)) { }

        // Play the audio source to start recording
        audioSource.Play();
    }

    void OnDisable()
    {
        // Stop the webcam and microphone when the script is disabled or the game exits
        if (webcamTexture != null)
        {
            webcamTexture.Stop();
        }

        Microphone.End(null);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
