using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.upArrowKey.isPressed)  
        {
            Debug.Log("key up pressed");            
        }
    }
}
