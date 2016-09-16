using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class MainMenuScript : MenuScreen
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
    private enum MenuOption { PLAY, OPTIONS, HIGH_SCORES, CREDITS, EXIT }
    private MenuOption _highlightedMenuOption = MenuOption.PLAY;

    private bool _reset = false;

    void Awake()
    {
        _menuHandler = _menuHandlerObject.GetComponent<MenuHandler>();
        _renderer = GetComponent<Renderer>();
        _renderer.material.mainTexture = textures[(int)_highlightedMenuOption];
        //print("Length: " + Enum.GetNames(typeof(MenuOption)).Length);
    }

    void Start()
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
        _highlightedMenuOption += ((int)Input.GetAxisRaw("Vertical") * -1);
        _highlightedMenuOption = (MenuOption)Mathf.Clamp((int)_highlightedMenuOption, 0, textures.Length - 1);
        print(_highlightedMenuOption);
        _renderer.material.mainTexture = textures[(int)_highlightedMenuOption];
    }

    //increment the menu depth of our cursor
    //each increment is a further submenu down
    private void selectionAxisCommands()
    {
        //Camera animations
        switch (_highlightedMenuOption)
        {
            case MenuOption.PLAY:
                print("Selected Play option");
                //Camera animation towards the level select camera position
                _menuHandler.SetScreen(_menuHandler._playScreen, true);
                //GameObject.Find("Camera2").SetActive(true);
                //GameObject.Find("Camera3").SetActive(false);
                //Camera.main.gameObject.SetActive(false);
                break;
            case MenuOption.OPTIONS:
                print("Selected Options option");
                //Camera animation towards the options camera position
                _menuHandler.SetScreen(_menuHandler._optionsScreen, true);
                break;
            case MenuOption.HIGH_SCORES:
                print("Selected High Scores option");
                SceneManager.LoadScene("HighscoreScene");
                break;
            case MenuOption.CREDITS:
                print("Selected Credits option");
                //Camera animation towards the credits camera position
                _menuHandler.SetScreen(_menuHandler._creditsScreen, true);
                break;
            case MenuOption.EXIT:
                print("Selected Exit option");
                //Maybe a exit game camera animation?? if time left ofcourse
                Application.Quit();
                break;
        }
    }

    //
    private void returnAxisCommands()
    {
        if (_reset == true){ return; }
        print("Currently inside menu, pressed right shift, going to startScreen");
        _menuHandler.SetScreen(_menuHandler._startScreen, true);
    }

    public override void ResetCall()
    {
        _reset = true;
    }

    public override void Reset()
    {
        //actual reset
        _reset = false;
        print("MainMenuScreen reset");
        _highlightedMenuOption = MenuOption.PLAY;
        _renderer.material.mainTexture = textures[(int)_highlightedMenuOption];
    }
}
