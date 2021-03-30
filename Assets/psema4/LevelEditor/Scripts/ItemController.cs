using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemController : MonoBehaviour
{
	public int ID;
	public int quantity;
	public TextMeshProUGUI quantityText;
	public bool Clicked = false;
	private LevelEditorManager editor;
	
    void Start()
    {
		quantityText.text = quantity.ToString();
		editor = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
    }
	
	public void ButtonClicked() {
		if (quantity > 0) {
			Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
		
			Clicked = true;
			Instantiate(editor.ItemImages[ID], new Vector3(worldPosition.x, worldPosition.y, 0f), Quaternion.identity);
			
			quantity--;
			quantityText.text = quantity.ToString();
			editor.CurrentButtonPressed = ID;
		}
	}
}
