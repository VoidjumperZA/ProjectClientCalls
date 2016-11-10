using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuHandler : MonoBehaviour
{
    private MenuScreen _currentScreen = null;

    public MenuScreen _startScreen;
    public MenuScreen _mainMenuScreen;
    public MenuScreen _playScreen;
    public MenuScreen _difficultyScreen;
    public MenuScreen _optionsScreen;
    public MenuScreen _creditsScreen;

    private bool _freeze = false;

    private void Awake()
    {
        _currentScreen = _startScreen;
        print("Currently in " + _currentScreen.GetType().Name);
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (_freeze == false)
        {
            _currentScreen.MenuUpdate();
        }
    }

    public void SetScreen(MenuScreen pScreen, bool pReset)
    {
        print("Setting screen to: " + pScreen.GetType().Name);
        _currentScreen = pScreen;
        if (pReset) { pScreen.ResetCall(); }
    }

    public void Freeze(bool state)
    {
        _freeze = state;
    }

    public void ResetScreen(MenuScreen pScreen)
    {
        pScreen.ResetCall();
    }
}
