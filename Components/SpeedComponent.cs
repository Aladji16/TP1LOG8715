using System;
using System.Collections.Generic;
using UnityEngine;

public struct SpeedComponent : IComponent
{
	public uint id;
	public Vector2 speed;
	public bool isStatic;
}
