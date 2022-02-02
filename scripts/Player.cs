using Godot;
using System;

public class Player : KinematicBody2D {
	// Declare member variables here. Examples:
	public static readonly uint DefaultCollisionLayer = 16;
	float speed = 500f;
	Timer shotTimer;
	Vector2 _velocity = Vector2.Zero;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		shotTimer = GetNode<Timer>("./ShotTimer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta) {
		if (shotTimer.TimeLeft == 0 && Input.GetActionStrength("ui_select") > 0.1f) {
			shotTimer.Start();
			ShotLaser();
		}
	}

	public override void _PhysicsProcess(float delta) {
		base._PhysicsProcess(delta);
		var move_direction = new Vector2(
			Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"),
			0f
		);
		_velocity.x = move_direction.x * speed;
		MoveAndCollide(_velocity * delta);
	}

	public void ShotLaser() {
		var playerLaser = ResourceLoader.Load<PackedScene>("res://scenes/Laser.tscn")
			.Instance<Laser>();

		playerLaser._velocity = new Vector2(0f, -500f);
		playerLaser.GlobalPosition = GlobalPosition + new Vector2(0f, -36f);
		playerLaser.CollisionMask |= Enemy.DefaultCollisionLayer;
		playerLaser.GetChild<Sprite>(1).Texture = ResourceLoader.Load<Texture>("res://texture/PlayerLaser.png");
		GetParent().AddChild(playerLaser);
	}

	public void GetHit() {
		GetNode<Global>("/root/Global").GameOver();
	}
}
