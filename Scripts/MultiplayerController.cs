using System.Linq;
using Godot;
using PingPong_GameExecutive;

public partial class MultiplayerController : Control
{
    [Export]
    string address = "127.0.0.1";
    int port = 8000;
    int maximumPlayers = 2;
    private ENetMultiplayerPeer _peer;
    private int _playerId;

    [Export]
    public PackedScene GameScene;

    public override void _Ready()
        {
            GD.Print("<<< START SERVER >>>");
            Multiplayer.PeerConnected += PlayerConnected;
            Multiplayer.PeerDisconnected += PlayerDisconnected;
            Multiplayer.ConnectedToServer += ConnectionSuccessful;
            Multiplayer.ConnectionFailed += ConnectionFailed;

            // Also valid option
            // if(OS.GetCmdlineArgs().Contains("--server"))

            foreach (string argument in OS.GetCmdlineArgs())
            {
                if (argument == "--server") { HostGame(); }
            }
        }

    #region Server Actions
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    public void StartGame()
    {
        Node2D gameScene = ResourceLoader.Load<PackedScene>("res://Scenes/Game.tscn").Instantiate<Node2D>();
        GetTree().Root.AddChild(gameScene);
        this.Hide();
    }

    private void HostGame()
    {
        _peer = new ENetMultiplayerPeer();

        var status = _peer.CreateServer(port, maximumPlayers);
        
        if (status != Error.Ok)
        {
            GD.PrintErr("Server could not be created:");
            GD.PrintErr($"Port: {port}");
            return;
        }

        _peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
        Multiplayer.MultiplayerPeer = _peer;
        GD.Print("Server started SUCCESSFULLY.");
        GD.Print("Waiting for players to connect ...");
    }

    private void ConnectToServer()
    {
        _peer = new ENetMultiplayerPeer();

        var status = _peer.CreateClient(address, port);

        if (status != Error.Ok)
        {
            GD.PrintErr("Creating client FAILED.");
            return;
        }

        _peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
        Multiplayer.MultiplayerPeer = _peer;
    }
    #endregion

    #region On Button Actions
    public void OnHostPressed()
    {
        HostGame();
        SendPlayerInformation(GetNode<LineEdit>("NAME").Text, 1);
    }

    public void OnJoinPressed()
    {
        ConnectToServer();
    }

    public void OnStartGamePressed()
    {
        Rpc("StartGame");
    }
    #endregion

    [Rpc(MultiplayerApi.RpcMode.AnyPeer)]
    private void SendPlayerInformation(string name, int id)
    {
        Player playerObject = new()
        {
            Name = name,
            ID = id
        };


        if (!GameExecutive.MultiplayerPlayers.Contains(playerObject))
        {
            GameExecutive.MultiplayerPlayers.Add(playerObject);
        }

        if(!Multiplayer.IsServer()){
			foreach (var item in GameExecutive.MultiplayerPlayers)
			{
				Rpc("SendPlayerInformation", item.Name, item.ID);
			}
		}
    }

    #region Client Side Connection
    private void ConnectionFailed()
    {
        GD.Print("Connection FAILED");
    }

    private void ConnectionSuccessful()
    {
        GD.Print("Connection SUCCESSFULL.");

        _playerId = Multiplayer.GetUniqueId();

        GD.Print($"Sending player {_playerId} information to server.");
        GD.Print($"Id: {_playerId}");

        RpcId(1, "SendPlayerInformation", _playerId);
    }
    #endregion

    #region Server Side Connection
    private void PlayerConnected(long id)
    {
        GD.Print($"Player <{id}> connected.");
    }

    private void PlayerDisconnected(long id)
    {
        GD.Print($"Player <{id}> disconected.");

        GameExecutive.MultiplayerPlayers.Remove(GameExecutive.MultiplayerPlayers.Where(i => i.ID == id).First<Player>());

        var players = GetTree().GetNodesInGroup("Player");

        foreach (var item in players)
        {
            if (item.Name == id.ToString())
            {
                item.QueueFree();
            }
        }
    }
    #endregion
}