using UnityEngine;
using System.Collections;
using System.IO;

public class LoadFromFileExample : MonoBehaviour
{
    ObjectData[] objects;

    private void Start()
    {   //get all objects in scene that contain objectdata
        objects = FindObjectsOfType<ObjectData>();
        //check if there's new assets for their prefabs
        LoadAssets("decoshrooms",new string[] { "DecoShroom" });
        LoadAssets("fireflies", new string[] { "Firefly" });
        LoadAssets("flatshrooms", new string[] { "FlatShroom" });
        //LoadAssets("logs", new string[] { "Log" });
        LoadAssets("plants", new string[] { "Plant1", "Plant2", "Plant3", "Plant4" });
        LoadAssets("stones", new string[] { "Stone1", "Stone2", "Stone3", "Stone4" , "Stone5" });
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

        for (int i = 0; i < objects.Length; i++)
        {
            for (int j = 0; j < prefabNames.Length; j++)
            {
                GameObject prefab = myLoadedAssetBundle.LoadAsset<GameObject>(prefabNames[j]);
                if (prefab != null)
                {
                    if (objects[i].gameObject == null) continue;
                    string[] newName = objects[i].gameObject.name.Split(' ');
                    if (prefabNames[j] == newName[0])
                    {
                        prefab.tag = objects[i].gameObject.tag;
                        Instantiate(prefab, objects[i].transform.position, objects[i].transform.rotation);
                        Destroy(objects[i].gameObject);
                    }
                }
            }
        }

        myLoadedAssetBundle.Unload(false);
    }

}
