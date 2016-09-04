using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Builder : MonoBehaviour {

    private Stack<GameObject> spawnedObjects = new Stack<GameObject>();

    public Vector3 position = Vector3.zero;
    public Vector3 rotation = new Vector3(0f, 0f, 0f);
    public Vector3 localScale = new Vector3(1f,1f,1f);


    private GameObject[] prefabs;

    private void Start()
    {
        prefabs = Resources.LoadAll<GameObject>("Prefabs");
    }

    public void AddObject(int i)
    {
        GameObject tempObj = (GameObject)Instantiate(prefabs[i], position, Quaternion.Euler(rotation));
        tempObj.transform.localScale = localScale;
        spawnedObjects.Push(tempObj);
    }

    public GameObject[] GetGameObjects()
    {
        if (prefabs.Length == 0 || prefabs == null)
        {
            RefreshListOfObjects();
        }
        return prefabs;
    }

    public void Undo()
    {
        if (spawnedObjects.Count > 0)
        {
            DestroyImmediate(spawnedObjects.Peek());
            spawnedObjects.Pop();
        }
            
    }

    public void RefreshListOfObjects()
    {
        prefabs = Resources.LoadAll<GameObject>("Prefabs");
    }
}
