using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    public float DashDistance;
    public FloatingJoystick Joystick;

    private Rigidbody rb;
    private Vector3 moveVector;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Update()
    {

        moveVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
        moveVector = Joystick.Direction;
        rb.AddForce(moveVector * (MoveSpeed*200) * Time.deltaTime);
        //rb.MovePosition(transform.position + moveVector * MoveSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Dash(moveVector);
        }
        
    }
    public void Dash(Vector3 dir)
    {
        rb.AddForce(dir.normalized * (DashDistance * 1000) * Time.deltaTime);
    }
}
