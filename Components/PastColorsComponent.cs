using System;
using System.Collections.Generic;
using UnityEngine;


public struct PastColorsComponent : IComponent
{
	public uint id;
	public Queue<Color> colors;
    public Queue<float> timestamps;
}
