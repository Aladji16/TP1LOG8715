using System;
using System.Collections.Generic;
using UnityEngine;


//état pour chaque cercle
public class CircleState
{
    private ColorComponent colorComponent;
    private PositionComponent positionComponent;
    private SizeComponent sizeComponent;
    private SpeedComponent speedComponent;
    private TypeComponent typeComponent;
    
    public CircleState(ColorComponent color, PositionComponent pos, SizeComponent size, SpeedComponent speed, TypeComponent type)
    {
        colorComponent = color;
        positionComponent = pos;
        sizeComponent = size;
        speedComponent = speed;
        typeComponent = type;

    }
}

public class SaveStateSystem : ISystem
{
    private ECSManager manager = ECSManager.Instance;
    private World world = World.Instance;
    //private int frameCounter = 1;
    private List<List<CircleState>> savedStates = new List<List<CircleState>>();

    public void UpdateSystem()
    {

        if (savedStates.Count == 60) //si on a atteint une taille de 60, on a donc atteint 2 secondes de simulation
        {
            savedStates.RemoveAt(0); //on enlève l'élément d'il y a deux secondes, soit le premier élément de la liste

        }

        List<CircleState> actualState = new List<CircleState>();
        for (int i = 0; i < world.PositionComponents.Count; i++)
        {
            ColorComponent color = world.ColorComponents[i];
            PositionComponent pos = world.PositionComponents[i];
            SizeComponent size = world.SizeComponents[i];
            SpeedComponent speed = world.SpeedComponents[i];
            TypeComponent type = world.TypeComponents[i];
            actualState.Add(new CircleState(color, pos, size, speed, type));
        }

        savedStates.Add(actualState);

        //frameCounter += 1;




    }


    public String Name => "SaveStateSystem";
}