using UnityEngine;

public class Fuel : MonoBehaviour
{
    private float fuelAmount = 10f;

    public float GetFuelAmount()
    {
        return fuelAmount;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
