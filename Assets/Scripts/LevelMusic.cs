using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    public AudioClip music;

    void Start()
    {
        AudioManager.Instance.PlayMusic(music);
    }
}