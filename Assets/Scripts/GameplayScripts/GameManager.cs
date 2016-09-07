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
    private float _chargedJumpAngle = 1.0f;
    [SerializeField]
    private float _chargedJumpPower = 5.0f;
    [SerializeField]
    private float _cameraRotationInAir = 30.0f;
    [SerializeField]
    private float _playerRotationSpeed = 2.0f;
    [SerializeField]
    private float _cameraShakeDistanceOnJump = 0.4f;
    [SerializeField]
    private float _cameraShakeDistanceOnLand = 0.6f;
    [SerializeField]
    private float _currentSanityPoints = 0.0f;
    [SerializeField]
    private float _sanityPerLume = 5.0f;
    [SerializeField]
    private float _slowDownScale = 0.2f;
    [SerializeField]
    private float _slowDownInterpolationValue = 0.01f;
    [SerializeField]
    private float _normalGravity = 9.8f;
    [SerializeField]
    private float _lowGravity = 2.0f;
    [SerializeField]
    private float _jumpBarPoints = 0.0f;
    [SerializeField]
    private float _jumpBarInterpolationValue = 1.0f;

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
    public float ChargedJumpAngle
    {
        get { return _chargedJumpAngle; }
        set { _chargedJumpAngle = value; }
    }
    public float ChargedJumpPower
    {
        get { return _chargedJumpPower; }
        set { _chargedJumpPower = value; }
    }
    public float CameraRotationInAir
    {
        get { return _cameraRotationInAir; }
        set { _cameraRotationInAir = value; }
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
    public float CurrentSanityPoints
    {
        get { return _currentSanityPoints; }
        set { _currentSanityPoints = value; }
    }
    public float SanityPerLume
    {
        get { return _sanityPerLume; }
        set { _sanityPerLume = value; }
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
    public float NormalGravity
    {
        get { return _normalGravity; }
        set { _normalGravity = value; }
    }
    public float LowGravity
    {
        get { return _lowGravity; }
        set { _lowGravity = value; }
    }
    public float JumpBarPoints
    {
        get { return _jumpBarPoints; }
        set { _jumpBarPoints = value; }
    }
    public float JumpBarInterpolationValue
    {
        get { return _jumpBarInterpolationValue; }
        set { _jumpBarInterpolationValue = value; }
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
