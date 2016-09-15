using UnityEngine;
using System.Collections;

public class DespawnOnTouch : MonoBehaviour
{
    private GameObject target;
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
        if (col.gameObject == target)
        {
            GameObject.Destroy(gameObject);
        }
    }

    public void SetTargetObject(GameObject pTarget)
    {
        target = pTarget;
    }
}
