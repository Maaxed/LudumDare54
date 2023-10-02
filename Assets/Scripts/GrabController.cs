using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    public Rigidbody GrabbedObject;
    public GameObject Crosshair;
    public float Force = 1.0f;
    public float ThrowForce = 1.0f;
    public float RotationForce = 1.0f;
    public float MaxGrabDistance = 10.0f;
    public float HoldDistance = 3.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            if (GrabbedObject != null)
            {
                if (Input.GetAxis("Vertical") > 0.0001f)
                    GrabbedObject.AddForce(transform.forward * ThrowForce, ForceMode.VelocityChange);
                GrabbedObject = null;
            }
        }

        bool showCrosshair = false;
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, MaxGrabDistance))
        {
            if (hitInfo.rigidbody != null && hitInfo.rigidbody.gameObject.CompareTag("Item"))
            {
                showCrosshair = true;
                if (Input.GetButtonDown("Fire1"))
                {
                    GrabbedObject = hitInfo.rigidbody;
                }
            }
            else
            {
                ButtonController button = hitInfo.collider.GetComponentInParent<ButtonController>();
                if (button != null)
                {
                    showCrosshair = true;
                    if (Input.GetButtonDown("Fire1") && button.OnClocked != null)
                    {
                        button.OnClocked.Invoke();
                    }
                }
            }
        }
        if (Crosshair != null)
        {
            Crosshair.SetActive(showCrosshair);
        }
    }

    void FixedUpdate()
    {
        if (GrabbedObject != null)
        {
            float holdDistance = HoldDistance;
            GrabObject grabObject = GrabbedObject.GetComponentInParent<GrabObject>();
            if (grabObject != null)
            {
                holdDistance = grabObject.HoldDistance;
            }

            Vector3 rot = transform.rotation.eulerAngles;
            float pitch = Mathf.DeltaAngle(0.0f, rot.x);
            Vector3 targetPos = transform.position + transform.forward * holdDistance;
            if (pitch > 0.0f) // When looking down
            {
                pitch = Mathf.Min(45.0f, pitch);
                rot.x = pitch;

                holdDistance /= Mathf.Cos(Mathf.Deg2Rad * pitch);
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
