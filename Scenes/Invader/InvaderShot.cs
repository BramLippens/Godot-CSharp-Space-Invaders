using Godot;
using System;

public partial class InvaderShot : Area2D
{
	[Export]
	public int Speed = 200;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position = Position with { Y = (float)(Position.Y + Speed * delta) };
	}

	public void OnVisibleOnScreenNotifier2dScreenExited()
	{
		QueueFree();
	}

	public void OnAreaEntered(Area2D area)
	{
        if (area is PlayerController)
		{
            PlayerController player = (PlayerController)area;
			player.OnPlayerDestroyed();
            QueueFree();
        }
    }
}
