using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rings
{
    public const float PI = 3.1415926535897931f;
    public static Vector3 polar_coo_p(float r, float theta)
    {
        return new Vector3(r * Mathf.Cos(theta), r * Mathf.Sin(theta),0.0f);
    }
    
    public static Vector3 polar_coo_m(float r, float theta)
    {
        return new Vector3(r * Mathf.Cos(theta), r * Mathf.Sin(theta), -0.05f);
    }
    
    public static Vector3 Rotation_y(Vector3 xyz,float angle_y)
    {
        return new Vector3(Mathf.Cos(angle_y) * xyz[0] + Mathf.Sin(angle_y) * xyz[2], xyz[1],- Mathf.Sin(angle_y) * xyz[0] + Mathf.Cos(angle_y) * xyz[2]);
//        return new Vector3(Mathf.Cos(angle_y) * xyz[0]+Mathf.Sin(angle_y) * xyz[1], Mathf.Cos(angle_y) * xyz[0] - Mathf.Sin(angle_y) * xyz[1], xyz[2]);
    }

    public static Vector2 polar_uv(float r, float theta)
    {
        return new Vector2(Mathf.Cos(theta),Mathf.Sin(theta));
    }
    public static GameObject CreateRing(float c1, float c2, float c3, float radius, bool collider, float angle_y, Material Ring_mat)
    {
        GameObject go = new GameObject("Plane");
        MeshFilter mf = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer mr = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

        float theta = 3.1415926535897931f / 180.0f;
        Vector3 Center = new Vector3(c1, c3, c2);
        Vector2 Center2 = new Vector3(0.5f,0.5f);

        Mesh m = new Mesh();

        float res_angle = 20f;
        float T1 = res_angle * theta;
        float r1 = radius * Mathf.Cos(T1);


        Vector3[] vertices;
        Vector2[] uvs;
        int[] triangles;

        const int Size = 18;

        vertices = new Vector3[4*Size*1];
        triangles = new int[6*Size*2];

        uvs = new Vector2[4 * Size * 1];

        for (int i = 0; i < Size; i++)
        {

            vertices[4 * i + 0] = polar_coo_p(2.0f, T1 * i);// + Center;
            vertices[4 * i + 1] = polar_coo_p(2.2f, T1 * i);// + Center;
            vertices[4 * i + 2] = polar_coo_p(2.2f, T1 * (i + 1));// + Center;
            vertices[4 * i + 3] = polar_coo_p(2.0f, T1 * (i + 1));// + Center;
            
            /*
            vertices[4 * i + 0] = Rotation_y(polar_coo_p(2.0f, T1 * i), angle_y) + Center;
            vertices[4 * i + 1] = Rotation_y(polar_coo_p(2.2f, T1 * i), angle_y) + Center;
            vertices[4 * i + 2] = Rotation_y(polar_coo_p(2.2f, T1 * (i + 1)), angle_y) + Center;
            vertices[4 * i + 3] = Rotation_y(polar_coo_p(2.0f, T1 * (i + 1)), angle_y) + Center;
            */
        }
        /*
        for (int i = 0; i < Size; i++)
        {
            vertices[4 * Size+4 * i + 0] = Rotation_y(polar_coo_m(2.0f, T1 * i), angle_y) + Center;
            vertices[4 * Size+4 * i + 1] = Rotation_y(polar_coo_m(2.2f, T1 * i), angle_y) + Center;
            vertices[4 * Size+4 * i + 2] = Rotation_y(polar_coo_m(2.2f, T1 * (i + 1)), angle_y) + Center;
            vertices[4 * Size+4 * i + 3] = Rotation_y(polar_coo_m(2.0f, T1 * (i + 1)), angle_y) + Center;
        }
        */

        m.vertices = vertices;



        /*
        m.uv = new Vector2[]
        {
            new Vector2 (0, 0),
            new Vector2 (1, 0),
            new Vector2 (0, 1),
            new Vector2 (1, 1),
        };
        */
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
        /*
        for (int i = 0; i < Size; i++)
        {
            triangles[6 * Size+6 * i + 0] = 4 * Size + 4 * i + 0;
            triangles[6 * Size+6 * i + 1] = 4 * Size + 4 * i + 2;
            triangles[6 * Size+6 * i + 2] = 4 * Size + 4 * i + 1;
            triangles[6 * Size+6 * i + 3] = 4 * Size + 4 * i + 2;
            triangles[6 * Size+6 * i + 4] = 4 * Size + 4 * i + 0;
            triangles[6 * Size+6 * i + 5] = 4 * Size + 4 * i + 3;
        }
        */
        
        for (int i = 0; i < Size; i++)
        {
//            uvs[4 * i + 0] = new Vector2(0.0f,0.0f);
//            uvs[4 * i + 1] = new Vector2(1.0f, 0.0f);
//            uvs[4 * i + 2] = new Vector2(1.0f, 1.0f);
//            uvs[4 * i + 3] = new Vector2(0.0f, 1.0f);
              uvs[4 * i + 0] = polar_uv(0.5f, T1 * i) + Center2;
              uvs[4 * i + 1] = polar_uv(0.5f, T1 * i) + Center2;
              uvs[4 * i + 2] = polar_uv(0.5f, T1 * (i + 1)) + Center2;
              uvs[4 * i + 3] = polar_uv(0.5f, T1 * (i + 1)) + Center2;
        }
        m.uv = uvs;
        mr.material = Ring_mat;
        
        m.triangles = triangles;
        mf.mesh = m;
        if (collider)
        {
            (go.AddComponent(typeof(MeshCollider)) as MeshCollider).sharedMesh = m;
        }
//        mr.material = mat;
        m.RecalculateBounds();
        m.RecalculateNormals();

        return go;//
    }
}

