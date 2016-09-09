using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Checkpoints : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject;

    private Transform transform;
    private DataHandler dataHandler;

    private List<Vector3> checkpointPositions;

    private int numberOfCheckpointsCreated = 0;
    private bool checkpointCreatable = false;  

    // Use this for initialization
    void Start()
    {
        checkpointPositions = new List<Vector3>();
        transform = playerObject.transform;
        dataHandler = GetComponent<DataHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("current sanity: " + dataHandler.GetCurrentSanity() + ", sanityBuffer: " + dataHandler.GetSanityBuffer() + ", sanityToCheckpointSegment: " + dataHandler.GetSanityToCheckpointSegment(0) + ", Checkpoint Stack: " + dataHandler.GetCheckpointStack() + ", number of checkpoints: " + checkpointPositions.Count + ", int difficulty: " + (int)dataHandler.difficulty);
        //if (dataHandler.GetCurrentSanity() % dataHandler.GetSanityToCheckpointSegment((int)dataHandler.difficulty) == 0 && dataHandler.GetCurrentSanity() != 0)
        if(dataHandler.GetCheckpointStack() > 0)
        {
            createCheckpoint();
            dataHandler.IncrementCheckpointStack(-1);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("current sanity: " + dataHandler.GetCurrentSanity() + ", sanityBuffer: " + dataHandler.GetSanityBuffer() + ", sanityToCheckpointSegment: " + dataHandler.GetSanityToCheckpointSegment(0) + ", Checkpoint Stack: " + dataHandler.GetCheckpointStack() + ", int difficulty: " + (int)dataHandler.difficulty);
        }
    }

    //create a check point
    private void createCheckpoint()
    {
        //create a new transform and add it to the list
        Vector3 newCheckpointPosition = transform.position; //transform seems to be a reference type, manually copy data
        //Debug.Log("numberOfCheckPoints: " + numberOfCheckpointsCreated);
        checkpointPositions.Add(newCheckpointPosition);
        numberOfCheckpointsCreated++;
        //Debug.Log("New checkpoint created. \nCheckpoint number: " + numberOfCheckpointsCreated + "\nCheckpoint position: " + newCheckpointPosition + ", while player position is: " + transform.position);
    }

    //remove the last checkpoint in the list
    public void ClearUsedCheckpoint()
    {
        checkpointPositions.RemoveAt(checkpointPositions.Count - 1);
    }
    
    public List<Vector3> GetCheckpointsList()
    {
        return checkpointPositions;
    }

    public Vector3 GetCheckpointsListAtIndex(int pIndex)
    {
        return checkpointPositions[pIndex];
    }

}
