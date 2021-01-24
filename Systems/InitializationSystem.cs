using System;
using System.Collections.Generic;
using UnityEngine;




public class InitializationSystem : ISystem
{
    private uint compteur = 0;
    private bool isInit = false;
    private ECSManager manager = ECSManager.Instance;
    private World world = World.Instance;

    public void UpdateSystem()
    {
        //création des cercles
        if (!isInit)
        {
            foreach (Config.ShapeConfig config in manager.Config.allShapesToSpawn)
            {
                manager.CreateShape(compteur, config);
                manager.UpdateShapePosition(compteur, config.initialPos);
                
                PositionComponent posComponent = new PositionComponent();
                posComponent.id = compteur;
                posComponent.pos = config.initialPos;
                world.PositionComponents.Add(posComponent);

                SpeedComponent spdComponent = new SpeedComponent();
                spdComponent.id = compteur;
                spdComponent.speed = config.initialSpeed;
                world.SpeedComponents.Add(spdComponent);



                SizeComponent sizeComponent = new SizeComponent();
                sizeComponent.id = compteur;
                sizeComponent.size = config.size;
                world.SizeComponents.Add(sizeComponent);


                compteur++;
            }
            isInit = true;
            //Debug.Log("Compteur " + compteur);


            //AF : Initialiser couleurs
        }

    }


    public String Name => "InitializationSystem";
}