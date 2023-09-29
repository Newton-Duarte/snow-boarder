using UnityEngine;

public class DustTrail : MonoBehaviour
{
    [SerializeField] ParticleSystem dustTrailEffect;
    [SerializeField] AudioClip snowboardingSFX;

    AudioSource audioSource;
    GameController gameController;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameController = FindAnyObjectByType<GameController>();
    }

    void Update()
    {
        if (gameController.gameStatus == GameStatus.Title && audioSource.isPlaying)
        {
            StopEffect();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && gameController.gameStatus == GameStatus.Play)
        {
            audioSource.clip = snowboardingSFX;
            audioSource.loop = true;
            audioSource.Play();

            dustTrailEffect.Play();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            StopEffect();
        }
    }

    public void StopEffect()
    {
        audioSource.Stop();
        audioSource.clip = null;
        audioSource.loop = false;

        dustTrailEffect.Stop();
    }
}
