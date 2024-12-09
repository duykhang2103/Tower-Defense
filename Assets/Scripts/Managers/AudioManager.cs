using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

  [Header("Audio Sources")]
  [SerializeField] public AudioSource bgmAudioSource;

  [Header("Audio Clips")]
  [SerializeField] public AudioClip bgmClip;

  public static AudioManager instance { get;  set; }

  private void Update() {

    if (SceneManager.GetActiveScene().name == "HomeScene" || SceneManager.GetActiveScene().name == "MapScene") {
    } else {
      bgmAudioSource.volume = 0;
    }
  }

  private void Awake() {
    // if (instance == null || instance != this) {
    //   instance = this;
    //   DontDestroyOnLoad(gameObject);
    // } else {
    //   Destroy(gameObject);
    //   return;
    // }

    if(instance != null && instance != this) {
      Destroy(gameObject);
    } else {
      instance = this;
      DontDestroyOnLoad(gameObject);
    }
  }

  public float GetVolume() {
    return instance.bgmAudioSource.volume;
  }

  private void Start() {
    instance.bgmAudioSource.clip = bgmClip;
    instance.bgmAudioSource.loop = true;
    instance.bgmAudioSource.Play();
  }

  public void SetVolume(float value) {
    Debug.Log("Setting volume to " + value);
    // AudioSource audioSource = GetComponent<AudioSource>();
    // audioSource.volume = value;
    // Debug.Log("Volume set to " + audioSource.volume);
    instance.bgmAudioSource.volume = value;
    // instance.bgmAudioSource.pitch = 0.1f;
  }
}