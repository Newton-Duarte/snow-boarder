using System;
using UnityEngine;
using TMPro;

public enum GameStatus
{
    Title,
    Play
}

public class GameController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject canvas;
    [SerializeField] TextMeshProUGUI txtScore;

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

    void Start()
    {
        effector2D.speed = 0f;
        txtScore.text = score.ToString();
        PlayTitleClip();
    }

    public void PlayGame()
    {
        canvas.SetActive(false);
        effector2D.speed = effectorSpeed;
        gameStatus = GameStatus.Play;
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

    public void GetCoin()
    {
        score++;
        txtScore.text = score.ToString();
        sfxSource.PlayOneShot(coinClip);
    }
}
