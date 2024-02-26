using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
    //     Debug.Log("SceneLoader Start");

    // // Check if TimerManager is null
    //     if (TimerManager.Instance == null)
    //     {
    //         Debug.Log("TimerManager is null");
    //     }else{
            
           
    //         var countdownTimer = TimerManager.Instance.GetComponent<CountdownTimer>();
    //         if (countdownTimer != null)
    //         {
    //             // countdownTimer.PauseTimer();
    //             Debug.Log("CountdownTimer Paused");
    //         }
    //         else
    //         {
    //             Debug.Log("CountdownTimer is null");
    //         }
    //     }
        // Example of pausing the timer when switching scenes
        //TimerManager.Instance.GetComponent<CountdownTimer>().PauseTimer();
    }
    
    public void LoadScene(string sceneName)
    {
        // var countdownTimer = TimerManager.Instance.GetComponent<CountdownTimer>();
        // if(sceneName != "SampleScene"){
        //     countdownTimer.PauseTimer();
        // }else{
        //     PlayerPrefs.SetFloat("TimeRemaining", countdownTimer.timeRemaining);
        //     countdownTimer.ResumeTimer();
        // }
       // PlayerPrefs.SetFloat("TimeRemaining", TimerManager.Instance.GetComponent<CountdownTimer>().timeRemaining);

        SceneManager.LoadScene(sceneName);
    //     if (TimerManager.Instance != null)
    // {
        

    //     // if (countdownTimer != null)
    //     // {
    //     //     PlayerPrefs.SetFloat("TimeRemaining", countdownTimer.timeRemaining);
    //     // }
    //     // else
    //     // {
    //     //     Debug.Log("CountdownTimer component is null");
    //     // }
    // }
    // else
    // {
    //     Debug.Log("TimerManager is null");
    // }



        // // if(sceneName == "SampleScene"){
        // PlayerPrefs.SetFloat("TimeRemaining", TimerManager.Instance.GetComponent<CountdownTimer>().timeRemaining);
        // // }
        
        // SceneManager.LoadScene(sceneName);
    }
    // void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     // Example of resuming the timer when the new scene is loaded
    //     TimerManager.Instance.GetComponent<CountdownTimer>().ResumeTimer();
    //     // var countdownTimer = TimerManager.Instance.GetComponent<CountdownTimer>();

    //     // if (scene.name == "SampleScene")
    //     // {
    //     //     // Resume the timer when switching back to Scene1
    //     //     countdownTimer.ResumeTimer();
    //     // }
    //     // else
    //     // {
    //     //     // Load the remaining time from PlayerPrefs when switching to Scene1
    //     //     if (PlayerPrefs.HasKey("TimeRemaining"))
    //     //     {
    //     //         countdownTimer.timeRemaining = PlayerPrefs.GetFloat("TimeRemaining");
    //     //     }
    //     // }
    // }
}