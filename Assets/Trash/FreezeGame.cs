using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FreezeGame : MonoBehaviour
{
    public PlayerInput playerInput;
    public static FreezeGame instance;
    bool freezed = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            this.enabled = false;
        }


    }
    private void OnEnable()
    {
        DebuggerManager.onPauseDebugEnter += ActivatePauseDebug;
        DebuggerManager.onPauseDebugExit += DeactivatePauseDebug;
    }
    void ActivatePauseDebug()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
     
    }
    void DeactivatePauseDebug()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
   
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("ji");
            if (freezed)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
                freezed = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0;
                freezed = true;
            }
        }
    }

 
}
