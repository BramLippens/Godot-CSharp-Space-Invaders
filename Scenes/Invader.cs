using Godot;
using System;

public partial class Invader : Area2D
{
	public Sprite2D Sprite { get; set; }
	public Resource Config { get; set; }
	public AnimationPlayer AnimationPlayer { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Sprite = GetNode<Sprite2D>("Sprite2D");
		AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

		Sprite.Texture = (Config as InvaderConfig).Sprite;
		AnimationPlayer.Play((Config as InvaderConfig).AnimationName);

	}

	public void OnAreaEntered(Area2D area)
	{
        if (area is Laser)
		{
			AnimationPlayer.Play("Destroy");
            area.QueueFree();
        }
    }

	public void OnAnimationPlayerAnimationFinished(string animationName)
	{
        if (animationName == "Destroy")
		{
            QueueFree();
        }
    }
}
