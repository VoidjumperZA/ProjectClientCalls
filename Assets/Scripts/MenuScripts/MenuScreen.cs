using UnityEngine;
using System.Collections;

abstract public class MenuScreen : MonoBehaviour
{
    protected Animator _animator;
    // Use this for initialization

    public abstract void ResetCall();
    public abstract void Reset();
    public abstract void MenuUpdate();
}
