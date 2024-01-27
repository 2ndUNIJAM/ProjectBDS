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

	[Export]
	AudioStream BackgroundMusic;

	string _MapPath;

	public string MapPath { get =>_MapPath; }

	public void LoadStage1() => LoadStage(0);
	public void LoadStage2() => LoadStage(1);
	public void LoadStage3() => LoadStage(2);
	public void LoadStage4() => LoadStage(3);
	public void LoadStage5() => LoadStage(4);
	public void LoadStage6() => LoadStage(5);

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
