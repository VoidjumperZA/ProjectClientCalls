using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeCounterScript : MonoBehaviour {

    [SerializeField]
    private Text _timerText;
    private float _timer;
    private bool _counting = true;


    private void Start()
    {
        _timer = 0;
        _timerText.text = _timer.ToString();
    }

    private void Update()
    {
        if (_counting)
        {
            _timer += Time.deltaTime;
            _timerText.text = ((int)_timer).ToString();
        }
    }

    public void SetCounting(bool value)
    {
        _counting = value;
    }

    public int GetTimerInInt()
    {
        return (int)_timer;
    }
}
