using Godot;
using System;

public partial class Shooting : Node2D
{
	[Export]
	PackedScene laserScene;

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

		if(Input.IsActionJustPressed("shoot")){
			var laser = laserScene.Instantiate() as Laser;
			laser.GlobalPosition = ((Node2D)GetParent()).GlobalPosition - new Vector2(0, 20);
			GetTree().Root.GetNode<Node>("Main").AddChild(laser); 
		}
    }
}
