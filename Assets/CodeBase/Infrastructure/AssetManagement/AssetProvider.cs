using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
        public GameObject Instantiate(GameObject prefab,Vector3 at)
        {
            return Object.Instantiate(prefab,at,Quaternion.identity);
        }
        public GameObject Instantiate(GameObject prefab)
        {
            return Object.Instantiate(prefab);
        }
       
    }
}