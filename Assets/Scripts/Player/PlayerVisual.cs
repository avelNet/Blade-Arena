using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private PlayerMovement _movement;
    private SpriteRenderer _render;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _render = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Animations();
        FlipXPlayer();
    }

    private void Animations()
    {
        _animator.SetFloat("Speed", _movement.SpeedValue, 0.1f, Time.deltaTime);
    }

    private void FlipXPlayer()
    {
        if (_movement.MoveInput.x != 0)
        {
            _render.flipX = _movement.MoveInput.x < 0;
        }
    }
}
