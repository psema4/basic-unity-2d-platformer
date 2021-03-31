using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarManager : MonoBehaviour
{
	private LevelEditorManager editor;
	
	public int currentTab = 0;
	public GameObject[] tabs;
	
	private bool keyPressed = false;
	
	void Start() {
		editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
	}
	
	void Update() {
		if (!keyPressed) {
			if (Input.GetKeyDown(KeyCode.BackQuote)) {
				keyPressed = true;
				editor.editorToolbar.SetActive(!editor.editorToolbar.activeInHierarchy);
				editor.UpdateLevel(editor.levelManager.currentLevel);
			}
			
		} else {
			if (Input.GetKeyUp(KeyCode.BackQuote)) {
				keyPressed = false;
			}
		}
	}
	
	public void NextTab() {
		tabs[currentTab].SetActive(false);
		
		if (currentTab < tabs.Length-1) {			
			currentTab++;
		
		} else {
			currentTab = 0;
		}
		
		tabs[currentTab].SetActive(true);
	}
	
	public void PreviousTab() {
		tabs[currentTab].SetActive(false);
		
		if (currentTab > 0) {
			currentTab--;
			
		} else {
			currentTab = tabs.Length-1;
		}
		
		tabs[currentTab].SetActive(true);
	}
	
	public void NextLevel() {
		editor.levelManager.NextLevel();
	}
	
	public void PreviousLevel() {
		editor.levelManager.PreviousLevel();
	}
}
