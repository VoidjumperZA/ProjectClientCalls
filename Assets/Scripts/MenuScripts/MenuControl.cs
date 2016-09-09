using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour {

    [SerializeField]
    private Button _startGame;
    [SerializeField]
    private Button _exitGame;

    public void LoadScene(int pSceneIndex)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(pSceneIndex);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
