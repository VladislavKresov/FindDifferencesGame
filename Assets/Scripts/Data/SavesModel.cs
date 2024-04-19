using System;

[Serializable]
public class SavesModel{
    public bool IsFirstLaunch;
    public int Level;

    public SavesModel(bool isFirstLaunch = true, int level = 0) {
        IsFirstLaunch = isFirstLaunch;
        Level = level;
    }
}
