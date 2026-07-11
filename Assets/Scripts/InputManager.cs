using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    InputAction keyLeft;
    [SerializeField]
    InputAction keyUp;
    [SerializeField]
    InputAction keyRight;

    public event UnityAction<int> OnInput;

    void Start()
    {
        keyLeft.Enable();
        keyUp.Enable();
        keyRight.Enable();
    }

    void Update()
    {
        if (OnInput == null)
            return;

        if (keyLeft.triggered)
            OnInput.Invoke(0);
        else if (keyUp.triggered)
            OnInput.Invoke(1);
        else if (keyRight.triggered)
            OnInput.Invoke(2);
    }
}
