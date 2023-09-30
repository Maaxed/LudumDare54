using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DoorController;

public class DoorController : MonoBehaviour
{
    public GameObject Door;
    public float OpeningTime = 1.0f;
    public float CloseingTime = 1.0f;
    public DoorStatus DoorState = DoorStatus.Closed;

    public IEnumerator Open()
    {
        if (DoorState == DoorStatus.Open)
            yield break;

        DoorState = DoorStatus.Opening;
        yield return new WaitForSeconds(OpeningTime);

        Door.SetActive(false);
        DoorState = DoorStatus.Open;
    }

    public IEnumerator Close()
    {
        if (DoorState == DoorStatus.Closed)
            yield break;

        DoorState = DoorStatus.Closing;
        yield return new WaitForSeconds(CloseingTime);

        Door.SetActive(true);
        DoorState = DoorStatus.Closed;
    }

    public enum DoorStatus
    {
        Open,
        Closed,
        Opening,
        Closing
    }
}
