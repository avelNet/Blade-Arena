using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public float SpeedValue { get; private set; }

    private Rigidbody2D _rb;

    private float _moveSpeed = 5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        _rb.velocity = MoveInput.normalized * _moveSpeed;
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        MoveInput = new Vector2(h, v);
        SpeedValue = MoveInput.normalized.magnitude;
    }
}
