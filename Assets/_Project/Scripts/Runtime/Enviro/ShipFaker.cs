using DG.Tweening;
using UnityEngine;

namespace Route69
{
    public class ShipFaker : MonoBehaviour
    {
        [SerializeField] Vector3 scale;
        [SerializeField] float scaleDuration;
        [SerializeField] AnimationCurve scaleEase;
        private void Start()
        {
            transform.DOScale(scale, scaleDuration).SetLoops(-1, LoopType.Yoyo).SetEase(scaleEase);
        }
    }
}
