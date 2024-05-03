using Godot;
using System;

public partial class InvaderConfig : Resource
{
	[Export]
	public Texture2D Sprite { get; set; }
	[Export]
	public int Width { get; set; }
	[Export]
	public string AnimationName { get; set; }
	[Export]
	public int Score { get; set; }
}
