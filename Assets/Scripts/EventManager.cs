using UnityEngine;

public class EventManager : MonoBehaviour
{
    public float EnterAsteroidFieldTime = 0.0f;
    public Product ShieldProduct;
    public AudioSource AsteroidAlarm;

    private float CurrentTime = 0.0f;
    private bool AsteroidFieldEventActive = false;

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
        GameController.Instance.EnableProduct(ShieldProduct);
    }
}
