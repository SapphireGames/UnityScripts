using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CameraController : MonoBehaviour {
 
    public Transform Camera;
    public Transform Player;
    public Vector3 offset;
    [Range(1, 10)]
    public float speed;
    public float smoothSpeed = 0.125f;
 
    void FixedUpdate()
    {
        Vector3 desiredPosition = Player.position + Vector3.up.normalized * speed * Time.deltaTime;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
 
        if (Input.GetKey(KeyCode.W))
        {
            Player.transform.Translate(Vector3.up.normalized * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            Player.transform.Translate(Vector3.left.normalized * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Player.transform.Translate(Vector3.down.normalized * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            Player.transform.Translate(Vector3.right.normalized * speed * Time.deltaTime);
        }
    }
}
