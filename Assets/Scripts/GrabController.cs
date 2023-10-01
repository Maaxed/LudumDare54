using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    public Rigidbody GrabbedObject;
    public float Force = 1.0f;
    public float RotationForce = 1.0f;
    public float MaxGrabDistance = 10.0f;
    public float HoldDistance = 3.0f;

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
                if (hitInfo.rigidbody != null && hitInfo.rigidbody.gameObject.CompareTag("Item"))
                {
                    GrabbedObject = hitInfo.rigidbody;
                }
            }
        }

    }

    void FixedUpdate()
    {
        if (GrabbedObject != null)
        {
            Vector3 rot = transform.rotation.eulerAngles;
            float pitch = Mathf.DeltaAngle(0.0f, rot.x);
            Vector3 targetPos = transform.position + transform.forward * HoldDistance;
            if (pitch > 0.0f) // When looking down
            {
                pitch = Mathf.Min(45.0f, pitch);
                rot.x = pitch;

                float holdDistance = HoldDistance / Mathf.Cos(Mathf.Deg2Rad * pitch);
                targetPos = transform.position + Quaternion.Euler(rot) * Vector3.forward * holdDistance;
            }
            GrabbedObject.velocity = (targetPos - GrabbedObject.position) * Force;

            Vector3 anglDiff = Quaternion.Inverse(GrabbedObject.rotation).eulerAngles;

            anglDiff.x = Mathf.DeltaAngle(0.0f, anglDiff.x);
            anglDiff.y = 0.0f;
            anglDiff.z = Mathf.DeltaAngle(0.0f, anglDiff.z);

            GrabbedObject.angularVelocity = anglDiff * RotationForce;
        }
    }
}
