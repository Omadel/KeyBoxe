using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Route69
{
    public class ZoomCam : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _durationZooming = 0f;

        public static ZoomCam Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void StartZoomingCam(float addTime)
        {
            StartCoroutine(Zooming(addTime));
        }

        private IEnumerator Zooming(float timeAdded)
        {
            float getField = gameObject.GetComponent<Camera>().fieldOfView;
            float elapsedTime = 0f;

            var _addDuration = timeAdded + _durationZooming;

            while (elapsedTime < _addDuration)
            {
                elapsedTime += Time.deltaTime;
                float strength = _curve.Evaluate(elapsedTime / _addDuration ) * 10;
                //transform.position = startPosition + Random.insideUnitSphere * strength;
                gameObject.GetComponent<Camera>().fieldOfView = getField + strength;
                yield return null;
            }

            gameObject.GetComponent<Camera>().fieldOfView = getField;
        }
    }
}
