using UnityEngine;
using System.Collections;
using System;

public class ControlsScript : MenuScreen
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
    private enum ControlsOption { IN_MENU, IN_GAME, OK_I_GOT_IT }
    private ControlsOption _highlightedControlsOption = ControlsOption.IN_MENU;

    private bool _reset = false;

    void Awake()
    {
        _menuHandler = _menuHandlerObject.GetComponent<MenuHandler>();
        _renderer = GetComponent<Renderer>();
        _renderer.material.mainTexture = textures[(int)_highlightedControlsOption];
    }

    // Use this for initialization
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

    }

    //increment the menu depth of our cursor
    //each increment is a further submenu down
    private void selectionAxisCommands()
    {
        //Camera animations
        switch (_highlightedControlsOption)
        {
            case ControlsOption.IN_MENU:
                print("Selected IN_MENU option");
                //Camera animation towards the level select camera position
                break;
            case ControlsOption.IN_GAME:
                print("Selected IN_GAME option");
                //Camera animation towards the options camera position
                break;
            case ControlsOption.OK_I_GOT_IT:
                print("Selected OK_I_GOT_IT option");
                //Camera animation towards the credits camera position
                break;
        }
    }

    //
    private void returnAxisCommands()
    {
        if (_reset == true) { return; }
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
        print("ControlsScreen reset");
        _highlightedControlsOption = ControlsOption.IN_MENU;
        _renderer.material.mainTexture = textures[(int)_highlightedControlsOption];
    }
}
