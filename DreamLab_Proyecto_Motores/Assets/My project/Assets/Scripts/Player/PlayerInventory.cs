using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour {
    [Header("Player Inventory settings")]
    private List<KeyData> keyList = new List<KeyData>();
    private bool pageCollected = false;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Door")) {
            DoorController doorController = collision.gameObject.GetComponent<DoorController>();
            if (doorController == null) {
                Debug.Log("PlayerInventory: doorController not found on door");
                return;
            }

            int keyIndex = findKeyWithId(doorController.getKeyId());
            if (keyIndex > -1) {
                doorController.open();
                keyList.RemoveAt(keyIndex);
                Debug.Log("Key Removed -> id: " + doorController.getKeyId() + ", name: " + doorController.getKeyName() + ", total keys: " + keyList.Count);
            } 
            else 
                showMessage(doorController.getKeyName());
        }

        if (collision.gameObject.CompareTag("Portal")) {
            if (pageCollected) {
                SceneManager.LoadScene("Victory");
            } 
            else {
                Debug.Log("Need page to use portal");
            }
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Key")) {
            KeyController keyController = other.gameObject.GetComponent<KeyController>();
            if (keyController == null) {
                Debug.Log("PlayerInventory: KeyController not found on key");
                return;
            }

            KeyData key = new KeyData(keyController.getId(), keyController.getKeyName());
            keyList.Add(key);
            Debug.Log("Key Added -> id: " + key.getId() + ", name: " + key.getName() + ", total keys: " + keyList.Count);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Page")) {
            pageCollected = true;
            Destroy(other.gameObject);
        }
    }

    private int findKeyWithId(int keyId) {
        if (keyList.Count == 0)                 return -1;

        for (int i = 0; i < keyList.Count; i++)
            if (keyList[i].getId() == keyId)    return i;
        

        return -1;
    }

    public bool hasPage() {
        return pageCollected;
    }
    private void showMessage(string message) {
        Debug.Log("You need key: " + message);
    }
}
