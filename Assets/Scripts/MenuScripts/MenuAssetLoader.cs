using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuAssetLoader : MonoBehaviour {

    GameObject[] objects;

    private void Start()
    {   //get all objects in scene that contain objectdata
        objects = FindObjectsOfType<GameObject>();
        //check if there's new assets for their prefabs
        LoadAssets("menusounds", new string[] { "MenuSound" });
        LoadAssets("backgrounds", new string[] { "Background" });
        LoadAssets("menutextures", new string[] { "BookTexture" });
        LoadAssets("scenes", new string[] { "MenuScene" });
    }

    public static string GetBundlePathForLoadFromFile(string relativePath)
    {
        string streamingAssetsPath = Application.streamingAssetsPath;

        return Path.Combine(streamingAssetsPath, relativePath);
    }

    private void LoadAssets(string bundleName, string[] prefabNames)
    {
        AssetBundle myLoadedAssetBundle = AssetBundle.LoadFromFile(GetBundlePathForLoadFromFile(bundleName));
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }

        switch (bundleName)
        {
            case "menusounds":
                AudioSource audiosource = FindObjectOfType<AudioSource>();
                if (audiosource == null) return;

                for (int j = 0; j < prefabNames.Length; j++)
                {
                    AudioClip clip = myLoadedAssetBundle.LoadAsset<AudioClip>(prefabNames[j]);
                    if (clip != null)
                    {
                        audiosource.clip = clip;
                        audiosource.Play();
                    }
                }
                myLoadedAssetBundle.Unload(false);
                break;
            case "backgrounds":
                GameObject background = GameObject.Find("Background");
                if (background == null) return;

                for (int j = 0; j < prefabNames.Length; j++)
                {
                    Texture2D pic = myLoadedAssetBundle.LoadAsset<Texture>(prefabNames[j]) as Texture2D;
                    if (pic != null)
                    {
                        background.GetComponent<Image>().sprite = Sprite.Create(pic, new Rect(0,0,pic.width,pic.height),Vector2.zero);
                    }
                }
                myLoadedAssetBundle.Unload(false);
                break;
            case "menutextures":
                break;
            case "scenes":
                string[] scenePath = myLoadedAssetBundle.GetAllScenePaths();
                if (scenePath.Length == 0) return;
                var async = SceneManager.LoadSceneAsync(Path.GetFileNameWithoutExtension(scenePath[0]));
                myLoadedAssetBundle.Unload(false);
                break;
            default:
                break;
        }
    }

}
