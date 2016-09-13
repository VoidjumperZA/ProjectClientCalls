using UnityEngine;
using System.Collections;

public class DespawnAfterDistance : MonoBehaviour
{
    private GameObject target;
    private int targetDistance;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.transform.position, transform.position) > targetDistance)
        {
            GameObject.Destroy(gameObject);
        }
    }

    public void SetTargetObject(GameObject pTarget)
    {
        target = pTarget;
    }

    public void SetTargetDistance(int pTargetDistance)
    {
        targetDistance = pTargetDistance;
    }
}
