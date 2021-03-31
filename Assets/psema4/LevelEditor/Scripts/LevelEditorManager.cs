using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelEditorManager : MonoBehaviour
{
	[HideInInspector]
	public LevelManager levelManager;
	
	public GameObject editorToolbar;
	public TextMeshProUGUI levelText;
	public TextMeshProUGUI levelMaxText;
	public GameObject spritesContainer;
	
	public ItemController[] ItemButtons;
	public GameObject[] ItemPrefabs;
	public GameObject[] ItemImages;
	public int CurrentButtonPressed;
	
	public int snapSize = 10;
	
	void Start() {
		levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
	}
	
	void Update() {
		// snap to grid derived from https://gamedev.stackexchange.com/a/33146
		// ItemFollowMouse must use the the same grid (snapSize)
		int snapX = (int) Mathf.Round(Input.mousePosition.x / snapSize) * snapSize;
		int snapY = (int) Mathf.Round(Input.mousePosition.y / snapSize) * snapSize;

		Vector2 screenPosition = new Vector2(snapX, snapY);
		Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
		
		if (Input.GetMouseButtonDown(0) && ItemButtons[CurrentButtonPressed].Clicked) {
			ItemButtons[CurrentButtonPressed].Clicked = false;
			GameObject item = Instantiate(ItemPrefabs[CurrentButtonPressed], new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity);
			item.transform.parent = spritesContainer.transform;
			Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
		}
	}
	
	public void UpdateLevel(int level) {
		int levelPretty = level + 1;
		if (levelText != null) {
			levelText.text = levelPretty.ToString();
		}
		
		if (levelMaxText != null && levelManager != null) {
			levelMaxText.text = levelManager.maxLevels.ToString();
		}
	}
}
