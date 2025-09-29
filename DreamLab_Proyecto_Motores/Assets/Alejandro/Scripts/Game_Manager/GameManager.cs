using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private static GameManager instance;

    static private int score = 0;
    static private int lives = 3;
     
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        Destroy(gameObject);

    }

    
    public static GameManager getInstance() { return instance; }

    public static void scorePoints(int scorePoints) {
        score += scorePoints;
    }

    public static void updateWaveUI() {
       
    }

    public static void startScoreUI() {  }
    
    public static void levelCompleted() {
        Debug.Log("GameManager: Implement Level Complete.");
        SceneManager.LoadScene("Level_Completed");
    }
    public static void gameOver() {
        Debug.Log("GameManager: Implement Game Over.");
        //SceneManager.LoadScene("Level_Game_Over");
    }

    public static void restartGame() {
        lives = 3;
        score = 0;
    }

    public void decrementLives() { lives--; }
}
