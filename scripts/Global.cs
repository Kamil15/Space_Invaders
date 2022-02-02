using Godot;
using System;

public class Global : Node2D {

    public uint Score { get; set; } = 0;

    public override void _Ready() {
        base._Ready();

    }


    public void GameOver() {
        GetTree().ChangeScene("res://scenes/EndGame.tscn");
    }

    public void RestartState() {
        Score = 0;
    }
}
