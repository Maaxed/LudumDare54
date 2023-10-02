using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricityManager : MonoBehaviour
{
    public AudioSource Alarm;
    public Product ProductType;
    public Material LightMaterial;
    public Material WhiteLightMaterial;
    public Material RedLightMaterial;
    private bool IsAlarmOn = false;

    private void Start()
    {
        LightMaterial.CopyMatchingPropertiesFromMaterial(WhiteLightMaterial);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.GetProductValue(ProductType) > 0.0f)
        {
            if (IsAlarmOn)
            {
                Alarm.Stop();
                IsAlarmOn = false;
                LightMaterial.CopyMatchingPropertiesFromMaterial(WhiteLightMaterial);
            }
        }
        else
        {
            if (!IsAlarmOn)
            {
                Alarm.Play();
                IsAlarmOn = true;
                LightMaterial.CopyMatchingPropertiesFromMaterial(RedLightMaterial);
            }
        }
    }
}
