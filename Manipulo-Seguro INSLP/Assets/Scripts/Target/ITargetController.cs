using UnityEngine;

public class ITargetController : MonoBehaviour, ItargetInterface {
    [SerializeField] private float targetScoreValue;

    public void TargetShot() {
        PlayAnimation();
        PlayAudio();

        GameManager.Instance.PlayerScored(targetScoreValue);
    }

    public void PlayAnimation() {
        //To Do
    }

    public void PlayAudio() {
    }
}