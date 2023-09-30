using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{
    public Product ProductType;
    public Transform IndicatorBar;
    public Renderer IndicatorModel;
    public Gradient ColorGradient;

    private void Update()
    {
        float value = GameController.Instance.GetProductValue(ProductType) / ProductType.maxValue;
        Color color = ColorGradient.Evaluate(value);
        IndicatorBar.localScale = new Vector3(1.0f, Mathf.Max(0.025f, value), 1.0f);
        IndicatorModel.material.color = color;
        IndicatorModel.material.SetColor("_EmissionColor", color);
    }
}
