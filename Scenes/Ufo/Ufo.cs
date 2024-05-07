using Godot;
using System;

public partial class Ufo : Area2D
{
	[Export]
	public int Speed = 100;

	public Sprite2D Sprite2D { get; set; }
	public Node2D UfoShootingPoint { get; set; }
	public Texture2D UfoExplosionTexture { get; set; }


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Sprite2D = GetNode<Sprite2D>("Sprite2D");
		UfoShootingPoint = GetNode<Node2D>("ShootingPoint");
		UfoExplosionTexture = GD.Load<Texture2D>("res://space-invaders-assets/images/Destroyed.png");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position = Position with { X = (float)(Position.X - Speed * delta) };
	}

	public void OnVisibleOnScreenNotifier2dScreenExited()
	{
		QueueFree();
	}

	public async void OnAreaEntered(Area2D area)
	{
        if (area is Laser)
		{
            UfoShootingPoint.QueueFree();
			Speed = 0;
			Sprite2D.Texture = UfoExplosionTexture;
			await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
			QueueFree();
        }
    }
}
