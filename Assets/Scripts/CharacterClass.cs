using System.Collections.Generic;
using UnityEngine;

public enum ClassName {
    King, 
    Queen
}

public class CharacterClass {

    private static Dictionary<ClassName, GameObject> classPrefabs = new Dictionary<ClassName, GameObject>() {
        { ClassName.King, Resources.Load("Prefabs/Classes/King") as GameObject },
        { ClassName.Queen, Resources.Load("Prefabs/Classes/Queen") as GameObject },
    };

    public static GameObject GetClassPrefab(ClassName className) {
        return classPrefabs[className];
    }
}
