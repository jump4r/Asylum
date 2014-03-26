using UnityEngine;
using System.Collections;

public class CutsceneScripts : MonoBehaviour {

	GameController GC;
	GameObject player;
	bool cs_active = false;
	bool testing = true;
	
	void Awake ()
	{
		//GC = GameObject.Find("GameVariables").GetComponent<GameController>();
		player = GameObject.Find ("First Person Controller");
	}

	void startCutscene(int cutscene_id)
	{
		/* load some specific dialog with cutscene_id, or trigger w/e */

		/* stop player movement */
		cs_active = true;
		player.GetComponent<CharacterMotor>().canControl = false;
	}

	/* exit cutscene mode */
	void endCutscene()
	{
		cs_active = false;
		player.GetComponent<CharacterMotor>().canControl = true;
	}


	void Update ()
	{
		if(!cs_active)
			return;

		/* if dialog is over, call endCutscene() */

		/* test chaos stuff in cutscene */
		if(testing)
		{

		}

	}
}
