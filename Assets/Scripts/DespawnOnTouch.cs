using UnityEngine;
using System.Collections;

public class DespawnOnTouch : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "DespawnWall")
        {
            GameObject.Destroy(gameObject);
        }
    }
}
