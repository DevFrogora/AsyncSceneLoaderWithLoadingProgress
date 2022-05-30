using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Example : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public GameObject LocationMarkerWorldMap;
    public GameObject LocationMarkerMiniMap;

    TextMeshProUGUI markerTextOnWorldMap;
    TextMeshProUGUI markerTextOnMiniMap;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        markerTextOnWorldMap = LocationMarkerWorldMap.GetComponentInChildren<TextMeshProUGUI>();
        markerTextOnMiniMap = LocationMarkerMiniMap.GetComponentInChildren<TextMeshProUGUI>();

    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);



    }

    private void LateUpdate()
    {   
        if (LocationMarkerWorldMap.active)
        {
            float distance = Vector3.Distance(LocationMarkerWorldMap.transform.position, transform.position);
            distance = (float)Mathf.Round(distance);
            markerTextOnWorldMap.text = distance.ToString() + " M";

            LocationMarkerWorldMap.GetComponent<LineRenderer>().SetPosition(0, new Vector3(transform.position.x, 10, transform.position.z));
            LocationMarkerWorldMap.GetComponent<LineRenderer>().SetPosition(1, new Vector3(LocationMarkerWorldMap.transform.position.x, 10, LocationMarkerWorldMap.transform.position.z));

            markerTextOnMiniMap.text = distance.ToString() + " M";

            LocationMarkerMiniMap.GetComponent<LineRenderer>().SetPosition(0, new Vector3(transform.position.x, 0, transform.position.z));
            LocationMarkerMiniMap.GetComponent<LineRenderer>().SetPosition(1, new Vector3(LocationMarkerMiniMap.transform.position.x, 0, LocationMarkerMiniMap.transform.position.z));
        }
    }
}