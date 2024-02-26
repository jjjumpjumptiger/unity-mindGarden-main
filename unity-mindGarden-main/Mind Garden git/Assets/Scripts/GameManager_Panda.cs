using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Panda : MonoBehaviour
{
    public static GameManager_Panda Instance;
    public GameObject dialogueBackground;
    public GameObject dialoguePanel;
    public GameObject colorPanel;
    public Canvas canvas;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        colorPanel.SetActive(false);

    }
    // Start is called before the first frame update
    public void StartGame()
    {
        dialogueBackground.SetActive(false);
        dialoguePanel.SetActive(false); 
        canvas.gameObject.SetActive(true);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
