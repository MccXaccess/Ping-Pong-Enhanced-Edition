using System.Collections.Generic;
using Godot;

namespace PingPong_GameExecutive
{
    public partial class GameExecutive : Node2D
	{

		public int[] scores = new int[2];
		public static List<Player> MultiplayerPlayers = new();
		public void OnTimerOut()
		{
			GetNode<BallMechanic>("Ball").ResetBallPosition();
		}

		public void OnGoalAreaEntered_P1(Node2D node)
		{
			GD.Print("Player 2 Scored!");
			scores[1]++;
			GetNode<TextEdit>("HUD/Score P2").Text = scores[1].ToString();
			GetNode<Timer>("Restart Timer").Start();
		}

		public void OnGoalAreaEntered_P2(Node2D node)
		{
			GD.Print("Player 1 Scored!");
			scores[0]++;
			GetNode<TextEdit>("HUD/Score P1").Text = scores[0].ToString();
			GetNode<Timer>("Restart Timer").Start();
		}
	}	
}