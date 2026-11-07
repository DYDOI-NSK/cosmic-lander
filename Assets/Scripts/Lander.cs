using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class LanderSettings
{
    public float force = 20f;
    public float rotationForce = 5f;
    public float rotationRightDirection = -1;
    public float rotationLeftDirection = +1;
}

public class Lander : MonoBehaviour
{
    [SerializeField]
    private LanderSettings settings;

    private Rigidbody2D landerRigidbody;

    private void Awake()
    {
        landerRigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Keyboard.current.upArrowKey.isPressed)
        {
            landerRigidbody.AddForce(settings.force * transform.up);
            Debug.Log("key up pressed");
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            landerRigidbody.AddTorque(settings.rotationForce * settings.rotationLeftDirection);
            Debug.Log("key left pressed");
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            landerRigidbody.AddTorque(settings.rotationForce * settings.rotationRightDirection);
            Debug.Log("key right pressed");
        }
    }
}
