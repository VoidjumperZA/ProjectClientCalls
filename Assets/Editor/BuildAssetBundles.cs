using UnityEngine;
using System.Collections;
using UnityEditor;

public class BuildAssetBundles {


    [MenuItem("Assets/Build Asset Bundles")]
    public static void ExecBuildAssetBundles()
    {
        BuildPipeline.BuildAssetBundles(Application.dataPath+"/Bundles", BuildAssetBundleOptions.None,BuildTarget.StandaloneWindows);
    }
}
