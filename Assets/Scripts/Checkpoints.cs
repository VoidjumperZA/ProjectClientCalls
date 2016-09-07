using UnityEngine;
using System.Collections;

public class Checkpoints : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    private int[] sanityGainOnFirefly;

    [SerializeField]
    [Range(0, 100)]
    private int[] sanityToCheckpointSegment;

    private int fullSanity;
    private int currentSanity; 

    enum DifficultyLevel {Easy, Medium, Hard};
    private DifficultyLevel difficulty = new DifficultyLevel();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentSanity % sanityToCheckpointSegment[(int)difficulty] == 0)
        {
           
        }
    }
}
