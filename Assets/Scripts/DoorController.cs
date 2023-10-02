using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator DoorAnimator;
    public float OpeningTime = 1.0f;
    public float CloseingTime = 1.0f;
    public DoorStatus DoorState = DoorStatus.Closed;

    public IEnumerator Open()
    {
        if (DoorState == DoorStatus.Open)
            yield break;

        DoorState = DoorStatus.Opening;

        DoorAnimator.SetBool("Open", true);

        while (DoorState != DoorStatus.Open)
            yield return null;
    }

    public IEnumerator Close()
    {
        if (DoorState == DoorStatus.Closed)
            yield break;

        DoorState = DoorStatus.Closing;

        DoorAnimator.SetBool("Open", false);

        while (DoorState != DoorStatus.Closed)
            yield return null;
    }

    public void OnFinishOpen()
    {
        DoorState = DoorStatus.Open;
    }

    public void OnFinishClose()
    {
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
