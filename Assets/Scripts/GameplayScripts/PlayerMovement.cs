using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameManagerObject;
    [SerializeField]
    private GameObject _firefly;

    private DataHandler dataHandler;

    private ScriptableData[] _scriptData;

    private GameManager _gameManager;
    private Rigidbody _rigidBody;
    private ChaseCamera _camera;

    private bool _grounded = false;
    private Vector3 _spawnPosition;
    private Quaternion _spawnRotation;

    private void Awake()
    {
        //contains all the settings for all available difficulties
        _scriptData = Resources.LoadAll<ScriptableData>("Data");
        
        _gameManager = _gameManagerObject.GetComponent<GameManager>();
        dataHandler = _gameManager.GetComponent<DataHandler>();
        _rigidBody = GetComponent<Rigidbody>();
        _camera = Camera.main.GetComponent<ChaseCamera>();

        _spawnPosition = transform.position;
        _spawnRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        forcedMovement();
    }

    private void forcedMovement()
    {
        //Forced Movement
        if (_grounded)
        {
            //_camera.transform.eulerAngles = transform.eulerAngles;
            _camera.LookingNormal(_gameManager.CameraRotationTime);
            transform.Translate(new Vector3(0, 0, _gameManager.PlayerMovementSpeed) * Time.deltaTime, Space.Self);
        }
        else
        {
            //Maybe make this a function in ChaseCamera
            Vector3 newCameraRotation = transform.eulerAngles - new Vector3(-_gameManager.CameraRotationInAir, 0.0f, 0.0f);
            _camera.LookingDown(newCameraRotation, _gameManager.CameraRotationTime);
            transform.Translate(new Vector3(0, 0, _gameManager.PlayerMovementSpeed) * Time.deltaTime, Space.Self);
        }
    }

    public void Rotating(float pRotationValue)
    {
        pRotationValue *= _gameManager.PlayerRotationSpeed;
        transform.Rotate(0.0f, pRotationValue, 0.0f);
        _camera.transform.Rotate(0.0f, pRotationValue, 0.0f, Space.World);
    }

    public void Jump()
    {
        if (!_grounded) { return; }

        _rigidBody.AddForce(transform.up * _gameManager.PlayerJumpHeight, ForceMode.Impulse);
        _grounded = false;
        _camera.Shake(_gameManager.CameraShakeDistanceOnJump);
    }

    public void ChargingJump()
    {
        _gameManager.JumpBarPoints += _gameManager.JumpBarInterpolationValue;
        _gameManager.JumpBarPoints = Mathf.Clamp(_gameManager.JumpBarPoints, 0.0f, 100.0f);
    }

    public void ChargeJump()
    {
        Vector3 jumpDirection = transform.forward + new Vector3(0.0f, _gameManager.ChargedJumpAngle, 0.0f);
        jumpDirection.Normalize();
        _rigidBody.AddForce(jumpDirection * ((_gameManager.ChargedJumpPower * _gameManager.JumpBarPoints) / 35), ForceMode.Impulse);
        _gameManager.JumpBarPoints = 0.0f;
    }


    public void DecreaseGravity()
    {
        Physics.gravity = new Vector3(0.0f, -_gameManager.LowGravity, 0.0f);
    }

    public void IncreaseGravity()
    {
        Physics.gravity = new Vector3(0.0f, -_gameManager.NormalGravity, 0.0f);
    }

    public void SlowDownTime()
    {
        if (dataHandler.GetCurrentSanity() <= 0) { return; }

        if (Time.timeScale > _gameManager.SlowDownScale)
        {
            Time.timeScale -= _gameManager.SlowDownInterpolationValue;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        dataHandler.IncrementCurrentSanity(-0.1f); ;
    }

    public void SpeedUpTime()
    {
        if (Time.timeScale < 1.0f)
        {
            Time.timeScale += _gameManager.SlowDownInterpolationValue;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Lume")
        {
            dataHandler.IncrementCurrentSanity(dataHandler.GetSanityGainOnFirefly((int)dataHandler.difficulty));
            dataHandler.IncrementSanityBuffer(dataHandler.GetSanityGainOnFirefly((int)dataHandler.difficulty));
            Destroy(col.gameObject);
            //need to just switch off the renderer and collider and reanble later
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "JumpableObject")
        {
            print("Landed upon a Jumpablebject");
            _grounded = true;
            _camera.Shake(_gameManager.CameraShakeDistanceOnLand);
        }

        if (collision.transform.tag == "DeathFloor")
        {
            print("You died");
            transform.position = _spawnPosition;
            transform.rotation = _spawnRotation;
            _rigidBody.velocity = Vector3.zero;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "JumpableObject")
        {
            //print("STAY");
            _grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "JumpableObject")
        {
            _grounded = false;
        }
    }

}
