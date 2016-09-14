using UnityEngine;
using System.Collections;

public class StartScreenScript : MenuScreen
{
    [SerializeField]
    private GameObject _menuHandlerObject;

    private MenuHandler _menuHandler;

    private bool xAxisInUse = false;
    private bool yAxisInUse = false;
    private bool selectionAxisInUse = false;
    private bool returnAxisInUse = false;

    private void Awake()
    {
        _menuHandler = _menuHandlerObject.GetComponent<MenuHandler>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (_menuHandler._menuState == MenuHandler.MenuState.STARTSCREEN)
        {
            checkAxisCommands("Horizontal", ref xAxisInUse);
            checkAxisCommands("Vertical", ref yAxisInUse);
            checkAxisCommands("Jump", ref selectionAxisInUse);
        }
    }

    private void checkAxisCommands(string pAxisName, ref bool pAxisToggle)
    {
        if (Input.GetAxisRaw(pAxisName) != 0) //if we've pressed button
        {
            print("Goes back to the Main Menu");
            _menuHandler._menuState = MenuHandler.MenuState.MENU;
            //_menuHandler.reset = true;
            //_menuHandler.ResetScreen(this);
        }
    }

    public override void Reset()
    {
        print("StartScreen reset");
    }
}
