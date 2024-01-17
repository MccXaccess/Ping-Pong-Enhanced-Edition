using System;
using Godot;
using PingPong_GameExecutive;
using PingPong_Extensions;

public partial class SceneExecutive : Node2D
{
    [Export]
    public PackedScene PlayerScene { get; set; }

    public override void _Ready() 
    {
        int index = 0;

        foreach (var player in GameExecutive.multiplayer_players)
        {
            Node currentPlayer = PlayerScene.Instantiate();

            currentPlayer.Name = player.Name;

            AddChild(currentPlayer);

            foreach (Node spawnPoint in GetTree().GetNodesInGroup("PlayerSpawnPoints"))
            {
                if (spawnPoint.Name == index.ToString())
                {
                    currentPlayer.AsNode2D().GlobalPosition = spawnPoint.AsNode2D().GlobalPosition;
                }
            }

            index++;
        }
    }
}