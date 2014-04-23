using UnityEngine;
using System.Collections;

public class CutsceneTrigger : MonoBehaviour {

	public int cutsceneID;
	public int prereqNum;
	public bool played = false;
	private CutsceneScripts cutsceneScripts;

	void Awake()
	{
		cutsceneScripts = GameObject.FindGameObjectWithTag("GameController").GetComponent<CutsceneScripts>();
	}

	// Update is called once per frame
	void Update ()
	{
		/* disable when dialog is over */

	}

	void OnTriggerEnter(Collider other)
	{
		if(!played && other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			cutsceneScripts.StartCutscene(cutsceneID);
			played = true;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(played && other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			// we can use this for something ? 
		}
	}
}
