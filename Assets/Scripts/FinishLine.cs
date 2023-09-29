using UnityEngine;


public class FinishLine : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem finishEffect;

    GameController gameController;

    private void Start()
    {
        gameController = FindAnyObjectByType<GameController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            finishEffect.Play();
            GetComponent<AudioSource>().Play();
            Invoke(nameof(Finish), loadDelay);
        }
    }

    void Finish()
    {
        gameController.CompleteLevel();
    }
}
