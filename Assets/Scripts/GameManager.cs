using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    //Serializable fields for the Level Designer/Builder
    [SerializeField]
    private float _playerMovementSpeed = 10.0f;
    [SerializeField]
    private float _playerJumpHeight = 8.0f;
    [SerializeField]
    private float _playerRotationSpeed = 2.0f;
    [SerializeField]
    private float _cameraShakeDistanceOnJump = 0.4f;
    [SerializeField]
    private float _cameraShakeDistanceOnLand = 0.6f;
    [SerializeField]
    private float _sanityPoints = 0.0f;
    [SerializeField]
    private float _slowDownScale = 0.2f;
    [SerializeField]
    private float _slowDownInterpolationValue = 0.01f;

    //All fields accessible through properties
    public float PlayerMovementSpeed
    {
        get { return _playerMovementSpeed; }
        set { _playerMovementSpeed = value; }
    }
    public float PlayerJumpHeight
    {
        get { return _playerJumpHeight; }
        set { _playerJumpHeight = value; }
    }
    public float PlayerRotationSpeed
    {
        get { return _playerRotationSpeed; }
        set { _playerRotationSpeed = value; }
    }
    public float CameraShakeDistanceOnJump
    {
        get { return _cameraShakeDistanceOnJump; }
        set { _cameraShakeDistanceOnJump = value; }
    }
    public float CameraShakeDistanceOnLand
    {
        get { return _cameraShakeDistanceOnLand; }
        set { _cameraShakeDistanceOnLand = value; }
    }
    public float SanityPoints
    {
        get { return _sanityPoints; }
        set { _sanityPoints = value; }
    }
    public float SlowDownScale
    {
        get { return _slowDownScale; }
        set { _slowDownScale = value; }
    }
    public float SlowDownInterpolationValue
    {
        get { return _slowDownInterpolationValue; }
        set { _slowDownInterpolationValue = value; }
    }



    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }
}
