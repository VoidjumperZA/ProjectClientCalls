﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameManagerObject;
    [SerializeField]
    private GameObject _firefly;
    private PlayerInput playerInput;

    private DataHandler dataHandler;

    private ScriptableData[] _scriptData;

    private GameManager _gameManager;
    private Rigidbody _rigidBody;
    private ChaseCamera _camera;
    private Respawning respawningSystem;

    private bool _grounded = false;
    private Vector3 _spawnPosition;
    private Quaternion _spawnRotation;
    
    //stops the game detecting you're not pressing a key and speeding time back up
    private bool slowDownDueToRespawn = false;

    private void Awake()
    {
        //contains all the settings for all available difficulties
        _scriptData = Resources.LoadAll<ScriptableData>("Data");
        
        _gameManager = _gameManagerObject.GetComponent<GameManager>();
        dataHandler = _gameManager.GetComponent<DataHandler>();
        _rigidBody = GetComponent<Rigidbody>();
        _camera = Camera.main.GetComponent<ChaseCamera>();
        respawningSystem = _gameManager.GetComponent<Respawning>();

        _spawnPosition = transform.position;
        _spawnRotation = transform.rotation;

        playerInput = GetComponent<PlayerInput>();
        
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

        _rigidBody.AddForce((transform.up * _gameManager.PlayerJumpHeight / 2), ForceMode.Impulse);
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

    public void SlowDownTime(bool pUseSanity)
    {
        if (dataHandler.GetCurrentSanity() <= 0) { return; }

        if (Time.timeScale > _gameManager.SlowDownScale)
        {
            playerInput.SetInputToggle(false); //switch to raw movement, more precise when in slowmo
            Time.timeScale -= _gameManager.SlowDownInterpolationValue;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        if (pUseSanity == true) //we shouldn't use up sanity slowing time when it happens after a respawn
        {
            dataHandler.IncrementCurrentSanity(-0.1f); 
        }
    }

    public void SpeedUpTime()
    {
        if (Time.timeScale < 1.0f)
        {
            playerInput.SetInputToggle(true); //reenable smooth left-right movement
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
            Debug.Log("Entered Deathfloor");
            respawningSystem.RespawnPlayerAtLastCheckpoint();
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

    public bool GetGroundedState()
    {
        return _grounded;
    }

    public void SetGroundedState(bool pInputValue)
    {
        _grounded = pInputValue;
    }

    public bool GetSlowDownDueToRespawn()
    {
        return slowDownDueToRespawn;
    }

    public void SetSlowDownDueToRespawn(bool pInputValue)
    {
        slowDownDueToRespawn = pInputValue;
    }

}
