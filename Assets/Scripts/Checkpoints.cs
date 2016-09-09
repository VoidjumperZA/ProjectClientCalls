using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Checkpoints : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject;

    private Transform transform;
    private DataHandler dataHandler;

    private List<Transform> checkpointPositions;

    private int numberOfCheckpointsCreated = 0;
    private bool checkpointCreatable = false;  

    // Use this for initialization
    void Start()
    {
        checkpointPositions = new List<Transform>();
        transform = playerObject.transform;
        dataHandler = GetComponent<DataHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("current sanity: " + dataHandler.GetCurrentSanity() + ", sanityBuffer: " + dataHandler.GetSanityBuffer() + ", sanityToCheckpointSegment: " + dataHandler.GetSanityToCheckpointSegment(0) + ", Checkpoint Stack: " + dataHandler.GetCheckpointStack() + ", int difficulty: " + (int)dataHandler.difficulty);
        //if (dataHandler.GetCurrentSanity() % dataHandler.GetSanityToCheckpointSegment((int)dataHandler.difficulty) == 0 && dataHandler.GetCurrentSanity() != 0)
        if(dataHandler.GetCheckpointStack() > 0)
        {
            createCheckpoint();
            dataHandler.IncrementCheckpointStack(-1);
        }
        if (Input.GetKey(KeyCode.F))
        {
            Debug.Log("player position: " + transform.position + ", checkpoint position: " + checkpointPositions[0].position);
        }
    }

    //create a check point
    private void createCheckpoint()
    {
        //create a new transform and add it to the list
        Transform newCheckpointTransform = transform; //transform seems to be a reference type, manually copy data
        Debug.Log("numberOfCheckPoints: " + numberOfCheckpointsCreated);
        checkpointPositions.Add(newCheckpointTransform);
        numberOfCheckpointsCreated++;
        Debug.Log("New checkpoint created. \nCheckpoint number: " + numberOfCheckpointsCreated + "\nCheckpoint position: " + newCheckpointTransform.position + ", while player position is: " + transform.position);
    }
    
   
}
