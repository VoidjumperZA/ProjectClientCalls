using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private ChaseCamera _camera;
    private float _moveSpeed = 5f;
    private float _jumpHeight = 8.0f;
    private bool _grounded = false;

    private Vector3 _spawnPosition;
    [SerializeField]
    private Color _sanityBarColour;
    private Texture2D _sanityTexture;

    private float _sanityPoints;


   private float _xRotationValue = 0.0f;
   

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _camera = Camera.main.GetComponent<ChaseCamera>();
        _spawnPosition = transform.position;

        _sanityTexture = new Texture2D(1, 1);
        _sanityTexture.SetPixel(0, 0, _sanityBarColour);
        _sanityTexture.Apply();

        _sanityPoints = 20.0f;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Movement();
        SlowDownTime();

        print(Screen.height);
    }

    private void Update()
    {
        Jump();
    }

    private void Movement()
    {
        float yRotationValue = 0.0f;

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) { yRotationValue -= 1.0f; _xRotationValue += 1.0f; }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) { yRotationValue += 1.0f; _xRotationValue += 1.0f; }
        else if (Input.GetKey(KeyCode.A)) { yRotationValue -= 1.0f; }
        else if (Input.GetKey(KeyCode.D)) { yRotationValue += 1.0f; }
        else if (Input.GetKey(KeyCode.S)) { _xRotationValue += 1.0f; }
        else if (Input.GetKey(KeyCode.W)) { _xRotationValue -= 1.0f; }

        transform.Rotate(0.0f, yRotationValue, 0.0f);
        _camera.CameraXRotation(_xRotationValue);
        if (_grounded)
        {
            transform.Translate(new Vector3(0, 0, _moveSpeed) * Time.deltaTime, Space.Self);
        }
        else
        {
            transform.Translate((_camera.transform.forward * _moveSpeed) * Time.deltaTime, Space.Self);
        }
    }

    private void Jump()
    {
        if (_grounded && Input.GetKeyDown(KeyCode.Space))
        {
            //_rigidBody.AddForce(0, _jumpHeight, 0, ForceMode.Impulse);
            Vector3 jumpVector = _camera.transform.forward;
            jumpVector.y += 1.5f;
            _rigidBody.AddForce(jumpVector * 5, ForceMode.Impulse);
            _grounded = false;
            _camera.Shake(0.4f);
            print("Jump");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "JumpableObject")
        {
            _grounded = true;
            _camera.Shake(0.6f);
        }

        if (collision.transform.name == "Terrain")
        {
            print("You died");
            transform.position = _spawnPosition;
            _rigidBody.velocity = Vector3.zero;
        }
    }

    private void SlowDownTime()
    {
        if(Input.GetMouseButton(0) && _sanityPoints > 0.0f)
        {
            if (Time.timeScale > 0.2f)
            {
                Time.timeScale -= 0.05f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
            _sanityPoints -= 0.1f;
            //print(Time.fixedDeltaTime);
            //print(Time.deltaTime);
        }
        else if (Time.timeScale < 1.0f)
        {
            Time.timeScale += 0.05f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width - 236, 50, _sanityPoints, 20), _sanityTexture);
    }

    void OnTriggerEnter(Collider col)
    {
        print("OnTriggerEnter");
        if (col.tag == "Lume")
        {
            print("Colliders with Player");
            _sanityPoints += 20.0f;
            if (_sanityPoints > 160.0f)
            {
                _sanityPoints = 160.0f;
            }
            Destroy(col.gameObject);
        }
    }
}
