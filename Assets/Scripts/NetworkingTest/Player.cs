using UnityEngine;
using UnityEngine.Networking;

namespace NetworkingTest
{
    public class Player : NetworkBehaviour
    {   
        private void Start()
        {
            if (!isLocalPlayer) return;
            transform.parent = Camera.main.transform;
        }
    }
}