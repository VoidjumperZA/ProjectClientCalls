using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    //This can be deleted if PlayerInput will be attached to the Player
    [SerializeField]
    private bool _jumpGravityOption = true;
    [SerializeField]
    private GameObject _jumpBarBorder;

    private PlayerMovement _playerMovement;
    private JumpBarHandler _jumpBarHandler;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _jumpBarHandler = _jumpBarBorder.GetComponent<JumpBarHandler>();
    }

    private void Update()
    {
        if (_jumpGravityOption)
        {
            jumpCheck();
        }
        else
        {
            jumpCheck2();
        }
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
        else if (Input.GetKey(KeyCode.Space))
        {
            _playerMovement.DecreaseGravity();
        }
        else
        {
            _playerMovement.IncreaseGravity();
        }
    }

    private void jumpCheck2()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Start bar + Fade in
            _jumpBarHandler.Starting();
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            //Charge bar
            _playerMovement.ChargeJump();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            //Jump + Fade out
            _jumpBarHandler.Ending();
        }
    }

    private void SlowDownCheck()
    {
        if (Input.GetMouseButton(0))
        {
            _playerMovement.SlowDownTime();
        }
        else
        {
            _playerMovement.SpeedUpTime();
        }
    }
}
