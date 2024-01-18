using Godot;
using PingPong_GameExecutive;
using PingPong_Extensions;

namespace PingPong_SceneExecutive
{
    public partial class SceneExecutive : Node2D
    {
        [Export]
        public PackedScene PlayerScene { get; set; }
        public PackedScene GameScene;

        public override void _Ready() 
        {
            int index = 0;

            foreach (var player in GameExecutive.MultiplayerPlayers)
            {
                PlayerMovement currentPlayer = PlayerScene.Instantiate<PlayerMovement>();

                currentPlayer.Name = player.Name;

                AddChild(currentPlayer);

                foreach (Node2D spawnPoint in GetTree().GetNodesInGroup("PlayerSpawnPoints"))
                {
                    if (spawnPoint.Name == index.ToString())
                    {
                        currentPlayer.AsNode2D().GlobalPosition = spawnPoint.GlobalPosition;
                    }
                }

                index++;
            }
        }
    }
}