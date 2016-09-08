using UnityEngine;
using System.Collections;

public class ReenableColliders : MonoBehaviour
{
    private int colliderTimeToReenable;
    private int colliderType;
    private int counter = 0;

    //for handling different types of object colliders
    private BoxCollider boxCollider;
    private SphereCollider sphereCollider;
    private CapsuleCollider capsuleCollider; //note: cylinders and capsules both use capsule colliders

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counter++;
        if (counter > 1000)
        {
            counter = 0;
        }

        if (counter % colliderTimeToReenable == 0 && counter != 0)
        {
            reenableColliders(colliderType, true);
        }
    }

    public void AssignColliderTime(int pColliderTimeToReenable)
    {
        colliderTimeToReenable = pColliderTimeToReenable;
    }

    public void AssignColliderType(int pColliderType)
    {
        colliderType = pColliderType;
    }

    private void reenableColliders(int pColliderType, bool pIncomingState)
    {
        switch (pColliderType)
        {
            case 0:
                boxCollider = GetComponent<BoxCollider>();
                boxCollider.enabled = pIncomingState;
                break;
            case 1:
                sphereCollider = GetComponent<SphereCollider>();
                sphereCollider.enabled = pIncomingState;
                break;
            case 2:
                capsuleCollider = GetComponent<CapsuleCollider>();
                capsuleCollider.enabled = pIncomingState;
                break;
            case 3:
                capsuleCollider = GetComponent<CapsuleCollider>();
                capsuleCollider.enabled = pIncomingState;
                break;
        }
    }
}
