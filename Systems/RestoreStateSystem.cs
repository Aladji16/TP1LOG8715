using System;
using System.Collections.Generic;
using UnityEngine;




public class RestoreStateSystem : ISystem
{
    private ECSManager manager = ECSManager.Instance;
    private World world = World.Instance;

    public void UpdateSystem()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!SaveStateSystem.cooldownActive)
            {
                List<CircleState> currentStates = SaveStateSystem.savedStates[0];
                for (int i = 0; i < currentStates.Count; i++)
                {
                    CircleState current_state_i = currentStates[i];

                    ColorComponent color = current_state_i.colorComponent;
                    PositionComponent pos = current_state_i.positionComponent;
                    SizeComponent size = current_state_i.sizeComponent;
                    SpeedComponent speed = current_state_i.speedComponent;
                    TypeComponent type = current_state_i.typeComponent;


                    world.PositionComponents[i] = pos;
                    manager.UpdateShapePosition(world.PositionComponents[i].id, world.PositionComponents[i].pos);

                    world.ColorComponents[i] = color;
                    manager.UpdateShapeColor(world.ColorComponents[i].id, world.ColorComponents[i].color);

                    world.SizeComponents[i] = size;
                    manager.UpdateShapeSize(world.SizeComponents[i].id, world.SizeComponents[i].size);

                    world.SpeedComponents[i] = speed;
                    world.TypeComponents[i] = type;

                }
            }

        }
    }



    public String Name => "RestoreStateSystem";
}