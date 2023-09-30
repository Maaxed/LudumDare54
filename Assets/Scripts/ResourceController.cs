using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private Resource resource;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Resource GetResource() {
        return resource;
    }
}
