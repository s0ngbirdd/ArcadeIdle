using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerTouchMovement : MonoBehaviour
{
    [SerializeField] private Vector2 _joystickSize = new Vector2(100, 100);
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _playerSpeed = 5f;
    
    private CharacterController _characterController;
    private Animator _animator;
    private Finger _movementFinger;
    private Vector2 _movementAmount;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleFingerUp;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleFingerUp;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        Vector3 scaledMovement = _playerSpeed * Time.deltaTime * new Vector3(_movementAmount.x, 0, _movementAmount.y);
        
        _characterController.transform.LookAt(_characterController.transform.position + scaledMovement, Vector3.up);
        _characterController.Move(scaledMovement);
    }

    private void HandleFingerDown(Finger touchedFinger)
    {
        if (_movementFinger == null)
        {
            _animator.SetBool("IsRunning", true);
            
            _movementFinger = touchedFinger;
            _movementAmount = Vector2.zero;
            _joystick.gameObject.SetActive(true);
            _joystick.rectTransform.sizeDelta = _joystickSize;
            _joystick.rectTransform.anchoredPosition = ClampStartPosition(touchedFinger.screenPosition);
        }
    }
    
    private void HandleFingerUp(Finger lostFinger)
    {
        if (lostFinger == _movementFinger)
        {
            _animator.SetBool("IsRunning", false);
            
            _movementFinger = null;
            _joystick.knob.anchoredPosition = Vector2.zero;
            _joystick.gameObject.SetActive(false);
            _movementAmount = Vector2.zero;
        }
    }
    
    private void HandleFingerMove(Finger movedFinger)
    {
        if (movedFinger == _movementFinger)
        {
            Vector2 knobPosition;
            float maxMovement = _joystickSize.x / 2f;
            ETouch.Touch currentTouch = movedFinger.currentTouch;

            if (Vector2.Distance(currentTouch.screenPosition, _joystick.rectTransform.anchoredPosition) > maxMovement)
            {
                knobPosition = (currentTouch.screenPosition - _joystick.rectTransform.anchoredPosition).normalized * maxMovement;
            }
            else
            {
                knobPosition = currentTouch.screenPosition - _joystick.rectTransform.anchoredPosition;
            }

            _joystick.knob.anchoredPosition = knobPosition;
            _movementAmount = knobPosition / maxMovement;
        }
    }

    private Vector2 ClampStartPosition(Vector2 startPosition)
    {
        if (startPosition.x < _joystickSize.x / 2)
        {
            startPosition.x = _joystickSize.x / 2;
        }
        else if (startPosition.x > Screen.width - _joystickSize.x / 2)
        {
            startPosition.x = Screen.width - _joystickSize.x / 2;
        }

        if (startPosition.y < _joystickSize.y / 2)
        {
            startPosition.y = _joystickSize.y / 2;
        }
        else if (startPosition.y > Screen.height - _joystickSize.y / 2)
        {
            startPosition.y = Screen.height - _joystickSize.y / 2;
        }

        return startPosition;
    }
}
