using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeController : MonoBehaviour
{

    public void Pause(InputAction.CallbackContext context) // PAUSE
    {
        if(context.performed)
        {
            
            if(Time.timeScale == 1f)
            {
                Time.timeScale = 0f;
            }
            else if(Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void SpeedUp (InputAction.CallbackContext context) // AUGMENTE LA VITESSE DU JEU
    {
        if(context.performed)
        {
            Time.timeScale *= 2.0f;
        }
    }

    public void SpeedNormal(InputAction.CallbackContext context) // LA RAMENE A LA NORMAL
    {
        if(context.performed)
        {
            Time.timeScale = 1.0f;
        }
    }
}
