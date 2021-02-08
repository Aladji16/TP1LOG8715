using System;
using System.Collections.Generic;
using UnityEngine;


public struct PastSizesComponent : IComponent
{
	public uint id;
	public Queue<float> sizes;
    public Queue<float> timestamps;
}
