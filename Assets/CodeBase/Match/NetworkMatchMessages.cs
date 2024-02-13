using System;
using Mirror;


    /// Match message to be sent to the server
    public struct ServerMatchMessage : NetworkMessage
    {
        public ServerMatchOperation serverMatchOperation;
        public Guid matchId;
        public byte team;
    }

    /// Match message to be sent to the client
    public struct ClientMatchMessage : NetworkMessage
    {
        public ClientMatchOperation clientMatchOperation;
        public Guid matchId;
        public string sceneName;
        public MatchInfo[] matchInfos;
        public PlayerInfo[] playerInfos;
    }

    /// Information about a match
    [Serializable]
    public struct MatchInfo
    {
        public Guid matchId;
        public byte players;
        public byte maxPlayers;
    }

    /// Information about a player
    [Serializable]
    public struct PlayerInfo
    {
        public int playerIndex;
        public bool ready;
        public byte team;
        public Guid matchId;
    }

    [Serializable]
    public struct MatchPlayerData
    {
        public int playerIndex;
        public byte team;
        public int currentScore;
    }

    public struct MatchHitMessage : NetworkMessage
    {
        public NetworkIdentity killer;
        public NetworkIdentity target;
    }

    /// Match operation to execute on the server
    public enum ServerMatchOperation : byte
    {
        None,
        Create,
        Cancel,
        Start,
        Join,
        Leave,
        Ready,
        TeamChange,
        Spawn
    }

    /// Match operation to execute on the client
    public enum ClientMatchOperation : byte
    {
        None,
        List,
        Created,
        Cancelled,
        Joined,
        Departed,
        UpdateRoom,
        Started
    }

