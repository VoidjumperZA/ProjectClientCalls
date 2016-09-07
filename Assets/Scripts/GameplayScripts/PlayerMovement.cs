using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameManagerObject;
    [SerializeField]
    private GameObject _firefly;

    private ScriptableData[] _scriptData;

    private GameManager _gameManager;
    private Rigidbody _rigidBody;
    private ChaseCamera _camera;

    private bool _grounded = false;
    private Vector3 _spawnPosition;
    private Quaternion _spawnRotation;

    private void Awake()
    {
        _gameManager = _gameManagerObject.GetComponent<GameManager>();
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
            _camera.transform.eulerAngles = transform.eulerAngles;
            transform.Translate(new Vector3(0, 0, _gameManager.PlayerMovementSpeed) * Time.deltaTime, Space.Self);
        }
        else
        {
            //Maybe make this a function in ChaseCamera
            Vector3 newCameraRotation = transform.eulerAngles - new Vector3(-_gameManager.CameraRotationInAir, 0.0f, 0.0f);
            _camera.transform.eulerAngles = newCameraRotation;
            transform.Translate(new Vector3(0, 0, _gameManager.PlayerMovementSpeed) * Time.deltaTime, Space.Self);
        }
    }

    public void Rotating(float pRotationValue)
    {
        pRotationValue *= _gameManager.PlayerRotationSpeed;
        transform.Rotate(0.0f, pRotationValue, 0.0f);
        _camera.transform.Rotate(0.0f, pRotationValue, 0.0f);
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
        Vector3 jumpDirection = transform.forward + new Vector3(0.0f,_gameManager.ChargedJumpAngle, 0.0f);
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
        if (_gameManager.CurrentSanityPoints <= 0.0f) { return; }

        if (Time.timeScale > _gameManager.SlowDownScale)
        {
            Time.timeScale -= _gameManager.SlowDownInterpolationValue;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        _gameManager.CurrentSanityPoints -= 0.1f;
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
            _gameManager.CurrentSanityPoints += _gameManager.SanityPerLume;
            _gameManager.CurrentSanityPoints = Mathf.Clamp(_gameManager.CurrentSanityPoints, 0.0f, 100.0f);
            Destroy(col.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "JumpableObject")
        {
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

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "JumpableObject")
        {
            _grounded = false;
        }
    }

}
