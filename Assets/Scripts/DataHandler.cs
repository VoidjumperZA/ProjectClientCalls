using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataHandler : MonoBehaviour
{
    //editable by designer
    [SerializeField]
    [Range(0, 100)]
    private int[] sanityGainOnFirefly;

    //internal, using designer data, but not number of elements
    private int[] internalSanityGainOnFirefly;

    //editable by designer
    [SerializeField]
    [Range(0, 100)]
    private int[] sanityToCheckpointSegment;

    //internal, using designer data, but not number of elements
    private int[] internalSanityToCheckpointSegment;

    private int fullSanity = 100;
    private float currentSanity;
    private List<float> savedSanityOnCheckpoint = new List<float>();
    private float sanityBuffer;
    private bool fullSanityAchieved = false;
    private int checkpointStack;

    public enum DifficultyLevel { Easy, Medium, Hard };
    public DifficultyLevel difficulty = new DifficultyLevel();

    // Use this for initialization
    void Start()
    {
        //set our difficulty: only for testing
        difficulty = DifficultyLevel.Easy;
        //used for getting the amount of elements in an enum
        string[] elementsInEnum = System.Enum.GetNames(typeof(DifficultyLevel));

        //ensure our arrays are calibrated to the same size as the amount of 
        //difficulty levels available
        internalSanityGainOnFirefly = new int[elementsInEnum.Length];
        internalSanityToCheckpointSegment = new int[elementsInEnum.Length];

        //save only the parts we need, in case the designer made more 
        //elements in the array than there are difficulty levels
        for (int i = 0; i < elementsInEnum.Length; i++)
        {
            internalSanityGainOnFirefly[i] = sanityGainOnFirefly[i];
            internalSanityToCheckpointSegment[i] = sanityToCheckpointSegment[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        EmptySanityBuffer(true);
        if (currentSanity >= fullSanity)
        {
            fullSanityAchieved = true;
        }
        if (currentSanity < 1)
        {
            currentSanity = 0;
        }
    }


    //returns the current sanity
    public float GetCurrentSanity()
    {
        return currentSanity;
    }

    //returns how much sanity we gain on touching a firefly
    public int GetSanityGainOnFirefly(int pDifficulty)
    {
        return internalSanityGainOnFirefly[pDifficulty];
    }

    //returns the entire array
    public int[] GetSanityToCheckpointSegment()
    {
        return internalSanityToCheckpointSegment;
    }

    //
    public float GetSanityBuffer()
    {
        return sanityBuffer;
    }

    //
    public int GetCheckpointStack()
    {
        return checkpointStack;
    }

    //returns only a spesific index in the array
    public int GetSanityToCheckpointSegment(int pIndex)
    {
        return internalSanityToCheckpointSegment[pIndex];
    }

    //increments the current sanity
    public void IncrementCurrentSanity(float pSanityIncrease)
    {
        if (currentSanity < fullSanity)
        {
            currentSanity += pSanityIncrease;
        }
    }

    //
    public void IncrementSanityBuffer(float pIncrement)
    {
        sanityBuffer += pIncrement;
    }

    //
    public void IncrementCheckpointStack(int pIncrement)
    {
        checkpointStack += pIncrement;
    }

    //overrides and replaces the current sanity
    public void SetCurrentSanity(int pNewSanityValue)
    {
        currentSanity = pNewSanityValue;
    }

    //overrides and replaces the current sanity buffer 
    public void SetSanityBuffer(int pNewBufferValue)
    {
        sanityBuffer = pNewBufferValue;
    }

    //

    //casts our sanity back into an integer value
    public void ReturnSanityToIntValue()
    {
        currentSanity = Mathf.Floor(currentSanity);
    }

    //when the player fills a segment of the sanity bar, empty
    //the buffer and increment the stack of checkpoints allowed
    //to be created
    public void EmptySanityBuffer(bool pOpenStack)
    {
        if (sanityBuffer >= sanityToCheckpointSegment[(int)difficulty])
        {
            sanityBuffer = 0;
            if (pOpenStack == true)
            {
                checkpointStack += 1;
            }
        }
    }

    public List<float> GetSavedSanityOnCheckpointList()
    {
        return savedSanityOnCheckpoint;
    }

    public float GetSavedSanityOnCheckpoint(int pIndex)
    {
        return savedSanityOnCheckpoint[pIndex];
    }

    public void SetSavedSanityOnCheckpoint(float pSanityValue)
    {
        savedSanityOnCheckpoint.Add(pSanityValue);
        //Debug.Log("Assigning " + currentSanity + " to index " + (savedSanityOnCheckpoint.Count - 1));
    }

    public void RemoveSavedSanity(int pIndex)
    {
        savedSanityOnCheckpoint.RemoveAt(pIndex);
    }
}
