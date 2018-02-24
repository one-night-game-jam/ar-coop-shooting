using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.iOS;

namespace NetworkingTest
{
    public class Marker : MonoBehaviour
    {
        [SerializeField] private Button _trigger;
        [SerializeField] private Image _image;

        private void Start()
        {
            _trigger.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    Toggle();
                    if (!_image.enabled) return;

                    UnityARSessionNativeInterface.GetARSessionNativeInterface().SetWorldOrigin(Camera.main.transform);
                })
                .AddTo(this);
        }

        private void Toggle()
        {
            _image.enabled = !_image.enabled;
        }
    }
}