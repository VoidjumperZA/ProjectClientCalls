using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{

    [SerializeField]
    private float xRotation;

    [SerializeField]
    private float yRotation;

    [SerializeField]
    private float zRotation;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(xRotation, yRotation, zRotation);
    }
}
