using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public Button enterButton;
    public Button quitButton;
    
    private bool entering = false;
    private AsyncOperation loadOperation;

    // Start is called before the first frame update

    void Start()
    {
        enterButton.onClick.AddListener(enterGame);
        quitButton.onClick.AddListener(quitGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (entering && loadOperation.progress >= 1) {
            leftDoor.transform.Translate(Vector3.left * 1000 * Time.deltaTime);
            rightDoor.transform.Translate(Vector3.right * 1000 * Time.deltaTime);
            if (leftDoor.transform.position.x <= 0) {
                entering = false;
                SceneManager.UnloadSceneAsync("TitleScreen");
            }
        }
    }

    void enterGame()
    {
        entering = true;
        loadOperation = SceneManager.LoadSceneAsync("SampleSceneMathilde", LoadSceneMode.Additive);
    }

    void quitGame() {
        Application.Quit();
    }
}
