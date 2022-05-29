using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
public class IslandMap : MonoBehaviour ,IPointerDownHandler ,IPointerUpHandler ,IBeginDragHandler,IEndDragHandler , IDragHandler
{
    private Vector3 _mouseReference;

    [SerializeField] private PlayerInput _playerInput;
    private InputActionMap _actionMap;
    private InputAction _pos;

    public Camera worldMiniMapCamera;

    public Image Marker;
    public TextMeshProUGUI markerDistance;
    public SpriteRenderer originalMap;

    void Start()
    {
        //enable is required only if you're not using PlayerInput anywhere else
        _playerInput = GetComponent<PlayerInput>();

        _actionMap = _playerInput.actions.FindActionMap("Lobby");
        _pos = _actionMap.FindAction("Position");

    }

    public RectTransform mapUiImage;
    public GameObject pinkPixel;
    public void OnPointerDown(PointerEventData eventData)
    {
        _mouseReference = _pos.ReadValue<Vector2>();
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            if (result.gameObject.name == "WholeMinimapRenderTexture")
            {
                //Debug.Log(result.screenPosition);
                //Debug.Log(result.worldPosition);
                //Debug.Log(result.worldNormal);
                Marker.gameObject.GetComponent<RectTransform>().position = result.screenPosition;

                float distance = Mathf.Round(Vector2.Distance(transform.position, Marker.transform.position));

                markerDistance.text = distance.ToString() + " M";


                Vector3 screenPoint = Camera.main.ScreenToViewportPoint(_mouseReference);
                screenPoint.x = Mathf.InverseLerp(0.38f, 0.9f, screenPoint.x);
                //Debug.Log(screenPoint.x);
                Vector2 minimapScreenPoint = screenPoint; // normalise value
                minimapScreenPoint.x = minimapScreenPoint.x * 0.20f; //tweking width;
                minimapScreenPoint.y = minimapScreenPoint.y * 0.35f;
                var mousePosition = minimapScreenPoint * new Vector2(Screen.width, Screen.height)  ; 
                Ray r = worldMiniMapCamera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, 0));
                //Debug.Log(worldMiniMapCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0)));
                RaycastHit hit;
                if (Physics.Raycast(r, out hit))
                {
                    GameObject clickPoint = Instantiate(pinkPixel);
                    clickPoint.transform.position = hit.point;
                    Debug.DrawRay(r.origin, r.direction * 500, Color.red, 100, true);
                }
                //Debug.Log(eventData.pointerCurrentRaycast.screenPosition);

            }
        }
        


    }
    public RectTransform closePanel;

    public void CastRay()
    {
        Vector3 screenPoint = Camera.main.ScreenToViewportPoint(_mouseReference);
        Debug.Log(screenPoint.x);
        screenPoint.x = Mathf.InverseLerp(0.38f, 0.9f, screenPoint.x);
        Vector2 minimapScreenPoint = screenPoint;
        var mousePosition = minimapScreenPoint * new Vector2(Screen.width, Screen.height);
        Ray r = worldMiniMapCamera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, 0));
        RaycastHit hit;
        if (Physics.Raycast(r, out hit))
        {
            GameObject clickPoint = Instantiate(pinkPixel);
            clickPoint.transform.position = hit.point;
            Debug.DrawRay(r.origin, r.direction * 500, Color.red, 100, true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("PointerUp");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("EndDrag");
    }

}
