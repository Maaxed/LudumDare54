using UnityEngine;

public class PlayerStepController : MonoBehaviour
{
    public PlayerController Player;
    public AudioSource StepAudio;
    public AudioClip[] StepSounds;
    public float StepDist = 1.0f;
    private float CurrentDist = 0.0f;

    private void Awake()
    {
        if (Player == null)
        {
            Player = GetComponentInParent<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        float speed = Mathf.Min(1.0f, velocity.magnitude);

        if (Player != null )
        {
            speed *= Player.Speed;
        }

        CurrentDist += speed * Time.deltaTime;

        if (CurrentDist >= StepDist / 2.0f)
        {
            CurrentDist -= StepDist;
            StepAudio.PlayOneShot(StepSounds[Random.Range(0, StepSounds.Length)]);
        }
    }
}
