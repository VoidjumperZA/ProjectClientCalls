using UnityEngine;
using System.Collections;

abstract public class MenuScreen : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    public abstract void ResetCall();
    public abstract void Reset();
    public abstract void MenuUpdate();
}
