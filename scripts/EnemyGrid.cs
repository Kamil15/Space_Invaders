using Godot;
using System;

public class EnemyGrid : Node2D {

	public Vector2 EndPosition { get; set; } = Vector2.Zero;
	public bool MovingEnded { get; protected set; } = false;
	public float Speed { get; set; } = 10;
	public uint InnerUnitsAmount { get; set; } = 50;

	public override void _Ready() {

		EndPosition = new Vector2(Position.x, 240f);
	}

	public override void _Process(float delta) {
		base._Process(delta);

	}

	public override void _PhysicsProcess(float delta) {
		base._PhysicsProcess(delta);
		if (!MovingEnded) {
			GlobalPosition = GlobalPosition.MoveToward(EndPosition, delta * Speed);
			if (GlobalPosition == EndPosition)
				EndOfTheLine();
		}

	}

	public void EndOfTheLine() {
		MovingEnded = true;
		GetNode<Global>("/root/Global").GameOver();
	}


}
