using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour
{
    //This can be deleted if PlayerInput will be attached to the Player
    private GameObject _gameManager;
    [SerializeField]
    private GameObject _pauseScreen;

    private PlayerMovement _playerMovement;
    private DataHandler _dataHandler;

    private float yRotationValue = 0.0f;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _dataHandler = _gameManager.GetComponent<DataHandler>();
    }

    private void Update()
    {
        jumpCheck();
        pauseScreenCheck();
        mouseUpCheck();
    }

    private void FixedUpdate()
    {
        rotationCheck();
        slowDownCheck();
    }

    private void rotationCheck()
    {
        //if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) { yRotationValue--; }
        //else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) { yRotationValue++; }
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
        }
        //else if (Input.GetKey(KeyCode.S)) {; }
        //else if (Input.GetKey(KeyCode.W)) {; }
        else
        {
            yRotationValue = 0.0f;
            return;
        }

        yRotationValue = Mathf.Clamp(yRotationValue, -1.0f, 1.0f);
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

    private void slowDownCheck()
    {
        if (Input.GetMouseButton(0) && _dataHandler.GetCurrentSanity() > 0)
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

    //These needs to be placed somewhere else, probably make a script sceneloader or something and add it to the gamemanager
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
