using Godot;
using System;

public class Enemy : KinematicBody2D {
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public static readonly uint DefaultCollisionLayer = 4;
	private Timer shotTimer;

	public override void _Ready() {
		shotTimer = GetNode<Timer>("./ShotTimer");
		shotTimer.Start(GetNextTime());
	}

	public override void _Process(float delta) {
		if (shotTimer.TimeLeft == 0) {
			shotTimer.Start(GetNextTime());
			ShotLaser();
		}
	}

	float GetNextTime() {
		return (GD.Randi() % 25) * 0.5f;
	}

	public void GetHit() {
		GetParent().GetParent<EnemyGrid>().OneDown(this);
		this.QueueFree();
	}
	public override void _InputEvent(Godot.Object viewport, InputEvent @event, int shapeIdx) {
		
	}

	
	public void ShotLaser() {
		var playerLaser = ResourceLoader.Load<PackedScene>("res://scenes/Laser.tscn")
			.Instance<Laser>();
		
		playerLaser._velocity = new Vector2(0f, 200f);
		playerLaser.GlobalPosition = GlobalPosition + new Vector2(0f, -36f);
		playerLaser.CollisionMask |= Player.DefaultCollisionLayer;
		GetParent().GetParent().GetParent().AddChild(playerLaser);
	}
}
