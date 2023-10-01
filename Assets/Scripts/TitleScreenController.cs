using UnityEngine;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public Button enterButton;
    
    private bool entering = false;

    // Start is called before the first frame update

    void Start()
    {
        enterButton.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (entering) {
            leftDoor.transform.Translate(Vector3.left * 1000 * Time.deltaTime);
            rightDoor.transform.Translate(Vector3.right * 1000 * Time.deltaTime);
        }
    }

    void OnClick()
    {
        entering = true;
    }
}
