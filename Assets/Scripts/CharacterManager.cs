using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour {

	private Dictionary<string, bool> isCharActive = new Dictionary<string, bool>();
	// Use this for initialization
	void Start () {
		isCharActive.Add ("Alistar", false);
		isCharActive.Add ("George", false);
		isCharActive.Add ("Matthew", false);
		isCharActive.Add ("Marissa", false);
		isCharActive.Add ("Renata", false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ActivateCharacter(string key) {
		isCharActive [key] = true;
		Debug.Log(key + " has been activated");
	}

	public bool IsActive(string key) {
		return isCharActive [key];
	}
}
