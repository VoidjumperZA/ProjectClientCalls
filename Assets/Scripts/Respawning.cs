using UnityEngine;
using System.Collections;

public class Respawning : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Checkpoints checkpointsScript;

    private int checkpointStack;

    // Use this for initialization
    void Start()
    {
        checkpointsScript = GetComponent<Checkpoints>();
        checkpointStack = checkpointsScript.GetCheckpointsList().Count;
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
        }
        
    }
}
