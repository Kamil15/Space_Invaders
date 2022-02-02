using Godot;
using System;

public class EnemyGrid : Node2D {

    public Vector2 EndPosition { get; set; } = Vector2.Zero;
    public bool MovingEnded { get; protected set; } = false;
    public float Speed { get; set; } = 2f;
    public uint InnerUnitsAmount { get; set; } = 50;

    private Global global;

    public override void _Ready() {
        EndPosition = new Vector2(Position.x, 240f);
        global = GetNode<Global>("/root/Global");
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
        global.GameOver();
    }

    public void OneDown(Enemy enemy) {
        global.Score += 1;
        if (global.Score >= 50)
            EndOfTheLine();
    }
}
