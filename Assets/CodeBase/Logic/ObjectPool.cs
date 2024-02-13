using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class ObjectPool : NetworkBehaviour
{
    public GameObject _prefab; 
    public int _initialSize = 10; 

    public List<GameObject> pool = new List<GameObject>();
    private int _nextObject;
    public void Construct(GameObject prefab,int initialSize)
    {
        _prefab = prefab;
        _initialSize = initialSize;
    }

    public GameObject GetObject()
    {
        foreach (var obj in pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                RpcReactivate(obj);
                return obj;
            }
        }

        if (pool.Count >= _initialSize)
        {
            if (_nextObject >= pool.Count) 
                _nextObject = 0;
            
            GameObject firstObj = pool[_nextObject];
           
            _nextObject++;
            RpcReactivate(firstObj);
            return firstObj;
        }
        
        GameObject newObj = Instantiate(_prefab);
        newObj.gameObject.SetActive(true);
        pool.Add(newObj);
        return newObj;
    }

    [ClientRpc]
    public void RpcReactivate(GameObject obj)
    {
        IObjectOfPool objectOfPool = obj.GetComponent<IObjectOfPool>();
        objectOfPool.Reset();
        obj.gameObject.SetActive(true);
    }
    
    [Command(requiresAuthority = false)]
    public void ReturnObject(GameObject obj)
    {
        RpcDeactivate(obj);
    }

    [ClientRpc]
    private void RpcDeactivate(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }
}