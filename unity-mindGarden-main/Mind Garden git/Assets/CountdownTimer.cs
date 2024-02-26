using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CountdownTimer : MonoBehaviour
{
    public float maxTime = 30f;
    public float timeRemaining;
    [SerializeField] TextMeshProUGUI countdownText;

    private bool isTimerPaused = false;

    void Start()
    {
        // Load the timer state from PlayerPrefs
        if (PlayerPrefs.HasKey("TimeRemaining"))
        {
            timeRemaining = PlayerPrefs.GetFloat("TimeRemaining");
        }else{
            timeRemaining = maxTime;
        }

        UpdateUI();
    }


    void Update()
    {
        if (!isTimerPaused)
        {

            if(timeRemaining > 0){
                timeRemaining -= Time.deltaTime;
            }else{
                timeRemaining = 30;
                Debug.Log("Times out");
            }

            
            UpdateUI();
            
        }
    }

    void UpdateUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
      
        if(timeRemaining == 0){
            countdownText.text = string.Format("{0:00}:{1:00}", 0, 0);
        }else{
            countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        
    }

    public void PauseTimer()
    {
        isTimerPaused = true;
    }

    public void ResumeTimer()
    {
        isTimerPaused = false;
    }
}
