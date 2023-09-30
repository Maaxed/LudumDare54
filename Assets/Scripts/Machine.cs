using UnityEngine;

[CreateAssetMenu(fileName = "Machine", menuName = "Scriptable/Machine")]
public class Machine : ScriptableObject
{
    public string label;
    public Resource inputResource;
    public float outputQuantity;
    public Product outputProduct;
}
