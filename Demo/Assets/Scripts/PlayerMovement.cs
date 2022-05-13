using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum EPlayerMovementState { Idle, Walk, Done}
    public EPlayerMovementState playerMovementState;

    public FloatingJoystick floatingJoystick;
    private float HInput;
    private float VInput;
    public float Speed;

    private Rigidbody _rb;

    private void OnEnable()
    {
        Actions.OnGameStart += GameStart;
        Actions.OnGameDone += GameDone;
    }

    private void OnDisable()
    {
        Actions.OnGameStart -= GameStart;
        Actions.OnGameDone -= GameDone;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void GameStart()
    {
        playerMovementState = EPlayerMovementState.Walk;
    }

    private void GameDone()
    {
        playerMovementState = EPlayerMovementState.Done;
    }
    
    private void Update()
    {
        if(playerMovementState == EPlayerMovementState.Walk)
        {
            HInput = floatingJoystick.Horizontal * 0.15f;
            VInput = floatingJoystick.Vertical * 0.15f;
        }
    }
    
    private void FixedUpdate()
    {
        if(playerMovementState == EPlayerMovementState.Walk)
        {
            transform.LookAt(_rb.position + new Vector3(HInput, 0, VInput));
           _rb.MovePosition((_rb.position + new Vector3(HInput, 0, VInput) * Speed));
        }
    }
}
