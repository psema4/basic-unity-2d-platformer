using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFollowMouse : MonoBehaviour
{
	private LevelEditorManager editor;
	private int snapSize;
	
	void Start() {
		editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
		snapSize = editor.snapSize;
	}
	
    void Update()
    {
		// Snap to grid derived from https://gamedev.stackexchange.com/a/33146
		int snapX = (int) Mathf.Round(Input.mousePosition.x / snapSize) * snapSize;
		int snapY = (int) Mathf.Round(Input.mousePosition.y / snapSize) * snapSize;

		Vector2 screenPosition = new Vector2(snapX, snapY);
		Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

		transform.position = worldPosition;
    }
}
