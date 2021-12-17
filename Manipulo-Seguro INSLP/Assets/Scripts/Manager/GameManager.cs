using UnityEngine;

public class GameManager : MonoBehaviour {

    #region SINGLETON

    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    private void Awake() {
        if (Instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this);
    }

    #endregion SINGLETON

    private float score;

    public float Score {
        get { return score; }
    }

    public void PlayerScored(float targetValue) {
        score += targetValue;
    }
}