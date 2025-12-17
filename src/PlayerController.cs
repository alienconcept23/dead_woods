using UnityEngine;

// Basic player controller for initial MVP.
// Movement is implemented in world X/Z plane for an isometric camera.
// TODO: Replace Input with Unity InputSystem; hook up to networking (Mirror) later.

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public Transform cameraTransform;

    private CharacterController _controller;
    private Vector3 _velocity;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    SimpleInput _input;

    void Start()
    {
        _input = FindObjectOfType<SimpleInput>();
        if (_input == null)
        {
            var inputGO = new GameObject("SimpleInput");
            _input = inputGO.AddComponent<SimpleInput>();
        }
    }

    void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    void HandleMovement()
    {
        Vector2 move = _input != null ? _input.Move : new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Vector3 right = cameraTransform.right;
        Vector3 forward = Vector3.Cross(right, Vector3.up); // ensures movement aligned to camera

        Vector3 direction = (right * move.x + forward * move.y).normalized;

        _controller.Move(direction * moveSpeed * Time.deltaTime);

        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    void HandleAttack()
    {
        if (_input != null && _input.Attack)
        {
            var sc = GetComponent<SimpleCombat>();
            if (sc != null) sc.MeleeAttack();
        }
    }

    // TODO: Add animation hooks, attack methods, class abilities, network sync.
}
