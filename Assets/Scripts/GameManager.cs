using UnityEngine;
using UnityEngine.Assertions;


public enum GameState {
    PlayerTurn,
    EnemyTurn
}


public class GameManager: MonoBehaviour {

    [SerializeField] private Board _board;
    public Board Board {
        get { 
            Assert.IsNotNull(_board);
            return _board; 
        }
    }

    public void Initialize() {}

    // Static members

    private static GameManager _instance;
    public static GameManager Instance { 
        get {
            if (!_instance) FindInstance();
            return _instance;
        }
    }

    private static void FindInstance() {
        GameManager[] gameManagers = FindObjectsOfType<GameManager>(true);
        Assert.IsTrue(gameManagers.Length == 1);
        _instance = gameManagers[0];
        _instance.Initialize();
    }

}
