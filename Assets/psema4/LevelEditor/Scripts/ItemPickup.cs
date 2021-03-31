using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	private GameManager gm;
	
	private void Start() {
		gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		// play the second sound effect
		AudioManager am = gm.GetComponent<AudioManager>();
		int firstSoundEffect = am.effectsStartAtId;
		gm.PlaySound(firstSoundEffect+1);
		
        Destroy(this.gameObject);
    }
}