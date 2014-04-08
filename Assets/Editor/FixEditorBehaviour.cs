using UnityEngine;



using UnityEditor;







public class FixEditorBehaviour : MonoBehaviour {

	[MenuItem("GameObject/Create Empty Child #&c")]
	static void createEmptyChild() {
		GameObject go = new GameObject("GameObject");
		if(Selection.activeTransform != null)
		{
			go.transform.parent = Selection.activeTransform;
			go.transform.Translate(Selection.activeTransform.position);	
		}
	}
}