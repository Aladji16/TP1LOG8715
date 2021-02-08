using System;
using System.Collections.Generic;
using UnityEngine;

public struct PastSpeedsComponent : IComponent
{
	public uint id;
	public Queue<Vector2> speeds;
    public Queue<float> timestamps;
}
