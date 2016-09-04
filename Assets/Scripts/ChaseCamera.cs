using UnityEngine;
using System.Collections;

public class ChaseCamera : MonoBehaviour
{
    private Transform _Target;
    private Rigidbody _TargetsRigidbody;

    private void Awake()
    {
        _Target = GameObject.FindGameObjectWithTag("Player").transform;
        _TargetsRigidbody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _Target.position, 0.3f);

        //Vector3 newRotation = new Vector3(0.0f, _Target.eulerAngles.y, 0.0f);
        //transform.eulerAngles = newRotation;

    }

    public void Shake(float pShakeDistance)
    {
        Vector3 shakeDistance = new Vector3(0, -pShakeDistance, 0);
        transform.position += shakeDistance;
    }

    public void CameraXRotation(float pXRotation)
    {
        Vector3 newRotation = new Vector3(pXRotation, _Target.eulerAngles.y, transform.eulerAngles.z);
        transform.eulerAngles = newRotation;
    }


}
