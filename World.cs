using System;
using System.Collections.Generic;
using UnityEngine;


public class World
{
    #region Private attributes
    [SerializeField]
    private List<PositionComponent> _posComponents = new List<PositionComponent>();
    private List<SpeedComponent> _speedComponents = new List<SpeedComponent>();
    private List<TypeComponent> _typeComponents = new List<TypeComponent>();
    private List<SizeComponent> _sizeComponents = new List<SizeComponent>();
    private List<InitialSizeComponent> _initsizeComponents = new List<InitialSizeComponent>();
    private List<ColorComponent> _colorComponents = new List<ColorComponent>();
    private List<PastPositionsComponent> _pposComponents = new List<PastPositionsComponent>();
    private List<PastSpeedsComponent> _pspeedsComponents = new List<PastSpeedsComponent>();
    private List<PastSizesComponent> _psizesComponents = new List<PastSizesComponent>();
    private List<PastColorsComponent> _pcolorsComponents = new List<PastColorsComponent>();
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

    public List<PositionComponent> PositionComponents { get { return _posComponents; } }


    public List<SpeedComponent> SpeedComponents { get { return _speedComponents; } }

    public List<TypeComponent> TypeComponents { get { return _typeComponents; } }

    public List<SizeComponent> SizeComponents { get { return _sizeComponents; } }

    public List<InitialSizeComponent> InitialSizeComponents { get { return _initsizeComponents; } }

    public List<ColorComponent> ColorComponents { get { return _colorComponents; } }

    public List<PastPositionsComponent> PastPositionsComponents { get { return _pposComponents; } }
    public List<PastSpeedsComponent> PastSpeedsComponents { get { return _pspeedsComponents; } }
    public List<PastSizesComponent> PastSizesComponents { get { return _psizesComponents; } }
    public List<PastColorsComponent> PastColorsComponents { get { return _pcolorsComponents; } }

    #endregion

}
