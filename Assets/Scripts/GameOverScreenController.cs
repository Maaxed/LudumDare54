using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreenController : MonoBehaviour
{
    public Button newGameButton;
    public TextMeshProUGUI text;

    private const string textContent = "You died because ";

    // Start is called before the first frame update

    void Start()
    {
        newGameButton.onClick.AddListener(enterGame);
        text.text = textContent + GameController.Instance.GetLoseReason() + ".";
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void enterGame()
    {
        SceneManager.LoadScene("SampleSceneMaaxed");
    }
}
