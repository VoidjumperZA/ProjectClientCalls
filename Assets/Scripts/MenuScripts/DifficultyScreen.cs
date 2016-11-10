using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class DifficultyScreen : MenuScreen
{
    [SerializeField]
    private GameObject _menuHandlerObject;
    [SerializeField]
    private Texture2D[] textures;

    private MenuHandler _menuHandler;
    private Renderer _renderer;

    private bool xAxisInUse = false;
    private bool yAxisInUse = false;
    private bool selectionAxisInUse = false;
    private bool returnAxisInUse = false;
    private enum Difficulty { EASY, MEDIUM, HARD }
    private Difficulty _highlightedDifficulty = Difficulty.EASY;
    private bool _reset = false;

    private PlayScript _playScript;

    private void Awake()
    {
        _menuHandler = _menuHandlerObject.GetComponent<MenuHandler>();
        _renderer = GetComponent<Renderer>();
        _renderer.material.mainTexture = textures[(int)_highlightedDifficulty];
        _playScript = _menuHandler._playScreen as PlayScript;
    }

    private void Start()
    {

    }

    public override void MenuUpdate()
    {
        checkAxisCommands("Horizontal", ref xAxisInUse);
        checkAxisCommands("Vertical", ref yAxisInUse);
        checkAxisCommands("Jump", ref selectionAxisInUse);
        checkAxisCommands("Fire1", ref returnAxisInUse);

        if (_reset)
        {
            Reset();
        }
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
                    case "Fire1":
                        returnAxisCommands();
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

    }

    //input manager commands using vertical keys
    private void yAxisCommands()
    {
        _highlightedDifficulty += ((int)Input.GetAxisRaw("Vertical") * -1);
        _highlightedDifficulty = (Difficulty)Mathf.Clamp((int)_highlightedDifficulty, 0, textures.Length - 1);
        print("yAxisCommands, " + _highlightedDifficulty);
        _renderer.material.mainTexture = textures[(int)_highlightedDifficulty];
    }

    //increment the menu depth of our cursor
    //each increment is a further submenu down
    private void selectionAxisCommands()
    {
        if (_reset) { return; }
        print("selectionAxisCommands, selected an option in the MenuOptions");
        //Camera animations
        switch (_highlightedDifficulty)
        {
            case Difficulty.EASY:
                PlayerPrefs.SetString("Difficulty", "Easy");
                switch (_playScript._highlightedLevel)
                {
                    case PlayScript.Levels.TUTORIAL:
                        print("LOADING TUTORIAL");
                        SceneManager.LoadScene("Test Scene");
                        break;
                    case PlayScript.Levels.MAIN_LEVEL:
                        print("LOADING MAIN_LEVEL");
                        SceneManager.LoadScene("Testplay Scene");
                        break;
                    case PlayScript.Levels.TRAINING_GROUND:
                        SceneManager.LoadScene("Dominick - Testbench");
                        break;
                }
                break;
            case Difficulty.MEDIUM:
                PlayerPrefs.SetString("Difficulty", "Medium");
                break;
            case Difficulty.HARD:
                PlayerPrefs.SetString("Difficulty", "Hard");
                break;
        }
    }

    //
    private void returnAxisCommands()
    {
        print("returnAxisCommands, we were highlighting so now we will go back to the mainMenu");
        //_menuHandler.SetScreen(_menuHandler._playScreen, true);
        StartCoroutine(Diff2Play());
    }

    private IEnumerator Diff2Play()
    {
        if (_animator == null) { _animator = Camera.main.GetComponent<Animator>(); }
        _animator.SetBool("Play2Diff", false);
        _menuHandler.Freeze(true);
        yield return new WaitForSeconds(1.0f);
        _menuHandler.SetScreen(_menuHandler._playScreen, true);
        _menuHandler.Freeze(false);
    }

    public override void ResetCall()
    {
        _reset = true;
    }

    public override void Reset()
    {
        _highlightedDifficulty = Difficulty.EASY;
        _renderer.material.mainTexture = textures[(int)_highlightedDifficulty];
        _reset = false;
    }

}
