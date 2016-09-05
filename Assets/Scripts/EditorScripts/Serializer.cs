using UnityEngine;
using System.Collections;
using System.IO;
using CCEditor;
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

//Allow script to run in edit mode
[ExecuteInEditMode]
public class Serializer : MonoBehaviour
{

    BinaryFormatter bf = new BinaryFormatter();

    public void SaveData()
    {
        //find all objects containing ObjectData
        ObjectData[] objects = (ObjectData[])FindObjectsOfType(typeof(ObjectData));

        //create an array of serializable objData we want to send
        ObjData[] objs = new ObjData[objects.Length];

        //fill it with the data from the editor
        for (int i = 0; i < objects.Length; i++)
        {
            objs[i] = new ObjData(objects[i].id, objects[i].gameObject.tag, objects[i].gameObject.name, objects[i].gameObject.layer, objects[i].gameObject.transform.position.x, objects[i].gameObject.transform.position.y, objects[i].gameObject.transform.position.z, objects[i].gameObject.transform.rotation.x, objects[i].gameObject.transform.rotation.y, objects[i].gameObject.transform.rotation.z, objects[i].gameObject.transform.rotation.w, objects[i].gameObject.transform.localScale.x, objects[i].gameObject.transform.localScale.y, objects[i].gameObject.transform.localScale.z, (int)objects[i]._type);
            print(objs[i].tag);
            print(objects[i].gameObject.tag);
        }

        //wrapper class
        ObjectDataList objList = new ObjectDataList(objs);

        FileStream fileStream = new FileStream(Application.dataPath + "/Saves/Datafile.dat", FileMode.Create);
        try
        {
            bf.Serialize(fileStream, objList);
        }
        catch (SerializationException e)
        {
            print(e.Message);
            throw;
        }
        finally
        {
            fileStream.Close();
            print("Level SaveD");
        }

    }

    //deserialize data from the file
    public void LoadData()
    {
        FileStream fileStream = new FileStream(Application.dataPath + "/Saves/Datafile.dat", FileMode.Open);
        ObjectDataList newList = null;
        try
        {
            object deserialized = bf.Deserialize(fileStream);
            newList = deserialized as ObjectDataList;
        }
        catch (SerializationException e)
        {
            print(e.Message);
            throw;
        }
        finally
        {
            fileStream.Close();
            DeleteExistingObjects();
            InstatiateObjects(newList);
            print("Level LoadeD");
        }

    }

    //spawn each object in the editor view
    private void InstatiateObjects(ObjectDataList pobjDatalist)
    {
        ObjectDataList list = pobjDatalist;

        GameObject[] ob = Resources.LoadAll<GameObject>("Prefabs");

        if (ob.Length > 0 && list.objects.Length > 0)
        {
            for (int i = 0; i < list.objects.Length; i++)
            {
                for (int y = 0; y < ob.Length; y++)
                {
                    if (ob[y].GetComponent<ObjectData>() != null && ob[y].GetComponent<ObjectData>().id == list.objects[i].id)
                    {
                        GameObject tempObj = (GameObject)Instantiate(ob[y], new Vector3(list.objects[i].positionX, list.objects[i].positionY, list.objects[i].positionZ), new Quaternion(list.objects[i].rotationX, list.objects[i].rotationY, list.objects[i].rotationZ, list.objects[i].rotationW));
                        if (list.objects[i].tag != null)
                        {
                            tempObj.gameObject.tag = list.objects[i].tag;
                        }
                            
                        tempObj.gameObject.layer = list.objects[i].layer;
                        tempObj.gameObject.name = list.objects[i].name;
                        tempObj.gameObject.transform.localScale = new Vector3(list.objects[i].scaleX, list.objects[i].scaleY, list.objects[i].scaleZ);
                    }
                }
            }
        }

    }

    private void DeleteExistingObjects()
    {
        ObjectData[] objects = (ObjectData[])FindObjectsOfType(typeof(ObjectData));
        //GameObject[] go = (GameObject[])FindObjectsOfType(typeof(ObjectData));

        for (int i = objects.Length-1; i >= 0; i--)
        {
            DestroyImmediate(objects[i].gameObject);
        }
    }

    public void SaveTerrainData()
    {
        Terrain TD = FindObjectOfType<Terrain>();

        if(TD != null)
        {
            try
            {
                if (AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Prefabs/"+TD.name+".prefab"))
                {
                    Object fab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Resources/Prefabs/" + TD.name + ".prefab");
                    PrefabUtility.ReplacePrefab(TD.gameObject, fab, ReplacePrefabOptions.Default);
                    PrefabUtility.DisconnectPrefabInstance(TD.gameObject);
                }
                else
                {
                    Object fab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Prefabs/" + TD.name + ".prefab");
                    PrefabUtility.ReplacePrefab(TD.gameObject, fab, ReplacePrefabOptions.Default);
                    PrefabUtility.DisconnectPrefabInstance(TD.gameObject);
                }
            }
            catch (SerializationException e)
            {
                print(e.Message);
                throw;
            }
            finally
            {
                print("Terrain Saved");
            }
        }
        else
        {
            print("No terrain found");
        }
    }

    public void LoadTerrainData()
    {
        Terrain TD = FindObjectOfType<Terrain>();
        if (TD != null)
        {
            DestroyImmediate(TD.gameObject);
        }

        try
        {
            GameObject[] ob = Resources.LoadAll<GameObject>("Prefabs");

            if (ob.Length > 0)
            {
                for (int i = 0; i < ob.Length; i++)
                {
                    if (ob[i].GetComponent<Terrain>())
                    {
                        GameObject fab = PrefabUtility.InstantiatePrefab(ob[i]) as GameObject;
                        PrefabUtility.DisconnectPrefabInstance(fab);
                    }
                }
            }
        }
        catch (SerializationException e)
        {
            print(e.Message);
            throw;
        }
        finally
        {
            print("Terrain loaded");
        }
    }
}
