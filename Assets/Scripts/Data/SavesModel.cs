using System;

[Serializable]
public class SavesModel{
    public int Level;

    public SavesModel(int level = 0) {
        Level = level;
    }
}
