using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;  
public class PopupAchievement: MonoBehaviour
{
    public CanvasGroup canvasGroup; 
    public float fadeDuration = 1f;
    public float displayDuration = 3f;

    private enum FadeState { None, FadeIn, Display, FadeOut }
    private FadeState currentState = FadeState.None;

    [SerializeField] private float timer = 0f;
    public Image achievementIcon;  
    public TextMeshProUGUI achievementText;   

    private void Start()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
        }
        currentState = FadeState.FadeIn;
    }

    private void Update()
    {
        switch (currentState)
        {
            case FadeState.FadeIn:
                FadeIn();
                break;

            case FadeState.Display:
                Display();
                break;

            case FadeState.FadeOut:
                FadeOut();
                break;
        }
    }

    private void FadeIn()
    {   
        timer += Time.deltaTime;
        canvasGroup.alpha = Mathf.Clamp01(timer / fadeDuration);

        if (canvasGroup.alpha >= 1f)
        {
            currentState = FadeState.Display;
            timer = 0f;
        }
    }

    private void Display()
    {
        timer += Time.deltaTime;
        if (timer >= displayDuration)
        {
            currentState = FadeState.FadeOut;
            timer = 0f;
        }
    }

    private void FadeOut()
    {
        timer += Time.deltaTime;
        canvasGroup.alpha = Mathf.Clamp01(1 - (timer / fadeDuration));

        if (canvasGroup.alpha <= 0f)
        {
            currentState = FadeState.None;
            gameObject.SetActive(false);
        }
    }
    
    
    public void Init(string text)
    {
        if (achievementText != null)
        {
            achievementText.text = text;
        }
    }
    
}
