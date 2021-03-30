using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float movementSpeed;
	public Rigidbody2D rb;
	
	public float jumpForce = 20f;
	public Transform feet;
	public LayerMask groundLayers;
	
	private float mx;
	
	private void Update() {
		mx = Input.GetAxisRaw("Horizontal");
		
		if (Input.GetButtonDown("Jump") && IsGrounded()) {
			Jump();
		}
	}
	
	private void FixedUpdate() {
		Vector2 movement = new Vector2(mx * movementSpeed, rb.velocity.y);
		rb.velocity = movement;
	}
	
	private void Jump() {
		Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
		
		rb.velocity = movement;
	}
	
	public bool IsGrounded() {
		Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.005f, groundLayers);
		int numContacts = 0;
		
		if (groundCheck != null) {
			ContactPoint2D[] contacts = new ContactPoint2D[8];
			
			numContacts = groundCheck.GetContacts(contacts);
			
			if (numContacts > 0) {
				// Debug.Log("groundCheck: numContacts: " + numContacts.ToString());
				foreach (ContactPoint2D contact in contacts) {
					if (contact.point.x != 0) {
						Debug.Log("groundCheck: rogue contact: " + contact.point.x + ", " + contact.point.y);
					}
				}
			}
			
			return true;
		}
		
		return false;
	}
}
