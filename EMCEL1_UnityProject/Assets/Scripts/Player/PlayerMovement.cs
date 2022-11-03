using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private float moveSpeed = 5f;
    
    private Rigidbody2D rb;
    private Vector2 movement;

    public bool IsMoving => rb.velocity.magnitude > 0.1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.velocity = movement.normalized * (moveSpeed * 10 * Time.fixedDeltaTime);
    }
}
