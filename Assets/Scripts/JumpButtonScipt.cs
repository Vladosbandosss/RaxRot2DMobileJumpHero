using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButtonScipt : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (PlayerScriptJump.instance != null)
        {
            PlayerScriptJump.instance.SetPower(true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (PlayerScriptJump.instance != null)
        {
            PlayerScriptJump.instance.SetPower(false);
        }
    }
}
