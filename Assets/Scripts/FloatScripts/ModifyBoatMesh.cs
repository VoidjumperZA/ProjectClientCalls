﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModifyBoatMesh
{
    private Transform _floatObjTrans;

    private Vector3[] _floatObjVertices;

    private int[] _floatObjTriangles;

    public Vector3[] globalFloatObjVertices;

    private float[] _allDistancesToWater;

    public List<TriangleData> underWaterTriangleData = new List<TriangleData>();

    private float _waterHeight;

    public ModifyBoatMesh(GameObject boatObj, float pwaterHeight)
    {
        _floatObjTrans = boatObj.transform;
        _waterHeight = pwaterHeight;

        _floatObjVertices = boatObj.GetComponent<MeshFilter>().mesh.vertices;
        _floatObjTriangles = boatObj.GetComponent<MeshFilter>().mesh.triangles;

        globalFloatObjVertices = new Vector3[_floatObjVertices.Length];

        _allDistancesToWater = new float[_floatObjVertices.Length];
    }

    public void GenerateUnderwaterMesh()
    {
        //Reset
        underWaterTriangleData.Clear();

        //Find all the distances to water once because some triangles share vertices, so reuse
        for (int j = 0; j < _floatObjVertices.Length; j++)
        {
            //The coordinate should be in global position
            Vector3 globalPos = _floatObjTrans.TransformPoint(_floatObjVertices[j]);

            //Save the global position so we only need to calculate it once here
            //And if we want to debug we can convert it back to local
            globalFloatObjVertices[j] = globalPos;

            _allDistancesToWater[j] = FloatHelper.DistanceToWater(globalPos, _waterHeight);
        }

        //Add the triangles that are below the water
        AddTriangles();
    }

    //Add all the triangles that's part of the underwater mesh
    private void AddTriangles()
    {
        //List that will store the data we need to sort the vertices based on distance to water
        List<VertexData> vertexData = new List<VertexData>();

        //Add init data that will be replaced
        vertexData.Add(new VertexData());
        vertexData.Add(new VertexData());
        vertexData.Add(new VertexData());


        //Loop through all the triangles (3 vertices at a time = 1 triangle)
        int i = 0;
        while (i < _floatObjTriangles.Length)
        {
            //Loop through the 3 vertices
            for (int x = 0; x < 3; x++)
            {
                //Save the data we need
                vertexData[x].distance = _allDistancesToWater[_floatObjTriangles[i]];

                vertexData[x].index = x;

                vertexData[x].globalVertexPos = globalFloatObjVertices[_floatObjTriangles[i]];

                i++;
            }


            //All vertices are above the water
            if (vertexData[0].distance > 0f && vertexData[1].distance > 0f && vertexData[2].distance > 0f)
            {
                continue;
            }


            //Create the triangles that are below the waterline

            //All vertices are underwater
            if (vertexData[0].distance < 0f && vertexData[1].distance < 0f && vertexData[2].distance < 0f)
            {
                Vector3 p1 = vertexData[0].globalVertexPos;
                Vector3 p2 = vertexData[1].globalVertexPos;
                Vector3 p3 = vertexData[2].globalVertexPos;

                //Save the triangle
                underWaterTriangleData.Add(new TriangleData(p1, p2, p3,_waterHeight));
            }
            //1 or 2 vertices are below the water
            else
            {
                //Sort the vertices
                vertexData.Sort((x, y) => x.distance.CompareTo(y.distance));

                vertexData.Reverse();

                //One vertice is above the water, the rest is below
                if (vertexData[0].distance > 0f && vertexData[1].distance < 0f && vertexData[2].distance < 0f)
                {
                    AddTrianglesOneAboveWater(vertexData);
                }
                //Two vertices are above the water, the other is below
                else if (vertexData[0].distance > 0f && vertexData[1].distance > 0f && vertexData[2].distance < 0f)
                {
                    AddTrianglesTwoAboveWater(vertexData);
                }
            }
        }
    }

    //Build the new triangles where one of the old vertices is above the water
    private void AddTrianglesOneAboveWater(List<VertexData> vertexData)
    {
        //H is always at position 0
        Vector3 H = vertexData[0].globalVertexPos;

        //Left of H is M
        //Right of H is L

        //Find the index of M
        int M_index = vertexData[0].index - 1;
        if (M_index < 0)
        {
            M_index = 2;
        }

        //We also need the heights to water
        float h_H = vertexData[0].distance;
        float h_M = 0f;
        float h_L = 0f;

        Vector3 M = Vector3.zero;
        Vector3 L = Vector3.zero;

        //This means M is at position 1 in the List
        if (vertexData[1].index == M_index)
        {
            M = vertexData[1].globalVertexPos;
            L = vertexData[2].globalVertexPos;

            h_M = vertexData[1].distance;
            h_L = vertexData[2].distance;
        }
        else
        {
            M = vertexData[2].globalVertexPos;
            L = vertexData[1].globalVertexPos;

            h_M = vertexData[2].distance;
            h_L = vertexData[1].distance;
        }


        //Now we can calculate where we should cut the triangle to form 2 new triangles
        //because the resulting area will always form a square

        //Point I_M
        Vector3 MH = H - M;

        float t_M = -h_M / (h_H - h_M);

        Vector3 MI_M = t_M * MH;

        Vector3 I_M = MI_M + M;


        //Point I_L
        Vector3 LH = H - L;

        float t_L = -h_L / (h_H - h_L);

        Vector3 LI_L = t_L * LH;

        Vector3 I_L = LI_L + L;


        //Save the data, such as normal, area, etc      
        //2 triangles below the water  
        underWaterTriangleData.Add(new TriangleData(M, I_M, I_L,_waterHeight));
        underWaterTriangleData.Add(new TriangleData(M, I_L, L,_waterHeight));
    }

    //Build the new triangles where two of the old vertices are above the water
    private void AddTrianglesTwoAboveWater(List<VertexData> vertexData)
    {
        //H and M are above the water
        //H is after the vertice that's below water, which is L
        //So we know which one is L because it is last in the sorted list
        Vector3 L = vertexData[2].globalVertexPos;

        //Find the index of H
        int H_index = vertexData[2].index + 1;
        if (H_index > 2)
        {
            H_index = 0;
        }


        //We also need the heights to water
        float h_L = vertexData[2].distance;
        float h_H = 0f;
        float h_M = 0f;

        Vector3 H = Vector3.zero;
        Vector3 M = Vector3.zero;

        //This means that H is at position 1 in the list
        if (vertexData[1].index == H_index)
        {
            H = vertexData[1].globalVertexPos;
            M = vertexData[0].globalVertexPos;

            h_H = vertexData[1].distance;
            h_M = vertexData[0].distance;
        }
        else
        {
            H = vertexData[0].globalVertexPos;
            M = vertexData[1].globalVertexPos;

            h_H = vertexData[0].distance;
            h_M = vertexData[1].distance;
        }


        //Now we can find where to cut the triangle

        //Point J_M
        Vector3 LM = M - L;

        float t_M = -h_L / (h_M - h_L);

        Vector3 LJ_M = t_M * LM;

        Vector3 J_M = LJ_M + L;


        //Point J_H
        Vector3 LH = H - L;

        float t_H = -h_L / (h_H - h_L);

        Vector3 LJ_H = t_H * LH;

        Vector3 J_H = LJ_H + L;


        //Save the data, such as normal, area, etc
        //1 triangle below the water
        underWaterTriangleData.Add(new TriangleData(L, J_H, J_M,_waterHeight));
    }

    //Help class to store triangle data so we can sort the distances
    private class VertexData
    {
        //The distance to water from this vertex
        public float distance;
        //An index so we can form clockwise triangles
        public int index;
        //The global Vector3 position of the vertex
        public Vector3 globalVertexPos;
    }
}



