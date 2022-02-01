using Godot;
using System;

public class Laser : KinematicBody2D {
	float speed = 500f;
	public Vector2 _velocity = Vector2.Zero;

	//public bool IsFriendly { get; set; } = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() {
		base._Ready();
	}

	public override void _PhysicsProcess(float delta) {
		base._PhysicsProcess(delta);

		KinematicCollision2D collider = MoveAndCollide(_velocity * delta);
		if (collider != null) {
			WhenColliding(collider);
		}
		

		//Check if projectile is out of screen (Up/Down)
		if (GlobalPosition.y < 0 || GlobalPosition.y > GetViewport().Size.y) {
			GD.Print("Freed!");
			this.QueueFree();
		}
	}

	void WhenColliding(KinematicCollision2D collider) {

		if (collider.Collider is BunkerTileMap tileMap) {
			var col_pos = TilePosFromColliderv02(collider);
			var tile_pos = tileMap.WorldToMap(tileMap.ToLocal(col_pos));

			tileMap.SetCellv(tile_pos, -1);
			tileMap.UpdateBitmaskRegion();
			tileMap.UpdateDirtyQuadrants();
		} else if(collider.Collider is Enemy enemy) {
			enemy.GetHit();
		} else if(collider.Collider is Player player) {
			player.GetHit();
		}

		//Always remove projectile after hit
		this.QueueFree();
	}

	void DebugMove() {
		var move_direction = new Vector2(
			Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"),
			Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up")
		);

		_velocity.y = move_direction.y * speed;
		_velocity.x = move_direction.x * speed;
	}

	private Vector2 TilePosFromColliderv01(KinematicCollision2D collider) {
		var newNormal = collider.Normal;

		if (newNormal.x > 0.9f || newNormal.y < -0.9f) {
			newNormal.x += 0.01f;
			newNormal.y += 0.01f;
		}


		return collider.Position - newNormal;
	}

	private Vector2 TilePosFromColliderv02(KinematicCollision2D collider) {
		//Domyślnie kolizja występuje w najbardziej oddalonym prawym/dolnym rogu.
		//Problemem jest, gdy obiekt uderzy płasko z prawej/dolnej strony, wtedy jedna z pól w "Normal" ma równy 0
		//Z tego powodu, gdy pocisk uderzy z prawej/dolnej strony, SetCell może ustawić sąsiada zamiast docelowego bloku
		//Funkcja bierze pod uwagę kierunek i dodaje troszkę "normalizaji" do wartości bliski zer, aby wektor był na konkretnym Tile
		var newNormal = collider.Normal;

		GD.Print(newNormal);

		if (newNormal.x > 0.9f) // Gdy kolizja jest z prawej
		{
			//przenieś wskaźnik trochę wyżej żeby SetCellv nie oznczył dolnego bloku
			newNormal.y += 0.01f;
		}
		if (newNormal.y < -0.9f) // Gdy kolizja jest z góry
		{
			//przenieś wskaźnik trochę w lewo żeby SetCellv nie oznczył prawego bloku
			newNormal.x += 0.01f;
		}


		return collider.Position - newNormal;
	}
}
