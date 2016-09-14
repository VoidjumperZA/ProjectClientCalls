using UnityEngine;
using System.Collections;

public class FloatHelper : MonoBehaviour {

    //calculate the volume of a triangle in a mesh
    private static float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float v123 = p1.x * p2.y * p3.z;
        float v132 = p1.x * p3.y * p2.z;
        float v231 = p2.x * p3.y * p1.z;
        float v213 = p2.x * p1.y * p3.z;
        float v321 = p3.x * p2.y * p1.z;
        float v312 = p3.x * p1.y * p2.z;

        return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
    }

    //loops through the mesh triangles and gets the total volume using signed volume theory
    public static float MeshVolume(MeshFilter meshFilter)
    {
        Mesh mesh = meshFilter.sharedMesh;
        float volume = 0;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Vector3 p1 = vertices[triangles[i + 0]];
            Vector3 p2 = vertices[triangles[i + 1]];
            Vector3 p3 = vertices[triangles[i + 2]];
            volume += SignedVolumeOfTriangle(p1, p2, p3);
        }
        volume *= meshFilter.gameObject.transform.localScale.x * meshFilter.gameObject.transform.localScale.y * meshFilter.gameObject.transform.localScale.z;
        return Mathf.Abs(volume);
    }

    //calculate the object's distance to water's surface
    public static float DistanceToWater(Vector3 position, float waterHeight)
    {
        float distanceToWater = position.y - waterHeight;

        return distanceToWater;
    }
}
