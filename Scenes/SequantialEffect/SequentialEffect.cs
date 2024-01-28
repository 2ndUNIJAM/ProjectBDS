using Godot;
using System;
using System.Collections.Generic;

public partial class SequentialEffect : Node2D
{
	Queue<Vector2I> EffectQueue;
	[Export]
	GpuParticles2D[] particles = new GpuParticles2D[2];

	bool FirstTileAvalable = true;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		EffectQueue = new Queue<Vector2I>();
		GetNode<Timer>("Timer").Timeout += Handle;

		for(int i = 0; i < 10; ++i)
		{
			EffectQueue.Enqueue(new Vector2I(50 * i, 360));
		}
		Handle();
	}

	public void RequestEffects(List<Vector2I> globalPositions)
	{
		foreach(Vector2I position in globalPositions)
		{
			EffectQueue.Enqueue(position);
		}

		Handle();
	}

	public void Handle()
	{
		if (EffectQueue.Count == 0)
		{
			return;
		}

		Vector2I newPosition = EffectQueue.Dequeue();
		GpuParticles2D particle = FirstTileAvalable ? particles[0] : particles[1];
		particle.GlobalPosition = newPosition;
		particle.Restart();
		GetNode<Timer>("Timer").Start();
		FirstTileAvalable = !FirstTileAvalable;
	}
}
