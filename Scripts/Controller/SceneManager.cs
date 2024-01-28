using Godot;
using System;
using System.Diagnostics;

public partial class SceneManager : Node
{
	[Export]
	PackedScene StageSelectScene;

	[Export]
	PackedScene MainScene;

	[Export]
	string[] MapPaths = new string[6];

	AudioStreamPlayer BackgroundMusicPlayer;

	string _MapPath;
	int _Stage;

	public string MapPath { get =>_MapPath; }
	public int Stage { get =>_Stage; }

	public void LoadStage1() => LoadStage(0);
	public void LoadStage2() => LoadStage(1);
	public void LoadStage3() => LoadStage(2);
	public void LoadStage4() => LoadStage(3);
	public void LoadStage5() => LoadStage(4);
	public void LoadStage6() => LoadStage(5);

	public void LoadStage(int stage)
	{
		_Stage = stage;
		_MapPath = MapPaths[Stage];
		GetNode<AudioStreamPlayer>("/root/SceneManager/ClickSound").Play();
		GetTree().ChangeSceneToPacked(MainScene);
		
	}

	public void Replay()
	{
		GetNode<AudioStreamPlayer>("/root/SceneManager/ClickSound").Play();
		GetTree().ChangeSceneToPacked(MainScene);
		
	}
	public void NextStage()
	{
		GetNode<AudioStreamPlayer>("/root/SceneManager/ClickSound").Play();
		++_Stage;
		_MapPath = MapPaths[Stage];
		GetTree().ChangeSceneToPacked(MainScene);
		
	}

	public bool IsMaxStage() => _Stage == (MapPaths.Length - 1);

	public void StageSelect()
	{
		GetNode<AudioStreamPlayer>("/root/SceneManager/ClickSound").Play();
		GetTree().ChangeSceneToPacked(StageSelectScene);
		
	}
	public override void _Ready()
	{
		_MapPath = MapPaths[0];
/*		BackgroundMusicPlayer = GetNode<AudioStreamPlayer>("../SceneManager/BackgroundMusicPlayer");
		Debug.Assert(BackgroundMusicPlayer == null);
		BackgroundMusicPlayer.Play();*/
	}
}
