using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloatingObject : MonoBehaviour {

    //the object that represents the surface of the water
    private GameObject _waterSurfaceObj;
    private float _waterHeight;

    //set the density of the object(an average log has around 700 density per m^3)
    [SerializeField]
    private float _objectDensity;

    private Rigidbody _floatObjRB;

    //density of the water the object is floating in
    [SerializeField]
    private float _waterDensity = 1027f;

    //script that handles functions needed with the object's mesh
    private ModifyBoatMesh _modifyBoatMesh;
    
    private void Start()
    {
        _waterSurfaceObj = GameObject.FindGameObjectWithTag("WaterSurface");
        if (_waterSurfaceObj == null) print("it's null");
        _floatObjRB = GetComponent<Rigidbody>();
        _waterHeight = _waterSurfaceObj.transform.position.y;

        _modifyBoatMesh = new ModifyBoatMesh(gameObject,_waterHeight);//

        float volume = FloatHelper.MeshVolume(GetComponent<MeshFilter>());
        _floatObjRB.mass = _objectDensity * volume;
    }

    private void Update()
    {
        //Generate the underwaterMesh
        _modifyBoatMesh.GenerateUnderwaterMesh();
    }

    private void FixedUpdate()
    {
        if (_modifyBoatMesh.underWaterTriangleData.Count > 0)
        {
            AddUnderWaterForces();
        }
    }

    private void AddUnderWaterForces()
    {
        //Get all the triangles
        List<TriangleData> underwaterTriangleData = _modifyBoatMesh.underWaterTriangleData;

        for (int i = 0; i < underwaterTriangleData.Count; i++)
        {
            //Calculate the buoyance force
            Vector3 buoyancyForce = BuoyancyForce(_waterDensity, underwaterTriangleData[i]);

            _floatObjRB.AddForceAtPosition(buoyancyForce, underwaterTriangleData[i].center);
        }
    }

    private Vector3 BuoyancyForce(float waterdensity, TriangleData triangleData)
    {
        //Buoyancy force is there even if the objects aren't moving
        //force buoyancy = density * gravity * Volume of fluid
        //volume = distance to surface * surface area*normal to the surface
        Vector3 buyoancyForce = waterdensity * Physics.gravity.y * triangleData.distanceToSurface * triangleData.area * triangleData.normal;

        //we only care about the y force for the floating
        buyoancyForce.x = 0f;
        buyoancyForce.z = 0f;

        return buyoancyForce;
    }
}
