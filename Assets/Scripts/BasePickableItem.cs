using UnityEngine;

public class BasePickableItem : MonoBehaviour
{
    private float value;

    public void Init(float initValue)
    {
        value = initValue;
    }

    public float GetValue()
    {
        return value;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
