using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions inputActions;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private PlayerState playerState;

    private float direction = 0;

    private bool isPreparedForRun;

    protected override void Awake()
    {
        base.Awake();

        inputActions = new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
    }
    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    private void Start()
    {
        playerState = PlayerState.Idle;
    }

    private void FixedUpdate()
    {
        switch (playerState)
        {
            case PlayerState.Idle:

                break;

            case PlayerState.Walk:
                HelperMove(walkSpeed);
                break;

            case PlayerState.Run:
                HelperMove(runSpeed);
                break;

            case PlayerState.Jump:
                _jump.Jump();
                playerState = PlayerState.InTheAir;
                break;

            case PlayerState.InTheAir:
                if (_jump.IsGrounded(transform.position))
                {
                    if (direction == 0) { playerState = PlayerState.Idle; }
                    else { playerState = isPreparedForRun ? PlayerState.Run : PlayerState.Walk;}
                }
                break;
        }
    }


    // ----------------------------------------- INTERFACE IMPLEMENTATION
    public void OnMove(InputAction.CallbackContext context)
    {
        direction = (float)Math.Round(context.ReadValue<Vector2>().x);
        playerState = isPreparedForRun ? PlayerState.Run : PlayerState.Walk;
    }
    public void OnPrepareRun(InputAction.CallbackContext context)
    {
        isPreparedForRun = context.canceled ? false : true;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (_jump.IsGrounded(transform.position)) { playerState = PlayerState.Jump; }
    }

    // ----------------------------------------- HELPER METHODS
    public void HelperMove(float speed)
    {
        _move.Move(direction, speed);
        playerState = isPreparedForRun ? PlayerState.Run : PlayerState.Walk;
    }
}