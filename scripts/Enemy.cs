using Godot;
using System;

public class Enemy : KinematicBody2D {
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public static readonly uint DefaultCollisionLayer = 4;
	public override void _Ready() {

	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }

	public void GetHit() {
		this.QueueFree();
	}
	public override void _InputEvent(Godot.Object viewport, InputEvent @event, int shapeIdx) {
		GD.Print("Oya?");
	}

	
	public void ShotLaser() {
		var playerLaser = ResourceLoader.Load<PackedScene>("res://scenes/Laser.tscn")
			.Instance<Laser>();
		
		//playerLaser.IsFriendly = false;
		playerLaser._velocity = new Vector2(0f, 200f);
		playerLaser.GlobalPosition = GlobalPosition + new Vector2(0f, -36f);
		playerLaser.CollisionMask |= Player.DefaultCollisionLayer;
		GetParent().AddChild(playerLaser);
	}
}
