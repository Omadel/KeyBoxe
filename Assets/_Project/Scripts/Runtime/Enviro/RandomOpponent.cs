using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Route69
{
    public class RandomOpponent : MonoBehaviour
    {
        [SerializeField] GameObject[] prefabs;

        private void Start()
        {
            GameObject.Instantiate(prefabs[Random.Range(0, prefabs.Length - 1)], transform);
        }
    }
}
