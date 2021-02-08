using System;
using System.Collections.Generic;
using UnityEngine;




public class RestoreStateSystem : ISystem
{
    private ECSManager manager = ECSManager.Instance;
    private World world = World.Instance;
    private float lastSpacePressTime = 0f;//on ne peut pas appuyer sur espace pendant les deux premières secondes

    public void UpdateSystem()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSpacePressTime < 2f)
            {
                string timeRemaining = (2f - (Time.time - lastSpacePressTime)).ToString();
                Debug.Log("Wait until the cooldown is over; time remaining in seconds : " + timeRemaining);

            }

            else
            {
                lastSpacePressTime = Time.time;
                Debug.Log("Space button pressed; activating 2 seconds cooldown");

                for (int i = 0; i < world.PastPositionsComponents.Count; i++)
                {

                    PositionComponent posC = new PositionComponent();
                    ColorComponent colorC = new ColorComponent();
                    SizeComponent sizeC = new SizeComponent();
                    SpeedComponent speedC = new SpeedComponent();

                    posC.id = world.PositionComponents[i].id;
                    colorC.id = world.ColorComponents[i].id;
                    sizeC.id = world.SizeComponents[i].id;
                    speedC.id = world.SpeedComponents[i].id;

                    posC.pos = world.PastPositionsComponents[i].pos.Dequeue();
                    world.PositionComponents[i] = posC;
                    world.PastPositionsComponents[i].timestamps.Dequeue();
                    manager.UpdateShapePosition(world.PositionComponents[i].id, world.PositionComponents[i].pos);

                    colorC.color = world.PastColorsComponents[i].colors.Dequeue();
                    world.ColorComponents[i] = colorC;
                    world.PastColorsComponents[i].timestamps.Dequeue();
                    manager.UpdateShapeColor(world.ColorComponents[i].id, world.ColorComponents[i].color);

                    sizeC.size = world.PastSizesComponents[i].sizes.Dequeue();
                    world.SizeComponents[i] = sizeC;
                    world.PastSizesComponents[i].timestamps.Dequeue();
                    manager.UpdateShapeSize(world.SizeComponents[i].id, world.SizeComponents[i].size);

                    speedC.speed = world.PastSpeedsComponents[i].speeds.Dequeue();
                    world.SpeedComponents[i] = speedC;
                    world.PastSpeedsComponents[i].timestamps.Dequeue();
                }

            }
        }

    }



    public String Name => "RestoreStateSystem";
}