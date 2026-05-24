using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class flickerUI : MonoBehaviour
{
    public float minInterval = 0.05f;  // Minimum time between flickers
    public float maxInterval = 0.3f;   // Maximum time between flickers

    private Graphic uiImage;       // For UI (Image/Text/RawImage)
    private SpriteRenderer sprite; // For world-space sprite

    private float timer;
    private float nextFlickerTime;
    private bool isVisible = true;

    void Start()
    {
        uiImage = GetComponent<Graphic>();
        sprite = GetComponent<SpriteRenderer>();

        ScheduleNextFlicker();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextFlickerTime)
        {
            isVisible = !isVisible;

            if (uiImage != null)
                uiImage.color = new Color(uiImage.color.r, uiImage.color.g, uiImage.color.b, isVisible ? 1f : 0f);

            if (sprite != null)
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, isVisible ? 1f : 0f);

            ScheduleNextFlicker();
        }
    }

    void ScheduleNextFlicker()
    {
        timer = 0f;
        nextFlickerTime = Random.Range(minInterval, maxInterval);
    }
}