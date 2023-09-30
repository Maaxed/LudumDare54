using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 1.0f;
    private Rigidbody rb;
    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        if (velocity.sqrMagnitude >= 1)
            velocity.Normalize();
    }

    void FixedUpdate()
    {
        rb.velocity = rb.rotation * velocity * Speed;
    }
}
