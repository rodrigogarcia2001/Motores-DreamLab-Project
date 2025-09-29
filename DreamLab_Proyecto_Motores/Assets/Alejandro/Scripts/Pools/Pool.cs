using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour, IPool {
    [Header("Pool Settings")]
    [Tooltip("object prefab to generate pool")]
    [SerializeField] protected GameObject objectPrefab;
    [Tooltip("Total length of the pool")]
    [SerializeField] protected int poolSize = 10;
    protected List<GameObject> pool = new List<GameObject>();

    void Start() {
        for (int i = 0; i < poolSize; i++) {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }
    public GameObject getObject() {
        foreach (GameObject obj in pool) {
            if (!obj.activeInHierarchy) {
                obj.SetActive(true);
                return obj;
            }
        }

        GameObject newObj = Instantiate(objectPrefab);
        pool.Add(newObj);
        newObj.SetActive(true);
        return newObj;
    }
    public void returnObject(GameObject obj) {
        obj.SetActive(false);
    }
}