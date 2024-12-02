using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;
    public float lowPitchRange = 0.9f;
    public float highPitchRange = 1.1f;
    
}

