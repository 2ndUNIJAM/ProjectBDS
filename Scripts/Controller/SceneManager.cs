using Godot;
using System;

public partial class SceneManager : Node
{
	[Export]
	PackedScene StageSelectScene;

	[Export]
	PackedScene MainScene;

	[Export]
	string[] MapPaths = new string[6];

	string _MapPath;

	public string MapPath { get =>_MapPath;  }

	public void LoadStage(int stage)
	{
		_MapPath = MapPaths[stage];
		GetTree().ChangeSceneToPacked(MainScene);
		
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_MapPath = MapPaths[0];
		var button = new Button();
		button.Text = "Game Start";
		button.Pressed += StartToStageSelect;
		AddChild(button);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void StartToStageSelect()
	{
		GetTree().ChangeSceneToFile("res://Scenes/StageSelect/StageSelect.tscn"); 
	}

	private void OnClickGameStart()
	{
		GD.Print("button click");
	}
}
