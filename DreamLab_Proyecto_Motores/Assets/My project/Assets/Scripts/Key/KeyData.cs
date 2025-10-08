using UnityEngine;

public class KeyData {
    private int id;
    private string name;

    public KeyData(int id, string name) { 
        this.id = id;
        this.name = name;
    }

    public int getId() { return id; }
    public string getName() { return name; }

}
