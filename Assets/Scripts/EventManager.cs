using UnityEngine;

public class EventManager : MonoBehaviour
{
    public float EnterAsteroidFieldTime = 0.0f;
    public Product ShieldProduct;

    private float CurrentTime = 0.0f;
    private bool AsteroidFieldEventActive = false;

    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;

        if (CurrentTime > EnterAsteroidFieldTime && !AsteroidFieldEventActive)
        {
            AsteroidFieldEventActive = true;
            GameController.Instance.EnableProduct(ShieldProduct);
        }
    }
}
