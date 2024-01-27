using Godot;
using System;

public partial class StageSelect : Node2D
{
	[Export]
	TextureButton[] Buttons = new TextureButton[6];
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Buttons[0].ButtonUp += GetNode<SceneManager>("/root/SceneManager").LoadStage1;
		Buttons[1].ButtonUp += GetNode<SceneManager>("/root/SceneManager").LoadStage2;
		Buttons[2].ButtonUp += GetNode<SceneManager>("/root/SceneManager").LoadStage3;
		Buttons[3].ButtonUp += GetNode<SceneManager>("/root/SceneManager").LoadStage4;
		Buttons[4].ButtonUp += GetNode<SceneManager>("/root/SceneManager").LoadStage5;
		Buttons[5].ButtonUp += GetNode<SceneManager>("/root/SceneManager").LoadStage6;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
