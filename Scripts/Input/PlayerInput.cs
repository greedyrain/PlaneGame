using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInput", menuName = "ScriptableObject/PlayerInput")]
public class PlayerInput : ScriptableObject, PlayerInputAction.IGamePlayActions
{
    private PlayerInputAction inputAction;
    public event UnityAction<Vector2> onMove = delegate { };
    public event UnityAction onStop = delegate { };

    public event UnityAction onFire = delegate { };
    public event UnityAction onFireStop = delegate { };

    private void OnEnable()
    {
        inputAction = new PlayerInputAction();
        inputAction.GamePlay.SetCallbacks(this);
    }

    private void OnDisable()
    {
        DisableGamePlayInput();
    }

    public void EnableGameplay()
    {
        inputAction.GamePlay.Enable();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DisableGamePlayInput()
    {
        inputAction.GamePlay.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onMove(context.ReadValue<Vector2>());
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            onStop();
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            onFire();
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            onFireStop();
        }
    }
}