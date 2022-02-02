using Godot;
using System;

public class ScoreLabel : Label {
	public override void _Ready() {
		uint score = GetNode<Global>("/root/Global").Score;
		Text = $"Score: {score}";
	}
}
