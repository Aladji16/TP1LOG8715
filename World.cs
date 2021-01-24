using System;
using System.Collections.Generic;
using UnityEngine;


public class World
{
    #region Private attributes
    [SerializeField]
    private List<PositionComponent> _posComp = new List<PositionComponent>();
    private List<VelocityComponent> _velComp = new List<VelocityComponent>();//change to array
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


    #region Public API
    public List<PositionComponent> PositionComponents { get { return _posComp; } }
    public List<VelocityComponent> VelocityComponents { get { return _velComp; } }
    public static List<IComponent> GetListOfComponents()
    {
        List<IComponent> compList = new List<IComponent>();
        //à compléter

        return compList;
    }

    #endregion

}
