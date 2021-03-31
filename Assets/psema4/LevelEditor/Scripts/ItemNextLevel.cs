using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNextLevel : MonoBehaviour
{
	private LevelEditorManager editor;
	private GameManager gm;
	
	void Start() {
		editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
		gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		// play the third sound effect
		AudioManager am = gm.GetComponent<AudioManager>();
		int firstSoundEffect = am.effectsStartAtId;
		gm.PlaySound(firstSoundEffect+2);

		Debug.Log(col.gameObject.name + " triggered ItemNextLevel");
		// change to the next level after a brief delay
		editor.levelManager.NextLevelAsync();

		// destroy the player object:
		if (col.gameObject.name.IndexOf("Player") == 0) {
			Destroy(col.gameObject);
		}
		
		// finally destroy our game object immediately
		Destroy(this.gameObject);		
    }
}
