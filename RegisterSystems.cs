using System.Collections.Generic;

public class RegisterSystems
{
    public static List<ISystem> GetListOfSystems()
    {
        // determine order of systems to add
        List<ISystem> toRegister = new List<ISystem>();

        // AJOUTEZ VOS SYSTEMS ICI
        
        toRegister.Add(new InitializationSystem());
        toRegister.Add(new CollisionSystem(false));
        toRegister.Add(new PositionUpdateSystem(false));
        toRegister.Add(new SaveStateSystem());
        toRegister.Add(new RestoreStateSystem());
        for (int i = 0; i < 4; i++)
        {
            toRegister.Add(new CollisionSystem(true));
            toRegister.Add(new PositionUpdateSystem(true));
        }
            
        return toRegister;
    }
}