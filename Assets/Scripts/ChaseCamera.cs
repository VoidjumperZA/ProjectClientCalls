using UnityEngine;
using System.Collections;

public class ChaseCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject _TargetObject;

    private Transform _Target;
    private Rigidbody _TargetsRigidbody;

    private void Awake()
    {
        //transform and rigidbody selection now no longer relies on manual getting
        //through tags, instead you can just assign an object in the inspector
        //this helps as you don't have to remember to tag your player and it can
        //be more useful in other projects
        _Target = _TargetObject.transform;
        _TargetsRigidbody = _TargetObject.GetComponent<Rigidbody>();
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
