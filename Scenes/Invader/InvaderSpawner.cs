using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class InvaderSpawner : Node2D
{
    const int ROWS = 5;
    const int COLUMNS = 11;
    const int HORIZONTAL_SPACING = 32;
	const int VERTICAL_SPACING = 32;
	const int INVADER_HEIGHT = 24;
	const int START_Y_POSITION = -50;
	const int INVADERS_POSITION_X_INCREMENT = 10;
    const int INVADERS_POSITION_Y_INCREMENT = 20;

	int movement_direction = 1;

	public PackedScene InvaderScene { get; set; }

    public PackedScene InvaderShotScene { get; set; }
    public Timer MovementTimer { get; set; }
    public Timer ShotTimer { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        InvaderConfig invader1Res = ResourceLoader.Load("res://Resources/Invader1.tres") as InvaderConfig;
        InvaderConfig invader2Res = ResourceLoader.Load("res://Resources/Invader2.tres") as InvaderConfig;
        InvaderConfig invader3Res = ResourceLoader.Load("res://Resources/Invader3.tres") as InvaderConfig;
        InvaderScene = GD.Load<PackedScene>("res://Scenes/Invader/Invader.tscn");
        InvaderShotScene = GD.Load<PackedScene>("res://Scenes/Invader/InvaderShot.tscn");
        MovementTimer = GetNode<Timer>("MovementTimer");
        MovementTimer.Connect("timeout", new Callable(this,nameof(MoveInvaders)));

        ShotTimer = GetNode<Timer>("ShotTimer");
        ShotTimer.Connect("timeout", new Callable(this, nameof(OnInvaderShot)));


        InvaderConfig invaderConfig;
		for(int row = 0; row < ROWS; row++)
		{
			if (row == 0 || row == 1)
			{
                invaderConfig = invader1Res;
            }
            else if (row == 2 || row == 3)

			{
                invaderConfig = invader2Res;
            }
            else
			{
                invaderConfig = invader3Res;
            }

			var rowWidth = (COLUMNS * invaderConfig.Width * 3) + (COLUMNS - 1) * HORIZONTAL_SPACING;
			var startX = (Position.X - rowWidth) / 2;

			for (int column = 0; column < COLUMNS - 1; column++)
			{
				var x = startX + (column * invaderConfig.Width * 3) + (column * HORIZONTAL_SPACING);
				var y = START_Y_POSITION + (row * INVADER_HEIGHT) + (row * VERTICAL_SPACING);
				var spawnPosition = new Vector2(x, y);

				SpawnInvader(invaderConfig, spawnPosition);
            }
		}
    }

    private void MoveInvaders()
    {
        Position = Position with { X = Position.X + (movement_direction * INVADERS_POSITION_X_INCREMENT) };
    }

    private void SpawnInvader(InvaderConfig invaderConfig, Vector2 spawnPosition)
    {
		// REMBER
		Invader invader = InvaderScene.Instantiate() as Invader;
		
		invader.Config = invaderConfig;
		invader.GlobalPosition = spawnPosition;
		AddChild(invader);
    }

    public void OnRightWallAreaEntered(Area2D area)
    {
        if (movement_direction == 1)
        {
            Position = Position with { Y = Position.Y + INVADERS_POSITION_Y_INCREMENT };
            movement_direction = -1;
        }
    }
    public void OnLeftWallAreaEntered(Area2D area)
    {
        if (movement_direction == -1)
        {
            Position = Position with { Y = Position.Y + INVADERS_POSITION_Y_INCREMENT };
            movement_direction = 1;
        }
    }
    public void OnBottomWallAreaEntered(Area2D area)
    {
        GD.Print("Right Wall");
    }

    public void OnInvaderShot()
    {
        List<Invader> invaders = GetChildren().Where(x => x is Invader).Cast<Invader>().ToList();
        // get random invader
        Invader randomInvader = invaders[GD.RandRange(0, invaders.Count)-1];

        var invaderShot = InvaderShotScene.Instantiate() as InvaderShot;
        invaderShot.GlobalPosition = randomInvader.GlobalPosition;

        GetTree().Root.AddChild(invaderShot);
    }
}
