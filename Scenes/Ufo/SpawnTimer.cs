using Godot;
using System;

public partial class SpawnTimer : Timer
{
	[Export]
	public int MinSpawnTime = 5;
	[Export]
	public int MaxSpawnTime = 10;

	public override void _Ready()
	{
		SetupTimer();
	}

    public void SetupTimer()
    {
        int randomTime = new Random().Next(MinSpawnTime, MaxSpawnTime);
		WaitTime = randomTime;
		Stop();
		Start();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
