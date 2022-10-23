using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAndAiming : MonoBehaviour
{
    public float moveSpeed=5f;
    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;
   

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        // we normalized the movement because if its not normalized two keys movements (W + A = 2) will be faster than single key movement (W = 1)
        // https://hernandonj.medium.com/why-to-normalize-movement-in-game-dev-cc6dad62e885
        rb.MovePosition(rb.position + movement.normalized * (moveSpeed * Time.fixedDeltaTime));

        Vector2 lookDir = mousePos - rb.position;

        float angle = Mathf.Atan2(lookDir.y,lookDir.x)* Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
