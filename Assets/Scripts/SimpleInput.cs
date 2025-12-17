using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

// Lightweight input adapter: reads from the new Input System when available, falls back to legacy Input.
public class SimpleInput : MonoBehaviour
{
    public Vector2 Move { get; private set; }
    public bool Attack { get; private set; }

    void Update()
    {
#if ENABLE_INPUT_SYSTEM
        // Try keyboard/gamepad via Input System if compiled
        if (Keyboard.current != null)
        {
            float x = 0f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) x = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) x = 1f;
            float y = 0f;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) y = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) y = -1f;
            Move = new Vector2(x, y).normalized;

            Attack = Mouse.current != null ? Mouse.current.leftButton.wasPressedThisFrame : false;
            return;
        }
#endif
        // Fallback to legacy input
        Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Attack = Input.GetButtonDown("Fire1");
    }
}