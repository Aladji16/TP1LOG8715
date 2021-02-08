using System;
using System.Collections.Generic;
using UnityEngine;


public struct PastPositionsComponent : IComponent
{
	public uint id;
	public Queue<Vector2> pos;
    public Queue<float> timestamps;
}
