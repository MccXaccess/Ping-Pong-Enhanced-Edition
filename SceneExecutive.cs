using System;
using Godot;
using PingPong_GameExecutive;

public partial class SceneExecutive : Node2D
{
    [Export]
    public PackedScene PlayerScene { get; set; }

    public override void _Ready() 
    {
        int index = 0;

        foreach (var player in GameExecutive.multiplayer_players)
        {
            
            index++;
        }
    }
}