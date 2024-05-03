using Godot;
using System;

public partial class InvaderSpawner : Node2D
{
	const int ROWS = 5;
	const int COLUMNS = 11;
	const int HORIZONTAL_SPACING = 32;
	const int VERTICAL_SPACING = 32;
	const int INVADER_HEIGHT = 24;
	const int START_Y_POSITION = -50;
	const int INVADERS_POSITION_X_INCREMENT = 10;

	int movement_direction = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
