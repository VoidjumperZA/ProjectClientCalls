using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuHandler : MonoBehaviour
{
    public enum MenuState { STARTSCREEN, MENU, PLAY, OPTIONS, CREDITS }
    public MenuState _menuState;

    public List<MenuScreen> _menuScreens = new List<MenuScreen>();

    public StartScreenScript _startScreenScript;
    public MainMenuScript _mainMenuScript;
    public OptionsScript _optionsScript;

    

    //public bool reset = false;

    // Use this for initialization
    void Start()
    {
        _menuState = MenuState.STARTSCREEN;
        _menuScreens.Add(_startScreenScript);
        _menuScreens.Add(_mainMenuScript);
        _menuScreens.Add(_optionsScript);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetScreen(MenuScreen pScreen)
    {
        pScreen.Reset();
    }
}
