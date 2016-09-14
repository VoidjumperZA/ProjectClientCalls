using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Checkpoints : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject;

    [SerializeField]
    private Text checkpointUINumber;

    [SerializeField]
    private GameObject checkpointMarker;

    [SerializeField]
    private float heightOfCheckpointSpawn;

    [SerializeField]
    private bool displayDebuggingMarker;

    private Transform transform;
    private DataHandler dataHandler;

    private List<Vector3> checkpointPositions;

    private int numberOfCheckpointsCreated = 0;
    private bool checkpointCreatable = false;
    private bool checkpointSlotFree = true;

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
        checkpointUINumber.text = "" + checkpointPositions.Count;
        Debug.Log("checkpoint stack: " + dataHandler.GetCheckpointStack());
        //Debug.Log("current sanity: " + dataHandler.GetCurrentSanity() + ", sanityBuffer: " + dataHandler.GetSanityBuffer() + ", sanityToCheckpointSegment: " + dataHandler.GetSanityToCheckpointSegment(0) + ", Checkpoint Stack: " + dataHandler.GetCheckpointStack() + ", number of checkpoints: " + checkpointPositions.Count + ", int difficulty: " + (int)dataHandler.difficulty);

        //Debug.Log("current sanity: " + dataHandler.GetCurrentSanity() + ", sanityBuffer: " + dataHandler.GetSanityBuffer() + ", sanityToCheckpointSegment: " + dataHandler.GetSanityToCheckpointSegment(0) + ", Checkpoint Stack: " + dataHandler.GetCheckpointStack() + ", int difficulty: " + (int)dataHandler.difficulty);

        //if (dataHandler.GetCurrentSanity() % dataHandler.GetSanityToCheckpointSegment((int)dataHandler.difficulty) == 0 && dataHandler.GetCurrentSanity() != 0)
        if (dataHandler.GetCheckpointStack() > 0)
        {
            startCheckpointCreation();
            dataHandler.IncrementCheckpointStack(-1);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("current sanity: " + dataHandler.GetCurrentSanity() + ", sanityBuffer: " + dataHandler.GetSanityBuffer() + ", sanityToCheckpointSegment: " + dataHandler.GetSanityToCheckpointSegment(0) + ", Checkpoint Stack: " + dataHandler.GetCheckpointStack() + ", number of checkpoints: " + checkpointPositions.Count + ", int difficulty: " + (int)dataHandler.difficulty);
        }
        Debug.Log("Current sanity: " + dataHandler.GetCurrentSanity());
    }

    //check if the circumstances are right to create a checkpoint
    private void startCheckpointCreation()
    {
        
        Debug.Log("numberOfCheckpointsCreated: " + numberOfCheckpointsCreated);

        //if our list of sanities to checkpoints is empty, add our fist one
        if (dataHandler.GetSavedSanityOnCheckpointList().Count == 0)
        {
            //Debug.Log("Create first saved section");
            createCheckpoint();
        }
        //otherwise:
        else
        {
            Debug.Log("In Else");
            //check if we already have a checkpoint for this sanity marker
            for (int i = 0; i < dataHandler.GetSavedSanityOnCheckpointList().Count; i++)
            {
                Debug.Log("Index: " + i + ", Saved sanity on this point: " + dataHandler.GetSavedSanityOnCheckpoint(i));
                //we already created an existing checkpoint for this sanity value
                if (dataHandler.GetSavedSanityOnCheckpoint(i) == dataHandler.GetCurrentSanity())
                {
                    Debug.Log("A checkpoint (" + i + ") exists for this sanity value (" + dataHandler.GetSavedSanityOnCheckpoint(i) + ") as our sanity is: " + dataHandler.GetCurrentSanity());
                    checkpointSlotFree = false;
                    return;
                }
                else
                {
                    checkpointSlotFree = true;
                }
            }
            if (checkpointSlotFree == true)
            {
                createCheckpoint();
            }
        }
        

        //Debug.Log("New checkpoint created. \nCheckpoint number: " + numberOfCheckpointsCreated + "\nCheckpoint position: " + newCheckpointPosition + ", while player position is: " + transform.position);
    }

    private void createCheckpoint()
    {
        Debug.Log("Actually in creatCheckpoint. Our sanity is: " + dataHandler.GetCurrentSanity());
        //create a new transform and add it to the list
        Vector3 newCheckpointPosition = playerObject.transform.position; //transform seems to be a reference type, manually copy data
        newCheckpointPosition.y += heightOfCheckpointSpawn;
        checkpointPositions.Add(newCheckpointPosition);
        numberOfCheckpointsCreated++;
        dataHandler.SetSavedSanityOnCheckpoint(dataHandler.GetCurrentSanity());


        //Debugging option: visually display checkpoint as a marker in-world
        if (displayDebuggingMarker == true)
        {
            Debug.Log("Creating marker");
            GameObject newCheckpointObject;
            newCheckpointObject = Instantiate(checkpointMarker);

            newCheckpointObject.transform.position = newCheckpointPosition;
        }
    }

    //remove the last checkpoint in the list
    public void ClearUsedCheckpoint()
    {
        checkpointPositions.RemoveAt(checkpointPositions.Count - 1);
        numberOfCheckpointsCreated--;
        dataHandler.RemoveSavedSanity(numberOfCheckpointsCreated - 1);
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
