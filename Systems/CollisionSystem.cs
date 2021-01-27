using System;
using System.Collections.Generic;
using UnityEngine;




public class CollisionSystem : ISystem
{
    private ECSManager manager = ECSManager.Instance;
    private World world = World.Instance;

    //fonction pour déterminer s'il y a collision ou non entre deux cercles
    private bool collision(Vector2 pos1, float size1, Vector2 pos2, float size2)
    {
        //https://www.developpez.net/forums/d728317/java/interfaces-graphiques-java/graphisme/2d/savoir-cercles-se-touchent/

        float delta = Vector2.Distance(pos1, pos2) - (size1 + size2)/2f; //le rayon d'un cercle est sa taille /2
        return (delta <= 0.0);
    
    }

    public void UpdateSystem()
    {
       
        for (int i = 0; i < world.PositionComponents.Count - 1; i++)
        {


            bool collisionExists = false;

            Vector2 pos1 = world.PositionComponents[i].pos;
            float size1 = world.SizeComponents[i].size;
            //on vérifie si la taille est supérieure à la taille min

            if (size1 > manager.Config.minSize)
            {
                int j = i + 1;
                //Vector2 pos2 = world.PositionComponents[j].pos;


                while (collisionExists == false && j < world.SizeComponents.Count)
                {
                    float size2 = world.SizeComponents[j].size;
                    //on vérifie si la taille est supérieure à la taille min
                    Vector2 pos2 = world.PositionComponents[j].pos;

                    if (size2 > manager.Config.minSize)
                    {
                        collisionExists = collision(pos1, size1, pos2, size2);
                    }

                    j = j + 1;


                }

                if (collisionExists)
                {
                    j = j - 1;


                    SpeedComponent newSpeedComp1;
                    SpeedComponent newSpeedComp2;
                    SizeComponent newSizeComp1;
                    SizeComponent newSizeComp2;

                    newSpeedComp1.id = world.SpeedComponents[i].id;
                    newSpeedComp1.speed = -world.SpeedComponents[i].speed;

                    newSizeComp1.id = world.SizeComponents[i].id;
                    newSizeComp1.size = world.SizeComponents[i].size / 2f;


                    newSpeedComp2.id = world.SpeedComponents[j].id;
                    newSpeedComp2.speed = -world.SpeedComponents[j].speed;

                    newSizeComp2.id = world.SizeComponents[j].id;
                    newSizeComp2.size = world.SizeComponents[j].size / 2f;


                    //mise à jour des infos dans world et manager
                    world.SpeedComponents[i] = newSpeedComp1;
                    world.SizeComponents[i] = newSizeComp1;

                    world.SpeedComponents[j] = newSpeedComp2;
                    world.SizeComponents[j] = newSizeComp2;

                    manager.UpdateShapeSize(world.SizeComponents[i].id, world.SizeComponents[i].size);
                    manager.UpdateShapeSize(world.SizeComponents[j].id, world.SizeComponents[j].size);


                }
            }



        }


    }



    public String Name => "CollisionSystem";
}