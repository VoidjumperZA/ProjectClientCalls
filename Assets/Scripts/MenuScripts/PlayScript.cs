using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class PlayScript : MenuScreen
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
    public enum Levels { TUTORIAL, MAIN_LEVEL, TRAINING_GROUND, BACK_TO_MENU }
    public Levels _highlightedLevel = Levels.TUTORIAL;
    private bool _reset = false;

    private void Awake()
    {
        _menuHandler = _menuHandlerObject.GetComponent<MenuHandler>();
        _renderer = GetComponent<Renderer>();
        _renderer.material.mainTexture = textures[(int)_highlightedLevel];
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
        print("xAxisCommands");
    }

    //input manager commands using vertical keys
    private void yAxisCommands()
    {
        _highlightedLevel += ((int)Input.GetAxisRaw("Vertical") * -1);
        _highlightedLevel = (Levels)Mathf.Clamp((int)_highlightedLevel, 0, textures.Length - 1);
        print("yAxisCommands, " + _highlightedLevel);
        _renderer.material.mainTexture = textures[(int)_highlightedLevel];
    }

    //increment the menu depth of our cursor
    //each increment is a further submenu down
    private void selectionAxisCommands()
    {
        if(_reset) { return; }
        print("selectionAxisCommands, selected an option in the Level select");
        //Camera animations
        switch (_highlightedLevel)
        {
            case Levels.TUTORIAL:
                print("selected TUTORIAL");
                //SceneManager.LoadScene("Test Scene");
                //_menuHandler.SetScreen(_menuHandler._difficultyScreen, true);
                PlayerPrefs.SetString("Difficulty", "Tutorial");
                break;
            case Levels.MAIN_LEVEL:
                print("selected MAIN_LEVEL");
                //SceneManager.LoadScene("Testplay Scene");
                StartCoroutine(Play2Diff());
                //_menuHandler.SetScreen(_menuHandler._difficultyScreen, true);
                break;
            case Levels.TRAINING_GROUND:
                print("selected TRAINING_GROUND");
                //SceneManager.LoadScene("Dominick - Testbench");
                //_menuHandler.SetScreen(_menuHandler._difficultyScreen, true);
                break;
            case Levels.BACK_TO_MENU:
                print("selected BACK_TO_MENU");
                returnAxisCommands();
                break;
        }
    }

    //
    private void returnAxisCommands()
    {
        print("Returning to the Menu");
        StartCoroutine(Play2Menu());
    }

    private IEnumerator Play2Menu()
    {
        if (_animator == null) { _animator = Camera.main.GetComponent<Animator>(); }
        _animator.SetBool("Menu2Play", false);
        _menuHandler.Freeze(true);
        yield return new WaitForSeconds(1.0f);
        _menuHandler.SetScreen(_menuHandler._mainMenuScreen, true);
        _menuHandler.Freeze(false);
    }

    private IEnumerator Play2Diff()
    {
        if (_animator == null) { _animator = Camera.main.GetComponent<Animator>(); }
        _animator.SetBool("Play2Diff", true);
        _menuHandler.Freeze(true);
        yield return new WaitForSeconds(1.0f);
        _menuHandler.SetScreen(_menuHandler._difficultyScreen, true);
        _menuHandler.Freeze(false);
    }

    public override void ResetCall()
    {
        _reset = true;
    }

    public override void Reset()
    {
        print("PlayScreen reset");
        _highlightedLevel = Levels.TUTORIAL;
        _renderer.material.mainTexture = textures[(int)_highlightedLevel];
        _reset = false;
    }
}
