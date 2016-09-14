using UnityEngine;
using System.Collections;

abstract public class MenuScreen : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Reset()
    {
        print("MenuScreen reset");
    }
}
