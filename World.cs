using System;
using System.Collections.Generic;
using UnityEngine;


public class World
{
    #region Private attributes
    [SerializeField]
    private List<PositionComponent> _posComponents = new List<PositionComponent>();
    private List<SpeedComponent> _speedComponents = new List<SpeedComponent>();
    private List<SizeComponent> _sizeComponents = new List<SizeComponent>();
    #endregion

    #region Singleton
    private static World _instance = null;
    private static bool _instanceInitialized = false;
    public static World Instance
    {
        get
        {
            if (!_instanceInitialized && _instance == null)
            {
                _instance = new World();
                _instanceInitialized = true;
            }
            return _instance;
        }
    }
    private World() { }
    #endregion

    public List<PositionComponent> PositionComponents { get { return _posComponents; } }


    public List<SpeedComponent> SpeedComponents  {  get { return _speedComponents; } }
 

    public List<SizeComponent> SizeComponents { get { return _sizeComponents; } }
        
 


    #region Public API

    //public void SetComponentPos(uint id, Vector2 pos)
    //{
    //    //pour trouver le cercle avec l'id correspondant : https://docs.microsoft.com/fr-fr/dotnet/api/system.collections.generic.list-1.find?view=net-5.0
    //    PositionComponent posComponent = _posComponents.Find(x => x.id == id);
    //    posComponent.pos = pos;
    //}

    //public void SetComponentSpeed(uint id, Vector2 speed)
    //{
    //    SpeedComponent speedComponent = _speedComponents.Find(x => x.id == id);
    //    speedComponent.speed = speed;

    //}

    //public void SetComponentSize(uint id, float size)
    //{
    //    SizeComponent sizeComponent = _sizeComponents.Find(x => x.id == id);
    //    sizeComponent.size = size;
    //}


    //public void AddPosComponent(PositionComponent posComponent)
    //{
        
    //    _posComponents.Add(posComponent);
    //}
    

    //public void AddSizeComponent(SizeComponent sizeComponent)
    //{
    //    _sizeComponents.Add(sizeComponent);
    //}

    //public void AddSpeedComponent(SpeedComponent speedComponent)
    //{
    //    _speedComponents.Add(speedComponent);
    //}



    #endregion

}
