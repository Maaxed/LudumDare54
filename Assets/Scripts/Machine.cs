using UnityEngine;

[CreateAssetMenu(fileName = "Machine", menuName = "Scriptable/Machine")]
public class Machine : ScriptableObject
{
    public string label;
    public Resource inputResource;
    public int outputQuantity;
    public Product outputProduct;
}
