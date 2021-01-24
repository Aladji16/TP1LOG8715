﻿using System.Collections.Generic;

public class RegisterSystems
{
    public static List<ISystem> GetListOfSystems()
    {
        // determine order of systems to add
        List<ISystem> toRegister = new List<ISystem>();

        // AJOUTEZ VOS SYSTEMS ICI
        
        toRegister.Add(new InitializationSystem());
        toRegister.Add(new CollisionSystem());
        toRegister.Add(new PositionUpdateSystem());
        toRegister.Add(new RestoreStateSystem());
        toRegister.Add(new SaveStateSystem());
        toRegister.Add(new TagSystem());
        return toRegister;
    }
}