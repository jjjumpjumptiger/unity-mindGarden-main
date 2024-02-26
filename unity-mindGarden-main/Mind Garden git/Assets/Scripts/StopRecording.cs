using UnityEngine;
using UnityEngine.UI;

public class StopRecording : MonoBehaviour
{
    private Button stopRecordingButton;

    private void Start()
    {
        // Find the Button component on the GameObject
        stopRecordingButton = GetComponent<Button>();

        // Add a click event listener to the button
        if (stopRecordingButton != null)
        {
            stopRecordingButton.onClick.AddListener(OnStopRecordingClick);
        }
    }

    private void OnStopRecordingClick()
    {
        // Call the StopRecordingOnClick method of the RecordingManager
        RecordingManager.Instance.StopRecording();
    }
}
