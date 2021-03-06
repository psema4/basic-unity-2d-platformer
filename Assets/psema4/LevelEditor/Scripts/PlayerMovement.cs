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
	private GameManager gm;
	
	private void Start() {
		gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
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
		
		// play the first sound effect
		AudioManager am = gm.GetComponent<AudioManager>();
		int firstSoundEffect = am.effectsStartAtId;
		gm.PlaySound(firstSoundEffect);
	}

	public bool IsGrounded() {
		Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayers);
		
		if (groundCheck != null) {			
			return true;
		}
		
		return false;
	}
}
