using UnityEngine;
using System.Collections;

public class ChaseCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject _TargetObject;

    private Transform _Target;

    private void Awake()
    {
        _Target = _TargetObject.transform;
    }

    private void FixedUpdate()
    {
        transform.position = _Target.position;
    }

    //Needs to lerp
    public void Shake(float pShakeDistance)
    {
        Vector3 shakeDistance = new Vector3(0, -pShakeDistance, 0);
        transform.position += shakeDistance;
    }

    public void LookingDown(Vector3 pLookingDownRotation, float pLerpDuration)
    {
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, pLookingDownRotation, pLerpDuration);
    }

    public void LookingNormal(float pLerpDuration)
    {
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, _Target.eulerAngles, pLerpDuration);
    }
}
