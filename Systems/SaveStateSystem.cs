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
    public TypeComponent typeComponent;
    
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
    private float lastSpacePressTime = 0f;

    public static List<List<CircleState>> savedStates = new List<List<CircleState>>();
    public static bool cooldownActive = true; //on ne peut pas appuyer sur espace pendant les deux premières secondes


    public void UpdateSystem()
    {
        //Debug.Log(Time.time);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cooldownActive)
            {
                string timeRemaining = (2f - (Time.time - lastSpacePressTime)).ToString();
                Debug.Log("Wait until the cooldown is over; time remaining in seconds : " + timeRemaining);

            }

            else
            {
                lastSpacePressTime = Time.time;
                Debug.Log("Space button pressed; activating 2 seconds cooldown");
                savedStates.RemoveRange(1, savedStates.Count - 1);
                cooldownActive = true;
                //Debug.Log("il y a surement une erreur là");
            }
        }


        if (Time.time - lastSpacePressTime  >= 2f) //si on a atteint 2 secondes après le dernier appui d'espace (ou après le début de la simulation)
        {
            savedStates.RemoveAt(0); //on enlève l'élément d'il y a 2 secondes, soit le premier élément de la liste
            cooldownActive = false;


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