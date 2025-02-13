using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class DebuggerManager : MonoBehaviour
{
    public static UnityAction onPauseDebugEnter;
    public static UnityAction onPauseDebugExit;

    bool activated;
    
    void Start()
    {
        
    }
    private void OnGUI()
    {
        if (activated)
        {

            GUI.Label(new Rect(500, 100, 200, 30), "PAUSED");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
               if(activated)
            {
                activated = false;
                Time.timeScale = 1;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                if (onPauseDebugExit!=null)
                {

                onPauseDebugExit.Invoke();
                }
            }
               else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                activated = true;
                Time.timeScale = 0;
                     if(onPauseDebugEnter!=null)
                {

                onPauseDebugEnter.Invoke();
                }
            }
        }
    }
    public static void DebugPauseEnter()
    {
        
    }
}
public interface IDebugPause
{
    public void OnDebugPauseEnter();
    public void OnDebugPauseExit();
}
