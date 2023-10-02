using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityManager : MonoBehaviour
{
    public AudioSource Alarm;
    public Product ProductType;

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.GetProductValue(ProductType) > 0.0f)
        {
            if (Alarm.isPlaying)
            {
                Alarm.Stop();
            }
        }
        else
        {
            if (!Alarm.isPlaying)
            {
                Alarm.Play();
            }
        }
    }
}
