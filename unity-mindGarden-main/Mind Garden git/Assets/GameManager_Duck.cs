using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_Duck : MonoBehaviour
{
    public static GameManager_Duck Instance;

    public duckSpawnSciript_new spawner; // Assign this in the inspector
    public GameObject topBarPanel; // Panel containing the life, level and time
    public Text timerText; // UI Text to display the timer
    public Text levelText; // Text element to display current level
    public Text lifeText; // Text element to display left life
    public GameObject questionPanel; // Panel containing the question and input field
    public Text questionText; // Text for the question
    public InputField answerInput; // Input field for the answer
    public Button submitButton; // Button to submit the answer
    public GameObject feedbackPanel; // Panel containing the feedback
    public Text feedbackText; // Text to display feedback
    public Button nextLevelButton; // Button to proceed to next level
    public Button retryLevelButton; // Button to retry the current level
    public Button quitGameButton; // Button to quit game

    //public GameObject gameOverPanel; // Panel to show when out of lives
    public GameObject dialoguePanel;

    public GameObject colorPanel;
    

    public GameObject dialogueBackground;
    public GameObject gameBackground;

    public int currentLevel = 1;
    public int playerLives = 3;
    public static int HighestLevelPassed = 0;
    private float roundTime;
    private bool roundActive;
    private string[] questions = new string[]
    {
        "How many ducks moved from left to right?",
        "How many ducks moved from right to left?"
    };

    private float[] timeLimits = new float[] { 20f, 15f, 10f }; // Time limits for each level
    private float[] duckSpeedRanges = new float[] { 3f, 4f, 6f }; // Max speeds for each level


    void AskRandomQuestion()
    {
        int randomIndex = Random.Range(0, questions.Length);
        questionText.text = questions[randomIndex];
    }

    void ShowDialogueBackground()
    {
        dialogueBackground.SetActive(true);
        gameBackground.SetActive(false);
    }

    void ShowGameBackground()
    {
        dialogueBackground.SetActive(false);
        gameBackground.SetActive(true);
    }


    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        submitButton.onClick.AddListener(CheckAnswer);
        nextLevelButton.onClick.AddListener(NextLevel);
        retryLevelButton.onClick.AddListener(RetryCurrentLevel);
        //quitGameButton.onClick.AddListener(startColoring);

        quitGameButton.onClick.AddListener(() => {
            if (currentLevel == 1)
            {
                // If the user is still on level 1 (not passed), load SampleScene
                SceneManager.LoadScene("SampleScene");
            }
            else
            {
                // Otherwise, invoke the startColoring method
                startColoring();
            }
        });

        ShowDialogueBackground();
        HideAllPanels();
        dialoguePanel.SetActive(true);
        spawner.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(StartRound());
        //StartLevel(currentLevel);
    }

    void HideAllPanels()
    {
        topBarPanel.SetActive(false);
        questionPanel.SetActive(false);
        feedbackPanel.SetActive(false);
        dialoguePanel.SetActive(false);
        colorPanel.SetActive(false);
    }

    public void StartGameplay()
    {
        // Logic to start or resume the game
        // This could mean enabling player controls, starting timers, etc.
        HideAllPanels(); // Hide UI panels like dialogue box, etc.
        topBarPanel.SetActive(true);
        spawner.enabled = true;
        ShowGameBackground();
        StartLevel(currentLevel);
    }

    void StartLevel(int level)
    {
        questionPanel.SetActive(false);
        feedbackPanel.SetActive(false);
        answerInput.text = "";
        currentLevel = level;
        spawner.maxSpeed = duckSpeedRanges[level - 1];
        roundTime = timeLimits[level - 1];
        //playerLives = 3; // Reset lives at the start of each level
        levelText.text = "Level " + currentLevel;
        lifeText.text = "Lives: " + playerLives;
        // Reset duck counts
        spawner.countLeftToRight = 0;
        spawner.countRightToLeft = 0;
        questionPanel.SetActive(false); // Hide the question panel
        spawner.enabled = true; // Enable the duck spawner
        StartCoroutine(StartRound());
    }

    void NextLevel()
    {
        

        if (currentLevel < timeLimits.Length)
        {
            playerLives = 3; // Reset lives at the start of each level
            StartLevel(currentLevel);
        }
        else
        {
            // Implement what happens when all levels are completed (e.g., show final win screen)
        }
    }

    void RetryCurrentLevel()
    {
        if (playerLives > 0)
        {
            StartLevel(currentLevel);
        }
    }

    IEnumerator StartRound()
    {
        roundActive = true;
        float timer = roundTime;

        while (timer > 0 && roundActive)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F2");
            yield return null;
        }

        if (roundActive)
        {
            EndRound();
        }
    }

    void EndRound()
    {
        roundActive = false;
        spawner.enabled = false; // Stop spawning ducks
        questionPanel.SetActive(true);
        AskRandomQuestion(); // Call this method to display a random question
    }

    void ShowFeedbackOptions(bool hasNextLevel, bool canRetry, bool isCorrect)
    {
        nextLevelButton.gameObject.SetActive(hasNextLevel && isCorrect);
        quitGameButton.gameObject.SetActive(true);
        retryLevelButton.gameObject.SetActive(canRetry && !isCorrect);
    }

    void CheckAnswer()
    {
        int playerAnswer;
        feedbackPanel.SetActive(true);

        if (int.TryParse(answerInput.text, out playerAnswer))
        {
            bool isCorrect = false;
            if (questionText.text == questions[0]) // "How many ducks moved from left to right?"
            {
                isCorrect = (playerAnswer == spawner.countLeftToRight);
            }
            else if (questionText.text == questions[1]) // "How many ducks moved from right to left?"
            {
                isCorrect = (playerAnswer == spawner.countRightToLeft);
            }

            if (isCorrect)
            {
                feedbackText.text = "Correct Answer!";
                feedbackText.color = Color.green;
                if (currentLevel > HighestLevelPassed)
                {
                    HighestLevelPassed = currentLevel;
                }
                currentLevel++;
                
                ShowFeedbackOptions(currentLevel <= timeLimits.Length, false, isCorrect);
            }
            else
            {
                playerLives--; // Decrement lives
                feedbackText.text = "Incorrect Answer. You have " + playerLives + " lives left.";
                feedbackText.color = Color.red;
                ShowFeedbackOptions(false, playerLives > 0, isCorrect);

            }
        }
        else
        {
            feedbackText.text = "Please enter a valid number.";
            feedbackText.color = Color.yellow;
        }
    }

    void startColoring()
    {
        colorPanel.SetActive(true);
    }

    void EndGame()
    {
        // Implement logic to end the game, such as loading the main menu or closing the application
        // For example, to load a main menu scene: SceneManager.LoadScene("MainMenuSceneName");
        // To quit the game (works only in built game, not in the Unity Editor): Application.Quit();
    }


}
