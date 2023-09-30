using UnityEngine;

public class MachineController : MonoBehaviour
{
    [SerializeField] private Machine machine;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Item")) {
            ResourceController resourceController = other.GetComponentInParent<ResourceController>();
            if (resourceController != null && resourceController.GetResource() == machine.inputResource) {
                GameController.Instance.AddProduct(machine.outputProduct, machine.outputQuantity);
                Destroy(resourceController.gameObject);
            }
        }
        
    }
    
}
