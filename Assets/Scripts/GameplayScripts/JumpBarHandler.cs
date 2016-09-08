using UnityEngine;
using System.Collections;

public class JumpBarHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameManagerObject;
    [SerializeField]
    private Color _jumpBarColour;

    private GameManager _gameManager;
    private Texture2D _jumpTexture;
    private JumpBarHandler _jumpBarHandler;
    private bool _visible = false;

    private void Awake()
    {
        _gameManager = _gameManagerObject.GetComponent<GameManager>();
        _jumpBarHandler = GetComponent<JumpBarHandler>();
        _jumpTexture = new Texture2D(1, 1);
        _jumpTexture.SetPixel(0, 0, _jumpBarColour);
        //_jumpTexture.alphaIsTransparency = true;
        _jumpTexture.Apply();
    }

    // Use this for initialization
    void Start()
    {
        print(_jumpBarColour.a);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fadingIn();
    }

    public void Starting()
    {
        _visible = true;
        _jumpBarColour.a = 0.0f;
        _jumpTexture.Apply();
    }

    public void Ending()
    {
        _visible = false;
    }

    private void fadingIn()
    {
        if (!_visible) { return; }
        //_jumpBarColour.a += 0.1f;
        print(_jumpBarColour.a);
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width - 236, Screen.height - 500, 50, -_gameManager.JumpBarPoints),_jumpTexture);
    }
}
