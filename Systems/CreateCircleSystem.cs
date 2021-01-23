using System;
using System.Collections.Generic;
using UnityEngine;




public class CreateCircleSystem : ISystem
{
    private int compteur = 0;
    private ECSManager manager = ECSManager.Instance;
    private Config.ShapeConfig config;


    public void UpdateSystem()
    {
        if (compteur == 0)
        {
            config.size = 2;
            config.initialPos = new Vector2(0f, 0f);
            //config.initialSpeed = new Vector2(1f, 0f);
            manager.CreateShape(0, config);
            manager.UpdateShapePosition(0, config.initialPos);
            

            compteur += 1;
        }
        //Debug.Log("TESTEST");
    }

    public void createCircle()
    {

    }

    public String Name => "Create Circle";
}