using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AirlockController : MonoBehaviour
{
    public DoorController InteriorDoor;
    public DoorController ExteriorDoor;
    public float OpenTime = 1.0f;
    public float ClosedTime = 1.0f;
    public float DestroyTime = 1.0f;
    public Vector3 EjectionForce;
    private bool EjectingItems = false;

    private void Start()
    {
        StartCoroutine(Run());
    }

    public IEnumerator Run()
    {
        while (true)
        {
            yield return PlayCycle();
        }
    }

    public IEnumerator PlayCycle()
    {
        yield return new WaitForSeconds(ClosedTime);

        GenerateItems();

        yield return InteriorDoor.Open();
        yield return new WaitForSeconds(OpenTime);
        yield return InteriorDoor.Close();

        yield return ExteriorDoor.Open();
        EjectingItems = true;
        yield return new WaitForSeconds(DestroyTime);
        EjectingItems = false;
        yield return ExteriorDoor.Close();
    }

    public void GenerateItems()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (!EjectingItems)
            return;

        Rigidbody rb = other.GetComponentInParent<Rigidbody>();
        if (rb == null)
            return;

        if (rb.CompareTag("Player") || rb.CompareTag("Item"))
        {
            rb.AddForce(EjectionForce);

            // No gravity / drag in space
            rb.useGravity = false;
            rb.drag = 0.0f;
            rb.angularDrag = 0.0f;

            PlayerController player = other.GetComponentInParent<PlayerController>();

            if (player != null)
            {
                player.enabled = false;
            }
        }
    }
}
