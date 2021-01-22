using System;
using System.Collections.Generic;
using UnityEngine;




public class CreateCircleSystem : ISystem
{
    private int compteur = 0;
    private ECSManager manager = ECSManager.Instance;
    private Config.ShapeConfig config;
    private List<ISystem> tests = new List<ISystem>();


    public void UpdateSystem()
    {
        if (compteur == 0)
        {
            config.size = 2;
            config.initialPos = new Vector2(-1f, -1f);
            config.initialSpeed = new Vector2(1f, 0f);
            tests = manager.AllSystems;
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