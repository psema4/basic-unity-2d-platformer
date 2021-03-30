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
		
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
		editor.levelManager.NextLevel();
    }
}
