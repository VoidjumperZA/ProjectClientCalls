using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    //This can be deleted if PlayerInput will be attached to the Player
    [SerializeField]
    private GameObject _player;

    private PlayerMovement _playerMovement;

    private void Awake()
    {
        //_playerMovement = _player.GetComponent<PlayerMovement>();
        _playerMovement = GetComponent<PlayerMovement>();
    }
    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        jumpCheck();
    }

    private void FixedUpdate()
    {
        rotationCheck();
        SlowDownCheck();
    }

    private void rotationCheck()
    {
        float yRotationValue = 0.0f;

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) { yRotationValue--; }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) { yRotationValue++; }
        else if (Input.GetKey(KeyCode.A)) { yRotationValue--; }
        else if (Input.GetKey(KeyCode.D)) { yRotationValue++; }
        //else if (Input.GetKey(KeyCode.S)) {; }
        //else if (Input.GetKey(KeyCode.W)) {; }
        else { return; }

        _playerMovement.Rotating(yRotationValue);
    }

    private void jumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _playerMovement.Jump();
        }
    }

    private void SlowDownCheck()
    {
        if (Input.GetMouseButton(0))
        {
            _playerMovement.SlowDown();
        }
        else
        {
            _playerMovement.SpeedUp();
        }
    }
}
