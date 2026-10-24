using UnityEngine;

[RequireComponent(typeof(MoveBehaviour))]
[RequireComponent(typeof(JumpBehaviour))]

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected MoveBehaviour _move;
    [SerializeField] protected JumpBehaviour _jump;

    protected virtual void Awake()
    {
        _move = GetComponent<MoveBehaviour>();
        _jump = GetComponent<JumpBehaviour>();
    }
}