using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private Board board;

    private Camera myCamera;
    private Vector3 boardCenter;

    void Awake() {
        myCamera = GetComponent<Camera>();
        boardCenter = board.GetCenter();
    }

    void Start() {
        LookAt(boardCenter);
        board.Focus.RemoveAllListeners();
        board.Focus.AddListener(LookAt);
    }

    void Update() {}

    public void LookAt(Vector3 lookAtPoint) {
        Vector2 lookAtPoint2D = lookAtPoint;
        Vector3 newPosition = new Vector3(lookAtPoint2D.x, lookAtPoint2D.y, transform.position.z);
        transform.position = newPosition;
    }
}
