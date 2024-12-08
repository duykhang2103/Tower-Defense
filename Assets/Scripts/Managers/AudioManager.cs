using UnityEngine;

public class AudioManager : MonoBehaviour {
  [Header("Audio Sources")]
  [SerializeField] AudioSource bgmAudioSource;

  [Header("Audio Clips")]
  [SerializeField] AudioClip bgmClip;

  AudioManager instance;

  private void Awake() {
    if (instance == null) {
      instance = this;
      DontDestroyOnLoad(gameObject);
    } else {
      Destroy(gameObject);
    }
      return;
  }
  
  private void Start() {
    bgmAudioSource.clip = bgmClip;
    bgmAudioSource.loop = true;
    bgmAudioSource.Play();
  }
}