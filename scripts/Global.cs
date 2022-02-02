using Godot;
using System;

public class Global : Node2D {

    public override void _Ready() {
        base._Ready();

    }


    public void GameOver() {
        GD.Print("Oya? Oya? Oya?");
    }
}
