using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNextLevel : MonoBehaviour
{
	private LevelEditorManager editor;
	
	void Start() {
		editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
	}
	
	void OnTriggerEnter2D(Collider2D col) {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
		editor.levelManager.NextLevel();
    }
}
