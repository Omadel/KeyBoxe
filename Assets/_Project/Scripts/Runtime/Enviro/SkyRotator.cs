using UnityEngine;

namespace Route69
{
    public class SkyRotator : MonoBehaviour
    {
        [SerializeField] float speed = 2f;
        [SerializeField] Material skyMaterial;

        int rotationID;

        private void Start()
        {
            rotationID = Shader.PropertyToID("_Rotation");
            skyMaterial.SetFloat(rotationID, 0);
        }

        private void Update()
        {
            var rotation = skyMaterial.GetFloat(rotationID);
            rotation += Time.deltaTime * speed;
            rotation %= 360f;
            skyMaterial.SetFloat(rotationID, rotation);
            transform.rotation = Quaternion.Euler(new Vector3(0, -rotation, 0));

        }
    }
}
