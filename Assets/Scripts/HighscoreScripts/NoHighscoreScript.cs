using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NoHighscoreScript : MonoBehaviour {

    private bool selectionAxisInUse = false;
    private bool returnAxisInUse = false;
    private int _score;

    [SerializeField]
    private Text _scoreText;
    
    private void Start()
    {
        _score = PlayerPrefs.GetInt("score");
        _scoreText.text = "Your score: " + _score;
    }

    private void Update()
    {
        checkAxisCommands("Jump", ref selectionAxisInUse);
        checkAxisCommands("Fire1", ref returnAxisInUse);
    }

    private void checkAxisCommands(string pAxisName, ref bool pAxisToggle)
    {
        if (Input.GetAxisRaw(pAxisName) != 0) //if we've pressed button
        {
            if (pAxisToggle == false) //if the key is not pressed down
            {
                switch (pAxisName)
                {
                    case "Jump":
                        PlayerPrefs.SetString("Replay", "True");
                        Time.timeScale = 1f;
                        SceneManager.LoadScene(7);
                        break;
                    case "Fire1":
                        Time.timeScale = 1f;
                        SceneManager.LoadScene(0);
                        break;
                }
                pAxisToggle = true; //key is now 'down'
            }
        }
        //key has now been lifted up
        if (Input.GetAxisRaw(pAxisName) == 0)
        {
            pAxisToggle = false;
        }
    }
}
