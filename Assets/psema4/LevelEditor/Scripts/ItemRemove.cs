using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRemove : MonoBehaviour
{
	public int ID;
	private LevelEditorManager editor;
	
    void Start() {
        editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
    }
	
	private void OnMouseOver() {
		if (Input.GetMouseButtonDown(1)) {
			Destroy(this.gameObject);
			
			if (ID == 0) {
				if (editor.ItemButtons[ID].quantity < 1) {
					editor.ItemButtons[ID].quantity++;
					editor.ItemButtons[ID].quantityText.text = editor.ItemButtons[ID].quantity.ToString();
				}
				
			} else if (editor.ItemButtons[ID].quantity < 99) {
				editor.ItemButtons[ID].quantity++;
				editor.ItemButtons[ID].quantityText.text = editor.ItemButtons[ID].quantity.ToString();
			}
		}
	}
}
