using UnityEngine;
using System.Collections;

public class OptionsScript : MonoBehaviour
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
    private enum OptionsOption { FOV, ROTATION_SENSITIVITY, CAMERA_ANGLE, BACK_TO_MENU }
    private OptionsOption _selectedOptionsOption = OptionsOption.FOV;

    private void Awake()
    {

    }

    private void Start()
    {

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
        if (_menuHandler.reset == true)
        {
            _menuHandler.reset = false;
            _selectedOptionsOption = OptionsOption.FOV;
            _renderer.material.mainTexture = textures[(int)_selectedOptionsOption];
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
        _selectedOptionsOption += ((int)Input.GetAxisRaw("Vertical") * -1);
        _selectedOptionsOption = (OptionsOption)Mathf.Clamp((int)_selectedOptionsOption, 0, textures.Length - 1);
        print(_selectedOptionsOption);
        _renderer.material.mainTexture = textures[(int)_selectedOptionsOption];
    }

    //increment the menu depth of our cursor
    //each increment is a further submenu down
    private void selectionAxisCommands()
    {
        //Camera animations
        switch (_selectedOptionsOption)
        {
            case OptionsOption.FOV:
  
                break;
            case OptionsOption.ROTATION_SENSITIVITY:

                break;
            case OptionsOption.CAMERA_ANGLE:

                break;
            case OptionsOption.BACK_TO_MENU:

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
