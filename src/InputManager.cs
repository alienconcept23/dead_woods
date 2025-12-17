using UnityEngine;

// Placeholder InputManager: abstracts input for keyboard/mouse, controller and touch.
// We'll replace/extend this with Unity Input System for mobile and controller mappings.
public static class InputManager
{
    public static Vector2 GetMove()
    {
        // On touch platforms this will be replaced with joystick/touch code.
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public static bool GetAttack()
    {
        return Input.GetButtonDown("Fire1");
    }

    // TODO: add support for remapping, controller, and touch gestures
}
