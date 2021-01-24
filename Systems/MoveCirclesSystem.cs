using System;
using System.Collections.Generic;
using UnityEngine;




public class MoveCirclesSystem : ISystem
{
    private uint compteur = 0;
    private ECSManager manager = ECSManager.Instance;


    public void UpdateSystem()
    {
        compteur = 0;

        foreach (Config.ShapeConfig config in manager.Config.allShapesToSpawn)
        {
            manager.UpdateShapePosition(compteur, config.initialPos);//changer pour currentPos + currentSpeed une fois les components implementés
            compteur += 1;
        }
        //Debug.Log("TESTEST");
    }

    public void createCircle()
    {

    }

    public String Name => "Move Circle";
}