using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddHighscoreScript : MonoBehaviour {

    private string[] _letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

    [SerializeField]
    private Text[] _uiTexts;

    //NEED TO HIGHLIGHT THIS SOMEHOW. HOW?? make it bold maybe?
    private Text _selectedText;
    private int _index = 0;

    private void Start()
    {
        Init();
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
    }

    private void SelectText()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
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
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
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
        }
    }

    private void SelectLetter()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
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
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            for (int i = 0; i < _letters.Length; i++)
            {
                if (_letters[i] == _selectedText.text)
                {
                    if (i == 0)
                    {
                        _selectedText.text = _letters[_letters.Length-1];
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

    //over here to change the input for submitting
    private void SubmitScore()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string name = "";
            for (int i = 0; i < _uiTexts.Length-1; i++)
            {
                name += _uiTexts[i].text;
            }
            //THE IDEA IS WHEN YOU FINISH THE GAME. IT CALCULATES THE SCORE THEN USES PLAYERPREFS.SETPREFS to save it and load it here.
            //also it uses the static IsItHighscore bool to check if a new score achieved if it is then loads this scene
            int score = PlayerPrefs.GetInt("score");

            HighscoreScript.AddScore(name, score);
            //How do you make it select replay button though?
        }
        
    }
}
