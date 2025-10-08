using UnityEngine;

public class DoorController : MonoBehaviour {
    [Header("Door controller")]
    [Tooltip("Id of the key that opens this door")]
    //[SerializeField] private int id;
    //[SerializeField] private string keyName;
    [SerializeField] private KeyController keyPrefab;

    private void Awake() {
        if (keyPrefab == null) Debug.Log("DoorController: Key Not found");
    }
    public int getKeyId() { return keyPrefab.getId(); }
    public string getKeyName() { return keyPrefab.getKeyName(); }

    public void open() { gameObject.SetActive(false); }
}
