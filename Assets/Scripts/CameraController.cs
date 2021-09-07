using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private GameObject lookAtGameObject;

    private Camera myCamera;

    void Awake() {
        myCamera = GetComponent<Camera>();
    }

    void Start() {
        // LookAtGameObject();
    }

    void Update() {
        
    }

    public void LookAt(Vector3 lookAtPoint) {
        Vector2 lookAtPoint2D = lookAtPoint;
        Vector3 newPosition = new Vector3(lookAtPoint2D.x, lookAtPoint2D.y, transform.position.z);
        transform.position = newPosition;
    }
}
