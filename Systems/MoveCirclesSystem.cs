using System;
using System.Collections.Generic;
using UnityEngine;




public class MoveCirclesSystem : ISystem
{
    private int compteur = 0;
    private ECSManager manager = ECSManager.Instance;


    public void UpdateSystem()
    {
        //if (compteur == 0)
        //{
            //manager.CreateShape(0, config);
            manager.UpdateShapePosition(0, new Vector2(compteur/100, compteur/100));


            compteur += 1;
        //}
        //Debug.Log("TESTEST");
    }

    public void createCircle()
    {

    }

    public String Name => "Move Circle";
}