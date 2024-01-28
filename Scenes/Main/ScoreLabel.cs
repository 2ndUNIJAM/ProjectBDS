using Godot;
using System;

public partial class ScoreLabel : Label
{
	[Export]
	double speed = 0.1f;
	int TargetScore;
	double CurrentScore = 0;
	GpuParticles2D particle;

	public void SetTargetScore(int targetScore)
	{
		TargetScore = targetScore;
	}
	
	public override void _Ready()
	{
		particle = GetNode<GpuParticles2D>("vfx_starGetting");
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
			if(!particle.Emitting)
			{
				particle.Restart();
			}
		}
		Text = $"{(int)CurrentScore}억원";
	}
}
