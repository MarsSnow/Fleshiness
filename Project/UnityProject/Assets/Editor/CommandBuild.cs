using System;
using UnityEditor;


public class CommandBuild
{
    static string[] s_levels = {
        "Assets/Scenes/Start.unity",
        "Assets/Scenes/Lobby.unity",
        "Assets/Scenes/Loading.unity",
        "Assets/Scenes/Battle.unity",
        "Assets/Scenes/WorlBossResult.unity",
    };

    [MenuItem("CmdBuild/Build Android")]
    public static void BuildAndroid()
    {
        BuildPipeline.BuildPlayer(s_levels, "E:\\TrunkBin\\3dmt\\android\\3dmt.apk", BuildTarget.Android, BuildOptions.None);
    }

    [MenuItem("CmdBuild/Build Windows")]
    public static void BuildWindows()
    {
        BuildPipeline.BuildPlayer(s_levels, "E:\\TrunkBin\\3dmt\\win\\3dmt.exe", BuildTarget.StandaloneWindows, BuildOptions.None);
    }

    [MenuItem("CmdBuild/Build Test Windows")]
    public static void BuildTestWindows()
    {
        BuildPipeline.BuildPlayer(s_levels, "E:\\TestBin\\3dmt\\win\\3dmt.exe", BuildTarget.StandaloneWindows, BuildOptions.None);
    }

    [MenuItem("CmdBuild/Build Test Android")]
    public static void BuildTestAndroid()
    {
        BuildPipeline.BuildPlayer(s_levels, "E:\\TestBin\\3dmt\\android\\3dmt.apk", BuildTarget.Android, BuildOptions.None);
    }


    [MenuItem("CmdBuild/Switch Windows Target")]
    public static void SwithWindowsTarget()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.StandaloneWindows);
    }

    [MenuItem("CmdBuild/Switch Android Target")]
    public static void SwithAndroidTarget()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
    }
}

