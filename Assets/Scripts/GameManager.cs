using UnityEngine;


public class GameManager: MonoBehaviour {

    void Start() {
        EventManager.AddListener("TEST_EVENT", TestEvent);
        EventManager.Invoke("TEST_EVENT");
        EventManager.AddListener("TEST_EVENT", TestEvent2);
        EventManager.Invoke("TEST_EVENT");
        EventManager.RemoveListener("TEST_EVENT", TestEvent);
        EventManager.Invoke("TEST_EVENT");
    }

    public void TestEvent() {
        Debug.Log("TestEvent");
    }
    public void TestEvent2() {
        Debug.Log("TestEvent2");
    }
}
