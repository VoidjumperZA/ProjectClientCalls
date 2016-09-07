using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Checkpoints : MonoBehaviour
{
    [SerializeField]
    private Transform transform;

    [SerializeField]
    [Range(0, 100)]
    private int[] sanityGainOnFirefly;

    [SerializeField]
    [Range(0, 100)]
    private int[] sanityToCheckpointSegment;

    private int fullSanity;
    private int currentSanity;

    private List<Transform> checkpointPositions;

    private int numberOfCheckpointsCreated = 0;

    enum DifficultyLevel {Easy, Medium, Hard};
    private DifficultyLevel difficulty = new DifficultyLevel();

    // Use this for initialization
    void Start()
    {
        difficulty = DifficultyLevel.Easy;
        checkpointPositions = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("current sanity: " + currentSanity + ", sanityToCheckpointSegment: " + );
        if (currentSanity % sanityToCheckpointSegment[(int)difficulty] == 0)
        {
            Transform newCheckpointTransform = transform;
            checkpointPositions[numberOfCheckpointsCreated] = newCheckpointTransform;
            numberOfCheckpointsCreated++;
            Debug.Log("New checkpoint created. \nCheckpoint number: " + numberOfCheckpointsCreated + "\nCheckpoint position: " + newCheckpointTransform.position + ", while player position is: " + transform.position);
        }
    }

    public int GetCurrentSanity()
    {
        return currentSanity;
    }
}
