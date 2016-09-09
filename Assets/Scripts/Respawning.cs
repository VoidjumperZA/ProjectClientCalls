using UnityEngine;
using System.Collections;

public class Respawning : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject deathScreen;
    private Checkpoints checkpointsScript;

    private int checkpointStack;

    // Use this for initialization
    void Start()
    {
        checkpointsScript = GetComponent<Checkpoints>();
        checkpointStack = checkpointsScript.GetCheckpointsList().Count;
        deathScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //set the player to the checkpoint's position and clear that checkpoint from the list
    public void RespawnPlayerAtLastCheckpoint()
    {
        checkpointStack = checkpointsScript.GetCheckpointsList().Count;

        if (checkpointStack >= 1)
        {
            player.transform.position = checkpointsScript.GetCheckpointsListAtIndex(checkpointsScript.GetCheckpointsList().Count - 1);
            checkpointsScript.ClearUsedCheckpoint();
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
}
