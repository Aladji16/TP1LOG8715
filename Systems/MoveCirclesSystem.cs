using System;
using System.Collections.Generic;
using UnityEngine;




public class MoveCirclesSystem : ISystem
{
    private ECSManager manager = ECSManager.Instance;
    private World world = World.Instance;

    public void UpdateSystem()
    {    
        for (int i=0; i < world.PositionComponents.Count; i++)
        {
            PositionComponent newPositionComponent;
            newPositionComponent.id = world.PositionComponents[i].id;
            newPositionComponent.pos = world.PositionComponents[i].pos + world.VelocityComponents[i].vel;
       
            world.PositionComponents[i] = newPositionComponent;
            manager.UpdateShapePosition(world.PositionComponents[i].id, world.PositionComponents[i].pos);
        }
        //Debug.Log("TESTEST");
    }

    public void createCircle()
    {

    }

    public String Name => "Move Circle";
}