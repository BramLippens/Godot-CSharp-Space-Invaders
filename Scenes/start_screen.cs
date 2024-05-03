using Godot;
using System;

public partial class start_screen : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	TextureRect Invader1Texture;
	Label Invader1Label;

	TextureRect Invader2Texture;
	Label Invader2Label;

	Timer timer;

	object[] controlArray;
	
	public override void _Ready()
	{
		Invader1Texture = GetNode<TextureRect>("%Invader1Texture");
		Invader1Label = GetNode<Label>("%Invader1Label");

		Invader2Texture = GetNode<TextureRect>("%Invader2Texture");
		Invader2Label = GetNode<Label>("%Invader2Label");

		timer = GetNode<Timer>("Timer");

		controlArray = new object[] { Invader1Texture, Invader1Label, Invader2Texture, Invader2Label };

		foreach(object control in controlArray){
			(control as Control).Modulate = new Color(1, 1, 1, 0);
		}
	}

	public void LoadGame(){
		GetTree().ChangeSceneToFile("res://scenes/Main.tscn");
	}

	public void ShowNextControl(){
		var control = controlArray[0] as Control;
		if(control is not null){
			control.Modulate = new Color(1,1,1,1);
			controlArray = controlArray[1..];
		}
		else{
			timer.Stop();
			timer.QueueFree();
		}
	}
}
