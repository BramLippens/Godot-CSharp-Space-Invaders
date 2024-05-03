using Godot;
using System;

public partial class Shooting : Node2D
{
	[Export]
	PackedScene laserScene;

	private Laser currentLaser = null;

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

		if(Input.IsActionJustPressed("shoot")){
			if(currentLaser == null){
				currentLaser = laserScene.Instantiate() as Laser;
				currentLaser.GlobalPosition = ((Node2D)GetParent()).GlobalPosition - new Vector2(0, 20);
				GetTree().Root.GetNode<Node>("Main").AddChild(currentLaser);
				currentLaser.Connect("tree_exited", new Callable(this, nameof(OnLaserExitedTree)));
			}
		}
    }
	public void OnLaserExitedTree()
	{
		currentLaser = null;
	}
}
