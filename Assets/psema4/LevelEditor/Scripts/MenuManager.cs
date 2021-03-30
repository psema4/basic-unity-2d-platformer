using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using TMPro;

public class MenuManager : MonoBehaviour
{
	private GameManager gm;
	
	public Button[] buttons;
	public string[] sceneNames;
	
    void Start() {
		gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
		
		int i=0;
		for (i=0; i<buttons.Length; i++) {
			// we can't use i directly, make a copy (see http://answers.unity.com/answers/1271908/view.html)
			int sceneIndex = i + 0;
			
			buttons[i].onClick.AddListener(delegate {
				Debug.Log("delegated listener, scene: " + sceneIndex);
				LoadScene(sceneIndex);
			});
		}
	}
	
	void LoadScene(int sceneIndex) {
		if (sceneIndex < 0 || sceneIndex >= sceneNames.Length) {
			Debug.Log("LoadScene: got sceneIndex " + sceneIndex + " which is out of bounds!");
			return;
		}
		
		string sceneName = sceneNames[sceneIndex];
		
		if (sceneName == "QuitScene") {
			gm.QuitGame();
			
		} else {
			gm.LoadScene(sceneName);
		}
    }
}
