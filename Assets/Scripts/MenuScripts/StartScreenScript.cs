using UnityEngine;
using System.Collections;
using System;

public class StartScreenScript : MenuScreen
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
        checkAxisCommands("Horizontal", ref xAxisInUse);
        checkAxisCommands("Vertical", ref yAxisInUse);
        checkAxisCommands("Jump", ref selectionAxisInUse);
        //checkAxisCommands("Fire1", ref returnAxisInUse);

        if (_reset)
        {
            Reset();
        }
    }

    private void checkAxisCommands(string pAxisName, ref bool pAxisToggle)
    {
        if (_reset == true){ return; }
        if (Input.GetAxisRaw(pAxisName) != 0) //if we've pressed button
        {
            StartCoroutine(startAnimation());
        }
    }

    private IEnumerator startAnimation()
    {
        if (_animator == null) { _animator = Camera.main.GetComponent<Animator>(); }
        _animator.SetBool("Start2Menu", true);
        _menuHandler.Freeze(true);
        yield return new WaitForSecondsRealtime(1.0f);
        _menuHandler.SetScreen(_menuHandler._mainMenuScreen, true);
        _menuHandler.Freeze(false);
    }

    public override void ResetCall()
    {
        _reset = true;
    }

    public override void Reset()
    {
        //the actual reset
        print("StartScreen reset");
        _reset = false;
    }
}
