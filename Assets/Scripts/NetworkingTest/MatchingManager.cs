using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace NetworkingTest
{
    public class MatchingManager : MonoBehaviour
    {
        public enum MatchingMode
        {
            NotMatching,
            Client,
            Host,
        }

        [SerializeField] private NetworkDiscovery _networkDiscovery;
        [SerializeField] private Button _startAsHostTrigger;
        [SerializeField] private Button _startAsClientTrigger;
        private MatchingMode _matchingMode = MatchingMode.NotMatching;

        private void Start()
        {
            _startAsHostTrigger.OnClickAsObservable()
                .Subscribe(_ => { StartMatch(MatchingMode.Client); })
                .AddTo(this);
            _startAsClientTrigger.OnClickAsObservable()
                .Subscribe(_ => { StartMatch(MatchingMode.Host); })
                .AddTo(this);
        }

        private void StartMatch(MatchingMode mode)
        {
            StopMatch();
            switch (mode)
            {
                case MatchingMode.Host:
                    _networkDiscovery.Initialize();
                    _networkDiscovery.StartAsServer();
                    NetworkManager.singleton.StartHost();
                    break;
                case MatchingMode.Client:
                    _networkDiscovery.Initialize();
                    _networkDiscovery.StartAsClient();
                    NetworkManager.singleton.StartClient();
                    break;
            }

            _matchingMode = mode;
        }

        private void StopMatch()
        {
            switch (_matchingMode)
            {
                case MatchingMode.Host:
                    _networkDiscovery.StopBroadcast();
                    NetworkManager.singleton.StopHost();
                    break;
                case MatchingMode.Client:
                    _networkDiscovery.StopBroadcast();
                    NetworkManager.singleton.StopClient();
                    break;
            }
        }
    }
}