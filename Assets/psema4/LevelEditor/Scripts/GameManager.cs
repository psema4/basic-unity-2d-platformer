using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	
	void Awake() {
		if (instance != null && instance != this) {
              Destroy(this.gameObject);
			  
		} else {
		  instance = this;
		  DontDestroyOnLoad(this.gameObject);
        }
	}
	
	public void QuitGame() {
		if (Application.isEditor) {
			Debug.Log("Please use the Unity Editor to quit");
			
		} else {
			Application.Quit();
		}
	}

	public void LoadScene(string name) {
		StartCoroutine(LoadSceneAsync(name));
	}

	IEnumerator LoadSceneAsync(string name) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

        while (!asyncLoad.isDone) {
            yield return null;
        }
    }
}
