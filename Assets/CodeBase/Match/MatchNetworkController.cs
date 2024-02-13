using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Mirror.Examples.MultipleMatch;
using UnityEngine;
using UnityEngine.UI;


    [RequireComponent(typeof(NetworkMatch))]
    public class MatchNetworkController : NetworkBehaviour
    {
        public readonly SyncDictionary<NetworkIdentity, MatchPlayerData> matchPlayerData = new SyncDictionary<NetworkIdentity, MatchPlayerData>();

        [Header("Diagnostics - Do Not Modify")]
        public LobbyController lobbyController;
        public List<NetworkIdentity> playersIdentities;
        public Dictionary<NetworkIdentity,Owner> players = new Dictionary<NetworkIdentity, Owner>();
        public List<PlayerInfo> playersInMatch;
        [SyncVar]
        public NetworkIdentity currentPlayerIdentity;

        public int defaultScore = 1000;

        public override void OnStartServer()
        {
            StartCoroutine(AddPlayersToMatchController());
            NetworkServer.RegisterHandler<MatchHitMessage>(OnServerMatchHit);
        }

        IEnumerator AddPlayersToMatchController()
        {
            
            yield return new WaitUntil(IsAllPlayersAdded);
            for (int i = 0; i < playersIdentities.Count; i++)
            {
                NetworkIdentity playerIdentity = playersIdentities[i];
                MatchPlayerData playerData = new MatchPlayerData
                {
                    playerIndex = LobbyController.playerInfos[playerIdentity.connectionToClient].playerIndex,
                    currentScore = defaultScore,
                    team = LobbyController.playerInfos[playerIdentity.connectionToClient].team
                };
                if (playerData.team == 0)
                {
                    foreach (var VARIABLE in playersInMatch)
                    {
                    }
                    Debug.Log("Here");

                }
                matchPlayerData.Add(playerIdentity, playerData);
                Owner owner = playerIdentity.GetComponent<Owner>();
                players.Add(playersIdentities[i],owner);
        
                owner.SetupPlayerData(playerData);
            }
        }

        private bool IsAllPlayersAdded()
        {
            return playersIdentities.Count == playersInMatch.Count && playersInMatch.Count > 0;
        }

        [ServerCallback]
        private void OnServerMatchHit(NetworkConnectionToClient conn, MatchHitMessage msg)
        {
            MatchPlayerData killerData = matchPlayerData[msg.killer];
            MatchPlayerData targetData = matchPlayerData[msg.target];

            if (msg.killer == msg.target)
            {
                targetData.currentScore -= 100;
                matchPlayerData[msg.target] = targetData;
                players[msg.killer].SetupPlayerData(matchPlayerData[msg.killer]);
                return;
            }
            
            killerData.currentScore += 100;
            matchPlayerData[msg.killer] = killerData;
            
            targetData.currentScore -= 100;
            matchPlayerData[msg.target] = targetData;
            
            players[msg.killer].SetupPlayerData(matchPlayerData[msg.killer]);
            players[msg.target].SetupPlayerData(matchPlayerData[msg.target]);
        }
    }
