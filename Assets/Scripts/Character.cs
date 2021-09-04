using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Stat armor;

    public CharacterClass characterClass { get; set; }

    public void Move(Vector3 newPosition) {
        gameObject.transform.position = newPosition;
    }

    public Vector3 GetPosition() {
        return gameObject.transform.position;
    }

}
