using Godot;
using System;

public partial class ScoreLabel : Label
{
	[Export]
	double speed = 0.1f;
	int TargetScore;
	double CurrentScore = 0;

	public void SetTargetScore(int targetScore)
	{
		TargetScore = targetScore;
	}

	public override void _Process(double delta)
	{
		if (CurrentScore >= TargetScore)
		{
			CurrentScore = TargetScore;
		}
		else if (CurrentScore < TargetScore)
		{
			CurrentScore += delta * speed;
		}
		Text = $"{(int)CurrentScore}억원";
	}
}
