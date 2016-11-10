using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SanityBarHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameManagerObject;
    [SerializeField]
    private Color _sanityBarColour;

    [SerializeField]
    private Slider _sanitySlider;

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

    private void Update()
    {
        _sanitySlider.value = dataHandler.GetCurrentSanity();
    }

    public void UpdateSanity()
    {
        //maybe use this one to globally access it and lerp the value.
    }


    private void OnGUI()
    {
        //Debug.Log("GUI sanity: " + dataHandler.GetCurrentSanity());
        GUI.DrawTexture(new Rect(Screen.width / 2, Screen.height / 2, (dataHandler.GetCurrentSanity() * 1.6f), 20), _sanityTexture);
        print("gets here");
    }
}
