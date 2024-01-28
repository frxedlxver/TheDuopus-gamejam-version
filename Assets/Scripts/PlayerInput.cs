using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerActions _input;
    
    public Vector2 MoveLeftValue { get; private set; } = Vector2.zero;
    public Vector2 MoveRightValue { get; private set; } = Vector2.zero;
    public bool SuctionLeftHeld { get; private set; }
    public bool SuctionRightHeld { get; private set; }

    private void OnEnable()
    {
        _input = new PlayerActions();
        _input.Enable();
        _input.gameplay.MoveLeft.performed += MoveLeft;
        _input.gameplay.MoveLeft.canceled += MoveLeft;
        
        _input.gameplay.MoveRight.performed += MoveRight;
        _input.gameplay.MoveRight.canceled += MoveRight;

        _input.gameplay.SuctionLeft.performed += SuctionLeft;
        _input.gameplay.SuctionLeft.canceled += SuctionLeft;
        
        _input.gameplay.SuctionRight.performed += SuctionRight;
        _input.gameplay.SuctionRight.canceled += SuctionRight;
    }
    
    private void OnDisable()
    {
        
        _input.gameplay.MoveLeft.performed -= MoveLeft;
        _input.gameplay.MoveLeft.canceled -= MoveLeft;
        
        _input.gameplay.MoveRight.performed -= MoveRight;
        _input.gameplay.MoveRight.canceled -= MoveRight;

        _input.gameplay.SuctionLeft.performed -= SuctionLeft;
        _input.gameplay.SuctionLeft.canceled -= SuctionLeft;
        
        _input.gameplay.SuctionRight.performed -= SuctionRight;
        _input.gameplay.SuctionRight.canceled -= SuctionRight;
        
        _input.gameplay.Disable();
    }

    private void MoveLeft(InputAction.CallbackContext ctx)
    {
        MoveLeftValue = ctx.ReadValue<Vector2>();
    }
    
    private void MoveRight(InputAction.CallbackContext ctx)
    {
        MoveRightValue = ctx.ReadValue<Vector2>();
    }

    private void SuctionRight(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            SuctionRightHeld = true;
        }
        else if (ctx.canceled)
        {
            SuctionRightHeld = false;
        }
    }
    
    private void SuctionLeft(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            SuctionLeftHeld = true;
        }
        else if (ctx.canceled)
        {
            SuctionLeftHeld = false;
        }
    }
    
}
    
