using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Ball : NetworkBehaviour, IObjectOfPool
{
   [SerializeField] private float _lifeTime;
   [SerializeField] private MeshRenderer _meshRenderer;
   [HideInInspector][SyncVar]
   public NetworkIdentity _ownerNetIdentity;
   [HideInInspector][SyncVar] 
   public ObjectPool _ballObjectPool;

   public Rigidbody _rb;

   private float _time = 0;
   [SyncVar(hook = nameof(SetupColor))]
   private Color _color;

   public void Construct(byte team)
   {
     
   }

   public void Construct(NetworkIdentity networkIdentity, ObjectPool ballObjectPool, in byte team)
   {
      _ownerNetIdentity = networkIdentity;
      _ballObjectPool = ballObjectPool;
      _color = GetColorByTeam(team);
   }

   private void SetupColor(Color oldColor, Color newColor)
   {
      _meshRenderer.material.color = _color;
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

   private void Update()
   {
      if(!_ownerNetIdentity) return;
      if(!_ownerNetIdentity.isLocalPlayer) return;
      if(!_ballObjectPool) return;
      _time += Time.deltaTime;
      if (_time >= _lifeTime)
      {
         _ballObjectPool.ReturnObject(gameObject);
      }
   }

   public void Reset()
   {
      transform.position = Vector3.zero;
      _time = 0;
   }
}


