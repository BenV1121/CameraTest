using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public new GameObject camera;

    public float Force = .05f;
    public float JumpForce;

    //public float dash;
    private bool isSprint = false;

    public float xForce;
    public float yForce;
    public float jump;

    public bool canMove = true;
    public bool canDash;
    public bool groundCheck = true;

    private Rigidbody playerRigidbody;

    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!canMove)
        {
            return;
        }
        canDash = true;

        transform.rotation = Quaternion.Euler(0, camera.GetComponent<MouseLook>().currentYRotation, 0);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprint = true;
        }

        xForce = Input.GetAxis("Horizontal") * Force;
        yForce = Input.GetAxis("Vertical") * Force;

        transform.Translate(new Vector3(0, jump, 0));


        if (isSprint != true)
        {
            transform.Translate(new Vector3(xForce, 0, yForce));
        }

        else
        {
            transform.Translate(new Vector3(xForce * 2f, 0, yForce * 2f));
        }
        isSprint = false;

        if (Input.GetKeyDown("space") && groundCheck)
        {
            playerRigidbody.AddForce(Vector3.up * 300);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        groundCheck = true;
    }

    void OnCollisionStay(Collision other)
    {
        groundCheck = true;
    }

    void OnCollisionExit(Collision other)
    {
        groundCheck = false;
    }
}
