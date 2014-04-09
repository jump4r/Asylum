using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public GameObject[] button_prefabs; // 1 TEXT_PREFAB Per Button.

	private int selected = 0; 
	private bool menu_active = false; // Is the menu active or not
	private GameObject[] buttons; // The Actual Buttons

	// my color and no one elses;
	private Color jblue = new Color(.16f, .67f, .73f);
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// Highlight selected
	}

	// Activate/Deactivate Menu;
	public void ActivateMenu () {
		Debug.Log ("Trigger Menu");
		if (!menu_active) {
			buttons = new GameObject[button_prefabs.Length];
			GameObject mc = GameObject.FindGameObjectWithTag ("MainCamera");
			for (int i = 0; i < buttons.Length; i++) {
					buttons [i] = (GameObject)Instantiate (button_prefabs [i], new Vector3 (mc.transform.position.x, mc.transform.position.y + 20 - (i * 20), mc.transform.position.z), Quaternion.identity);
					buttons [i].transform.parent = GameObject.FindGameObjectWithTag ("MainCamera").transform;
					buttons [i].transform.localRotation = Quaternion.Euler (mc.transform.localRotation.x, mc.transform.localRotation.y, mc.transform.localRotation.z);
					buttons [i].GetComponent<TextMesh> ().fontSize = 50;
					buttons [i].GetComponent<TextMesh> ().offsetZ = 70;
			}
			// Set Init select as red so I down waste it in Update
			buttons[0].renderer.material.color = jblue;
		} 

		else if (menu_active) {
			Debug.Log ("Destroy Buttons");
			for (int i = 0; i < button_prefabs.Length; i++) {
				Destroy ((GameObject)buttons[i]);
			}
			selected = 0;
		}

		// turn menu_active on/off;
		menu_active = !menu_active;
	}

	// Change the selected menu buttons (down)
	public void ChangeSelectedDown() {
		if (menu_active) {
			buttons[selected].renderer.material.color = Color.white;
			selected = (selected + 1) % buttons.Length;
			buttons[selected].renderer.material.color = jblue;
		}
	}

	// Change the selected menu button (up)
	public void ChangeSelectedUp() {
		if (menu_active) {
			buttons[selected].renderer.material.color = Color.white;
			selected = selected - 1;
			if (selected < 0)
				selected = buttons.Length - 1;
			buttons[selected].renderer.material.color = jblue;

		}
	}
}
