using UnityEngine;
using System.Collections;

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

    }


    //returns the current sanity
    public int GetCurrentSanity()
    {
        return (int)currentSanity;
    }

    //increments the current sanity
    public void IncrementCurrentSanity(float pSanityIncrease)
    {
        currentSanity += pSanityIncrease;
    }

    //overrides and replaces the current sanity
    public void SetCurrentSanity(int pNewSanityValue)
    {
        currentSanity = pNewSanityValue;
    }

    //returns how much sanity we gain on touching a firefly
    public int GetSanityGainOnFirefly(int pDifficulty)
    {
        return internalSanityGainOnFirefly[pDifficulty];
    }

    //casts our sanity back into an integer value
    public void ReturnSanityToIntValue()
    {
        currentSanity = (int)currentSanity;
    }

    //returns the entire array
    public int[] GetSanityToCheckpointSegment()
    {
        return internalSanityToCheckpointSegment;
    }

    //returns only a spesific index in the array
    public int GetSanityToCheckpointSegment(int pIndex)
    {
        return internalSanityToCheckpointSegment[pIndex];
    }
}
