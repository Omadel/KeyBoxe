using UnityEngine;

namespace Route69
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] float speed = 3f;
        [SerializeField] Vector3 up = new Vector3(0, 1, 0);
        void Update()
        {
            var angle = Time.deltaTime * speed;
            transform.Rotate(up, angle);
        }
    }
}
