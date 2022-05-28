using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter), typeof(MeshRenderer))]

public class Rings_rotate : MonoBehaviour {

    Mesh mesh;
    float theta = 3.1415926535897931f / 180.0f;
    public float radius;

    Vector3[] vertices;
    Vector2[] uvs;
    int[] triangles;
//    public Material Ring_mat;
//    MeshRenderer mr = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

    const int Size = 18;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }
    void Start()
    {
        MakeDiscreteProceduralGrid();
        UpdateMesh();
    }

    void MakeDiscreteProceduralGrid()
    {
        vertices = new Vector3[4 * Size * 1];
        triangles = new int[6 * Size * 2];
        uvs = new Vector2[4 * Size * 1];
        Vector2 Center2 = new Vector2(0.5f, 0.5f);

        float res_angle = 20f;
        float T1 = res_angle * theta;
        for (int i = 0; i < Size; i++)
        {
            vertices[4 * i + 0] = polar_coo(0.925f*radius, T1 * i);
            vertices[4 * i + 1] = polar_coo(radius, T1 * i);
            vertices[4 * i + 2] = polar_coo(radius, T1 * (i + 1));
            vertices[4 * i + 3] = polar_coo(0.925f*radius, T1 * (i + 1));
        }
        for (int i = 0; i < Size; i++)
        {
            triangles[6 * i + 0] = 4 * i + 0;
            triangles[6 * i + 1] = 4 * i + 1;
            triangles[6 * i + 2] = 4 * i + 2;
            triangles[6 * i + 3] = 4 * i + 0;
            triangles[6 * i + 4] = 4 * i + 2;
            triangles[6 * i + 5] = 4 * i + 3;
        }
        
        for (int i = 0; i < Size; i++)
        {
            triangles[6 * Size + 6 * i + 0] = 4 * i + 0;
            triangles[6 * Size + 6 * i + 1] = 4 * i + 2;
            triangles[6 * Size + 6 * i + 2] = 4 * i + 1;
            triangles[6 * Size + 6 * i + 3] = 4 * i + 2;
            triangles[6 * Size + 6 * i + 4] = 4 * i + 0;
            triangles[6 * Size + 6 * i + 5] = 4 * i + 3;
        }
                
        for (int i = 0; i < Size; i++)
        {

//            uvs[4 * i + 0] = new Vector2(vertices[4*i+0].x,vertices[4 * i + 0].z);
//            uvs[4 * i + 1] = new Vector2(vertices[4 * i + 1].x, vertices[4 * i + 1].z);
//            uvs[4 * i + 2] = new Vector2(vertices[4 * i + 2].x, vertices[4 * i + 2].z);
//            uvs[4 * i + 3] = new Vector2(vertices[4 * i + 3].x, vertices[4 * i + 3].z);

            uvs[4 * i + 0] = polar_uv(0.925f*1f, T1 * i) + Center2;
            uvs[4 * i + 1] = polar_uv(1f, T1 * (i)) + Center2;
            uvs[4 * i + 2] = polar_uv(1f, T1 * (i + 1)) + Center2;
            uvs[4 * i + 3] = polar_uv(0.925f * 1f, T1 * (i+1)) + Center2;
        }                
    }

    void UpdateMesh()
    {
        mesh.Clear();


//        mr.material = Ring_mat;
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        //        mesh.RecalculateBounds();
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }


    public static Vector3 polar_coo(float r, float theta)
    {
        return new Vector3(r * Mathf.Cos(theta), r * Mathf.Sin(theta), 0.0f);
    }

    
    public static Vector2 polar_uv(float r, float theta)
    {
        return new Vector2(r*Mathf.Cos(theta), r*Mathf.Sin(theta));
    }
    
}
