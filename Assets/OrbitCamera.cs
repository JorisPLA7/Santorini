using UnityEngine;
using System.Collections;

public class OrbitCamera : MonoBehaviour 
{
    //public Transform target;

    public float zoomSpeed = 2f;

    private Vector3 _offset;

    Vector2 input;

    Vector3 focalPoint = Vector3.zero; // Center of Game Board

    public bool rotateMode = false;

    Vector3 originalPosition;
    Quaternion originalRotation;

    public float rotAmt = 15f;



    private void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rotateMode = !rotateMode;
        }


        if (rotateMode)
        {
            float origZ = transform.position.z;

            
            float yRot = Input.GetAxis("Vertical") * rotAmt;

            float xRot = Input.GetAxis("Horizontal") * rotAmt;

            Vector3 eulerAngles = transform.eulerAngles;

            transform.RotateAround(focalPoint, Vector3.up, xRot);
            transform.RotateAround(focalPoint, Vector3.left, yRot);



            if(transform.position.y < .5f)
            {
                transform.position = new Vector3(transform.position.x, .7f, origZ);
            }

            transform.LookAt(focalPoint);
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                if (transform.position.y > .7)
                {
                    transform.position -= new Vector3(0, zoomSpeed * Time.deltaTime, 0);
                    transform.LookAt(focalPoint);
                }
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position += new Vector3(0, zoomSpeed * Time.deltaTime, 0);

                transform.LookAt(focalPoint);
            }
        }

        // Reset to original
        if (Input.GetKeyUp("o"))
        {
            transform.position = originalPosition;
            transform.rotation = originalRotation;
        }
    }
}
