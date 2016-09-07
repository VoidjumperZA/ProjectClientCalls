﻿using UnityEngine;
using System.Collections;

public class ChaseCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject _TargetObject;

    private Transform _Target;

    private void Awake()
    {
        //transform and rigidbody selection now no longer relies on manual getting
        //through tags, instead you can just assign an object in the inspector
        //this helps as you don't have to remember to tag your player and it can
        //be more useful in other projects
        _Target = _TargetObject.transform;
    }

    private void FixedUpdate()
    {
        transform.position = _Target.position;

        //Vector3 newRotation = new Vector3(0.0f, _Target.eulerAngles.y, 0.0f);
        //transform.eulerAngles = newRotation;

    }

    //Needs to lerp
    public void Shake(float pShakeDistance)
    {
        Vector3 shakeDistance = new Vector3(0, -pShakeDistance, 0);
        transform.position += shakeDistance;
    }
}