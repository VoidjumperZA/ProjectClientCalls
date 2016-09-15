using UnityEngine;
using System.Collections;

public class HauntPlayer : MonoBehaviour {

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private int distanceToStartFollowing;

    [SerializeField]
    private float movementSpeed;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector3.Distance(transform.position, target.transform.position) >= distanceToStartFollowing)
        {
            transform.LookAt(target.transform);
            transform.Translate(0, 0, movementSpeed);
        }
	}
}
