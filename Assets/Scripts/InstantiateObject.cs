using UnityEngine;
using System.Collections;

public class InstantiateObject : MonoBehaviour
{

    [SerializeField]
    private GameObject prefabToInstantiate;

    [SerializeField]
    private bool preInstantiate;

    [SerializeField]
    private int preInstantiatePasses;

    [SerializeField]
    private int preInstantiateDepth;

    private int yMin;
    private int yMax;
    private int xMin;
    private int xMax;
    private int zMin;
    private int zMax;

    /*
    [SerializeField]
    private int spawnFieldWidth;

    [SerializeField]
    private int spawnFieldHeight;

    [SerializeField]
    private int spawnFieldDepth;
    */
    [SerializeField]
    private int framesBetweenSpawns;

    [SerializeField]
    private int maxObjectsAWave;

    [SerializeField]
    private bool maxObjectsAWaveRandom;

    [SerializeField]
    private bool addScript;

    [SerializeField]
    private MonoBehaviour[] scriptsToAdd;

    [SerializeField]
    [Range(0, 10)]
    private int chanceOfRescalingObject;

    [SerializeField]
    private float rescalingLowerBound;

    [SerializeField]
    private float rescalingUpperBound;

    [SerializeField]
    private bool hideRenderer;

    private int counter = 0;
    private Vector3[] worldSpaceVertices = new Vector3[8];

    void Awake()
    {
        Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;        

        //Only need the first 8 vertices, the others are UV co-ordinates
        for (int i = 0; i < 8; i++)
        {
            //transform total co-ordinates to world space
            worldSpaceVertices[i] = transform.TransformPoint(vertices[i]);
            Debug.Log("world space vert " + i + ": " + worldSpaceVertices[i]);
        }

        //take our mess of co-ordinates and find the min and max's
        sortVertexBounds();

        //disable the spawner's collider otherwise objects will be trapped inside it
        BoxCollider collider = gameObject.GetComponent<BoxCollider>();
        collider.enabled = false;

        if (hideRenderer == true)
        {
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;
        }
    }
    // Use this for initialization
    void Start()
    {
        if (preInstantiate == true)
        {
            for (int i = 0; i < preInstantiatePasses; i++)
            {
                generateObject();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //for spawning
        counter++;
        if (counter == 1000)
        { counter = 0; }

        //only spawn on a spesified frame
        if (counter % framesBetweenSpawns == 0)
        {
            generateObject();
        }
    }

    private void generateObject()
    {
        int numObjectsSpawning = maxObjectsAWave;
        if (maxObjectsAWaveRandom)
        {
            numObjectsSpawning = Random.Range(0, maxObjectsAWave);
        }
        
        for (int i = 0; i < numObjectsSpawning; i++)
        {
            //create a new object and instantiate it
            GameObject newObject;
            newObject = Instantiate(prefabToInstantiate);

            //if we want to add a script, do so
            if (addScript == true)
            {
                //add all the scripts in our array
                for (i = 0; i < scriptsToAdd.Length; i++)
                {
                    newObject.AddComponent(scriptsToAdd[i].GetType());
                }
            }                

            int fieldX = Random.Range(xMin, xMax);
            int fieldY = Random.Range(yMax, yMin); //y is inverted because it works top down
            int fieldZ = Random.Range(zMin, zMax);

            newObject.transform.position = new Vector3(fieldX, fieldY, fieldZ);

            //chance of rescaling, based on user input
            int rescaleInt = Random.Range(0, 10);
            if (rescaleInt <= chanceOfRescalingObject)
            {
                float rescaleFloat = Random.Range(rescalingLowerBound, rescalingUpperBound);
                //make a vector of the rescale
                Vector3 rescale = new Vector3(rescaleFloat, rescaleFloat, rescaleFloat);
                //save the objects's scale
                Vector3 scaleStorage = newObject.transform.localScale;
                //scale the clone
                scaleStorage.Scale(rescale);
                //reapply the scale
                newObject.transform.localScale = scaleStorage;
            }
          
        }
    }

    //find the min and max vertex bounds
    private void sortVertexBounds()
    {
        //you shouldn't be building your level more than this far from your scene's origin anyways
        yMax = -999999999;
        yMin = 999999999;

        xMax = -999999999;
        xMin = 999999999;

        zMax = -999999999;
        zMin = 999999999;

        //sort
        for (int i = 0; i < worldSpaceVertices.Length; i++)
        {
            if (worldSpaceVertices[i].y > yMax) { yMax = (int)worldSpaceVertices[i].y; }
            if (worldSpaceVertices[i].y < yMin) { yMin = (int)worldSpaceVertices[i].y; }

            if (worldSpaceVertices[i].x > xMax) { xMax = (int)worldSpaceVertices[i].x; }
            if (worldSpaceVertices[i].x < xMin) { xMin = (int)worldSpaceVertices[i].x; }

            if (worldSpaceVertices[i].z > zMax) { zMax = (int)worldSpaceVertices[i].z; }
            if (worldSpaceVertices[i].z < zMin) { zMin = (int)worldSpaceVertices[i].z; }
        }
    }

}
