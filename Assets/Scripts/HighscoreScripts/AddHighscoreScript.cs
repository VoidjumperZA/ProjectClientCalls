using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AddHighscoreScript : MonoBehaviour
{

    private string[] _letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

    [SerializeField]
    private Text[] _uiTexts;

    [SerializeField]
    private Text _newScore;
    private int _score;

    private Text _selectedText;
    private int _index = 0;

    private bool xAxisInUse = false;
    private bool yAxisInUse = false;
    private bool selectionAxisInUse = false;
    private bool returnAxisInUse = false;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        checkAxisCommands("Horizontal", ref xAxisInUse);
        checkAxisCommands("Vertical", ref yAxisInUse);
        checkAxisCommands("Jump", ref selectionAxisInUse);
        checkAxisCommands("Fire1", ref returnAxisInUse);
    }

    private void Init()
    {
        for (int i = 0; i < _uiTexts.Length; i++)
        {
            _uiTexts[i].text = _letters[0];
        }

        if (_uiTexts.Length > 0)
        {
            _selectedText = _uiTexts[0];
        }
        _score = PlayerPrefs.GetInt("score");
        _newScore.text = "Score: " + _score;
        HighlightText();
    }



    //generic command check list, scanning all command axes at once
    //feed it the axis and bool that controls it's "KeyDown" status
    private void checkAxisCommands(string pAxisName, ref bool pAxisToggle)
    {
        if (Input.GetAxisRaw(pAxisName) != 0) //if we've pressed button
        {
            if (pAxisToggle == false) //if the key is not pressed down
            {
                switch (pAxisName)
                {
                    case "Horizontal":
                        xAxisCommands();
                        break;
                    case "Vertical":
                        yAxisCommands();
                        break;
                    case "Jump":
                        selectionAxisCommands();
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

    //input manager commands using horizontal keys
    private void xAxisCommands()
    {
        if (Input.GetAxisRaw("Horizontal") == -1)//left
        {
            if (_index == 0)
            {
                _index = _uiTexts.Length - 1;
            }
            else
            {
                _index--;
            }
            _selectedText = _uiTexts[_index];
            HighlightText();
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)//right
        {
            if (_index == _uiTexts.Length - 1)
            {
                _index = 0;
            }
            else
            {
                _index++;
            }
            _selectedText = _uiTexts[_index];
            HighlightText();
        }

    }

    private void HighlightText()
    {
        for (int i = 0; i < _uiTexts.Length; i++)
        {
            if (_uiTexts[i] == _selectedText)
            {
                _uiTexts[i].color = Color.cyan;
            }
            else
            {
                _uiTexts[i].color = Color.white;
            }
        }
    }

    //input manager commands using vertical keys
    private void yAxisCommands()
    {
        if (Input.GetAxisRaw("Vertical") == -1)//Down
        {
            for (int i = 0; i < _letters.Length; i++)
            {
                if (_letters[i] == _selectedText.text)
                {
                    if (i == _letters.Length - 1)
                    {
                        _selectedText.text = _letters[0];
                        break;
                    }
                    else
                    {
                        _selectedText.text = _letters[i + 1];
                        break;
                    }
                }
            }
        }
        else if (Input.GetAxisRaw("Vertical") == 1)//Up
        {
            for (int i = 0; i < _letters.Length; i++)
            {
                if (_letters[i] == _selectedText.text)
                {
                    if (i == 0)
                    {
                        _selectedText.text = _letters[_letters.Length - 1];
                        break;
                    }
                    else
                    {
                        _selectedText.text = _letters[i - 1];
                        break;
                    }
                }
            }
        }
    }

    //increment the menu depth of our cursor
    //each increment is a further submenu down
    private void selectionAxisCommands()
    {
        print("selectionAxisCommands");
        string name = "";
        for (int i = 0; i < _uiTexts.Length; i++)
        {
            name += _uiTexts[i].text;
        }
        //THE IDEA IS WHEN YOU FINISH THE GAME. IT CALCULATES THE SCORE THEN USES PLAYERPREFS.SETPREFS to save it and load it here.
        //also it uses the static IsItHighscore bool to check if a new score achieved if it is then loads this scene

        HighscoreScript.AddScore(name, _score);
        //How do you make it select replay button though?
        print("it gets here");

        PlayerPrefs.SetString("Replay", "True");
        Time.timeScale = 1f;
        SceneManager.LoadScene(6);
    }
}
