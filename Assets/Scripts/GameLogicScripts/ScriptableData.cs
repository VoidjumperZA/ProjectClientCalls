using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Difficulty Data", menuName ="Game Logic/Difficulty", order = 1)]
public class ScriptableData : ScriptableObject {
    //please put any variables that affect difficulty here first: easy, then medium then hard
    public string difficulty = "Medium"; //easy,medium/hard
    public int sanityGain = 0; //example 50,20,0
}
