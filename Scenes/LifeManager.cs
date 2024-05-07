using Godot;
using System;

public partial class LifeManager : Node
{
	[Export]
	public int Lives = 5;

    [Signal]
    public delegate void LifeLostEventHandler(int lifesLeft);
    
	public Player Player { get; set; }
	public PackedScene PlayerScene { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = GetNode<Player>("../Player");
		GD.Print(Player);
		PlayerScene = GD.Load<PackedScene>("res://Scenes/Player/Player.tscn");
		Player.Connect(nameof(Player.DestroyedEventHandler), new Callable(this, nameof(OnPlayerDestroyed)));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnPlayerDestroyed()
	{
		Lives--;
        EmitSignal(SignalName.LifeLost, Lives);
		if(Lives > 0)
		{
			Player = PlayerScene.Instantiate() as Player;
			Player.GlobalPosition = new Vector2(0, 280);
            Player.Connect(nameof(Player.DestroyedEventHandler), new Callable(this, nameof(OnPlayerDestroyed)));
            GetTree().Root.GetNode("Main").AddChild(Player);
        }
    }
}
