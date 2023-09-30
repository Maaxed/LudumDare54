using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    public Transform GrabPosition;
    public Rigidbody GrabbedObject;
    public float Force = 1.0f;
    public float MaxGrabDistance = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            GrabbedObject = null;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, MaxGrabDistance))
            {
                GrabbedObject = hitInfo.rigidbody;
            }
        }

    }

    void FixedUpdate()
    {
        if (GrabbedObject != null)
        {
            GrabbedObject.velocity = (GrabPosition.position - GrabbedObject.position) * Force;
        }
    }
}
