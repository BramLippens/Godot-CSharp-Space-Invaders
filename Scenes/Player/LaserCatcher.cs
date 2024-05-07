using Godot;
using System;

public partial class LaserCatcher : Area2D
{
	public void OnAreaEntered(Area2D area)
	{
		// GD.Print(area);
		//not needed since using layers
		// if(area is Laser)
		// {
		// 	area.QueueFree();
		// }
		area.QueueFree();
	}
}
