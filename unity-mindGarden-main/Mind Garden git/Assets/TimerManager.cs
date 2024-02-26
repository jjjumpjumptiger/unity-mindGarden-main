using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    // Add other timer-related variables as needed

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            
        }
    }
}
