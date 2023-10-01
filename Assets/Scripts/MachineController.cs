using UnityEngine;

public class MachineController : MonoBehaviour
{
    [SerializeField] private Machine machine;
    public AudioSource UseAudio;

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
        ResourceController resourceController = other.GetComponentInParent<ResourceController>();
        if (resourceController == null)
            return;

        if (resourceController.GetResource() != machine.inputResource)
            return;

        if (!GameController.Instance.IsProductEnabled(machine.outputProduct))
            return;

        GameController.Instance.AddProduct(machine.outputProduct, machine.outputQuantity);
        if (UseAudio != null)
        {
            UseAudio.Play();
        }
        Destroy(resourceController.gameObject);
    }
    
}
