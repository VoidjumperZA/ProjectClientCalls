using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    //This can be deleted if PlayerInput will be attached to the Player
    [SerializeField]
    private bool _jumpGravityOption = true;
    [SerializeField]
    private GameObject _jumpBarBorder;
    [SerializeField]
    private GameObject gameManager;
    [SerializeField]
    private GameObject _pauseScreen;

    private PlayerMovement _playerMovement;
    private JumpBarHandler _jumpBarHandler;
    private DataHandler dataHandler;
    private int inputToggler = 1;

    private float yRotationValue = 0.0f;

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

        pauseScreenCheck();
    }

    private void FixedUpdate()
    {
        rotationCheck();
        SlowDownCheck();
        mouseUpCheck();
    }

    private void rotationCheck()
    {
        if (Input.GetMouseButtonDown(1))
        {
            inputToggler *= -1;   
        }
        if (inputToggler == 1)
        {
            yRotationValue = Input.GetAxisRaw("Horizontal") * 0.75f;
           // Debug.Log("Raw Axis: " + Input.GetAxisRaw("Horizontal"));
        }
        else
        {
            yRotationValue = Input.GetAxis("Horizontal") * 0.75f;
           // Debug.Log("Axis: " + Input.GetAxis("Horizontal"));
        }
        

        /*
        if (Input.GetKey(KeyCode.A))
        {
            if (yRotationValue > 0.0f)
            {
                yRotationValue = 0.0f;
            }

            yRotationValue -= 0.075f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (yRotationValue < 0.0f)
            {
                yRotationValue = 0.0f;
            }
            yRotationValue += 0.075f;
        }*/
        //else if (Input.GetKey(KeyCode.S)) {; }
        //else if (Input.GetKey(KeyCode.W)) {; }
        //else
        //{
            //yRotationValue = 0.0f;
            //return;
        //}

        yRotationValue = Mathf.Clamp(yRotationValue, -1.0f, 1.0f);
        _playerMovement.Rotating(yRotationValue);
    }

    private void jumpCheck()
    {
        if (Input.GetAxisRaw("Jump") > 0 && _playerMovement.GetGroundedState())
        {
            _playerMovement.Jump();
            _playerMovement.SetGroundedState(false);
            Debug.Log("JUMPING");
        }

        if (Input.GetAxisRaw("Jump") > 0)
        {
            _playerMovement.DecreaseGravity();
        }
        else
        {
            _playerMovement.IncreaseGravity();
        }
        
        
        /*
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
        }*/
    }

    private void jumpCheck2()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpBarHandler.Starting();
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            _playerMovement.ChargingJump();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            _playerMovement.ChargeJump();
            _jumpBarHandler.Ending();
        }
    }

    private void SlowDownCheck()
    {
        if (Input.GetMouseButton(0) && dataHandler.GetCurrentSanity() > 0)
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
            //dataHandler.ReturnSanityToIntValue();
        }
    }
    private void pauseScreenCheck()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0.0f)
            {
                Time.timeScale = 0.0f;
                _pauseScreen.SetActive(true);
            }
            else
            {
                Time.timeScale = 1.0f;
                _pauseScreen.SetActive(false);
            }
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel(int pLevel)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(pLevel);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void Continue()
    {
        Time.timeScale = 1.0f;
        _pauseScreen.SetActive(false);
    }
}
