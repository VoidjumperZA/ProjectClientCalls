using UnityEngine;
using System;
using System.Collections;

public class MainMenuScript : MonoBehaviour
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
    private enum MenuOption { PLAY, OPTIONS, CREDITS, EXIT }
    private MenuOption _selectedMenuOption = MenuOption.PLAY;

    void Awake()
    {
        _menuHandler = _menuHandlerObject.GetComponent<MenuHandler>();
        _renderer = GetComponent<Renderer>();
        _renderer.material.mainTexture = textures[(int)_selectedMenuOption];
        //print("Length: " + Enum.GetNames(typeof(MenuOption)).Length);
    }

    void Start()
    {

    }

    void Update()
    {
        //check all axes with a generic command checking method
        if (_menuHandler._menuState == MenuHandler.MenuState.MENU)
        {
            checkAxisCommands("Horizontal", ref xAxisInUse);
            checkAxisCommands("Vertical", ref yAxisInUse);
            checkAxisCommands("Jump", ref selectionAxisInUse);
            checkAxisCommands("Fire1", ref returnAxisInUse);
        }
        if (_menuHandler.reset == true)
        {
            _menuHandler.reset = false;
            _selectedMenuOption = MenuOption.PLAY;
            _renderer.material.mainTexture = textures[(int)_selectedMenuOption];
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
        _selectedMenuOption += ((int)Input.GetAxisRaw("Vertical") * -1);
        _selectedMenuOption = (MenuOption)Mathf.Clamp((int)_selectedMenuOption, 0, textures.Length - 1);
        print(_selectedMenuOption);
        _renderer.material.mainTexture = textures[(int)_selectedMenuOption];
    }

    //increment the menu depth of our cursor
    //each increment is a further submenu down
    private void selectionAxisCommands()
    {
        //Camera animations
        switch (_selectedMenuOption)
        {
            case MenuOption.PLAY:
                _menuHandler._menuState = MenuHandler.MenuState.PLAY;
                print("Selected Play option");
                //Camera animation towards the level select camera position
                break;
            case MenuOption.OPTIONS:
                print("Selected Options option");
                _menuHandler._menuState = MenuHandler.MenuState.OPTIONS;
                //Camera animation towards the options camera position
                break;
            case MenuOption.CREDITS:
                print("Selected Credits option");
                _menuHandler._menuState = MenuHandler.MenuState.CREDITS;
                //Camera animation towards the credits camera position
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
        //This is just a temporary test, this needs to be in the other Menu parts.
        _menuHandler._menuState = MenuHandler.MenuState.STARTSCREEN;
        print("returned to the Press a button to start screen");
        print(_menuHandler._menuState);
    }

}
