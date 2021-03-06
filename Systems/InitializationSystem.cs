﻿using System;
using System.Collections.Generic;
using System.Drawing;
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
            Camera cam = Camera.main;

            Vector3 transfo = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            float width = transfo.x;
            float height = transfo.y;

            foreach (Config.ShapeConfig config in manager.Config.allShapesToSpawn)
            {
                manager.CreateShape(compteur, config);
                manager.UpdateShapePosition(compteur, config.initialPos);



                SpeedComponent spdComponent = new SpeedComponent();
                spdComponent.id = compteur;
                spdComponent.speed = config.initialSpeed;

                TypeComponent typeComponent = new TypeComponent();
                typeComponent.isStatic = false;
                if (compteur <= manager.Config.allShapesToSpawn.Count/4)
                {
                    typeComponent.isStatic = true;
                    spdComponent.speed = new Vector2(0f, 0f);
                }
                world.SpeedComponents.Add(spdComponent);
                world.TypeComponents.Add(typeComponent);


                SizeComponent sizeComponent = new SizeComponent();
                sizeComponent.id = compteur;
                sizeComponent.size = config.size;
                world.SizeComponents.Add(sizeComponent);

                InitialSizeComponent initSizeComponent = new InitialSizeComponent();
                initSizeComponent.id = compteur;
                initSizeComponent.size = config.size;
                world.InitialSizeComponents.Add(initSizeComponent);

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



                ColorComponent colComponent = new ColorComponent();
                colComponent.id = compteur;
                if (typeComponent.isStatic == true)
                {
                    colComponent.color = UnityEngine.Color.red;
                    manager.UpdateShapeColor(compteur, UnityEngine.Color.red);

                }

                else if (sizeComponent.size < manager.Config.minSize)
                {
                    colComponent.color = UnityEngine.Color.green;
                    manager.UpdateShapeColor(compteur, UnityEngine.Color.green);

                }


                else
                {
                    colComponent.color = UnityEngine.Color.blue;
                    manager.UpdateShapeColor(compteur, UnityEngine.Color.blue);

                }
                world.ColorComponents.Add(colComponent);

                PastPositionsComponent ppc = new PastPositionsComponent();
                PastSpeedsComponent pspc = new PastSpeedsComponent();
                PastSizesComponent psic = new PastSizesComponent();
                PastColorsComponent pcc = new PastColorsComponent();

                ppc.id = compteur;
                pspc.id = compteur;
                psic.id = compteur;
                pcc.id = compteur;

                ppc.timestamps = new Queue<float>();
                pspc.timestamps = new Queue<float>();
                psic.timestamps = new Queue<float>(); ;
                pcc.timestamps = new Queue<float>();

                ppc.pos = new Queue<Vector2>();
                pspc.speeds = new Queue<Vector2>();
                psic.sizes = new Queue<float>();
                pcc.colors = new Queue<UnityEngine.Color>();


                world.PastPositionsComponents.Add(ppc);
                world.PastSpeedsComponents.Add(pspc);
                world.PastSizesComponents.Add(psic);
                world.PastColorsComponents.Add(pcc);

                compteur++;


            }



            isInit = true;
            //Debug.Log("Compteur " + compteur);

        }

    }


    public String Name => "InitializationSystem";
}