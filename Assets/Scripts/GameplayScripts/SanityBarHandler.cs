using UnityEngine;
using System.Collections;

public class SanityBarHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameManagerObject;
    [SerializeField]
    private Color _sanityBarColour;

    private DataHandler dataHandler;

    private GameManager _gameManager;
    private Texture2D _sanityTexture;
    
    private void Awake()
    {
        _gameManager = _gameManagerObject.GetComponent<GameManager>();
        dataHandler = _gameManagerObject.GetComponent<DataHandler>();
        _sanityTexture = new Texture2D(1, 1);
        _sanityTexture.SetPixel(0, 0, _sanityBarColour);
        _sanityTexture.Apply();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        //Debug.Log("GUI sanity: " + dataHandler.GetCurrentSanity());
        GUI.DrawTexture(new Rect(Screen.width - 236, 50, (dataHandler.GetCurrentSanity() * 1.6f), 20), _sanityTexture);
    }
}
