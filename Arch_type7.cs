using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Arch_type7 : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    Vector2[] uvs;
    int[] triangles;

    const float PI = 3.1415926535897931f;


    const int N = 5;

    public float c1 = 1.0f;//x-direction
    public float c2 = 1.25f;//y-direction
    public float c3 = 0.5f;//z-direction
    public float c4 = -2.0f;//Length of Sides


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
    public static Vector3 Arch_eq(float x, float z, float c1, float c2)
    {
        float y;
        y = 0.8f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x) / (0.8f * c1 + 0.00001f));
        y = y + 0.8f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x * x) / (0.8f * c1 * 0.8f * c1 + 0.00001f));
        y = y / 2.0f;

        return new Vector3(x, y, z);
    }
    public static Vector3 Arch_eq_2(float x, float z, float c1, float c2)
    {
        float y;
        y = 0.85f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x) / (0.85f * c1 + 0.00001f));
        y = y + 0.85f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x * x) / (0.85f * c1 * 0.85f * c1 + 0.00001f));
        y = y / 2.0f;

        return new Vector3(x, y, z);
    }

    public static Vector3 Arch_eq_3(float x, float z, float c1, float c2)
    {
        float y;
        y = 0.90f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x) / (c1 * 0.90f + 0.00001f));
        y = y + 0.90f * c2 * Mathf.Sqrt(1 - Mathf.Abs(x * x) / ((c1 * 0.90f) * (c1 * 0.90f) + 0.00001f));
        y = y / 2.0f;
        return new Vector3(x, y, z);
    }


    public static Vector3 Arch_eq_4(float x, float z, float c1, float c2)
    {
        float y;
        y = c2 * Mathf.Sqrt(1 - Mathf.Abs(x) / (c1 + 0.00001f));
        y = y + c2 * Mathf.Sqrt(1 - Mathf.Abs(x * x) / (c1 * c1 + 0.00001f));
        y = y / 2.0f;
        return new Vector3(x, y, z);
    }


    void MakeDiscreteProceduralGrid()
    {
        GameObject go = new GameObject("Plane");
        MeshFilter mf = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer mr = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;


        //Should be based on the equations:
        //x=-c2((y/c1)^2-1)   for  x>0
        //x=-x for x<0

        Mesh m = new Mesh();

        //        N=Grid size in the y-direction
        //        N+1 in the y-direction
        //        2(N+1) in the x-direction
        //        2(N + 1) in the z-direction


        vertices = new Vector3[2 * (2 * N + 1) * 5 + 20];
        triangles = new int[3 * 4 * N * 9 + 108]; //3 * 4 * N * 7

        uvs = new Vector2[2 * (2 * N + 1) * 5 + 20];

        float x;
        float z;


        float f1 = 0.80f;
        float f2 = 0.85f;
        float f3 = 0.90f;

        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = c1 * Mathf.Cos(PI - i * delta_theta);
            z = 0.0f;
            vertices[i] = Arch_eq_4(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = f3 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 0.0f;
            vertices[i + (2 * N + 1)] = Arch_eq_3(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = f3 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = c3 / 5.0f;
            vertices[i + 2 * (2 * N + 1)] = Arch_eq_3(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = f2 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = c3 / 5.0f;
            vertices[i + 3 * (2 * N + 1)] = Arch_eq_2(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = f1 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 2.0f * c3 / 5.0f;
            vertices[i + 4 * (2 * N + 1)] = Arch_eq(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = f1 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 3.0f * c3 / 5.0f;
            vertices[i + 5 * (2 * N + 1)] = Arch_eq(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = f2 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 4.0f * c3 / 5.0f;
            vertices[i + 6 * (2 * N + 1)] = Arch_eq_2(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = f3 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = 4.0f * c3 / 5.0f;
            vertices[i + 7 * (2 * N + 1)] = Arch_eq_3(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = f3 * c1 * Mathf.Cos(PI - i * delta_theta);
            z = c3;
            vertices[i + 8 * (2 * N + 1)] = Arch_eq_3(x, z, c1, c2);
        }
        for (int i = 0; i < 2 * N + 1; i++)
        {
            x = c1 * Mathf.Cos(PI - i * delta_theta);
            z = c3;
            vertices[i + 9 * (2 * N + 1)] = Arch_eq_4(x, z, c1, c2);
        }

        //        m.vertices = vertices;



        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * i + 0] = i + 0;
            triangles[6 * i + 1] = i + 1;
            triangles[6 * i + 2] = i + (2 * N + 1);
            triangles[6 * i + 3] = i + (2 * N + 1);
            triangles[6 * i + 4] = i + 1;
            triangles[6 * i + 5] = i + (2 * N + 1) + 1;
        }

        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 2 * N + 6 * i + 0] = (2 * N + 1) + i + 0;
            triangles[6 * 2 * N + 6 * i + 1] = (2 * N + 1) + i + 1;
            triangles[6 * 2 * N + 6 * i + 2] = (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 2 * N + 6 * i + 3] = (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 2 * N + 6 * i + 4] = (2 * N + 1) + i + 1;
            triangles[6 * 2 * N + 6 * i + 5] = (2 * N + 1) + i + (2 * N + 1) + 1;
        }

        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 4 * N + 6 * i + 0] = 2 * (2 * N + 1) + i + 0;
            triangles[6 * 4 * N + 6 * i + 1] = 2 * (2 * N + 1) + i + 1;
            triangles[6 * 4 * N + 6 * i + 2] = 2 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 4 * N + 6 * i + 3] = 2 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 4 * N + 6 * i + 4] = 2 * (2 * N + 1) + i + 1;
            triangles[6 * 4 * N + 6 * i + 5] = 2 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }

        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 6 * N + 6 * i + 0] = 3 * (2 * N + 1) + i + 0;
            triangles[6 * 6 * N + 6 * i + 1] = 3 * (2 * N + 1) + i + 1;
            triangles[6 * 6 * N + 6 * i + 2] = 3 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 6 * N + 6 * i + 3] = 3 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 6 * N + 6 * i + 4] = 3 * (2 * N + 1) + i + 1;
            triangles[6 * 6 * N + 6 * i + 5] = 3 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }

        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 8 * N + 6 * i + 0] = 4 * (2 * N + 1) + i + 0;
            triangles[6 * 8 * N + 6 * i + 1] = 4 * (2 * N + 1) + i + 1;
            triangles[6 * 8 * N + 6 * i + 2] = 4 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 8 * N + 6 * i + 3] = 4 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 8 * N + 6 * i + 4] = 4 * (2 * N + 1) + i + 1;
            triangles[6 * 8 * N + 6 * i + 5] = 4 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }
        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 10 * N + 6 * i + 0] = 5 * (2 * N + 1) + i + 0;
            triangles[6 * 10 * N + 6 * i + 1] = 5 * (2 * N + 1) + i + 1;
            triangles[6 * 10 * N + 6 * i + 2] = 5 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 10 * N + 6 * i + 3] = 5 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 10 * N + 6 * i + 4] = 5 * (2 * N + 1) + i + 1;
            triangles[6 * 10 * N + 6 * i + 5] = 5 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }
        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 12 * N + 6 * i + 0] = 6 * (2 * N + 1) + i + 0;
            triangles[6 * 12 * N + 6 * i + 1] = 6 * (2 * N + 1) + i + 1;
            triangles[6 * 12 * N + 6 * i + 2] = 6 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 12 * N + 6 * i + 3] = 6 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 12 * N + 6 * i + 4] = 6 * (2 * N + 1) + i + 1;
            triangles[6 * 12 * N + 6 * i + 5] = 6 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }
        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 14 * N + 6 * i + 0] = 7 * (2 * N + 1) + i + 0;
            triangles[6 * 14 * N + 6 * i + 1] = 7 * (2 * N + 1) + i + 1;
            triangles[6 * 14 * N + 6 * i + 2] = 7 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 14 * N + 6 * i + 3] = 7 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 14 * N + 6 * i + 4] = 7 * (2 * N + 1) + i + 1;
            triangles[6 * 14 * N + 6 * i + 5] = 7 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }
        for (int i = 0; i < 2 * N; i++)
        {
            triangles[6 * 16 * N + 6 * i + 0] = 8 * (2 * N + 1) + i + 0;
            triangles[6 * 16 * N + 6 * i + 1] = 8 * (2 * N + 1) + i + 1;
            triangles[6 * 16 * N + 6 * i + 2] = 8 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 16 * N + 6 * i + 3] = 8 * (2 * N + 1) + i + (2 * N + 1);
            triangles[6 * 16 * N + 6 * i + 4] = 8 * (2 * N + 1) + i + 1;
            triangles[6 * 16 * N + 6 * i + 5] = 8 * (2 * N + 1) + i + (2 * N + 1) + 1;
        }



        x = -c1;
        z = 0.0f * c3 / 5.0f;
        float y = c4;
        int index = 10 * (2 * N + 1);
        vertices[index] = new Vector3(x, y, z);

        x = -f3 * c1;
        z = 0.0f * c3 / 5.0f;
        vertices[index + 1] = new Vector3(x, y, z);

        x = -f3 * c1;
        z = 1.0f * c3 / 5.0f;
        vertices[index + 2] = new Vector3(x, y, z);

        x = -f2 * c1;
        z = 1.0f * c3 / 5.0f;
        vertices[index + 3] = new Vector3(x, y, z);

        x = -f1 * c1;
        z = 2.0f * c3 / 5.0f;
        vertices[index + 4] = new Vector3(x, y, z);

        x = -f1 * c1;
        z = 3.0f * c3 / 5.0f;
        vertices[index + 5] = new Vector3(x, y, z);

        x = -f2 * c1;
        z = 4.0f * c3 / 5.0f;
        vertices[index + 6] = new Vector3(x, y, z);

        x = -f3 * c1;
        z = 4.0f * c3 / 5.0f;
        vertices[index + 7] = new Vector3(x, y, z);

        x = -f3 * c1;
        z = 5.0f * c3 / 5.0f;
        vertices[index + 8] = new Vector3(x, y, z);

        x = -c1;
        z = 5.0f * c3 / 5.0f;
        vertices[index + 9] = new Vector3(x, y, z);


        //Other side:
        x = c1;
        z = 0.0f * c3 / 5.0f;
        y = c4;
        vertices[index + 10] = new Vector3(x, y, z);

        x = f3 * c1;
        z = 0.0f * c3 / 5.0f;
        vertices[index + 11] = new Vector3(x, y, z);

        x = f3 * c1;
        z = 1.0f * c3 / 5.0f;
        vertices[index + 12] = new Vector3(x, y, z);

        x = f2 * c1;
        z = 1.0f * c3 / 5.0f;
        vertices[index + 13] = new Vector3(x, y, z);

        x = f1 * c1;
        z = 2.0f * c3 / 5.0f;
        vertices[index + 14] = new Vector3(x, y, z);

        x = f1 * c1;
        z = 3.0f * c3 / 5.0f;
        vertices[index + 15] = new Vector3(x, y, z);

        x = f2 * c1;
        z = 4.0f * c3 / 5.0f;
        vertices[index + 16] = new Vector3(x, y, z);

        x = f3 * c1;
        z = 4.0f * c3 / 5.0f;
        vertices[index + 17] = new Vector3(x, y, z);

        x = f3 * c1;
        z = 5.0f * c3 / 5.0f;
        vertices[index + 18] = new Vector3(x, y, z);

        x = c1;
        z = 5.0f * c3 / 5.0f;
        vertices[index + 19] = new Vector3(x, y, z);





        int ii = 2 * N - 1;
        int tri_index = 6 * 16 * N + 6 * ii + 5;

        triangles[tri_index + 1] = 0;
        triangles[tri_index + 2] = index + 1;
        triangles[tri_index + 3] = index;

        triangles[tri_index + 4] = (2 * N + 1);
        triangles[tri_index + 5] = index + 1;
        triangles[tri_index + 6] = 0;

        triangles[tri_index + 7] = (2 * N + 1);
        triangles[tri_index + 8] = index + 2;
        triangles[tri_index + 9] = index + 1;

        triangles[tri_index + 10] = 2 * (2 * N + 1);
        triangles[tri_index + 11] = index + 2;
        triangles[tri_index + 12] = (2 * N + 1);

        
        triangles[tri_index + 13] = 2 * (2 * N + 1);
        triangles[tri_index + 14] = index + 3;
        triangles[tri_index + 15] = index + 2;

        triangles[tri_index + 16] = 3 * (2 * N + 1);
        triangles[tri_index + 17] = index + 3;
        triangles[tri_index + 18] = 2 * (2 * N + 1);

        triangles[tri_index + 19] = 3 * (2 * N + 1);
        triangles[tri_index + 20] = index + 4;
        triangles[tri_index + 21] = index + 3;

        triangles[tri_index + 22] = 4 * (2 * N + 1);
        triangles[tri_index + 23] = index + 4;
        triangles[tri_index + 24] = 3 * (2 * N + 1);

        
        triangles[tri_index + 25] = 4 * (2 * N + 1);
        triangles[tri_index + 26] = index + 5;
        triangles[tri_index + 27] = index + 4;

        
        triangles[tri_index + 28] = 5 * (2 * N + 1);
        triangles[tri_index + 29] = index + 5;
        triangles[tri_index + 30] = 4 * (2 * N + 1);

        
        triangles[tri_index + 31] = 5 * (2 * N + 1);
        triangles[tri_index + 32] = index + 6;
        triangles[tri_index + 33] = index + 5;

        triangles[tri_index + 34] = 6 * (2 * N + 1);
        triangles[tri_index + 35] = index + 6;
        triangles[tri_index + 36] = 5 * (2 * N + 1);

        triangles[tri_index + 37] = 6 * (2 * N + 1);
        triangles[tri_index + 38] = index + 7;
        triangles[tri_index + 39] = index + 6;

        triangles[tri_index + 40] = 7 * (2 * N + 1);
        triangles[tri_index + 41] = index + 7;
        triangles[tri_index + 42] = 6 * (2 * N + 1);

        
        triangles[tri_index + 43] = 7 * (2 * N + 1);
        triangles[tri_index + 44] = index + 8;
        triangles[tri_index + 45] = index + 7;

        triangles[tri_index + 46] = 8 * (2 * N + 1);
        triangles[tri_index + 47] = index + 8;
        triangles[tri_index + 48] = 7 * (2 * N + 1);


        triangles[tri_index + 49] = 8 * (2 * N + 1);
        triangles[tri_index + 50] = index + 9;
        triangles[tri_index + 51] = index + 8;

        triangles[tri_index + 52] = 9 * (2 * N + 1);
        triangles[tri_index + 53] = index + 9;
        triangles[tri_index + 54] = 8 * (2 * N + 1);




        //Other side:
        triangles[tri_index + 55] = 0 + (2 * N);
        triangles[tri_index + 56] = index + 10;
        triangles[tri_index + 57] = index + 11;

        triangles[tri_index + 58] = 1 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 59] = 0 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 60] = index + 11;

        
        triangles[tri_index + 61] = (2 * N + 1) + (2 * N);
        triangles[tri_index + 62] = index + 11;
        triangles[tri_index + 63] = index + 12;

        triangles[tri_index + 64] = 2 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 65] = 1 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 66] = index + 12;

        
        triangles[tri_index + 67] = 2 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 68] = index + 12;
        triangles[tri_index + 69] = index + 13;

        triangles[tri_index + 70] = 3 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 71] = 2 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 72] = index + 13;

        triangles[tri_index + 73] = 3 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 74] = index + 13;
        triangles[tri_index + 75] = index + 14;

        triangles[tri_index + 76] = 4 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 77] = 3 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 78] = index + 14;

        triangles[tri_index + 79] = 4 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 80] = index + 14;
        triangles[tri_index + 81] = index + 15;

        triangles[tri_index + 82] = 5 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 83] = 4 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 84] = index + 15;

        triangles[tri_index + 85] = 5 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 86] = index + 15;
        triangles[tri_index + 87] = index + 16;

        triangles[tri_index + 88] = 6 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 89] = 5 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 90] = index + 16;

        triangles[tri_index + 91] = 6 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 92] = index + 16;
        triangles[tri_index + 93] = index + 17;

        triangles[tri_index + 94] = 7 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 95] = 6 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 96] = index + 17;

        triangles[tri_index + 97] = 7 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 98] = index + 17;
        triangles[tri_index + 99] = index + 18;

        triangles[tri_index + 100] = 8 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 101] = 7 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 102] = index + 18;

        triangles[tri_index + 103] = 8 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 104] = index + 18;
        triangles[tri_index + 105] = index + 19;

        triangles[tri_index + 106] = 9 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 107] = 8 * (2 * N + 1) + (2 * N);
        triangles[tri_index + 108] = index + 19;





        float offset1 = 0.25f;
        float Scale = 0.5f;        
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i] = new Vector2(offset1 + Scale * i / (2.00001f * N), 0.0f);
            uvs[i + 2 * N + 1] = new Vector2(offset1 + Scale * i / (2.00001f * N), 1.0f / 9.0f);
            uvs[i + 1] = new Vector2(offset1 + Scale * (i + 1) / (2.00001f * N), 0.0f);
            uvs[i + 2 * N + 1 + 1] = new Vector2(offset1 + Scale * (i + 1) / (2.00001f * N), 1.0f / 9.0f);
        }
        
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 2 * N + 1] = new Vector2(offset1 + Scale * i / (2.00001f * N), 1.0f / 9.0f);
            uvs[i + 2 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 2.0f / 9.0f);
            uvs[i + 2 * N + 1 + 1] = new Vector2(offset1 + Scale * (i + 1) / (2.00001f * N), 1.0f / 9.0f);
            uvs[i + 2 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1) / (2.00001f * N), 2.0f / 9.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 2 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 2.0f / 9.0f);
            uvs[i + 3 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 3.0f / 9.0f);
            uvs[i + 2 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1) / (2.00001f * N), 2.0f / 9.0f);
            uvs[i + 3 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1) / (2.00001f * N), 3.0f / 9.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 3 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 3.0f / 9.0f);
            uvs[i + 4 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 4.0f / 9.0f);
            uvs[i + 3 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1.0f) / (2.00001f * N), 3.0f / 9.0f);
            uvs[i + 4 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1.0f) / (2.00001f * N), 4.0f / 9.0f);
        }
        
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 4 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 4.0f / 9.0f);
            uvs[i + 5 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 5.0f / 9.0f);
            uvs[i + 4 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1.0f) / (2.00001f * N), 4.0f / 9.0f);
            uvs[i + 5 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1.0f) / (2.00001f * N), 5.0f / 9.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 5 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 5.0f / 9.0f);
            uvs[i + 6 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 6.0f / 9.0f);
            uvs[i + 5 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1.0f) / (2.00001f * N), 5.0f / 9.0f);
            uvs[i + 6 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1.0f) / (2.00001f * N), 6.0f / 9.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 6 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 6.0f / 9.0f);
            uvs[i + 7 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 7.0f / 9.0f);
            uvs[i + 6 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1.0f) / (2.00001f * N), 6.0f / 9.0f);
            uvs[i + 7 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1.0f) / (2.00001f * N), 7.0f / 9.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 7 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 7.0f / 9.0f);
            uvs[i + 8 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 8.0f / 9.0f);
            uvs[i + 7 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1.0f) / (2.00001f * N), 7.0f / 9.0f);
            uvs[i + 8 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1.0f) / (2.00001f * N), 8.0f / 9.0f);
        }
        for (int i = 0; i < 2 * N; i++)
        {
            uvs[i + 8 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 8.0f / 9.0f);
            uvs[i + 9 * (2 * N + 1)] = new Vector2(offset1 + Scale * i / (2.00001f * N), 9.0f / 9.0f);
            uvs[i + 8 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1.0f) / (2.00001f * N), 8.0f / 9.0f);
            uvs[i + 9 * (2 * N + 1) + 1] = new Vector2(offset1 + Scale * (i + 1.0f) / (2.00001f * N), 9.0f / 9.0f);
        }

        int texture_index = 2 * N - 1 + 9 * (2 * N + 1) + 1;

        uvs[texture_index + 1] = new Vector2(0.0f, 0.0f);
        uvs[texture_index + 2] = new Vector2(0.0f, 1.0f / 9.0001f);
        uvs[texture_index + 3] = new Vector2(0.0f, 2.0f / 9.0001f);
        uvs[texture_index + 4] = new Vector2(0.0f, 3.0f / 9.0001f);
        uvs[texture_index + 5] = new Vector2(0.0f, 4.0f / 9.0001f);
        uvs[texture_index + 6] = new Vector2(0.0f, 5.0f / 9.0001f);
        uvs[texture_index + 7] = new Vector2(0.0f, 6.0f / 9.0001f);
        uvs[texture_index + 8] = new Vector2(0.0f, 7.0f / 9.0001f);
        uvs[texture_index + 9] = new Vector2(0.0f, 8.0f / 9.0001f);
        uvs[texture_index + 10] = new Vector2(0.0f, 9.0f / 9.0001f);

        uvs[texture_index + 11] = new Vector2(1.0f, 0.0f);
        uvs[texture_index + 12] = new Vector2(1.0f, 1.0f / 9.0001f);
        uvs[texture_index + 13] = new Vector2(1.0f, 2.0f / 9.0001f);
        uvs[texture_index + 14] = new Vector2(1.0f, 3.0f / 9.0001f);
        uvs[texture_index + 15] = new Vector2(1.0f, 4.0f / 9.0001f);
        uvs[texture_index + 16] = new Vector2(1.0f, 5.0f / 9.0001f);
        uvs[texture_index + 17] = new Vector2(1.0f, 6.0f / 9.0001f);
        uvs[texture_index + 18] = new Vector2(1.0f, 7.0f / 9.0001f);
        uvs[texture_index + 19] = new Vector2(1.0f, 8.0f / 9.0001f);
        uvs[texture_index + 20] = new Vector2(1.0f, 9.0f / 9.0001f);




        /*
        for (int i = 0; i < 2 * N + 1; i++)
        {
            uvs[2 * N + 1+i] = new Vector2(0.5f + 0.5f * Mathf.Cos(i * delta_theta - PI), 1.0f);
        }
        */

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

}