using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertIconController : MonoBehaviour
{
    public Product ProductType;
    public Renderer IconRenterer;
    public float StartWarningValue = 1.0f;
    public Gradient ColorGradient;

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.IsProductEnabled(ProductType))
        {
            float value = GameController.Instance.GetProductValue(ProductType);
            if (value < StartWarningValue)
            {
                Color color = ColorGradient.Evaluate(value / ProductType.maxValue);
                IconRenterer.enabled = true;
                IconRenterer.material.color = color;
                IconRenterer.material.SetColor("_EmissionColor", color);
            }
            else
            {
                IconRenterer.enabled = false;
            }

        }
        else
        {
            IconRenterer.enabled = false;
        }
    }
}
