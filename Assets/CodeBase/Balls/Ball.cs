using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Ball : NetworkBehaviour, IObjectOfPool
{
   [SerializeField] private float _lifeTime;

   [HideInInspector][SyncVar]
   public NetworkIdentity _ownerNetIdentity;

   [HideInInspector][SyncVar] 
   public ObjectPool _ballObjectPool;

   public Rigidbody _rb;

   private float _time = 0;

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


