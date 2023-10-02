using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Camera;
    public float Speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isFocused)
            return;

        float horizontal = Input.GetAxis("CameraHorizontal");
        if (Mathf.Abs(horizontal) > 0.001)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0.0f, horizontal * Speed, 0.0f));
        }
        float vertical = Input.GetAxis("CameraVertical");
        if (Mathf.Abs(vertical) > 0.001)
        {
            Vector3 newRotation = Camera.rotation.eulerAngles + new Vector3(vertical * Speed, 0.0f, 0.0f);
            newRotation.x = Mathf.Clamp(Mathf.DeltaAngle(0.0f, newRotation.x), -90.0f, 90.0f);
            Camera.rotation = Quaternion.Euler(newRotation);
        }
    }
}
