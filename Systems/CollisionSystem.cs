using System;
using System.Collections.Generic;
using UnityEngine;





public class CollisionSystem : ISystem
{
    private ECSManager manager = ECSManager.Instance;
    private World world = World.Instance;
    private bool isOnlyForTopHalf;

    public CollisionSystem(bool isTop)
    {
        isOnlyForTopHalf = isTop;
    }

    //fonction pour déterminer s'il y a collision ou non entre deux cercles
    private bool collision(Vector2 pos1, float size1, Vector2 pos2, float size2)
    {
        //https://www.developpez.net/forums/d728317/java/interfaces-graphiques-java/graphisme/2d/savoir-cercles-se-touchent/

        float delta = Vector2.Distance(pos1, pos2) - (size1 + size2)/2f; //le rayon d'un cercle est sa taille /2
        return (delta <= 0.0);
    
    }

    public void UpdateSystem()
    {

        for (int i = 0; i < world.PositionComponents.Count; i++)
        {
            if (!isOnlyForTopHalf || world.PositionComponents[i].pos[1] > 0)
            {
                bool collisionExists = false;

                Vector2 pos1 = world.PositionComponents[i].pos;
                float size1 = world.SizeComponents[i].size;


                //collision si on est sur un bord de l'écran
                Camera cam = Camera.main;

                Vector3 transfo = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

                float width = transfo.x;
                float height = transfo.y;


                if ((Math.Abs(pos1.x) + (size1 / 2f) > width || Math.Abs(pos1.y) + (size1 / 2f) > height))

                {
                    collisionExists = true;

                    SpeedComponent newSpeedComp1;

                    newSpeedComp1.id = world.SpeedComponents[i].id;
                    newSpeedComp1.speed = -world.SpeedComponents[i].speed;


                    SizeComponent newSizeComp1;
                    newSizeComp1.id = world.SizeComponents[i].id;
                    newSizeComp1.size = world.SizeComponents[i].initialSize;
                    newSizeComp1.initialSize = world.SizeComponents[i].initialSize;
                    float origSize = world.SizeComponents[i].initialSize;

                    if (origSize >= manager.Config.minSize)
                    {
                        ColorComponent newColComponent = new ColorComponent();
                        newColComponent.id = world.SizeComponents[i].id;
                        newColComponent.color = UnityEngine.Color.blue;
                        world.ColorComponents[i] = newColComponent;

                        manager.UpdateShapeColor(world.SizeComponents[i].id, UnityEngine.Color.blue);

                    }

                    //comme la taille augmente, le cercle est déjà en dehors des bords, il faut donc le rentrer dans le cadre 
                    //il faudra faire une disjonction de 4 cas *après* le changement de taille : 
                    /*
                     * cercle dépasse en haut
                     * cercle dépasse en bas
                     * cercle dépasse à droite
                     * cercle dépasse à gauche
                     * 
                     */

                    PositionComponent newPositionComponent; 
                    newPositionComponent.id = world.PositionComponents[i].id;
                    newPositionComponent.pos = pos1;
                    if (newPositionComponent.pos.y + (origSize / 2f) > height) //en haut
                    {
                        newPositionComponent.pos.y = height - (origSize / 2f);
                    }

                    if (newPositionComponent.pos.y - (origSize / 2f) < -height) //en bas
                    {
                        newPositionComponent.pos.y = -height + (origSize / 2f);
                    }

                    if (newPositionComponent.pos.x + (origSize / 2f) > width) //à droite
                    {
                        newPositionComponent.pos.x = width - (origSize / 2f);
                    }

                    if (newPositionComponent.pos.x - (origSize / 2f) < -width) //à gauche
                    {
                        newPositionComponent.pos.x = -width + (origSize / 2f);
                    }


                    //mise à jour des infos dans world et manager
                    world.PositionComponents[i] = newPositionComponent;
                    manager.UpdateShapePosition(world.PositionComponents[i].id, world.PositionComponents[i].pos);
                    world.SizeComponents[i] = newSizeComp1;

                    manager.UpdateShapeSize(world.SizeComponents[i].id, world.SizeComponents[i].size);
                    world.SpeedComponents[i] = newSpeedComp1;




                }


                //on vérifie si la taille est supérieure à la taille min
                if (size1 >= manager.Config.minSize && collisionExists == false)
                {
                    int j = i + 1;
                    //Vector2 pos2 = world.PositionComponents[j].pos;


                    while (collisionExists == false && j < world.SizeComponents.Count)
                    {
                        float size2 = world.SizeComponents[j].size;
                        //on vérifie si la taille est supérieure à la taille min
                        Vector2 pos2 = world.PositionComponents[j].pos;

                        if (size2 >= manager.Config.minSize)
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
                        if (world.TypeComponents[i].isStatic == false)
                        {
                            newSizeComp1.size = world.SizeComponents[i].size / 2f;
                        }
                        else
                        {
                            newSizeComp1.size = world.SizeComponents[i].size;

                        }
                        newSizeComp1.initialSize = world.SizeComponents[i].initialSize;


                        newSpeedComp2.id = world.SpeedComponents[j].id;
                        newSpeedComp2.speed = -world.SpeedComponents[j].speed;


                        newSizeComp2.id = world.SizeComponents[j].id;
                        if (world.TypeComponents[i].isStatic == false)
                        {
                            newSizeComp2.size = world.SizeComponents[j].size / 2f;
                        }

                        else
                        {
                            newSizeComp2.size = world.SizeComponents[i].size;

                        }
                        newSizeComp2.size = world.SizeComponents[j].size / 2f;
                        newSizeComp2.initialSize = world.SizeComponents[j].initialSize;


                        //mise à jour des infos dans world et manager
                        world.SpeedComponents[i] = newSpeedComp1;
                        world.SizeComponents[i] = newSizeComp1;

                        world.SpeedComponents[j] = newSpeedComp2;
                        world.SizeComponents[j] = newSizeComp2;

                        manager.UpdateShapeSize(world.SizeComponents[i].id, world.SizeComponents[i].size);
                        if (world.SizeComponents[i].size < manager.Config.minSize)
                        {
                            ColorComponent newColComponent = new ColorComponent();
                            newColComponent.id = world.SizeComponents[i].id;
                            newColComponent.color = UnityEngine.Color.green;
                            world.ColorComponents[i] = newColComponent;

                            manager.UpdateShapeColor(world.SizeComponents[i].id, UnityEngine.Color.green);

                        }


                        manager.UpdateShapeSize(world.SizeComponents[j].id, world.SizeComponents[j].size);
                        if (world.SizeComponents[j].size < manager.Config.minSize)
                        {
                            ColorComponent newColComponent = new ColorComponent();
                            newColComponent.id = world.SizeComponents[j].id;
                            newColComponent.color = UnityEngine.Color.green;
                            world.ColorComponents[j] = newColComponent;

                            manager.UpdateShapeColor(world.SizeComponents[j].id, UnityEngine.Color.green);

                        }


                    }
                }



            }
        }

    }



    public String Name => "CollisionSystem";
}