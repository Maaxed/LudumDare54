using UnityEngine;

[CreateAssetMenu(fileName = "Product", menuName = "Scriptable/Product")]
public class Product : ScriptableObject
{
    public string label;
    public float consumptionSpeed;
    public float initialValue;
    public float maxValue = 1.0f;
    public bool isVital = false;
}
