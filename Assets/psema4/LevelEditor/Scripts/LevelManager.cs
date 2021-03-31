using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelManager : MonoBehaviour
{
	private LevelEditorManager editor;
	private GameManager gm;

	public int maxLevels = 3;
	public int currentLevel = 0;
	public GameObject spritesContainer;
	public GameObject[] prefabs;
	
	public SpriteDataList spritesList = new SpriteDataList();
	
    void Start(){
		gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
		
		SpriteData playerSprite = new SpriteData();
		playerSprite.x = 0;
		playerSprite.y = 0;
		playerSprite.prefabIndex = 0;
		
		spritesList.SpriteData.Add(playerSprite);
		
		ResetLevel();
	}
	
    // void Update() {}
	
	public void LoadLevelData(int levelId) {
		if (levelId >= maxLevels) {
			Debug.Log("Stub: END GAME");
			
		} else {
			// Debug.Log("Loading Level " + currentLevel.ToString());
			ClearLevelData();
			ReadLevelData(currentLevel);
			GenerateLevel();
		}
	}
	
	public void PreviousLevel() {
		currentLevel -= 1;
		
		if (currentLevel < 0) {
			currentLevel = 0;
		}
		
		LoadLevelData(currentLevel);
	}
	
	public void NextLevel() {
		if (currentLevel < maxLevels) {
			currentLevel += 1;
			LoadLevelData(currentLevel);
			
		} else {
			Debug.Log("Stub: END GAME");
		}
	}
	
	// Triggered by game objects with the ItemNextLevel component
	public void NextLevelAsync() {
		StartCoroutine(DelayedNextLevel());
	}
	
	IEnumerator DelayedNextLevel() {
		yield return new WaitForSeconds(1);
		NextLevel();
	}
	
	public void ResetLevel() {
		// Derived from https://stackoverflow.com/a/46359133
		int i = 0;
		GameObject[] allChildren = new GameObject[spritesContainer.transform.childCount];

		foreach (Transform child in spritesContainer.transform) {
			allChildren[i] = child.gameObject;
			i += 1;
		}

		foreach (GameObject child in allChildren) {
			DestroyImmediate(child.gameObject);
		}
		// --
		
		LoadLevelData(currentLevel);
	}
	
	public void ClearLevelData() {
		spritesList.SpriteData.Clear();
		
		// remove sprites (FIXME: not dry)
		int i = 0;
		GameObject[] allChildren = new GameObject[spritesContainer.transform.childCount];

		foreach (Transform child in spritesContainer.transform) {
			allChildren[i] = child.gameObject;
			i += 1;
		}

		foreach (GameObject child in allChildren) {
			// DestroyImmediate(child.gameObject);
			Destroy(child.gameObject);
		}
		// --
	}
	
	private void WriteCurrentLevelData() {
		WriteLevelData(currentLevel);
	}
	
	public void WriteEmptyLevelData(int levelId) {
		if (levelId >= maxLevels) {
			return;
		}
		
		string message = "Created empty level " + levelId.ToString() + " data file";
		string path = Application.dataPath + "/level_" + levelId.ToString() + ".txt";
		File.WriteAllText(path, JsonUtility.ToJson(spritesList));
		Debug.Log(message);
	}
	
	public void WriteLevelData(int levelId) {
		if (levelId >= maxLevels) {
			return;
		}
		
		string message = "";
		string path = Application.dataPath + "/level_" + levelId.ToString() + ".txt";
		
		// WARNING:
		// repopulates spritesList from the spritesContainer!
		// FIXME: not dry, again
		
		int i = 0;
		GameObject[] allChildren = new GameObject[spritesContainer.transform.childCount];

		foreach (Transform child in spritesContainer.transform) {
			allChildren[i] = child.gameObject;
			i += 1;
		}

		foreach (GameObject child in allChildren) {
			SpriteData spr = new SpriteData();
			spr.x = child.transform.position.x;
			spr.y = child.transform.position.y;
			spr.prefabIndex = child.GetComponent<ItemRemove>().ID;
			
			spritesList.SpriteData.Add(spr);
		}
		// --
		
		if (!File.Exists(path)) {
			message = "Level " + levelId.ToString() + " data file does not exist, creating!";
		}
		
		File.WriteAllText(path, JsonUtility.ToJson(spritesList));
		
		if (message == "") {
			message = "Level " + levelId.ToString() + " data file updated!";
		}
		
		Debug.Log(message);
	}
	
	public void ReadLevelData(int levelId) {
		if (levelId >= maxLevels) {
			return;
		}
		
		string path = Application.dataPath + "/level_" + levelId.ToString() + ".txt";
		
		if (!File.Exists(path)) {
			WriteEmptyLevelData(levelId);
		}

		spritesList = JsonUtility.FromJson<SpriteDataList>(File.ReadAllText(path));		
	}
	
	private void GenerateLevel() {
		foreach (SpriteData sprite in spritesList.SpriteData) {
			GameObject sprGo = Instantiate(prefabs[sprite.prefabIndex], new Vector3(sprite.x, sprite.y, 0f), Quaternion.identity);
			sprGo.transform.parent = spritesContainer.transform;
		}
		
		editor.UpdateLevel(currentLevel);
	}
}
