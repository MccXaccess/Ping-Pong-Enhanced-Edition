using System.Collections.Generic;
using Godot;

// The Game Manager
namespace PingPong_GameExecutive
{
	public partial class GameExecutive : Node2D
	{
		public class Player
		{
			public int ID { get; set; }
			public string Name { get; set; }
			public int Score { get; set; }
		}


		public int[] scores = new int[2];
		public static List<Player> multiplayer_players = new();

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