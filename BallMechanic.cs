using System;
using Godot;

public partial class BallMechanic : CharacterBody2D
{
	public Vector2? windowSize = Vector2.Zero;
	public Vector2 startPosition = Vector2.Zero;
	public Vector2 currentDirection = Vector2.Zero;
	public Vector2 currentPosition;

	private const int SPEED = 200;
	private const int ACCELERATION_SPEED = 50;
	public int currentSpeed;

	private RandomNumberGenerator randomObj = new();

    public override void _Ready()
    {
		windowSize = GetViewportRect().Size;
    }

	public void ResetBallPosition()
	{	
		// Y VALUE REPLACED - randomObj.Next(200, (int)windowSize.Value.Y - 200)
		startPosition = new Vector2(windowSize.Value.X / 2, windowSize.Value.Y / 2);
		GlobalPosition = startPosition;
		currentSpeed = SPEED;
		currentDirection = GiveRandomDirection();
	}

    private Vector2 AssignNewDirection(GodotObject collider)
    {
		CollisionObject2D a = (CollisionObject2D)collider;
		
		float ballPosition_y = Position.Y;
		float paddlePosition_y = a.Position.Y;
		float distance = ballPosition_y - paddlePosition_y;
		Vector2 newTemporaryDirection = new();

		newTemporaryDirection.X = currentDirection.X > 0 ? -1: 1;
		// newTemporaryDirection.Y = (distance / (5.0f / 2)) * 0.6f;
		newTemporaryDirection.Y = 2 / 2;
		GD.Print(newTemporaryDirection);

		return newTemporaryDirection.Normalized();
        // throw new NotImplementedException();
    }
	
	private Vector2 GiveRandomDirection()
	{
		int[] directionArray_x = {1, -1};

		Vector2 newTemporaryDirection = new();
		newTemporaryDirection.X = directionArray_x[randomObj.Randi() % directionArray_x.Length];
		newTemporaryDirection.Y = randomObj.RandiRange(-1, 1);
		// newTemporaryDirection.Y = randomObj.Next(-1, 1);//(float)randomObj.NextDouble();
		GD.Print(newTemporaryDirection);
		GD.Print(newTemporaryDirection.Normalized());
		return newTemporaryDirection.Normalized();
	}

    public override void _PhysicsProcess(double delta)
    {
		var collision = MoveAndCollide(currentDirection * currentSpeed * (float)delta);

		if (collision != null)
		{
			var collider = collision.GetCollider();

			if (collider == GetNode("../Player 1") || collider == GetNode("../Player 2"))
			{
				currentSpeed += ACCELERATION_SPEED;
				currentDirection = AssignNewDirection(collider);
				return;
			}
			
			currentDirection = currentDirection.Bounce(collision.GetNormal());
		}
    }
}