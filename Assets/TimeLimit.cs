using UnityEngine;
using TMPro;

public class TimeLimit : MonoBehaviour
{
    [Header("Timer Settings")]
    public float startTime = 30f;
    public float timeRemaining;
    public bool timerIsRunning = true;

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;   // Assign in Inspector
    public TextMeshProUGUI winnerText;     // Assign in Inspector

    [Header("Win Condition")]
    public int scoreToWin = 10;

    private bool challengeEnded = false;
    private Transform player;

    void Start()
    {
        timeRemaining = startTime;
        UpdateTimerDisplay(timeRemaining);

        // Hide end-game texts at start
        if (gameOverText != null) gameOverText.gameObject.SetActive(false);
        if (winnerText != null) winnerText.gameObject.SetActive(false);

        // Find player by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
        // Press ENTER to restart after end
        if (challengeEnded && Input.GetKeyDown(KeyCode.Return))
        {
            RestartTimer();
        }

        if (!timerIsRunning) return;

        // WIN CONDITION
        if (GameManager.instance != null && GameManager.instance.score >= scoreToWin)
        {
            timerIsRunning = false;
            ChallengeComplete();
            return;
        }

        // TIMER COUNTDOWN
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay(timeRemaining);
        }
        else
        {
            timeRemaining = 0;
            timerIsRunning = false;
            TimeUp();
        }
    }

    void UpdateTimerDisplay(float time)
    {
        timerText.text = "Time: " + Mathf.CeilToInt(time);
    }

    void TimeUp()
    {
        Debug.Log("TIME UP - PRESS ENTER TO RESTART");
        challengeEnded = true;
        Time.timeScale = 0f;

        if (gameOverText != null) gameOverText.gameObject.SetActive(true);
        if (winnerText != null) winnerText.gameObject.SetActive(false);
    }

    void ChallengeComplete()
    {
        Debug.Log("YOU WIN - PRESS ENTER TO RESTART");
        challengeEnded = true;
        Time.timeScale = 0f;

        if (winnerText != null) winnerText.gameObject.SetActive(true);
        if (gameOverText != null) gameOverText.gameObject.SetActive(false);
    }

    public void RestartTimer()
    {
        Time.timeScale = 1f;
        timeRemaining = startTime;
        timerIsRunning = true;
        challengeEnded = false;

        if (GameManager.instance != null)
            GameManager.instance.score = 0;

        if (player != null)
            player.position = Vector2.zero;

        if (gameOverText != null) gameOverText.gameObject.SetActive(false);
        if (winnerText != null) winnerText.gameObject.SetActive(false);

        UpdateTimerDisplay(timeRemaining);
    }
}
