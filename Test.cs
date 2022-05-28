using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject go;
    public GameObject go2;
    public Material Dome_mat;
    public Material Dome_front_mat;
    public Material Figure_mat;
    //    public Material Ring_mat;

    float i = 0;
    float angle_y;
    float[,] Array;

    int Size = 50;
    int Split = 5;

    private void Start()
    {
    //        go = Rings.CreateRing(-1.0f, 9.5f, 5.0f, 2.0f, true, 0.0f * angle_y, Ring_mat);
    //        Random_points_generator.Mine_Plane();

        dome_script.CreateDome(-2.75f, 15.75f, 2f, 0.5f, "Low", true, Dome_mat);
        dome_script.CreateDome(-1f, 15.75f, 2.5f, 0.75f, "Medium", false, Dome_mat);
        dome_script.CreateDome(0.75f, 15.75f, 2f, 0.5f, "Low", true, Dome_mat);

//        Dome_front.CreateDomeFront(-2.75f, 15.75f, 2f, 0.5f, "Low", true, Dome_front_mat);
//        Dome_front.CreateDomeFront(0.75f, 15.75f, 2f, 0.5f, "Low", true, Dome_front_mat);
//        Dome_front.CreateDomeFront(-1f, 15.75f, 2.5f, 0.75f, "Medium", true, Dome_front_mat);


        //        Array = fitting_tanh.Init_Fitting(Size);
        //        go2 = Plot_in_Unity.CreatePlot(Array, 9, 9, -0.875f, 1f, 7.0f, Dome_mat);
        //        Destroy(go2, 0.025f);





    }
    bool mybool=true;
    int k = 0;
    float speed = 10f;

    
    private void Update()
    {
//        i++;
//        angle_y =i / 50;

//        go = Produce_Data.CreateFigure(-0.875f, 0.0001f, 8.0f, angle_y, Figure_mat, Size);
//        Destroy(go, 0.025f);

//        Array = Set_up_Taylor_fit.Init_Fitting(Size, Split, angle_y);
//        go2 = Plot_in_Unity.CreatePlot(Array, Size, Size, -0.875f, 0.0001f, 2.0f, Dome_mat);
//        Destroy(go2, 0.025f);




        /*
        go = Produce_Data.CreateFigure(-0.875f, 0.0001f, 7.0f, angle_y,Figure_mat,Size);
        Destroy(go, 0.025f);
        Array=fitting_tanh.Init_Fitting(Size);
        go2=Plot_in_Unity.CreatePlot(Array, 9, 9, -0.875f, 1f, 7.0f, Dome_mat);
        Destroy(go2, 0.025f);
        */
    }
}
