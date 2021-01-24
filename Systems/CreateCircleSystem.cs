using System;
using System.Collections.Generic;
using UnityEngine;




public class CreateCircleSystem : ISystem
{
    private uint compteur = 0;
    private bool isInit = false;
    private ECSManager manager = ECSManager.Instance;
    private World world = World.Instance;
    public void UpdateSystem()
    {
        if (!isInit)
        {
            foreach(Config.ShapeConfig config in manager.Config.allShapesToSpawn)
            {
                manager.CreateShape(compteur, config);
                //à ajouter: la définition des components vitesse, position et taille

                PositionComponent positionComponent;
                positionComponent.id = compteur;
                positionComponent.pos = config.initialPos;
                world.PositionComponents.Add(positionComponent);

                VelocityComponent velocityComponent;
                velocityComponent.id = compteur;
                velocityComponent.vel = config.initialSpeed;
                world.VelocityComponents.Add(velocityComponent);
                compteur++;
            }
            isInit = true;
            //Debug.Log("Compteur "+ compteur);
            //Debug.Log("PositionComponentsSize " + world.PositionComponents.Count);
        }
        
    }

    public void createCircle()
    {

    }

    public String Name => "Create Circle";
}