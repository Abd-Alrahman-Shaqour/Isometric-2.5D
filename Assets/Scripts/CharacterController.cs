using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Animator _animator;
    private Rigidbody2D _rb;
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleInput();
        UpdateAnimation();
    }

    private void HandleInput()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        var movement = new Vector2(horizontal, vertical);
        movement.Normalize(); // Normalize to ensure consistent speed in all directions

        _rb.velocity = movement * speed * Time.deltaTime;
    }

    private void UpdateAnimation()
    {
        _animator.SetFloat(Horizontal, _rb.velocity.x);
        _animator.SetFloat(Vertical, _rb.velocity.y);
        _animator.SetFloat(Speed, _rb.velocity.magnitude);
    }
}
