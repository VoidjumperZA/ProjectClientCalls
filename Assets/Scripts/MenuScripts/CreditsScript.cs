using UnityEngine;
using System.Collections;
using System;

public class CreditsScript : MenuScreen
{
    [SerializeField]
    private GameObject _menuHandlerObject;

    private MenuHandler _menuHandler;

    private bool xAxisInUse = false;
    private bool yAxisInUse = false;
    private bool selectionAxisInUse = false;
    private bool returnAxisInUse = false;

    private bool _reset = false;

    private void Awake()
    {
        _menuHandler = _menuHandlerObject.GetComponent<MenuHandler>();
    }

    private void Start()
    {

    }

    public override void MenuUpdate()
    {
        checkAxisCommands("Fire1", ref returnAxisInUse);

        if (_reset)
        {
            Reset();
        }
    }

    private void checkAxisCommands(string pAxisName, ref bool pAxisToggle)
    {
        if (_reset == true) { return; }
        if (Input.GetAxisRaw(pAxisName) != 0) //if we've pressed button
        {
            print("inside credits screen, going back to the menu");
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
        _reset = false;
    }


}
