using UnityEngine;

public class PlayerRotation : MonoBehaviour
{

    // Game Object 
    private Camera _camera;
    // Game Object Name 
    private string CameraName = "Game_Camera";
    // Hold position of the mouse 
    private Vector3 _mousePos;

    // Connects to the camera 
    private void Start() {_camera = GameObject.Find(CameraName).GetComponent<Camera>();  }

    private void Update()
    {
        // Gets the postion of the mouse 
        _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        // Getting the position differnce between the mouse and the object 
        var pos = _mousePos - transform.position;
        // Gets the Z rotation given that position 
        var rotZ = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        // Save that rotation to the player rotation 
        transform.rotation = Quaternion.Euler(0, 0, rotZ - 90);
    }

}
