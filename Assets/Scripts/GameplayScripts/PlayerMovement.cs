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
        //Forced Movement
        transform.Translate(new Vector3(0, 0, _gameManager.PlayerMovementSpeed) * Time.deltaTime, Space.Self);
    }

    public void Jump()
    {
        if (!_grounded) { return; }

        _rigidBody.AddForce(transform.up * _gameManager.PlayerJumpHeight, ForceMode.Impulse);
        _grounded = false;
        _camera.Shake(_gameManager.CameraShakeDistanceOnJump);
    }

    public void DecreaseGravity()
    {
        Physics.gravity = new Vector3(0.0f, -_gameManager.LowGravity, 0.0f);
    }

    public void IncreaseGravity()
    {
        Physics.gravity = new Vector3(0.0f, -_gameManager.NormalGravity, 0.0f);
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

    public void ChargeJump()
    {
        _gameManager.JumpBarPoints += _gameManager.JumpBarInterpolationValue;
        _gameManager.JumpBarInterpolationValue = Mathf.Clamp(_gameManager.JumpBarInterpolationValue, 0.0f, 100.0f);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Lume")
        {
            _gameManager.CurrentSanityPoints += _gameManager.SanityPerLume;
            _gameManager.CurrentSanityPoints = Mathf.Clamp(_gameManager.CurrentSanityPoints, 0.0f, 100.0f);
            Destroy(col.gameObject);
        }
    }

    public void Rotating(float pRotationValue)
    {
        pRotationValue *= _gameManager.PlayerRotationSpeed;
        transform.Rotate(0.0f, pRotationValue, 0.0f);
        _camera.transform.Rotate(0.0f, pRotationValue, 0.0f);
    }
}
