using System;
using System.Collections.Generic;
using UnityEngine;


//état pour chaque cercle
public struct CircleState
{
    public ColorComponent colorComponent;
    public PositionComponent positionComponent;
    public SizeComponent sizeComponent;
    public SpeedComponent speedComponent;
    
    public CircleState(ColorComponent color, PositionComponent pos, SizeComponent size, SpeedComponent speed)
    {
        colorComponent = color;
        positionComponent = pos;
        sizeComponent = size;
        speedComponent = speed;

    }
}

public class SaveStateSystem : ISystem
{
    private ECSManager manager = ECSManager.Instance;
    private World world = World.Instance;
    private float timestamp = 0f;

    public static List<List<CircleState>> savedStates = new List<List<CircleState>>();
    public static bool cooldownActive = true; //on ne peut pas appuyer sur espace pendant les deux premières secondes


    public void UpdateSystem()
    {
        //sauvegarder components actuels
        for (int i = 0; i < world.PositionComponents.Count; i++)
        {
            timestamp = Time.time;

            //ajouter nouveaux components
            world.PastPositionsComponents[i].pos.Enqueue(world.PositionComponents[i].pos);
            world.PastSpeedsComponents[i].speeds.Enqueue(world.SpeedComponents[i].speed);
            world.PastSizesComponents[i].sizes.Enqueue(world.SizeComponents[i].size);
            world.PastColorsComponents[i].colors.Enqueue(world.ColorComponents[i].color);
            //et timestamp aus queue
            world.PastPositionsComponents[i].timestamps.Enqueue(timestamp);
            world.PastSpeedsComponents[i].timestamps.Enqueue(timestamp);
            world.PastSizesComponents[i].timestamps.Enqueue(timestamp);
            world.PastColorsComponents[i].timestamps.Enqueue(timestamp);


            //enlever components au dela de 2 secondes
            while (world.PastPositionsComponents[i].timestamps.Peek() +2 < timestamp)
            {
                world.PastPositionsComponents[i].pos.Dequeue();
                world.PastPositionsComponents[i].timestamps.Dequeue();
                world.PastSpeedsComponents[i].speeds.Dequeue();
                world.PastSpeedsComponents[i].timestamps.Dequeue();
                world.PastSizesComponents[i].sizes.Dequeue();
                world.PastSizesComponents[i].timestamps.Dequeue();
                world.PastColorsComponents[i].colors.Dequeue();
                world.PastColorsComponents[i].timestamps.Dequeue();
            }

            

        }


    }


    public String Name => "SaveStateSystem";
}