using Godot;
using System;

public partial class UfoSpawner : Node2D
{

    public SpawnTimer SpawnTimer { get; set; }

    public PackedScene UfoScene { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        UfoScene = GD.Load<PackedScene>("res://Scenes/Ufo/Ufo.tscn");

        SpawnTimer = GetNode<SpawnTimer>("SpawnTimer");

        SpawnTimer.SetupTimer();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void OnSpawnTimerTimeout()
    {
        var Ufo = UfoScene.Instantiate() as Ufo;
        Ufo.GlobalPosition = Position;
        GetTree().Root.AddChild(Ufo);
    }
}
