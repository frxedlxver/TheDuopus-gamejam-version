using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerActions _input;
    
    public Vector2 MoveLeftValue { get; private set; } = Vector2.zero;
    public Vector2 MoveRightValue { get; private set; } = Vector2.zero;
    
    public bool SuctionLeftPressed { get; private set; }
    public bool SuctionLeftHeld { get; private set; }
    public bool SuctionRightHeld { get; private set; }
    public bool SuctionRightPressed { get; private set; }
    public float RightTriggerInputStrength { get; private set; }
    public float LeftTriggerAmount { get; private set; }

    private void OnEnable()
    {
        _input = new PlayerActions();
        _input.Enable();
        _input.gameplay.MoveLeft.performed += MoveLeft;
        _input.gameplay.MoveLeft.canceled += MoveLeft;
        
        _input.gameplay.MoveRight.performed += MoveRight;
        _input.gameplay.MoveRight.canceled += MoveRight;

        _input.gameplay.SuctionLeftHold.started += SuctionLeftHold;
        _input.gameplay.SuctionLeftHold.performed += SuctionLeftHold;
        _input.gameplay.SuctionLeftHold.canceled += SuctionLeftHold;
        
        _input.gameplay.SuctionRightHold.started += SuctionRightHold;
        _input.gameplay.SuctionRightHold.performed += SuctionRightHold;
        _input.gameplay.SuctionRightHold.canceled += SuctionRightHold;
    }

    

    private void OnDisable()
    {
        
        _input.gameplay.MoveLeft.performed -= MoveLeft;
        _input.gameplay.MoveLeft.canceled -= MoveLeft;
        
        _input.gameplay.MoveRight.performed -= MoveRight;
        _input.gameplay.MoveRight.canceled -= MoveRight;

        _input.gameplay.SuctionLeftHold.performed -= SuctionLeftHold;
        _input.gameplay.SuctionLeftHold.canceled -= SuctionLeftHold;
        
        _input.gameplay.SuctionRightHold.performed -= SuctionRightHold;
        _input.gameplay.SuctionRightHold.canceled -= SuctionRightHold;
        
        _input.gameplay.Disable();
    }

    private void Update()
    {
        var rightVal = _input.gameplay.SuctionRightHold.ReadValue<float>();
        var leftVal = _input.gameplay.SuctionLeftHold.ReadValue<float>();

        RightTriggerInputStrength = NormalizeTriggerInputValue(rightVal);
        LeftTriggerAmount = NormalizeTriggerInputValue(leftVal);


        Debug.Log("Left: " + LeftTriggerAmount + " | Right: " + RightTriggerInputStrength);
    }

    private float NormalizeTriggerInputValue(float inputValue)
    {
        return inputValue == 0 ? 0 : (inputValue - 0.5f) * 2;
    }
    private void MoveLeft(InputAction.CallbackContext ctx)
    {
        MoveLeftValue = ctx.ReadValue<Vector2>();
    }
    
    private void MoveRight(InputAction.CallbackContext ctx)
    {
        MoveRightValue = ctx.ReadValue<Vector2>();
    }

    private void SuctionRightHold(InputAction.CallbackContext ctx)
    {
        if(ctx.started)
        {
            SuctionRightHeld = true;
            SuctionRightPressed = true;
        }
        else if (ctx.performed)
        {
            SuctionRightHeld = true;
            SuctionRightPressed = false;

        }
        else if (ctx.canceled)
        {
            SuctionRightHeld = false;
            SuctionRightPressed = false;
        }
    }
    
    private void SuctionLeftHold(InputAction.CallbackContext ctx)
    {
        if(ctx.started)
        {
            SuctionLeftHeld = true;
            SuctionLeftPressed = true;
        }
        else if (ctx.performed)
        {
            SuctionLeftHeld = true;
            SuctionLeftPressed = false;

        }
        else if (ctx.canceled)
        {
            SuctionLeftHeld = false;
            SuctionLeftPressed = false;
        }
    }
}