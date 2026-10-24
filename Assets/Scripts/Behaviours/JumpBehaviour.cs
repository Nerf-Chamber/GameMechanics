using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class JumpBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private float jumpForce;
    [SerializeField] private float raycastDistance;
    [SerializeField] private LayerMask collisionLayer;

    private void Awake() => _rb = GetComponent<Rigidbody2D>();

    public void Jump() => _rb.AddForceY(jumpForce);
    public bool IsGrounded(Vector2 origin) 
    {
        Debug.DrawRay(origin, Vector2.down, Color.red);
        return Physics2D.Raycast(origin, Vector2.down, raycastDistance, collisionLayer); 
    }
}