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
    private enum OptionsOption { FOV, CAMERA_ANGLE, SOUND_VOLUME, LANTERN_ON, BACK_TO_MENU }
    private OptionsOption _highlightedOptionsOption = OptionsOption.FOV;
    private bool _selected = false;
    private bool _reset = false;

    private int _FOVValue = 0;
    private int _CameraAngleValue = 0;
    private int _SoundVolume = 0;
    private bool _LanternON = true;

    private void Awake()
    {
        _menuHandler = _menuHandlerObject.GetComponent<MenuHandler>();
        _renderer = GetComponent<Renderer>();
        _renderer.material.mainTexture = textures[(int)_highlightedOptionsOption];
    }

    private void Start()
    {
    }

    public override void MenuUpdate()
    {
        if (_selected)
        {
            checkAxisCommands("Horizontal", ref xAxisInUse);
        }
        else
        {
            checkAxisCommands("Vertical", ref yAxisInUse);
            checkAxisCommands("Jump", ref selectionAxisInUse);
        }
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

        switch(_highlightedOptionsOption)
        {
            case OptionsOption.FOV:
                _FOVValue += ((int)Input.GetAxisRaw("Horizontal"));
                _FOVValue = Mathf.Clamp(_FOVValue, 0, 100);
                print("Changing FOV value to: " + _FOVValue);
                break;
            case OptionsOption.CAMERA_ANGLE:
                _CameraAngleValue += ((int)Input.GetAxisRaw("Horizontal"));
                _CameraAngleValue = Mathf.Clamp(_CameraAngleValue, 0, 100);
                print("Changing CAMERA_ANGLE value to: " + _CameraAngleValue);
                break;
            case OptionsOption.SOUND_VOLUME:
                _SoundVolume += ((int)Input.GetAxisRaw("Horizontal"));
                _SoundVolume = Mathf.Clamp(_SoundVolume, 0, 100);
                print("Changing SOUND_VOLUME value to: " + _SoundVolume);
                break;
            case OptionsOption.LANTERN_ON:
                _LanternON = !_LanternON;
                print("Switched Lantern ON to: " + _LanternON);
                break;
        }

    }

    //input manager commands using vertical keys
    private void yAxisCommands()
    {
        _highlightedOptionsOption += ((int)Input.GetAxisRaw("Vertical") * -1);
        _highlightedOptionsOption = (OptionsOption)Mathf.Clamp((int)_highlightedOptionsOption, 0, textures.Length - 1);
        print("yAxisCommands, " + _highlightedOptionsOption);
        _renderer.material.mainTexture = textures[(int)_highlightedOptionsOption];
    }

    //increment the menu depth of our cursor
    //each increment is a further submenu down
    private void selectionAxisCommands()
    {
        if (_selected) { return; }
        print("selectionAxisCommands, selected an option in the MenuOptions");
        //Camera animations
        switch (_highlightedOptionsOption)
        {
            case OptionsOption.FOV:
                _selected = true;
                print("selected FOV");
                break;
            case OptionsOption.CAMERA_ANGLE:
                _selected = true;
                print("selected CAMERA_ANGLE");
                break;
            case OptionsOption.SOUND_VOLUME:
                _selected = true;
                print("selected SOUND_VOLUME");
                break;
            case OptionsOption.LANTERN_ON:
                _selected = true;
                print("selected LANTERN_ON");
                break;
            case OptionsOption.BACK_TO_MENU:
                print("selected BACK_TO_MENU");
                returnAxisCommands();
                break;
        }
    }

    //
    private void returnAxisCommands()
    {
        if (_selected)
        {
            print("returnAxisCommands, turned _selected to false, starting highlighting again");
            _selected = false;
        }
        else
        {
            print("returnAxisCommands, we were highlighting so now we will go back to the mainMenu");
            _menuHandler.SetScreen(_menuHandler._mainMenuScreen, true);
        }
    }

    public override void ResetCall()
    {
        _reset = true;
    }

    public override void Reset()
    {
        //the actual reset
        print("OptionsScreen reset");
        _highlightedOptionsOption = OptionsOption.FOV;
        _renderer.material.mainTexture = textures[(int)_highlightedOptionsOption];
        _reset = false;
        _selected = false;
    }
}
