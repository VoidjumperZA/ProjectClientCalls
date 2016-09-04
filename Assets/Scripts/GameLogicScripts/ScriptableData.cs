using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Difficulty Data", menuName ="Game Logic/Difficulty", order = 1)]
public class ScriptableData : ScriptableObject {
    //please put any variables that affect difficulty here first: easy, then medium then hard
    public string difficulty = "Medium"; //easy,medium/hard
    public int sanityGain = 0; //example 50,20,0

    //just a note for tomorrow: problem i see here already: these values are hard coded now
    //if we want to change them we have to come in here and edit this script manually
    //i've written my scripts so far to all be accessable through the editor so that
    //designers or we can easily slide a value, check, slide again, check in real time as the game plays
    //where now we have to start, play, open VS, edit, save, start again
    //i like the idea of scriptable objects and i'm not sure how exactly they're implemented so maybe this
    //could work out better than it seems but so far, it seems that this is less flexible than before
}
