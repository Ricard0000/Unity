using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Arch_type5Perimeter2 : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector2[] uvs;
    int[] triangles;

    const float PI = 3.1415926535897931f;


    const int N = 6;

    public float c1 = 1.25f;//x-direction
    public float c2 = 1.5f;//y-direction
    public float c3 = 0.75f;//z-direction

    public float s = 0.5f;//Thickness of sides
    public float t = 0.5f;//Thickness of Top of arch


    //    const float c1 = 0.90625f;//x-direction
    //    const float c2 = 1.125f;//y-direction
    //    const float c3 = 0.25f;//z-direction

    float delta_theta = (PI / 2) / N;


    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }
    void Start()
    {
        MakeDiscreteProceduralGrid();
        UpdateMesh();
    }

    public static Vector3 Arch_eq_6(float x, float z, float c1, float c2)
    {
        float y;
        y = c2 * Mathf.Sqrt(1 - Mathf.Abs(x) / (c1 + 0.00001f));
        y = y + c2 * Mathf.Sqrt(1 - Mathf.Abs(x * x) / (c1 * c1 + 0.00001f));
        y = y / 2.0f - 0.00001f;
        return new Vector3(x, y, z);
    }

    public static float uv_Arch_eq_6(float x)
    {
        float y;
        y = Mathf.Sqrt(1 - Mathf.Abs(x) / (1.0f + 0.00001f));
        y = y + Mathf.Sqrt(1 - Mathf.Abs((x) * (x)) / (1.0f + 0.00001f));
        y = y / 2.0f - 0.00001f;
        return y;
    }

    void MakeDiscreteProceduralGrid()
    {
        GameObject go = new GameObject("Plane");
        MeshFilter mf = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer mr = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;


        Mesh m = new Mesh();

        vertices = new Vector3[2 * (2 * N + 1 + 2) + 16];
        triangles = new int[3 * 4 * N + 3 * 2 * 10];

        uvs = new Vector2[2 * (2 * N + 1 + 2) + 16];

        float x;
        float z;

        for (int i = 0; i < 2 * N + 1; i++)//1
        {
            x = c1 * Mathf.Cos(PI - i * delta_theta);
            z = 0.0f;
            vertices[i] = Arch_eq_6(x, z, c1, c2);
        }
        vertices[2 * N + 1] = new Vector3(-c1, c2, 0.0f);
        vertices[2 * N + 2] = new Vector3(c1, c2, 0.0f);
        for (int i = 0; i < 2 * N + 1; i++)//1
        {
            x = c1 * Mathf.Cos(PI - i * delta_theta);
            z = c3;
            vertices[2 * N + 3 + i] = Arch_eq_6(x, z, c1, c2);
        }
        vertices[4 * N + 4] = new Vector3(-c1, c2, c3);
        vertices[4 * N + 5] = new Vector3(c1, c2, c3);

        //Front of wall
        //Left
        vertices[4 * N + 6] = new Vector3(-c1 - s, 0.0f, 0.0f);
        vertices[4 * N + 7] = new Vector3(-c1 - s, c2, 0.0f);
        vertices[4 * N + 8] = new Vector3(-c1 - s, c2 + t, 0.0f);

        //Top
        vertices[4 * N + 9] = new Vector3(-c1, c2 + t, 0.0f);
        vertices[4 * N + 10] = new Vector3(c1, c2 + t, 0.0f);

        //Right
        vertices[4 * N + 11] = new Vector3(c1 + s, 0.0f, 0.0f);
        vertices[4 * N + 12] = new Vector3(c1 + s, c2, 0.0f);
        vertices[4 * N + 13] = new Vector3(c1 + s, c2 + t, 0.0f);

        //Back of wall
        //Left
        vertices[4 * N + 14] = new Vector3(-c1 - s, 0.0f, c3);
        vertices[4 * N + 15] = new Vector3(-c1 - s, c2, c3);
        vertices[4 * N + 16] = new Vector3(-c1 - s, c2 + t, c3);

        //Top
        vertices[4 * N + 17] = new Vector3(-c1, c2 + t, c3);
        vertices[4 * N + 18] = new Vector3(c1, c2 + t, c3);

        //Right
        vertices[4 * N + 19] = new Vector3(c1 + s, 0.0f, c3);
        vertices[4 * N + 20] = new Vector3(c1 + s, c2, c3);
        vertices[4 * N + 21] = new Vector3(c1 + s, c2 + t, c3);









        for (int i = 0; i < N; i++)
        {
            triangles[3 * i + 0] = i + 0;
            triangles[3 * i + 1] = 2 * N + 1;
            triangles[3 * i + 2] = i + 1;
        }
        for (int i = N; i < 2 * N; i++)
        {
            triangles[3 * i + 0] = i + 0;
            triangles[3 * i + 1] = 2 * N + 2;
            triangles[3 * i + 2] = i + 1;
        }
        for (int i = 2 * N + 3; i < 3 * N + 3; i++)
        {
            triangles[3 * (i - 3) + 0] = 4 * N + 4;
            triangles[3 * (i - 3) + 1] = i + 0;
            triangles[3 * (i - 3) + 2] = i + 1;
        }
        for (int i = 3 * N + 3; i < 4 * N + 3; i++)
        {
            triangles[3 * (i - 3) + 0] = 4 * N + 5;
            triangles[3 * (i - 3) + 1] = i + 0;
            triangles[3 * (i - 3) + 2] = i + 1;
        }

        //Front of wall triangles
        triangles[12 * N] = 4 * N + 6;
        triangles[12 * N + 1] = 4 * N + 7;
        triangles[12 * N + 2] = 0;
        triangles[12 * N + 3] = 2 * N + 1;
        triangles[12 * N + 4] = 0;
        triangles[12 * N + 5] = 4 * N + 7;

        triangles[12 * N + 6] = 4 * N + 7;
        triangles[12 * N + 7] = 4 * N + 8;
        triangles[12 * N + 8] = 2 * N + 1;

        triangles[12 * N + 9] = 4 * N + 9;
        triangles[12 * N + 10] = 2 * N + 1;
        triangles[12 * N + 11] = 4 * N + 8;

        triangles[12 * N + 12] = 2 * N + 1;
        triangles[12 * N + 13] = 4 * N + 9;
        triangles[12 * N + 14] = 2 * N + 2;

        triangles[12 * N + 15] = 4 * N + 10;
        triangles[12 * N + 16] = 2 * N + 2;
        triangles[12 * N + 17] = 4 * N + 9;


        triangles[12 * N + 18] = 2 * N + 2;
        triangles[12 * N + 19] = 4 * N + 10;
        triangles[12 * N + 20] = 4 * N + 12;

        triangles[12 * N + 21] = 4 * N + 13;
        triangles[12 * N + 22] = 4 * N + 12;
        triangles[12 * N + 23] = 4 * N + 10;

        triangles[12 * N + 24] = 2 * N;
        triangles[12 * N + 25] = 2 * N + 2;
        triangles[12 * N + 26] = 4 * N + 11;

        triangles[12 * N + 27] = 4 * N + 12;
        triangles[12 * N + 28] = 4 * N + 11;
        triangles[12 * N + 29] = 2 * N + 2;

        //Back of wall triangles

        triangles[12 * N + 30] = 4 * N + 15;
        triangles[12 * N + 31] = 4 * N + 14;
        triangles[12 * N + 32] = 2 * N + 3;

        triangles[12 * N + 33] = 2 * N + 3;
        triangles[12 * N + 34] = 4 * N + 4;
        triangles[12 * N + 35] = 4 * N + 15;

        triangles[12 * N + 36] = 4 * N + 16;
        triangles[12 * N + 37] = 4 * N + 15;
        triangles[12 * N + 38] = 4 * N + 4;

        triangles[12 * N + 39] = 4 * N + 4;
        triangles[12 * N + 40] = 4 * N + 17;
        triangles[12 * N + 41] = 4 * N + 16;

        triangles[12 * N + 42] = 4 * N + 17;
        triangles[12 * N + 43] = 4 * N + 4;
        triangles[12 * N + 44] = 4 * N + 5;

        triangles[12 * N + 45] = 4 * N + 5;
        triangles[12 * N + 46] = 4 * N + 18;
        triangles[12 * N + 47] = 4 * N + 17;

        triangles[12 * N + 48] = 4 * N + 18;
        triangles[12 * N + 49] = 4 * N + 5;
        triangles[12 * N + 50] = 4 * N + 20;

        triangles[12 * N + 51] = 4 * N + 20;
        triangles[12 * N + 52] = 4 * N + 21;
        triangles[12 * N + 53] = 4 * N + 18;

        triangles[12 * N + 54] = 4 * N + 5;
        triangles[12 * N + 55] = 4 * N + 3;
        triangles[12 * N + 56] = 4 * N + 19;

        triangles[12 * N + 57] = 4 * N + 20;
        triangles[12 * N + 58] = 4 * N + 5;
        triangles[12 * N + 59] = 4 * N + 19;


        float xx;
        float scalex;
        float scaley;
        scalex = 2 * s + 2 * c1 + 0.00001f;
        scaley = t + c2 + 0.00001f;

        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = (s + c1) / scalex + c1 / scalex * Mathf.Cos(PI - i * delta_theta);
            xx = Mathf.Cos(PI - i * delta_theta);
            uvs[i] = new Vector2(x, c2 / scaley * uv_Arch_eq_6(xx));
        }
        uvs[2 * N + 1] = new Vector2(s / scalex, c2 / scaley);
        uvs[2 * N + 2] = new Vector2((s + 2 * c1) / scalex, c2 / scaley);

        uvs[4 * N + 6] = new Vector2(0.0f, 0.0f);
        uvs[4 * N + 7] = new Vector2(0.0f, c2 / scaley);
        uvs[4 * N + 8] = new Vector2(0.0f, 1.0f);

        uvs[4 * N + 9] = new Vector2(s / scalex, 1.0f);
        uvs[4 * N + 10] = new Vector2((s + 2 * c1) / scalex, 1.0f);

        uvs[4 * N + 11] = new Vector2(1.0f, 0.0f);
        uvs[4 * N + 12] = new Vector2(1.0f, c2 / scaley);
        uvs[4 * N + 13] = new Vector2(1.0f, 1.0f);

        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = (s + c1) / scalex + c1 / scalex * Mathf.Cos(PI - i * delta_theta);
            xx = Mathf.Cos(PI - i * delta_theta);
            uvs[2 * N + 3 + i] = new Vector2(x, c2 / scaley * uv_Arch_eq_6(xx));
        }

        uvs[4 * N + 4] = new Vector2(s / scalex, c2 / scaley);
        uvs[4 * N + 5] = new Vector2((s + 2 * c1) / scalex, c2 / scaley);

        uvs[4 * N + 14] = new Vector2(0.0f, 0.0f);
        uvs[4 * N + 15] = new Vector2(0.0f, c2 / scaley);
        uvs[4 * N + 16] = new Vector2(0.0f, 1.0f);

        uvs[4 * N + 17] = new Vector2(s / scalex, 1.0f);
        uvs[4 * N + 18] = new Vector2((s + 2 * c1) / scalex, 1.0f);

        uvs[4 * N + 19] = new Vector2(1.0f, 0.0f);
        uvs[4 * N + 20] = new Vector2(1.0f, c2 / scaley);
        uvs[4 * N + 21] = new Vector2(1.0f, 1.0f);





    }






    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }


}