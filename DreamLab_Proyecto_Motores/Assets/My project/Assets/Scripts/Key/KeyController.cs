using UnityEngine;

public class KeyController : MonoBehaviour {
    [Header("Key controller")]
    [Tooltip("Doors are opened using the id of keys")]
    [SerializeField] private int id;
    [SerializeField] private string keyName;

    public int getId() {  return id; }
    public string getKeyName() {  return keyName; }


}
