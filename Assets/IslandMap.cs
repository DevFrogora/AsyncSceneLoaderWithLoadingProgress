using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class IslandMap : MonoBehaviour ,IPointerDownHandler ,IPointerUpHandler ,IBeginDragHandler,IEndDragHandler , IDragHandler
{
    private Vector3 _mouseReference;

    [SerializeField] private PlayerInput _playerInput;
    private InputActionMap _actionMap;
    private InputAction _pos;

    public Camera worldMiniMapCamera;

    public Image Marker;

    void Start()
    {
        //enable is required only if you're not using PlayerInput anywhere else
        //_playerInput = GetComponent<PlayerInput>();

        //_actionMap = _playerInput.actions.FindActionMap("Lobby");
        //_pos = _actionMap.FindAction("Position");

    }


    public void OnPointerDown(PointerEventData eventData)
    {
        //_mouseReference = _pos.ReadValue<Vector2>();
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            if (result.gameObject.name == "WholeMinimapRenderTexture")
            {
                Debug.Log(result.screenPosition);
                Debug.Log(result.worldPosition);
                Debug.Log(result.worldNormal);
                Marker.gameObject.GetComponent<RectTransform>().position = result.screenPosition;

            }
        }
        //Debug.Log(_mouseReference);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("PointerUp");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
    }

}
