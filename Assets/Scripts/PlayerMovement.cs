using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameManagerObject;
    [SerializeField]
    private GameObject _firefly;

    private GameManager _gameManager;






    private Rigidbody _rigidBody;
    private ChaseCamera _camera;
    private bool _grounded = false;

    private Vector3 _spawnPosition;
    private Quaternion _spawnRotation;
    //[SerializeField]
    //private Color _sanityBarColour;
    //private Texture2D _sanityTexture;

    //private float _sanityPoints;
    //private float _xRotationValue = 0.0f;

    //private AudioSource[] _clips;

    private void Awake()
    {
        _gameManager = _gameManagerObject.GetComponent<GameManager>();





        _rigidBody = GetComponent<Rigidbody>();
        _camera = Camera.main.GetComponent<ChaseCamera>();
        _spawnPosition = transform.position;
        _spawnRotation = transform.rotation;

        //_sanityTexture = new Texture2D(1, 1);
        //_sanityTexture.SetPixel(0, 0, _sanityBarColour);
        //_sanityTexture.Apply();

        //_sanityPoints = 20.0f;


        //_clips = GetComponents<AudioSource>();
    }
    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        ForcedMovement();
    }

    private void Update()
    {
        //Jump();
    }

    private void ForcedMovement()
    {
        transform.Translate(new Vector3(0, 0, _gameManager.PlayerMovementSpeed) * Time.deltaTime, Space.Self);
    }

    public void Jump()
    {
        if (!_grounded) { return; }

        //Vector3 jumpVector = _camera.transform.forward;
        //jumpVector.y += 1.5f;
        _rigidBody.AddForce(transform.up * _gameManager.PlayerJumpHeight, ForceMode.Impulse);
        _grounded = false;
        _camera.Shake(_gameManager.CameraShakeDistanceOnJump);
        //_clips[0].Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "Floor")
        {
            _grounded = true;
            _camera.Shake(_gameManager.CameraShakeDistanceOnLand);
        }

        if (collision.transform.name == "Terrain")
        {
            print("You died");
            transform.position = _spawnPosition;
            transform.rotation = _spawnRotation;
            _rigidBody.velocity = Vector3.zero;
        }
    }


    public void SlowDown()
    {
        if (_gameManager.SanityPoints <= 0.0f) { return; }

        if (Time.timeScale > _gameManager.SlowDownScale)
        {
            Time.timeScale -= _gameManager.SlowDownInterpolationValue;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        _gameManager.SanityPoints -= 0.1f;
    }

    public void SpeedUp()
    {
        if (Time.timeScale < 1.0f)
        {
            Time.timeScale += _gameManager.SlowDownInterpolationValue;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }

    //private void OnGUI()
    //{
    //    GUI.DrawTexture(new Rect(Screen.width - 236, 50, _sanityPoints, 20), _sanityTexture);
    //}

    void OnTriggerEnter(Collider col)
    {
        print("OnTriggerEnter");
        if (col.tag == "Lume")
        {
            print("Colliders with Player");
            _gameManager.SanityPoints += 20.0f;
            //maybe use Clamp in the future
            if (_gameManager.SanityPoints > 160.0f)
            {
                _gameManager.SanityPoints = 160.0f;
            }
            Destroy(col.gameObject);
            //_clips[1].Play();
        }
    }

    public void Rotating(float pRotationValue)
    {
        pRotationValue *= _gameManager.PlayerRotationSpeed;
        transform.Rotate(0.0f, pRotationValue, 0.0f);
        _camera.transform.Rotate(0.0f, pRotationValue, 0.0f);
    }
}
