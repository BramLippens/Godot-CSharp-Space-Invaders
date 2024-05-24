using Godot;
using System;

public partial class PlayerController : Area2D
{
    [Signal]
    public delegate void DeathEventHandler();

    [Export]
	private float Speed = 400;
	private Vector2 direction = Vector2.Zero;

	private float bounding_size_x;
	private float startBound;
	private float endBound;

	CollisionShape2D _collisionShape2D;
    public AnimationPlayer AnimationPlayer { get; set; }
	public Sprite2D Sprite { get; set; }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		_collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
        AnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		Sprite = GetNode<Sprite2D>("Sprite2D");

		bounding_size_x = _collisionShape2D.Shape.GetRect().Size.X;

        var rect = GetViewport().GetVisibleRect();
		var camera = GetViewport().GetCamera2D();
		var camera_position = camera.Position;
		startBound = (camera_position.X - rect.Size.X) / 2 + 20f;
		endBound = (camera_position.X + rect.Size.X) / 2;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var input = Input.GetAxis("move_left", "move_right");

		direction = input > 0 ? Vector2.Right : input < 0 ? Vector2.Left : Vector2.Zero;

		var deltaMovement = Speed * direction.X * delta;
		if (Position.X + deltaMovement < startBound || Position.X + deltaMovement + bounding_size_x > endBound)
		{
			return;
		}

		Position +=  new Vector2((float)deltaMovement, 0);
	}

	public void OnPlayerDestroyed()
	{
		Speed = 0;
		AnimationPlayer.Play("Destroy");
    }

	public void OnAnimationPlayerAnimationFinished(string anim_name)
	{
        if (anim_name == "Destroy")
		{
            GD.Print("Player Destroyed");
			AnimationPlayer.Stop();
			Hide();
			EmitSignal(SignalName.Death);
        }
    }

	public void RespawnPlayer()
	{
		Sprite.Texture = GD.Load<Texture2D>("res://space-invaders-assets/images/Player.png");
		Show();
		
		Speed = 400;
	}
}
