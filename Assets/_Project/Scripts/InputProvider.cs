// Maded by Pedro M Marangon
using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace S2P_Test
{
    public class InputProvider : MonoBehaviour
    {
        public static Vector2 MousePosition;
        public static Action OnMouseClick;

        public void GetMousePosition(CallbackContext ctx)
		{
            MousePosition = ctx.ReadValue<Vector2>();
		}

        public void MouseClick(CallbackContext ctx) => OnMouseClick?.Invoke();
    }
}