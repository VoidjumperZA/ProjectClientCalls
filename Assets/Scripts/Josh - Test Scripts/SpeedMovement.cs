using UnityEngine;
using System.Collections;

public class SpeedMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject accelerationObject;

    private Rigidbody rigidBod;

    Vector3 targetPos;
    Vector3 differenceVec;
    // Use this for initialization
    void Start()
    {
        targetPos = accelerationObject.transform.position;
        differenceVec = transform.position - targetPos;
        rigidBod = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = accelerationObject.transform.position;
        //find the difference between our current position and our target's
        differenceVec = transform.position - targetPos;
        Debug.Log("XZXXX");
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, -1, 0);
            Debug.Log("Hello");
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 1, 0);
        }
        //vector.Scale(speedVector);
        rigidBod.AddForce(-differenceVec, ForceMode.Impulse);
    }
}
