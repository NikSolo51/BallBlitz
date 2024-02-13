using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper BootstrapperPrefab;

        private void Awake()
        {
            GameBootstrapper boostrapper = FindObjectOfType<GameBootstrapper>();

            if (boostrapper == null)
            {
                Instantiate(BootstrapperPrefab);
            }
        }
    }
}