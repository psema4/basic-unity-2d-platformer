using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarManager : MonoBehaviour
{
	private LevelEditorManager editor;
	
	public int currentTab = 0;
	public GameObject[] tabs;
	
	void Start() {
		editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
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
