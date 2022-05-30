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

    public GameObject player;

    void Start()
    {
        //enable is required only if you're not using PlayerInput anywhere else
        _playerInput = GetComponent<PlayerInput>();

        _actionMap = _playerInput.actions.FindActionMap("Lobby");
        _pos = _actionMap.FindAction("Position");

        StartCoroutine(waiter());
    }
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1);
        //RemoveMarkerClick();
    }

    public RectTransform mapUiImage;
    public GameObject locationMarkerWorldMap;
    public GameObject locationMarkerMiniMap;

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

                // 1
                //Marker.gameObject.GetComponent<RectTransform>().position = result.screenPosition;
                //Marker.gameObject.SetActive(true);

                //float distance = Mathf.Round(Vector2.Distance(transform.position, Marker.transform.position));


                #region castRegion
                Vector3 screenPoint = Camera.main.ScreenToViewportPoint(_mouseReference);
                screenPoint.x = Mathf.InverseLerp(0.38f, 0.9f, screenPoint.x);
                Vector2 minimapScreenPoint = screenPoint; // normalise value
                minimapScreenPoint.x = minimapScreenPoint.x * 0.19f; //tweking width;
                minimapScreenPoint.y = minimapScreenPoint.y * 0.33f; // tweaking height
                var mousePosition = minimapScreenPoint * new Vector2(Screen.width, Screen.height)  ; 
                Ray r = worldMiniMapCamera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, 0));
                RaycastHit hit;
                if (Physics.Raycast(r, out hit))
                {
                    //GameObject clickPoint = Instantiate(pinkPixel);

                    locationMarkerWorldMap.transform.position = hit.point;
                    locationMarkerMiniMap.transform.position = hit.point;
                    //1
                    //float distance = Vector3.Distance(locationMarker.transform.position ,player.transform.position);
                    //markerDistance.text = distance.ToString() + " M";

                    locationMarkerWorldMap.SetActive(true);
                    locationMarkerMiniMap.SetActive(true);
                    //1
                    //locationMarker.GetComponent<LineRenderer>().SetPosition(0, new Vector3(player.transform.position.x,4,player.transform.position.z));
                    //locationMarker.GetComponent<LineRenderer>().SetPosition(1, new Vector3(locationMarker.transform.position.x, 4, locationMarker.transform.position.z));

                    isMarkerOnMap = true;
                    Debug.DrawRay(r.origin, r.direction * 500, Color.red, 100, true);
                }
                #endregion



            }
        }

    }
    public bool isMarkerOnMap;

    //private void LateUpdate()
    //{
    //    if(isMarkerOnMap)
    //    {
    //        float distance = Vector3.Distance(locationMarker.transform.position, player.transform.position);
    //        markerDistance.text = distance.ToString() + " M";

    //        locationMarker.SetActive(true);
    //        locationMarker.GetComponent<LineRenderer>().SetPosition(0, new Vector3(player.transform.position.x, 4, player.transform.position.z));
    //        locationMarker.GetComponent<LineRenderer>().SetPosition(1, new Vector3(locationMarker.transform.position.x, 4, locationMarker.transform.position.z));
    //    }
    //}

    //public LineRenderer linedirection;
    public void RemoveMarkerClick()
    {
        Marker.gameObject.SetActive(false);
        locationMarkerWorldMap.SetActive(false);
        locationMarkerMiniMap.SetActive(false);
        isMarkerOnMap = false;
    }

    public RectTransform closePanel;

    public void CastRay()
    {
        Vector3 screenPoint = Camera.main.ScreenToViewportPoint(_mouseReference);
        screenPoint.x = Mathf.InverseLerp(0.38f, 0.9f, screenPoint.x);
        Vector2 minimapScreenPoint = screenPoint; // normalise value
        minimapScreenPoint.x = minimapScreenPoint.x * 0.20f; //tweking width;
        minimapScreenPoint.y = minimapScreenPoint.y * 0.35f; // tweaking height
        var mousePosition = minimapScreenPoint * new Vector2(Screen.width, Screen.height);
        Ray r = worldMiniMapCamera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, 0));
        RaycastHit hit;
        if (Physics.Raycast(r, out hit))
        {
            //GameObject clickPoint = Instantiate(pinkPixel);
            locationMarkerWorldMap.transform.position = hit.point;
            float distance = Vector3.Distance(locationMarkerWorldMap.transform.position, player.transform.position);
            markerDistance.text = distance.ToString() + " M";

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
