using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
  [SerializeField] private List<Mole> moles;

  [Header("UI objects")]
  [SerializeField] private GameObject playButton;
  [SerializeField] private GameObject gameUI;
  [SerializeField] private GameObject outOfTimeText;
  [SerializeField] private GameObject toNextLevelText;
  [SerializeField] private GameObject currentLevelFailText;
  [SerializeField] private GameObject bombText;
  [SerializeField] private TMPro.TextMeshProUGUI timeText;
  [SerializeField] private TMPro.TextMeshProUGUI scoreText;

  [SerializeField] private GameObject levelCompletePanel;
  [SerializeField] private GameObject nextLevelButton;
  [SerializeField] private GameObject returnToHomeButton;
  [SerializeField] private GameObject retryButton;
 

  // Hardcoded variables you may want to tune.
  private float startingTime = 30f;


  // Global variables
  private float timeRemaining;
  private HashSet<Mole> currentMoles = new HashSet<Mole>();
  private int score;
  private bool playing = false;

  public static GameManager Instance;
  private int currentLevel = 1; // Track the current level

  //public GameObject gameOverPanel; // Panel to show when out of lives
  public GameObject dialoguePanel;
  public Button returnToHome; // Button to quit game
  public GameObject colorPanel;


  public GameObject dialogueBackground;
  // This is public so the play button can see it.
  public void StartGame() {
    // Hide/show the UI elements we don't/do want to see.
    dialogueBackground.SetActive(false);

    dialoguePanel.SetActive(false);
    playButton.SetActive(false);
    outOfTimeText.SetActive(false);
    bombText.SetActive(false);
    gameUI.SetActive(true);
    levelCompletePanel.SetActive(false);
    nextLevelButton.SetActive(false);
    returnToHomeButton.SetActive(false);
    toNextLevelText.SetActive(false);
    currentLevelFailText.SetActive(false);

    // Hide all the visible moles.
    for (int i = 0; i < moles.Count; i++) {
      moles[i].Hide();
      moles[i].SetIndex(i);
    }
    // Remove any old game state.
    currentMoles.Clear();
    // Start with 30 seconds.
    timeRemaining = startingTime;
    score = 0;
    scoreText.text = "0";
    playing = true;
  }

  public void GameOver(int type)
  {
    if (type == 0)
    {
        outOfTimeText.SetActive(true);
    }
    else
    {
        bombText.SetActive(true);
    }

    foreach (Mole mole in moles)
    {
        mole.StopGame();
    }

    playing = false;

    if (score >= GetRequiredScoreForNextLevel())
    {
        levelCompletePanel.SetActive(true);
        toNextLevelText.SetActive(true);
        if (currentLevel < 3)
        {
            nextLevelButton.SetActive(true);
        }
        else
        {
            nextLevelButton.SetActive(false);
        }

        //retryButton.SetActive(true); // Show the retry button
        returnToHomeButton.SetActive(true);
    }
    else
    {
        currentLevelFailText.SetActive(true);
        levelCompletePanel.SetActive(true);
        retryButton.SetActive(true); // Show the retry button
        returnToHomeButton.SetActive(true);
    }
  }

  public void Retry()
  {
      StartGame(); // Restart the current level
  }
  // Update is called once per frame
  void Update() {
    if (playing) {
      // Update time.
      timeRemaining -= Time.deltaTime;
      if (timeRemaining <= 0) {
        timeRemaining = 0;
        GameOver(0);
      }
      timeText.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";
      // Check if we need to start any more moles.
      if (currentMoles.Count <= (score / 10)) {
        // Choose a random mole.
        int index = Random.Range(0, moles.Count);
        // Doesn't matter if it's already doing something, we'll just try again next frame.
        if (!currentMoles.Contains(moles[index])) {
          currentMoles.Add(moles[index]);
          moles[index].Activate(score / 10);
        }
      }
    }
  }

  public void AddScore(int moleIndex) {
    // Add and update score.
    score += 1;
    scoreText.text = $"{score}";
    // Increase time by a little bit.
    timeRemaining += 1;
    // Remove from active moles.
    currentMoles.Remove(moles[moleIndex]);
  }

  public void Missed(int moleIndex, bool isMole) {
    if (isMole) {
      // Decrease time by a little bit.
      timeRemaining -= 2;
    }
    // Remove from active moles.
    currentMoles.Remove(moles[moleIndex]);
  }


  void startColoring()
  {
      colorPanel.SetActive(true);
  }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        returnToHome.onClick.AddListener(startColoring);
        colorPanel.SetActive(false);

    }
  public void NextLevel()
    {
        currentLevel++;
        StartGame();
    }
  private int GetRequiredScoreForNextLevel()
    {
        switch (currentLevel)
        {
            case 1:
                return 60;
            case 2:
                return 75;
            case 3:
                return 80;
            default:
                return 0;
        }
    }
}
