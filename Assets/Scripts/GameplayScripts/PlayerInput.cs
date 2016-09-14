using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    //This can be deleted if PlayerInput will be attached to the Player
    [SerializeField]
    private bool _jumpGravityOption = true;
    [SerializeField]
    private GameObject gameManager;
    [SerializeField]
    private GameObject _pauseScreen;

    private PlayerMovement _playerMovement;
    private DataHandler dataHandler;
    private int inputToggler = -1;

    private bool fireAxisInUse = false;

    private float yRotationValue = 0.0f;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        dataHandler = gameManager.GetComponent<DataHandler>();
    }

    private void Update()
    {
        if (_jumpGravityOption)
        {
            jumpCheck();
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
            //Debug.Log("Raw Axis: " + Input.GetAxisRaw("Horizontal"));
        }
        else
        {
            yRotationValue = Input.GetAxis("Horizontal") * 0.75f;
            //Debug.Log("Axis: " + Input.GetAxis("Horizontal"));
        }
        
        yRotationValue = Mathf.Clamp(yRotationValue, -1.0f, 1.0f);
        _playerMovement.Rotating(yRotationValue);
    }

    private void jumpCheck()
    {
        if (Input.GetAxisRaw("Jump") > 0 && _playerMovement.GetGroundedState())
        {
            _playerMovement.Jump();
            _playerMovement.SetGroundedState(false);
        }

        if (Input.GetAxisRaw("Jump") > 0)
        {
            _playerMovement.DecreaseGravity();
        }
        else
        {
            _playerMovement.IncreaseGravity();
        }
        
        
    }

    private void SlowDownCheck()
    {
        //if we're holding fire and have sanity
        if (Input.GetAxisRaw("Fire1") != 0 && dataHandler.GetCurrentSanity() > 0)
        {
            _playerMovement.SlowDownTime(true); //true boolean uses up sanity
            fireAxisInUse = true; //pretty sure this has become redundant
        }
        //if they key is up, time is slow and that's not happening because of a respawn
        if (Input.GetAxisRaw("Fire1") == 0 && Time.timeScale < 1 && _playerMovement.GetSlowDownDueToRespawn() == false)
        {
            fireAxisInUse = false;
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

    //sets the toggle for smooth vs raw input manager controls
    public void SetInputToggle(bool pToggleToSmooth)
    {
        if (pToggleToSmooth == true)
        {
            inputToggler = -1;
        }
        else
        {
            inputToggler = 1;
        }
    }
}
