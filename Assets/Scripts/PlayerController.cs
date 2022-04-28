using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 2f;
    public float walkSpeed = 7f;

    public float groundDistance = 0.2f;
    public Vector3 groundOffset;
    public LayerMask groundMask;

    private Rigidbody rb;

    private Vector3 input;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position + groundOffset, groundDistance, groundMask, QueryTriggerInteraction.Ignore);

        input = Vector3.zero;
        input = transform.forward * (Input.GetAxis("Vertical")) + transform.right * (Input.GetAxis("Horizontal"));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * walkSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>())
            rb.velocity = collision.gameObject.GetComponent<Rigidbody>().velocity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.2f, 0.4f, 0.3f, 0.4f);
        Gizmos.DrawSphere(transform.position + groundOffset, groundDistance);
    }
}
