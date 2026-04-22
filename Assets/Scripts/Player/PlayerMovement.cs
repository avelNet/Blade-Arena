using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public float SpeedValue { get; private set; }

    [Header("Components")]
    private Rigidbody2D _rb;
    [Header("Settings")]
    private float _moveSpeed = 5f;

    [Header("Dash Settings")]
    [SerializeField] private float _dashSpeed = 12f;
    [SerializeField] private float _dashTime = 0.15f;
    private Vector2 _lastDirection = Vector2.right;
    private bool _isDashing;
    private float _coolDownTime = 3f;
    private float _coolDownTimer = 0f;

    private enum MoveState
    {
        Normal,
        Dashing
    }
    private MoveState _moveState;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        if(_coolDownTime > 0)
        {
            _coolDownTimer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !_isDashing && _coolDownTimer <= 0)
        {
            StartCoroutine(Dash(_lastDirection));
        }
    }

    private void FixedUpdate()
    {
        if (_isDashing) return;
        _rb.velocity = MoveInput.normalized * _moveSpeed;
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        MoveInput = new Vector2(h, v);
        if(MoveInput != Vector2.zero)
        {
            _lastDirection = MoveInput.normalized;
        }

        SpeedValue = MoveInput.normalized.magnitude;
    }

    private IEnumerator Dash(Vector2 dir)
    {
        _moveState = MoveState.Dashing;

        _isDashing = true;
        float dashTimer = 0;

        while(dashTimer < _dashTime)
        {
            _rb.velocity = dir * _dashSpeed;
            dashTimer += Time.deltaTime;
            CoolDown();
            yield return null;
        }
        _isDashing = false;
        _moveState = MoveState.Normal;
        _rb.velocity = Vector2.zero;

        _coolDownTimer = _coolDownTime;
    }

    private void CoolDown()
    {
        
    }

    public bool IsDashing()
    {
        return _isDashing;
    }
}
