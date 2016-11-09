using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {

    [SerializeField]
    private Text[] _optionTexts;

    private Text _selectedText;

    private bool xAxisInUse = false;
    private bool selectionAxisInUse = false;

    private int _index = 0;

    private void Start()
    {
        _selectedText = _optionTexts[0];
        HighlightText();
    }

    private void Update()
    {
        checkAxisCommands("Horizontal", ref xAxisInUse);
        checkAxisCommands("Jump", ref selectionAxisInUse);
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
                _index = _optionTexts.Length - 1;
            }
            else
            {
                _index--;
            }
            _selectedText = _optionTexts[_index];
            HighlightText();
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)//right
        {
            if (_index == _optionTexts.Length - 1)
            {
                _index = 0;
            }
            else
            {
                _index++;
            }
            _selectedText = _optionTexts[_index];
            HighlightText();
        }

    }

    private void HighlightText()
    {
        for (int i = 0; i < _optionTexts.Length; i++)
        {
            if (_optionTexts[i] == _selectedText)
            {
                _optionTexts[i].color = Color.cyan;
            }
            else
            {
                _optionTexts[i].color = Color.white;
            }
        }
    }

    //increment the menu depth of our cursor
    //each increment is a further submenu down
    private void selectionAxisCommands()
    {
        print("selectionAxisCommands");
        string name = "";
        for (int i = 0; i < _optionTexts.Length - 1; i++)
        {
            name += _optionTexts[i].text;
        }

        if (_selectedText == _optionTexts[0])
        {
            PlayerPrefs.SetString("Replay", "True");
            SceneManager.LoadScene(5);
        }
        else
        {
            SceneManager.LoadScene(0);
        }

    }
}
