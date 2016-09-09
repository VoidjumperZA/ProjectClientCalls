using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour {

    [SerializeField]
    private Button _startGame;
    [SerializeField]
    private Button _exitGame;

    public void OnStart()
    {
        SceneManager.LoadScene(2);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
