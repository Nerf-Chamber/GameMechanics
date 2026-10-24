using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class MoveBehaviour : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Awake() => _rb = GetComponent<Rigidbody2D>();

    public void Move(float direction, float speed) => _rb.linearVelocity = new Vector2(direction * speed, _rb.linearVelocity.y);
}