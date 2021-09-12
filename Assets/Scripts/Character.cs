using UnityEngine;
using System;

public class Character: MonoBehaviour, IHasDetails
{
    [SerializeField] private Stat armor;

    public CharacterClass characterClass { get; set; }

    public void Move(Vector3 newPosition) {
        gameObject.transform.position = newPosition;
    }

    public Vector3 GetPosition() {
        return gameObject.transform.position;
    }

    public string GetDetails() {
        return string.Format("A {0} named {1}.", Enum.GetName(typeof(ClassName), characterClass.ClassName), gameObject.name);
    }
}
