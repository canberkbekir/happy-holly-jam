using UnityEngine;

namespace Inputs
{
    public class HouseRotation : MonoBehaviour
    { 
        [SerializeField] private float keyRotationSpeed = 50f; // Speed for Q and E keys
        [SerializeField] private Vector3 rotationAxis = Vector3.up;

        private void Update()
        { 
            // Keyboard rotation (Q for left, E for right)
            if (Input.GetKey(KeyCode.Q))
            {
                transform.Rotate(rotationAxis, -keyRotationSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(rotationAxis, keyRotationSpeed * Time.deltaTime);
            }
        }
    }
}