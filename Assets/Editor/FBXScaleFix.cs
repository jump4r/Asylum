using UnityEditor;

public class FBXScaleFix : AssetPostprocessor {

	// Use this for initialization
	public void OnPreprocessModel()
	{
		ModelImporter modelImporter = (ModelImporter)assetImporter;
		modelImporter.globalScale = 1;
	}
}
