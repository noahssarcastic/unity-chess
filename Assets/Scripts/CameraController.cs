using UnityEngine;


public class CameraController : MonoBehaviour {

    [SerializeField] private Board board;
    [SerializeField] private Vector3 offset;
    private Camera myCamera;
    private Vector3 boardCenter;

    void Awake() {
        myCamera = GetComponent<Camera>();
        boardCenter = board.GetCenter();
    }

    void Start() {
        LookAt(boardCenter + offset);
    }

    void Update() {}

    public void LookAt(Vector3 lookAtPoint) {
        Vector2 lookAtPoint2D = lookAtPoint;
        Vector3 newPosition = new Vector3(lookAtPoint2D.x, lookAtPoint2D.y, transform.position.z);
        transform.position = newPosition;
    }
}
