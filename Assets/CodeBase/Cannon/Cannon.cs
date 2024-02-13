using System;
using System.Collections;
using CodeBase.Infrastructure.Services;
using CodeBase.Services.Input;
using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cannon : NetworkBehaviour
{
    [SerializeField]private Ball _ballPrefab;
    [SyncVar] [SerializeField] private ObjectPool _ballObjectPool;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxShootPower = 20;
    [SerializeField] private float _powerGrowRate = 10;
    private Owner _owner;
    private GlobalMap _globalMap;
    private byte _team;
    private Camera _camera;
    private float _currentShootPower; 
    private bool _isCharging;
    private IInputService _inputService;

    [ClientCallback]
    public void Construct(Owner owner, GlobalMap globalMap,byte team)
    {
        _team = team;
        _globalMap = globalMap;
        _owner = owner;
        _inputService = AllServices.Container.Single<IInputService>();
         SetupSpawnPointByTeam(team);
         SetupRotationByTeam(team);
         _camera = Camera.main;
         _ballObjectPool.Construct(_ballPrefab.gameObject,5);
    }

    private void SetupSpawnPointByTeam(byte team)
    {
        transform.position = _globalMap.spawnPoints[team].transform.position;
    }

    private void SetupRotationByTeam(byte team)
    {
        switch (team)
        {
            //red
            case 0:
                break;
            //blue
            case 1:
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, -180,
                    transform.rotation.eulerAngles.z);
                break;
            //yellow
            case 2: 
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 90f,
                    transform.rotation.eulerAngles.z);
                break;
            //green
            case 3: 
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, -90f,
                    transform.rotation.eulerAngles.z);
                break;
            default: 
                break;
        }
    }


    private void Update()
    {
        if (!_owner) return;
        if (!_owner.isLocalPlayer) return;
        if (!_globalMap) return;

        RotateCannonTowardsMouse();
        ChargeAndShoot();
    }
    void RotateCannonTowardsMouse()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero); 
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 point = ray.GetPoint(distance); 
            Vector3 direction = point - transform.position;
            direction.y = 0; 

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360 * Time.deltaTime * _rotationSpeed); 
        }
    }
    
    void ChargeAndShoot()
    {
        if (_inputService.IsClickButtonDown())
        {
            _isCharging = true;
            _currentShootPower = 0f; 
        }

        if (_isCharging)
        {
            _currentShootPower += _powerGrowRate * Time.deltaTime;
            _currentShootPower = Mathf.Min(_currentShootPower, _maxShootPower); 
        }

        if (_inputService.IsClickButtonUp() && _isCharging)
        {
            _isCharging = false;
            CmdShoot(_owner.netIdentity,_currentShootPower);
        }
    }
    
    [Command]
    void CmdShoot(NetworkIdentity networkIdentity,float shootPower)
    {
        GameObject ballGO = _ballObjectPool.GetObject();
        
        if(!ballGO)
            return;
        
        NetworkIdentity ballIdentity = ballGO.GetComponent<NetworkIdentity>();

        if(!NetworkServer.spawned.ContainsKey(ballIdentity.netId))
        {
            NetworkServer.Spawn(ballGO, networkIdentity.connectionToClient);
        }

        StartCoroutine(SetupForce(ballGO, shootPower, networkIdentity));
    }

    private IEnumerator SetupForce(GameObject ballGO,float shootPower,NetworkIdentity networkIdentity)
    {
        yield return new WaitUntil(() => ballGO.activeSelf);
        ballGO.transform.position =_spawnPoint.transform.position;
        Ball ball = ballGO.GetComponent<Ball>();
        if (ball._rb != null)
        {
            ball._rb.velocity = Vector3.zero;
            ball._rb.AddForce(_spawnPoint.transform.forward * shootPower, ForceMode.Impulse);
        }
        _currentShootPower = 0f;
        
        ball._ownerNetIdentity = networkIdentity;
        ball._ballObjectPool = _ballObjectPool;
    }
}