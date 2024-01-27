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

	public void Replay()
	{
		GetTree().ChangeSceneToPacked(MainScene);
		
	}

	public void StageSelect()
	{
		GetTree().ChangeSceneToPacked(StageSelectScene);
		
	}
	public override void _Ready()
	{
		_MapPath = MapPaths[0];
	}
}
