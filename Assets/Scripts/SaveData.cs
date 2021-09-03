using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CharacterData {
    public Coords<int> position;
    public ClassName className;
}

[System.Serializable]
public class SaveData {
    // You can also store primitives like this:
    // public int m_Score;

    public List<CharacterData> characters = new List<CharacterData>();
    
    public string ToJson() {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string a_Json) {
        JsonUtility.FromJsonOverwrite(a_Json, this);
    }
}

public interface ISaveable {
    void PopulateSaveData(SaveData a_SaveData);
    void LoadFromSaveData(SaveData a_SaveData);
}