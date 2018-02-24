using UniRx;
using UnityEngine;
using UnityEngine.Networking;

namespace NetworkingTest
{
    public class AutoConnectNetworkDiscovery : NetworkDiscovery
    {
        public override void OnReceivedBroadcast(string fromAddress, string data)
        {
            if (NetworkManager.singleton.IsClientConnected()) return;
            NetworkManager.singleton.networkAddress = fromAddress;
            NetworkManager.singleton.StartClient();
        }
    }
}