using System;
using System.Collections.Generic;
using UnityEngine;




public class PositionUpdateSystem : ISystem
{
    private ECSManager manager = ECSManager.Instance;
    private World world = World.Instance;
    private bool isOnlyForTopHalf;

    public PositionUpdateSystem(bool isTop)
    {
        isOnlyForTopHalf = isTop;
    }


    public void UpdateSystem()
    {


        for (int i = 0; i < world.PositionComponents.Count; i++)
        {

            if (!isOnlyForTopHalf || world.PositionComponents[i].pos[1] > 0)
            {
                PositionComponent newPositionComponent;
                newPositionComponent.id = world.PositionComponents[i].id;
                newPositionComponent.pos = world.PositionComponents[i].pos + world.SpeedComponents[i].speed;

                world.PositionComponents[i] = newPositionComponent;
                manager.UpdateShapePosition(world.PositionComponents[i].id, world.PositionComponents[i].pos);
            }

        }


        
    }

    public String Name => "PositionUpdateSystem";
}