using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

public static class PackageExporter 
{
    [MenuItem("Tools/Package Export")]
    private static void ExportProject()
    {
        AssetDatabase.ExportPackage(new string[2] { "Assets/_Game", "Assets/Plugins/facepunch" }, "Emurinoo Plugin.unitypackage", ExportPackageOptions.IncludeLibraryAssets | ExportPackageOptions.Default | ExportPackageOptions.Recurse | ExportPackageOptions.Interactive);
    }

    [MenuItem("Tools/Full Package Export")]
    private static void FullExportProject()
    {
        string[] paths = new string[8]
        {
             "Assets/_Game",
             "Assets/Plugins/facepunch",
             "Assets/Plugins/PolygonCity",
             "Assets/Plugins/PolygonFarm",
             "Assets/Plugins/PolygonNature",
             "Assets/Plugins/PolygonParticles",
             "Assets/Plugins/PolygonPirates",
             "Assets/Plugins/PolygonSciFiCity",
        };

        AssetDatabase.ExportPackage(paths, "Emurinoo Plugin + Extras.unitypackage", ExportPackageOptions.IncludeLibraryAssets | ExportPackageOptions.Default | ExportPackageOptions.Recurse | ExportPackageOptions.Interactive);
    }
}
#endif
