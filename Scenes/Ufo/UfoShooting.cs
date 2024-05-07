using Godot;
using System;

public partial class UfoShooting : Node2D
{
	public SpawnTimer SpawnTimer { get; set; }
	public PackedScene InvaderShotScene { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SpawnTimer = GetNode<SpawnTimer>("SpawnTimer");
        
		SpawnTimer.Connect("timeout", new Callable(this,nameof(OnSpawnTimerTimeout)));

		InvaderShotScene = GD.Load<PackedScene>("res://Scenes/Invader/InvaderShot.tscn");
	}

	public void OnSpawnTimerTimeout()
	{
        var projectile = InvaderShotScene.Instantiate() as InvaderShot;
		var projectileSprite = projectile.GetNode<Sprite2D>("Sprite2D");

        projectileSprite.Modulate = new Color(0.67f, 0.2f, 0.2f, 1);
		projectile.GlobalPosition = GlobalPosition;

		GetTree().Root.AddChild(projectile);
		SpawnTimer.SetupTimer();
    }
}
