using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    //This can be deleted if PlayerInput will be attached to the Player
    [SerializeField]
    private bool _jumpGravityOption = true;
    [SerializeField]
    private GameObject _jumpBarBorder;

    [SerializeField]
    private GameManager gameManager;

    private PlayerMovement _playerMovement;
    private JumpBarHandler _jumpBarHandler;
    
    private DataHandler dataHandler;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _jumpBarHandler = _jumpBarBorder.GetComponent<JumpBarHandler>();
        dataHandler = gameManager.GetComponent<DataHandler>();
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
        mouseUpCheck();
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
            _playerMovement.ChargingJump();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            //Jump + Fade out
            _playerMovement.ChargeJump();
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

    //if we stop draining sanity, cast sanity back
    //as an int for easy calculations
    private void mouseUpCheck()
    {
        if (Input.GetMouseButtonUp(0))
        {
            dataHandler.ReturnSanityToIntValue();
        }
    }
}
