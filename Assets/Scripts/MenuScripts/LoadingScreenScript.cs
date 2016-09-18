using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenScript : MonoBehaviour {

    private int _scene;

    [SerializeField]
    private Text _loadingText; //text that says LOADING

    [SerializeField]
    private Text _tipsText; //text that gives hints

    private void Start()
    {
        _scene = PlayerPrefs.GetInt("SceneToLoad");

        _tipsText.text = PlayerPrefs.GetString("TipsText");

        StartCoroutine(LoadNewScene());
    }
    //This time we're only using the update to indicate the scene is loading by flashing the loading text
    private void Update()
    {
        _loadingText.color = new Color(_loadingText.color.r, _loadingText.color.g, _loadingText.color.b, Mathf.PingPong(Time.time, 1));
    }

    private IEnumerator LoadNewScene()
    {
        //wait for x seconds before starting the loading of the new screen. Usefull when scene loads too fast
        yield return new WaitForSeconds(2);

        //start loading the new Scene
        AsyncOperation async = SceneManager.LoadSceneAsync(_scene);

        //stay in this scene till loading is complete.
        while (!async.isDone)
        {
            yield return null;
        }
    }
}
