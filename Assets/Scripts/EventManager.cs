using UnityEngine;

public class EventManager : MonoBehaviour
{
    [Header("Asteroid Field")]
    public float EnterAsteroidFieldTime = 0.0f;
    public float AsteroidFieldResistenceTime = 0.0f;
    public Product ShieldProduct;
    public AudioSource AsteroidAlarm;

    private float CurrentTime = 0.0f;
    public bool AsteroidFieldEventActive = false;

    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;

        if (CurrentTime > EnterAsteroidFieldTime)
        {
            AsteroidFieldEventActive = true;
        }

        if (AsteroidFieldEventActive)
        {
            if (CurrentTime > EnterAsteroidFieldTime + AsteroidFieldResistenceTime)
            {
                if (!GameController.Instance.IsProductEnabled(ShieldProduct))
                {
                    GameController.Instance.LoseGame("your spaceship has been destroyed by asteroids");
                }
            }

            if (GameController.Instance.IsProductEnabled(ShieldProduct))
            {
                if (AsteroidAlarm.isPlaying)
                {
                    AsteroidAlarm.Stop();
                }
            }
            else
            {
                if (!AsteroidAlarm.isPlaying)
                {
                    AsteroidAlarm.Play();
                }
            }
        }
    }

    public void ToggleShield()
    {
        if (AsteroidFieldEventActive)
        {
            GameController.Instance.EnableProduct(ShieldProduct);
        }
    }
}
