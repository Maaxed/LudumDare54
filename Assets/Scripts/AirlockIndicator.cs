using UnityEngine;

public class AirlockIndicator : MonoBehaviour
{
    public AirlockController AirlockController;
    public Transform IndicatorBar;
    public Renderer IndicatorModel;
    public Gradient ColorGradient;

    private void Update()
    {
        float value = 1.0f - AirlockController.Progress;
        Color color = ColorGradient.Evaluate(value);
        IndicatorBar.localScale = new Vector3(1.0f, Mathf.Max(0.025f, value), 1.0f);
        IndicatorModel.material.color = color;
        IndicatorModel.material.SetColor("_EmissionColor", color);
    }
}
