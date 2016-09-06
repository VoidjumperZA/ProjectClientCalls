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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            currentSanity / sanityToCheckpointSegment
        }
    }
}
