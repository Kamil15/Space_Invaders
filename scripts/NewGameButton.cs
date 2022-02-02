using Godot;
using System;

public class NewGameButton : Button {
    public override void _Ready() {

    }

    public override void _Pressed() {
        base._Pressed();
        GetNode<Global>("/root/Global").RestartState();
        GetTree().ChangeScene("res://MainGame.tscn");
    }
}
