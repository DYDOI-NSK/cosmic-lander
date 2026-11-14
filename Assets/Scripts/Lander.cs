using System;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class LanderSettings
{
    public float defaultFuelAmount = 20f;
    public float fuelConsuptionAmount = 0.05f;
    public float force = 20f;
    public float rotationForce = 5f;
    public float rotationRightDirection = -1;
    public float rotationLeftDirection = +1;
    public float accesiblaLandingDegree = 0.75f;
    public float accesiblaLandingVelocity = 4f;
}

public class Lander : MonoBehaviour
{
    [SerializeField]
    private LanderSettings settings;

    private Rigidbody2D landerRigidbody;

    private float fuelAmount;

    public event EventHandler OnBeforeForce;

    public event EventHandler OnForceUp;
    public event EventHandler OnForceLeft;
    public event EventHandler OnForceRight;

    private void Awake()
    {
        landerRigidbody = GetComponent<Rigidbody2D>();
        fuelAmount = settings.defaultFuelAmount;
    }

    private bool MovementKeyPressed()
    {
        return Keyboard.current.upArrowKey.isPressed
            | Keyboard.current.leftArrowKey.isPressed
            | Keyboard.current.rightArrowKey.isPressed;
    }

    private bool FuelIsEmpty()
    {
        return fuelAmount < 0;
    }

    private void FixedUpdate()
    {
        OnBeforeForce?.Invoke(this, EventArgs.Empty);

        if (MovementKeyPressed())
        {
            ConsumeFuel();
        }

        if (FuelIsEmpty())
        {
            return;
        }

        if (Keyboard.current.upArrowKey.isPressed)
        {
            landerRigidbody.AddForce(settings.force * transform.up);
            OnForceUp?.Invoke(this, EventArgs.Empty);
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            landerRigidbody.AddTorque(settings.rotationForce * settings.rotationLeftDirection);
            OnForceLeft?.Invoke(this, EventArgs.Empty);
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            landerRigidbody.AddTorque(settings.rotationForce * settings.rotationRightDirection);
            OnForceRight?.Invoke(this, EventArgs.Empty);
        }
    }

    private void ConsumeFuel()
    {
        if (FuelIsEmpty())
        {
            return;
        }

        fuelAmount -= settings.fuelConsuptionAmount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out LandingPad pad))
        {
            Debug.Log("CRASHED");
            return;
        }

        float collisionDegree = Vector2.Dot(Vector2.up, transform.up);

        if (collision.relativeVelocity.magnitude < settings.accesiblaLandingVelocity)
        {
            Debug.Log("correct speed");
        }

        if (collisionDegree >= settings.accesiblaLandingDegree)
        {
            Debug.Log("correct angle");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool found = collision.gameObject.TryGetComponent(out Fuel fuel);

        if (!found)
        {
            return;
        }

        fuelAmount += fuel.GetFuelAmount();
        fuel.DestroySelf();
    }
}
