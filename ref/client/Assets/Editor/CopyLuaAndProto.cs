using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class CopyLuaAndProto
{
    static List<string> files = new List<string>();


    ///-----------------------------------------------------------
    [MenuItem("Game/Copy Lua and Proto Files to Resources", false, 1)]
    public static void CopyLuaFilesToResourceMenu()
    {
        CopyLuaFilesToResource(true);
    }

    public static void CopyLuaFilesToResource(bool isTip)
    {
        string resLuaDir = Application.dataPath + "/Resources/Lua";
        if (Directory.Exists(resLuaDir))
            Directory.Delete(resLuaDir, true);
        string srcLuaDir = Application.dataPath + "/uLua/Lua";
        files.Clear();
        Recursive(srcLuaDir);
        foreach (string f in files)
        {
            if (!f.EndsWith(".lua")) continue;
            string newfile = f.Replace(srcLuaDir, "");
            string newpath = resLuaDir + newfile + ".txt";
            string path = Path.GetDirectoryName(newpath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.Copy(f, newpath, true);
        }

        string resProtoDir = Application.dataPath + "/Resources/Proto";
        if (Directory.Exists(resProtoDir))
            Directory.Delete(resProtoDir, true);
        string srcProtoDir = Application.dataPath + "/Proto";
        files.Clear();
        Recursive(srcProtoDir);
        foreach (string f in files)
        {
            if (!f.EndsWith(".proto")) continue;
            string newfile = f.Replace(srcProtoDir, "");
            string newpath = resProtoDir + newfile + ".txt";
            string path = Path.GetDirectoryName(newpath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.Copy(f, newpath, true);
        }

        AssetDatabase.Refresh();
        if (isTip)
        {
            EditorUtility.DisplayDialog("Packager", "Succeed!", "ok");
        }
    }

    /// <summary>
    /// 遍历目录及其子目录
    /// </summary>
    static void Recursive(string path)
    {
        string[] names = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);
        foreach (string filename in names)
        {
            string ext = Path.GetExtension(filename);
            if (ext.Equals(".meta"))
                continue;
            files.Add(filename.Replace('\\', '/'));
        }
        foreach (string dir in dirs)
        {
            Recursive(dir);
        }
    }
}
