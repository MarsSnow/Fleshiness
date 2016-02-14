using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

class LtPlatform : LtSingleton<LtPlatform>
{
    public string WriteablePath
    {
        get
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return Application.persistentDataPath;
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            {
                //string dir = Path.GetDirectoryName(Application.dataPath + "/../WriteblePath/");
                string dir = Path.GetDirectoryName(Application.dataPath + "/../Assets/Scripts/Archive");
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                return dir;
            }
            else
            {
                return Application.dataPath;
            }
        }
    }

    public string StreamingPath
    {
        get
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                return "jar:file://" + Application.dataPath + "!/assets/";
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return "file://" + Application.dataPath + "/Raw/";
            }
            else
            {
                return "file://" + Application.dataPath + "/StreamingAssets/";
            }
        }
    }

    //private QuickPlatform _platform;

    public LtPlatform()
    {
    }

}
