using Godot;
using System;

public class EnemyRow : Node2D {
	public Vector2 BeginMove {get; set;} = Vector2.Zero;
	public Vector2 EndMove { get; set; } = Vector2.Zero;
	public float Speed { get; set; } = 150;
	public bool DoReverse { get; set; } = false;
	public override void _Ready() {
		BeginMove = new Vector2(0f, Position.y);
		EndMove = new Vector2(500f, Position.y);

	}

	public override void _PhysicsProcess(float delta) {
		base._PhysicsProcess(delta);
		
		if(!DoReverse) {
			Position = Position.MoveToward(EndMove, delta * Speed);

			DoReverse = Position == EndMove;
		} else {
			Position = Position.MoveToward(BeginMove, delta * Speed);

			DoReverse = Position != BeginMove;
		}
		
	}

	void MakeReverseIfNeed() {

	}
}
