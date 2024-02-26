using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRecording : MonoBehaviour
{
    public RecordingManager recordingManager;

    public void OnStartButtonClick()
    {
        recordingManager.StartRecording();
    }


}
