using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


public enum ClassName {
    [Description("King")]   King, 
    [Description("Queen")]  Queen,
    [Description("Rook")]   Rook,
    [Description("Bishop")] Bishop,
    [Description("Knight")] Knight,
    [Description("Pawn")]   Pawn
}


public class CharacterClass {

    private static Dictionary<ClassName, GameObject> classPrefabs = new Dictionary<ClassName, GameObject>() {
        { ClassName.King,   Resources.Load<GameObject>("Prefabs/Classes/King")   },
        { ClassName.Queen,  Resources.Load<GameObject>("Prefabs/Classes/Queen")  },
        { ClassName.Rook,   Resources.Load<GameObject>("Prefabs/Classes/Rook")   },
        { ClassName.Bishop, Resources.Load<GameObject>("Prefabs/Classes/Bishop") },
        { ClassName.Knight, Resources.Load<GameObject>("Prefabs/Classes/Knight") },
        { ClassName.Pawn,   Resources.Load<GameObject>("Prefabs/Classes/Pawn")   },
    };

    public static GameObject GetClassPrefab(ClassName className) {
        return classPrefabs[className];
    }

    public ClassName ClassName {
        get;
    }

    public CharacterClass(ClassName _className) {
        ClassName = _className;
    }

    public bool CanMove(Coords<int> start, Coords<int> end) {
        switch (ClassName) {
            case ClassName.King:
                bool diagonal = IsDiagonalMove(start, end) && GetDiagonalLength(start, end) == 1;
                bool orthogonal = IsOrthogonalMove(start, end) && GetOrthogonalLength(start, end) == 1;
                return diagonal || orthogonal;
            case ClassName.Queen:
                diagonal = IsDiagonalMove(start, end);
                orthogonal = IsOrthogonalMove(start, end);
                return diagonal || orthogonal;
            case ClassName.Rook:
                orthogonal = IsOrthogonalMove(start, end);
                return orthogonal;
            case ClassName.Bishop:
                diagonal = IsDiagonalMove(start, end);
                return diagonal;
            case ClassName.Knight:
                return IsHorseMove(start, end);
            case ClassName.Pawn:
                diagonal = IsDiagonalMove(start, end) && GetDiagonalLength(start, end) == 1;
                orthogonal = IsOrthogonalMove(start, end) && GetOrthogonalLength(start, end) == 1;
                return diagonal || orthogonal;
            default:
                return false;
        }
    }

    public bool IsDiagonalMove(Coords<int> start, Coords<int> end) {
        Coords<int> offset = new Coords<int>(start.X - end.X, start.Y - end.Y); 
        Coords<int> absOffset = new Coords<int>(Mathf.Abs(offset.X), Mathf.Abs(offset.Y));
        return absOffset.X == absOffset.Y;
    }

    public int GetDiagonalLength(Coords<int> start, Coords<int> end) {
        Coords<int> offset = new Coords<int>(start.X - end.X, start.Y - end.Y); 
        Coords<int> absOffset = new Coords<int>(Mathf.Abs(offset.X), Mathf.Abs(offset.Y));
        return absOffset.X;
    }

    public bool IsOrthogonalMove(Coords<int> start, Coords<int> end) {
        Coords<int> offset = new Coords<int>(start.X - end.X, start.Y - end.Y); 
        Coords<int> absOffset = new Coords<int>(Mathf.Abs(offset.X), Mathf.Abs(offset.Y));
        return absOffset.X == 0 || absOffset.Y == 0;
    }

    public int GetOrthogonalLength(Coords<int> start, Coords<int> end) {
        Coords<int> offset = new Coords<int>(start.X - end.X, start.Y - end.Y); 
        Coords<int> absOffset = new Coords<int>(Mathf.Abs(offset.X), Mathf.Abs(offset.Y));
        return Mathf.Max(absOffset.X, absOffset.Y);
    }

    public bool IsHorseMove(Coords<int> start, Coords<int> end) {
        Coords<int> offset = new Coords<int>(start.X - end.X, start.Y - end.Y); 
        Coords<int> absOffset = new Coords<int>(Mathf.Abs(offset.X), Mathf.Abs(offset.Y));
        bool moveType1 = absOffset.X == 1 && absOffset.Y == 2;
        bool moveType2 = absOffset.X == 2 && absOffset.Y == 1;
        return moveType1 || moveType2;
    }
}
