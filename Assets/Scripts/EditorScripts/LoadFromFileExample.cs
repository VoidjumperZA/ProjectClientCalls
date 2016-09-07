using UnityEngine;
using System.Collections;
using System.IO;

public class LoadFromFileExample : MonoBehaviour
{

    private void Start()
    {
        AssetBundle myLoadedAssetBundle = AssetBundle.LoadFromFile(GetBundlePathForLoadFromFile("mushrooms"));
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
        GameObject prefab = myLoadedAssetBundle.LoadAsset<GameObject>("Mushroom");
        Instantiate(prefab);

        myLoadedAssetBundle.Unload(false);
    }

    public static string GetBundlePathForLoadFromFile(string relativePath)
    {
        string streamingAssetsPath = Application.streamingAssetsPath;

        return Path.Combine(streamingAssetsPath, relativePath);
    }

}
