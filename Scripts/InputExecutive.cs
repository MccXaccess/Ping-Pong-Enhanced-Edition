using Godot;

// Input Manager
public partial class InputExecutive
{
	public enum SelectedPlayer
	{
		P1,
		P2
	}

	public float GetAxisInput(SelectedPlayer currentPlayer)
	{
		if (currentPlayer.Equals(SelectedPlayer.P1))
			return Input.GetAxis("P1-UP", "P1-DOWN");

		if (currentPlayer.Equals(SelectedPlayer.P2))
			return Input.GetAxis("P2-UP", "P2-DOWN");

		return 0;
	}
}