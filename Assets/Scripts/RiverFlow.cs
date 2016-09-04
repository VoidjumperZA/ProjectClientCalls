using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RiverFlow : MonoBehaviour
{
    [SerializeField]
    private bool disgardHeight;

    [SerializeField]
    private bool invertVector;

    [SerializeField]
    private float flowSpeed;

    //create vectors
    Vector3 forceScale;
    Vector3 targetPos;
    Vector3 differenceVec;

    private int waypointIndex = 0;

    //create lists
    List<Transform> markerList;

    //grab components
    private Rigidbody rigidBod;
    private Transform target;
    private FlowMarkerManager markerManager;  
    
    // Use this for initialization
    void Awake()
    {
        rigidBod = GetComponent<Rigidbody>();
        
        //set target to our target's position
        markerManager = GameObject.FindGameObjectWithTag("FlowMarkerManager").GetComponent<FlowMarkerManager>();
        target = markerManager.GetWaypointTransform(waypointIndex);
        targetPos = target.position;
        markerManager.Report();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(target.up);
        flowToMarker();
    }

    //grabs our next marker in the list if our object passes over the current target
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "FlowMarker")
        {
            Debug.Log("Entered a flow marker. Increasing flow index.");
            waypointIndex++;
        }
        Debug.Log("Marker list size is: " + markerManager.GetWaypointList().Length + " while our waypoint index is: " + waypointIndex);
        if (markerManager.GetWaypointList().Length > waypointIndex)
        {
            target = markerManager.GetWaypointTransform(waypointIndex);
            Debug.Log("Assigning a new target marker.");
        }
        else
        {
            Debug.Log("Reached the end of our list of markers.");
        }
    }

    //moves our object smoothly towards or away from our marker
    private void flowToMarker()
    {
        //update our target position
        targetPos = target.position;
        //find the difference between our current position and our target's
        differenceVec = transform.position - targetPos;

        //generic option to ignore y value if you're on a static plane
        if (disgardHeight == true)
        {
            differenceVec.y = 0;
        }
        //generic option to flip the vector if you want to move away from an object instead of towards it
        if (invertVector == true)
        {
            differenceVec = -differenceVec;
        }

        if (flowSpeed != 1)
        {
            differenceVec.Scale(new Vector3(flowSpeed, flowSpeed, flowSpeed));
        }

        //apply a constant force
        rigidBod.AddForce(differenceVec, ForceMode.Force);
    }
}
