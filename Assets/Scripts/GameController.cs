using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum GameStatus
{
    Title,
    Play
}

public class GameController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject titleCanvas;
    [SerializeField] GameObject hudCanvas;
    [SerializeField] GameObject scoreCanvas;
    [SerializeField] TextMeshProUGUI txtScore;

    [Header("UI Score Panel")]
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI pointsText;

    [Header("Effector")]
    [SerializeField] SurfaceEffector2D effector2D;
    [SerializeField] float effectorSpeed = 25f;

    [Header("Audio")]
    [SerializeField] AudioClip titleClip;
    [SerializeField] AudioClip gameClip;
    [SerializeField] AudioClip coinClip;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource sfxSource;

    int score = 0;

    public GameStatus gameStatus = GameStatus.Title;

    Stopwatch stopwatch;

    void Start()
    {
        titleCanvas.SetActive(true);
        hudCanvas.SetActive(false);
        scoreCanvas.SetActive(false);
        stopwatch = FindAnyObjectByType<Stopwatch>();
        effector2D.speed = 0f;
        txtScore.text = score.ToString();
        PlayTitleClip();
    }

    public void PlayGame()
    {
        titleCanvas.SetActive(false);
        hudCanvas.SetActive(true);
        effector2D.speed = effectorSpeed;
        gameStatus = GameStatus.Play;
        stopwatch.StartTimer();
        PlayGameClip();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    void PlayTitleClip()
    {
        audioSource.clip = titleClip;
        audioSource.Play();
    }

    void PlayGameClip()
    {
        audioSource.clip = gameClip;
        audioSource.Play();
    }

    public void SetScore(int points = 1)
    {
        score += points;
        txtScore.text = score.ToString();
        sfxSource.PlayOneShot(coinClip);
    }

    public void CompleteLevel()
    {
        stopwatch.StopTimer();
        effector2D.speed = 0;
        gameStatus = GameStatus.Title;
        timeText.SetText(stopwatch.GetTime());
        pointsText.SetText(score.ToString());
        hudCanvas.SetActive(false);
        scoreCanvas.SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
