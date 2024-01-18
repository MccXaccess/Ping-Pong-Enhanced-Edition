using System;
using Godot;
using PingPong_SceneExecutive;

public partial class PlayerMovement : CharacterBody2D
{
	public InputExecutive InputExecutive = new();
	public Random random = new();

	[ExportCategory("Input Settings")] 
	[Export]
	public InputExecutive.SelectedPlayer selectedPlayer;
	[Export]
	public float Speed {get; set; } = 250;

	private MultiplayerSynchronizer sync;

    public override void _Ready()
    {
		GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer").SetMultiplayerAuthority(int.Parse(Name));
    }

    public override void _PhysicsProcess(double delta)
	{
		if(GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer").GetMultiplayerAuthority() == Multiplayer.GetUniqueId())
		{
			float inputDirection = InputExecutive.GetAxisInput(selectedPlayer);
	
			float maxHeight = GetViewportRect().Size.Y;

			float clampedValue = Mathf.Clamp((int)GlobalPosition.Y, 51.5f, maxHeight - 51.5f);

			Vector2 velocity = new Vector2(0, inputDirection * Speed * (float)delta);

			GlobalPosition = new Vector2(GlobalPosition.X, clampedValue);

			Translate(velocity);
		}
	}
}	