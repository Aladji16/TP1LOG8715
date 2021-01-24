using System;
using System.Collections.Generic;
using UnityEngine;




public class RestoreStateSystem : ISystem
{
    //private uint compteur = 0;
    //private bool isInit = false;
    //private ECSManager manager = ECSManager.Instance;

    public void UpdateSystem()
    {
        //    if (!isInit)
        //    {
        //        foreach (Config.ShapeConfig config in manager.Config.allShapesToSpawn)
        //        {
        //            manager.CreateShape(compteur, config);
        //            manager.UpdateShapePosition(compteur, config.initialPos);
        //            //à ajouter: la définition des components vitesse, position et taille
        //            compteur++;
        //        }
        //        isInit = true;
        //        Debug.Log("Compteur " + compteur);
    }

    //}

    //public void createCircle()
    //{

    //}

    public String Name => "RestoreStateSystem";
}