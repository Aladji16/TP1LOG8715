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

            float width = Screen.width / 100f;
            float height = Screen.height / 100f;

            foreach (Config.ShapeConfig config in manager.Config.allShapesToSpawn)
            {
                manager.CreateShape(compteur, config);
                manager.UpdateShapePosition(compteur, config.initialPos);



                SpeedComponent spdComponent = new SpeedComponent();
                spdComponent.id = compteur;
                spdComponent.speed = config.initialSpeed;
                spdComponent.isStatic = false;
                if (compteur <= manager.Config.allShapesToSpawn.Count/4)
                {
                    spdComponent.isStatic = true;
                    spdComponent.speed = new Vector2(0f, 0f);
                }
                world.SpeedComponents.Add(spdComponent);



                SizeComponent sizeComponent = new SizeComponent();
                sizeComponent.id = compteur;
                sizeComponent.size = config.size;
                sizeComponent.initialSize = config.size;
                world.SizeComponents.Add(sizeComponent);


                PositionComponent posComponent = new PositionComponent();
                posComponent.id = compteur;
                posComponent.pos = config.initialPos;

                /* prendre en compte le cas où : 
                 * cercle dépasse en haut
                 * cercle dépasse en bas
                 * cercle dépasse à droite
                 * cercle dépasse à gauche
                 * 
                 */

                if (posComponent.pos.y + (config.size / 2f) > height) //en haut
                {
                    posComponent.pos.y = height - (config.size / 2f);
                }

                if (posComponent.pos.y - (config.size / 2f) < -height) //en bas
                {
                    posComponent.pos.y = -height + (config.size / 2f);
                }

                if (posComponent.pos.x + (config.size / 2f) > width) //à droite
                {
                    posComponent.pos.x = width - (config.size / 2f);
                }

                if (posComponent.pos.x - (config.size / 2f) < -width) //à gauche
                {
                    posComponent.pos.x = -width + (config.size / 2f);
                }

                world.PositionComponents.Add(posComponent);




                compteur++;
            }



            isInit = true;
            //Debug.Log("Compteur " + compteur);


            //AF : Initialiser couleurs
        }

    }


    public String Name => "InitializationSystem";
}