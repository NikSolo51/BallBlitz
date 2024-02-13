using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Owner : NetworkBehaviour
{
    [SerializeField] private Goal _goalPrefab;
    [SerializeField] private Cannon _cannonPrefab;

    [SyncVar(hook = nameof(SetupGoal))] 
    private GameObject _goalGO;
    [SyncVar(hook = nameof(SetupCannon))] 
    private GameObject _cannonGO;
    private GlobalMap _globalMap;

    [SyncVar(hook = nameof(PlayerDataChanged))]
    private MatchPlayerData _playerData;

    private Goal _goal;
    private Cannon _cannon;

    public void SetupPlayerData(MatchPlayerData playerData)
    {
        _playerData = playerData;
    }

    public override void OnStartLocalPlayer()
    {
        CmdCreateGoal(netIdentity.connectionToClient);
        CmdCreateCannon(netIdentity.connectionToClient);
    }

    public void SetupGoal(GameObject oldGoal, GameObject newGoal)
    {
        _goal = _goalGO.GetComponent<Goal>();
        if (!_globalMap)
            _globalMap = FindObjectOfType<GlobalMap>();
        _goal.Construct(this, _globalMap, _playerData.team);
    }

    public void SetupCannon(GameObject oldCannon, GameObject newCannon)
    {
        _cannon = _cannonGO.GetComponent<Cannon>();
        if (!_globalMap)
            _globalMap = FindObjectOfType<GlobalMap>();
      
        _cannon.Construct(this, _globalMap, _playerData.team);
    }

    private void PlayerDataChanged(MatchPlayerData oldPlayerData, MatchPlayerData newPlayerData)
    {
        if (_goal)
            _goal.ChangeScoreText(newPlayerData.currentScore.ToString());
    }

    [Command(requiresAuthority = false)]
    private void CmdCreateGoal(NetworkConnectionToClient conn)
    {
        GameObject goal = Instantiate(_goalPrefab.gameObject);
        NetworkServer.Spawn(goal, conn);
        _goalGO = goal.gameObject;
    }
    [Command(requiresAuthority = false)]
    private void CmdCreateCannon(NetworkConnectionToClient conn)
    {
        GameObject cannon = Instantiate(_cannonPrefab.gameObject);
        NetworkServer.Spawn(cannon, conn);
        _cannonGO = cannon.gameObject;
    }
}