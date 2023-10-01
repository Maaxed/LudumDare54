using UnityEngine;
using UnityEngine.UI;

public class QuitButtonController : MonoBehaviour
{
    public Button quitButton;

    void Start()
    {
        quitButton.onClick.AddListener(QuitGame);
    }

    void QuitGame() 
    {
        Application.Quit();
    }
}
