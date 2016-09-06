using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Difficulty Data", menuName = "Game Logic/Difficulty", order = 1)]
public class ScriptableData : ScriptableObject {

    public string difficulty;

    public int sanityGain;

    public float playerMovementSpeed;

    public float playerJumpHeight;

    public float sanityPoints;

    public float slowDownScale;

    public float slowDownInterpolationValue;

    #region probably static engineered values. game designer may not be allowed to change them
    private float _playerRotationSpeed = 2.0f;
    private float _cameraShakeDistanceOnJump = 0.4f; //may be static value
    private float _cameraShakeDistanceOnLand = 0.6f; //may be static value
    #endregion

    [Header("Audio Settings")]
    #region Audio Settings
    public AudioClip onDeathClip;
    public AudioClip onJumpClip;
    public AudioClip onSanityGainClip;
    #endregion
}
