using Godot;
using System;

public partial class ScoreLabel : Label
{
	[Export]
	double speed = 0.1f;
	int TargetScore;
	double CurrentScore = 0;
	GpuParticles2D Particle;
	AudioStreamPlayer CoinSound;

	public void SetTargetScore(int targetScore)
	{
		TargetScore = targetScore;
	}
	
	public override void _Ready()
	{
		Particle = GetNode<GpuParticles2D>("vfx_starGetting");
		CoinSound = GetNode<AudioStreamPlayer>("CoinSound");
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
			if(!Particle.Emitting)
			{
				Particle.Restart();
				CoinSound.Play();
			}
		}
		Text = $"{(int)CurrentScore}억원";
	}
}
