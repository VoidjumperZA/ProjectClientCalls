using UnityEngine;
using System.Collections;

public class ReenableColliders : MonoBehaviour
{
    private int[] colliderTimeToReenable;
    private int[] colliderType;
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

        for (int i = 0; i < colliderType.Length; i++)
        {
            if (counter % colliderTimeToReenable[i] == 0 && counter != 0)
            {
                toggleCollidersEnabled(colliderType[i], true);
            }
        }
        
    }

    public void AssignColliderTime(int pIndex, int pColliderTimeToReenable)
    {
        colliderTimeToReenable[pIndex] = pColliderTimeToReenable;
    }

    public void AssignColliderType(int pIndex, int pColliderType)
    {
        colliderType[pIndex] = pColliderType;
    }

    private void toggleCollidersEnabled(int pColliderType, bool pIncomingState)
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

    public void SetNumberOfColliders(int pNumberOfCollidersToReenable)
    {
        colliderTimeToReenable = new int[pNumberOfCollidersToReenable];
        colliderType = new int[pNumberOfCollidersToReenable];
    }
}
