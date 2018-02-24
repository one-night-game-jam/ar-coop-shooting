using UnityEngine;
using UnityEngine.XR.iOS;

namespace NetworkingTest
{
    public class ResetClientPositionByMarker : MonoBehaviour
    {
        [SerializeField] private ARReferenceImage _referenceImage;

        private void Start()
        {
            UnityARSessionNativeInterface.ARImageAnchorAddedEvent += ResetWorldOriginToImageAnchorTransform;
        }

        private void ResetWorldOriginToImageAnchorTransform(ARImageAnchor anchor)
        {
            if (anchor.referenceImageName != _referenceImage.imageName) return;
            var newWorldOrigin = new GameObject().transform;
            newWorldOrigin.position = UnityARMatrixOps.GetPosition(anchor.transform);
            newWorldOrigin.rotation = UnityARMatrixOps.GetRotation(anchor.transform);
            UnityARSessionNativeInterface.GetARSessionNativeInterface().SetWorldOrigin(newWorldOrigin);
        }

        private void OnDestroy()
        {
            UnityARSessionNativeInterface.ARImageAnchorAddedEvent -= ResetWorldOriginToImageAnchorTransform;
        }
    }
}