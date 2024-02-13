using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class Goal : NetworkBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TextMeshPro _scoreText;
    [SerializeField] private float _stopPercentage;
    [SerializeField] private float _speed = 1.0f;
    [SyncVar(hook = nameof(ChangeColor))] 
    private Color _materialColor;
    private GlobalMap _globalMap;
    private float _time;
    private GameObject _leftPoint;
    private GameObject _rightPoint;
    private Owner _owner;
    private byte _team;

    public void Construct(Owner owner, GlobalMap globalMap, byte team)
    {
        _globalMap = globalMap;
        _owner = owner;
        _team = team;
        
        SetupPointsByTeam(team);
        SetupRotationByTeam(team);
        Color color = GetColorByTeam(team);
        CmdChangeColor(color);
    }
    [Command(requiresAuthority = false)]
    private void CmdChangeColor(Color color)
    {
        _materialColor = color;
    }

    public void ChangeColor(Color oldColor, Color newColor)
    {
        _renderer.material.color = _materialColor;
    }

    private Color GetColorByTeam(byte team)
    {
        switch (team)
        {
            case 0: return Color.red;
            case 1: return Color.blue;
            case 2: return Color.yellow;
            case 3: return Color.cyan;
            default: return Color.white;
        }
    }

    private void SetupPointsByTeam(byte team)
    {
        GameObject[] movementPoints = _globalMap.movementPoints;
        switch (team)
        {
            //red
            case 0:
                _leftPoint = movementPoints[0];
                _rightPoint = movementPoints[1];
                break;
            //blue
            case 1:
                _leftPoint = movementPoints[2];
                _rightPoint = movementPoints[3];
                break;
            //yellow
            case 2:
                _leftPoint = movementPoints[3];
                _rightPoint = movementPoints[0];
                break;
            //green
            case 3:
                _leftPoint = movementPoints[2];
                _rightPoint = movementPoints[1];
                break;
            default:
                _leftPoint = null;
                _rightPoint = null;
                break;
        }
    }

    private void SetupRotationByTeam(byte team)
    {
        switch (team)
        {
            //yellow
            case 2:
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, -90f,
                    transform.rotation.eulerAngles.z);
                break;
            //green
            case 3:
                transform.rotation =
                    Quaternion.Euler(transform.rotation.eulerAngles.x, 90f, transform.rotation.eulerAngles.z);
                break;
        }
    }

    public void ChangeScoreText(string text)
    {
        _scoreText.text = text;
    }

    private void Update()
    {
        if (!_owner) return;
        if (!_owner.isLocalPlayer) return;
        if (!_globalMap) return;
        if (!_leftPoint || !_rightPoint) return;

        float margin = _stopPercentage / 100f;
        _time = Mathf.PingPong(Time.time * _speed, 1);
        _time = Mathf.Clamp(_time, margin, 1 - margin);
        transform.position = Vector3.Lerp(_leftPoint.transform.position, _rightPoint.transform.position, _time);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var ball = other.GetComponent<Ball>();
        if (ball)
        {
            ball._ballObjectPool.ReturnObject(ball.gameObject);
            StartCoroutine(SendHitMessage(ball));
        }
    }

    IEnumerator SendHitMessage(Ball ball)
    {
        yield return new WaitUntil(()=> !ball.gameObject.activeSelf);
        NetworkClient.Send(new MatchHitMessage { killer = ball._ownerNetIdentity, target = _owner.netIdentity});
    }
}