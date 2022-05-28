using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class OnMouseNewInputSystem : MonoBehaviour ,IPointerDownHandler ,IPointerUpHandler ,IBeginDragHandler,IEndDragHandler , IDragHandler
{
    private float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating;

    [SerializeField] private PlayerInput _playerInput;
    private InputActionMap _actionMap;
    private InputAction _pos;

    void Start()
    {
        _sensitivity = 0.4f;
        _rotation = Vector3.zero;

        //enable is required only if you're not using PlayerInput anywhere else
        _playerInput = GetComponent<PlayerInput>();

        _actionMap = _playerInput.actions.FindActionMap("Lobby");
        _pos = _actionMap.FindAction("Position");

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        // rotating flag
        _isRotating = true;

        // store mouse
        //_mouseReference = Mouse.current.position.ReadValue();
        _mouseReference = _pos.ReadValue<Vector2>();

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // rotating flag
        _isRotating = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        if (_isRotating)
        {
            Vector3 mousePos = _pos.ReadValue<Vector2>();
            mousePos.z = Camera.main.nearClipPlane;

            // offset
            _mouseOffset = (mousePos - _mouseReference);

            // apply rotation
            _rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;

            // rotate
            transform.Rotate(_rotation);

            // store mouse
            _mouseReference = _pos.ReadValue<Vector2>();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag End");
    }

    //void OnMouseDown()
    //{
    //    // rotating flag
    //    _isRotating = true;

    //    // store mouse
    //    _mouseReference = Input.mousePosition;
    //}

    //void OnMouseUp()
    //{
    //    // rotating flag
    //    _isRotating = false;
    //}

}
