using Godot;


public partial class PlayerMovement : CharacterBody2D
{
	public InputExecutive InputExecutive = new();

	[ExportCategory("Input Settings")] 
	[Export]
	public InputExecutive.SelectedPlayer selectedPlayer;
	[Export]
	public float Speed {get; set; } = 5001;

	public override void _PhysicsProcess(double delta)
	{
		float inputDirection = InputExecutive.GetAxisInput(selectedPlayer);
	
		float maxHeight = GetViewportRect().Size.Y;

		float clampedValue = Mathf.Clamp((int)GlobalPosition.Y, 51.5f, maxHeight - 51.5f);

		Vector2 velocity = new Vector2(0, inputDirection * Speed * (float)delta);

		GlobalPosition = new Vector2(GlobalPosition.X, clampedValue);

		Translate(velocity);
	}
}	