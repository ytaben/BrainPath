using UnityEngine;
using System.Collections;

public class MenuCameraMover : MonoBehaviour {

    // Use this for initialization
    private Vector3 relCameraPos;       // The relative position of the camera from the player.
    private float relCameraPosMag;      // The distance of the camera from the player.
    private Vector3 newPos;             // The position the camera is trying to reach.
    private Vector3 offset;
    // Use this for initialization
    void Start()
    {
        offset = transform.position;

        relCameraPos = transform.position - FocusPoint.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;

    }
    public float turnSpeed = 4.0f;
    public Transform FocusPoint;
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = new Vector3(FocusPoint.position.x, FocusPoint.position.y, FocusPoint.position.z) + offset;
        transform.position = pos;
        offset = Quaternion.AngleAxis(turnSpeed, Vector3.up) * offset;
        //offset = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * turnSpeed, Vector3.left) * offset;
        transform.position = FocusPoint.position + offset;
        transform.LookAt(FocusPoint.position);
    }
}
