using UnityEngine;
using System.Collections;

public class OptionsScript : MenuScreen
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
    private enum OptionsOption { FOV, ROTATION_SENSITIVITY, CAMERA_ANGLE, SOUND_VOLUME, BACK_TO_MENU }
    private OptionsOption _highlightedOptionsOption = OptionsOption.FOV;
    private bool _selected = false;

    private void Awake()
    {
        _menuHandler = _menuHandlerObject.GetComponent<MenuHandler>();
        _renderer = GetComponent<Renderer>();
        _renderer.material.mainTexture = textures[(int)_highlightedOptionsOption];
    }

    private void Start()
    {
        print("OptionsMenu: _selectedOptionsOption = " + _highlightedOptionsOption);
    }

    private void Update()
    {
        //check all axes with a generic command checking method
        if (_menuHandler._menuState == MenuHandler.MenuState.OPTIONS)
        {
            checkAxisCommands("Horizontal", ref xAxisInUse);
            checkAxisCommands("Vertical", ref yAxisInUse);
            checkAxisCommands("Jump", ref selectionAxisInUse);
            checkAxisCommands("Fire1", ref returnAxisInUse);
        }
        //if (_menuHandler.reset == true)
        //{
        //    _menuHandler.reset = false;
        //    _highlightedOptionsOption = OptionsOption.FOV;
        //    _renderer.material.mainTexture = textures[(int)_highlightedOptionsOption];
        //    print("Option screen has been reset. highlightedOptionsOption is:" + _highlightedOptionsOption);
        //}
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
        _highlightedOptionsOption += ((int)Input.GetAxisRaw("Vertical") * -1);
        _highlightedOptionsOption = (OptionsOption)Mathf.Clamp((int)_highlightedOptionsOption, 0, textures.Length - 1);
        print(_highlightedOptionsOption);
        _renderer.material.mainTexture = textures[(int)_highlightedOptionsOption];
    }

    //increment the menu depth of our cursor
    //each increment is a further submenu down
    private void selectionAxisCommands()
    {
        //Camera animations
        switch (_highlightedOptionsOption)
        {
            case OptionsOption.FOV:

                break;
            case OptionsOption.ROTATION_SENSITIVITY:

                break;
            case OptionsOption.CAMERA_ANGLE:

                break;
            case OptionsOption.SOUND_VOLUME:

                break;
            case OptionsOption.BACK_TO_MENU:

                break;
        }
    }

    //
    private void returnAxisCommands()
    {
        //This is just a temporary test, this needs to be in the other Menu parts.
        //_menuHandler.reset = true;
        _menuHandler._menuState = MenuHandler.MenuState.MENU;
        _menuHandler.ResetScreen(this);
        //print("returned to the " + _menuHandler._menuState);
    }

    public override void Reset()
    {
        print("OptionsScreen reset");
    }
}
