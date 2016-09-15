using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Checkpoints : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject;

    [SerializeField]
    private GameObject monsterObject;

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
    private List<Vector3> monsterCheckpointPositions;
    private List<Quaternion> checkpointRotations;

    private int numberOfCheckpointsCreated = 0;
    private bool checkpointCreatable = false;
    private bool checkpointSlotFree = true;

    // Use this for initialization
    void Start()
    {
        checkpointPositions = new List<Vector3>();
        monsterCheckpointPositions = new List<Vector3>();
        checkpointRotations = new List<Quaternion>();
        transform = playerObject.transform;
        dataHandler = GetComponent<DataHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        checkpointUINumber.text = "" + checkpointPositions.Count;
        //Debug.Log("checkpoint stack: " + dataHandler.GetCheckpointStack());
        //Debug.Log("current sanity: " + dataHandler.GetCurrentSanity() + ", sanityBuffer: " + dataHandler.GetSanityBuffer() + ", sanityToCheckpointSegment: " + dataHandler.GetSanityToCheckpointSegment(0) + ", Checkpoint Stack: " + dataHandler.GetCheckpointStack() + ", number of checkpoints: " + checkpointPositions.Count + ", int difficulty: " + (int)dataHandler.difficulty);

        if (dataHandler.GetCheckpointStack() > 0)
        {
            startCheckpointCreation();
            dataHandler.IncrementCheckpointStack(-1);
        }
        Debug.Log("current sanity: " + dataHandler.GetCurrentSanity() + "while the buffer is: " + dataHandler.GetSanityBuffer());
        //Debug.Log("saved sanity list: " + dataHandler.GetSavedSanityOnCheckpointList().Count);
        //Debug.Log("Current sanity: " + dataHandler.GetCurrentSanity());
    }

    //check if the circumstances are right to create a checkpoint
    private void startCheckpointCreation()
    {
        //if our list of sanities to checkpoints is empty, add our fist one
        if (dataHandler.GetSavedSanityOnCheckpointList().Count == 0)
        {
            //Debug.Log("Create first saved section");
            createCheckpoint();
        }
        //otherwise:
        else
        {
            //Debug.Log("In Else");
            //check if we already have a checkpoint for this sanity marker
            for (int i = 0; i < dataHandler.GetSavedSanityOnCheckpointList().Count; i++)
            {
                //Debug.Log("Index: " + i + ", Saved sanity on this point: " + dataHandler.GetSavedSanityOnCheckpoint(i));
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
        Debug.Log("Actually in creatCheckpoint. Our sanity is: " + dataHandler.GetCurrentSanity() + "and our buffer is: " + dataHandler.GetSanityBuffer());
        
        //create a new vector3, moderate it's height and add it to the list
        Vector3 newCheckpointPosition = playerObject.transform.position;
        newCheckpointPosition.y += heightOfCheckpointSpawn;
        checkpointPositions.Add(newCheckpointPosition);

        //create a new vector3 for saving the "monster"'s position
        Vector3 newMonsterCheckpointPosition = monsterObject.transform.position;
        monsterCheckpointPositions.Add(newMonsterCheckpointPosition);

        //create a new quaternion for storing rotation and also save it to the list
        Quaternion newCheckpointRotation = playerObject.transform.rotation;
        checkpointRotations.Add(newCheckpointRotation);

        numberOfCheckpointsCreated++;
        dataHandler.SetSavedSanityOnCheckpoint(dataHandler.GetCurrentSanity());

        //Debugging option: visually display checkpoint as a marker in-world
        if (displayDebuggingMarker == true)
        {
            //Debug.Log("Creating marker");
            GameObject newCheckpointObject;
            newCheckpointObject = Instantiate(checkpointMarker);
            newCheckpointObject.transform.position = newCheckpointPosition;
        }
    }

    //remove the last checkpoint in the list
    public void ClearUsedCheckpoint()
    {
        checkpointPositions.RemoveAt(checkpointPositions.Count - 1);
        monsterCheckpointPositions.RemoveAt(checkpointPositions.Count - 1);
        checkpointRotations.RemoveAt(checkpointPositions.Count - 1);

        numberOfCheckpointsCreated--;

        if (numberOfCheckpointsCreated > 0)
        {
            dataHandler.RemoveSavedSanity(numberOfCheckpointsCreated - 1);
        }
        else
        {
            dataHandler.RemoveSavedSanity(numberOfCheckpointsCreated);
        }
    }
    
    public List<Vector3> GetCheckpointsList()
    {
        return checkpointPositions;
    }

    public Vector3 GetCheckpointsListAtIndex(int pIndex)
    {
        return checkpointPositions[pIndex];
    }

    public List<Vector3> GeMonsterCheckpointsList()
    {
        return monsterCheckpointPositions;
    }

    public Vector3 GetMonsterCheckpointsListAtIndex(int pIndex)
    {
        return monsterCheckpointPositions[pIndex];
    }

    public List<Quaternion> GetCheckpointRotationsList()
    {
        return checkpointRotations;
    }

    public Quaternion GetCheckpointRotationsListAtIndex(int pIndex)
    {
        return checkpointRotations[pIndex];
    }
}
