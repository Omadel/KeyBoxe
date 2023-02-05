using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeWordCard : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _durationShaking = 0f;
    [SerializeField] private float _strengthPower = 0f;

    public void StartShaking(float addTime)
    {
        StartCoroutine(Shaking(addTime));
    }

    private IEnumerator Shaking(float timeAdded)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        var _addDuration = timeAdded + _durationShaking;

        while (elapsedTime < _addDuration)
        {
            elapsedTime += Time.deltaTime;
            float strength = _curve.Evaluate(elapsedTime / _addDuration) * _strengthPower;
            transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.position = startPosition;
    }
}