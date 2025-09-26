using UnityEngine;
public interface IPool {
    public GameObject getObject();
    public void returnObject(GameObject obj);
}
