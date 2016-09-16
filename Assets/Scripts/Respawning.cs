using UnityEngine;
using System.Collections;

public class Respawning : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject monster;

    [SerializeField]
    private GameObject deathScreen;

    [SerializeField]
    private int framesTimeIsSlowedOnRespawn;
    private Checkpoints checkpointsScript;
    private PlayerMovement playerMovement;
    private DataHandler dataHandler;

    private int checkpointStack;
    private bool runTimeSlow = false;
    private int counter = 0;

    // Use this for initialization
    void Start()
    {
        checkpointsScript = GetComponent<Checkpoints>();
        checkpointStack = checkpointsScript.GetCheckpointsList().Count;
        deathScreen.SetActive(false);
        playerMovement = player.GetComponent<PlayerMovement>();
        dataHandler = GetComponent<DataHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (runTimeSlow == true)
        {
            Debug.Log("FixedUpdate run time slow");
            runTemporarySlowTime();
        }
    }

    //set the player to the checkpoint's position and clear that checkpoint from the list
    public void RespawnPlayerAtLastCheckpoint()
    {
        checkpointStack = checkpointsScript.GetCheckpointsList().Count;
        

        if (checkpointStack >= 1)
        {
            //position
            player.transform.position = checkpointsScript.GetCheckpointsListAtIndex(checkpointsScript.GetCheckpointsList().Count - 1);
            Camera.main.transform.eulerAngles = player.transform.eulerAngles;

            //monster position
            monster.transform.position = checkpointsScript.GetMonsterCheckpointsListAtIndex(checkpointsScript.GeMonsterCheckpointsList().Count - 1);

            //rotation
            player.transform.rotation = checkpointsScript.GetCheckpointRotationsListAtIndex(checkpointsScript.GetCheckpointRotationsList().Count - 1);
            Camera.main.transform.rotation = player.transform.rotation;

            //reset sanity
            dataHandler.SetCurrentSanity((int)dataHandler.GetSavedSanityOnCheckpoint(checkpointsScript.GetCheckpointsList().Count - 1));

            //empty buffer without increasing checkpoint stack
            dataHandler.EmptySanityBuffer(false);

            //clear the checkpoint
            checkpointsScript.ClearUsedCheckpoint();

            //slow time without using sanity
            runTimeSlow = true;
            playerMovement.SetSlowDownDueToRespawn(true);
        }
        else
        {
            Debug.Log("PERMADEATH");
            displayDeathScreen();
        }
        
    }
    
    private void displayDeathScreen()
    {
        deathScreen.SetActive(true);
    }

    private void runTemporarySlowTime()
    {
        counter++;
        if (counter < framesTimeIsSlowedOnRespawn)
        {
            playerMovement.SlowDownTime(false);
            Debug.Log("In slow time");
        }
        else
        {
            runTimeSlow = false;
            counter = 0;
            playerMovement.SetSlowDownDueToRespawn(false);
        }
        
    }
}
