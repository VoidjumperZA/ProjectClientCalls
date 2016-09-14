using UnityEngine;
using System.Collections;

public class MenuHandler : MonoBehaviour
{
    public enum MenuState { STARTSCREEN, MENU, PLAY, OPTIONS, CREDITS }
    public MenuState _menuState;
    public bool reset = false;

    // Use this for initialization
    void Start()
    {
        _menuState = MenuState.MENU;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
