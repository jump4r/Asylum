using UnityEngine;
using System.Collections;

public class CutsceneScripts : MonoBehaviour {

	GameController GC;
	GameObject player;
	Chaos playerChaos;
	public bool cs_active = false;
	bool testing = true;
	float testTimer = 4f;
	float resetTimer = 4f;
	
	void Awake ()
	{
		//GC = GameObject.Find("GameVariables").GetComponent<GameController>();
		player = GameObject.Find ("First Person Controller");
		playerChaos = player.GetComponent<Chaos>();
	}

	public void StartCutscene(int cutscene_id)
	{
		/* load some specific dialog with cutscene_id, or trigger w/e */

		/* stop player movement */
		cs_active = true;
		playerChaos.chaosActive = true;
		player.GetComponent<CharacterMotor>().canControl = false;
	}

	/* exit cutscene mode */
	public void EndCutscene()
	{
		cs_active = false;
		playerChaos.chaosActive = false;
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

			/* cutscene lasts arbitrary 3 seconds */
			if(testTimer > 0f)
				testTimer -= Time.deltaTime;
			else
			{
				EndCutscene();
				testTimer = resetTimer;
			}
		}

	}
}
