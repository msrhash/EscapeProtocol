using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class glitchUI : MonoBehaviour
{
    public float glitchInterval = 0.1f;    // How often to glitch
    public float glitchDuration = 0.05f;   // How long each glitch lasts

    [Header("Glitch Amounts")]
    public float positionJitter = 5f;      // How much it moves
    public float rotationJitter = 10f;     // How much it rotates
    public float scaleJitter = 0.1f;       // How much it scales (0.1 = 10%)

    private RectTransform rectTransform;
    private Vector3 originalPos;
    private Quaternion originalRot;
    private Vector3 originalScale;
    private float glitchTimer;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPos = rectTransform.localPosition;
        originalRot = rectTransform.localRotation;
        originalScale = rectTransform.localScale;
        glitchTimer = Random.Range(0f, glitchInterval);
    }

    void Update()
    {
        glitchTimer -= Time.deltaTime;

        if (glitchTimer <= 0f)
        {
            // Start glitch
            StartCoroutine(DoGlitch());
            // Schedule next glitch
            glitchTimer = Random.Range(glitchInterval * 0.5f, glitchInterval * 1.5f);
        }
    }

    private System.Collections.IEnumerator DoGlitch()
    {
        // Apply random jitter
        Vector3 posOffset = new Vector3(
            Random.Range(-positionJitter, positionJitter),
            Random.Range(-positionJitter, positionJitter),
            0f
        );

        float randomRot = Random.Range(-rotationJitter, rotationJitter);
        Vector3 scaleOffset = originalScale * (1 + Random.Range(-scaleJitter, scaleJitter));

        rectTransform.localPosition = originalPos + posOffset;
        rectTransform.localRotation = Quaternion.Euler(0f, 0f, randomRot);
        rectTransform.localScale = scaleOffset;

        yield return new WaitForSeconds(glitchDuration);

        // Reset to original
        rectTransform.localPosition = originalPos;
        rectTransform.localRotation = originalRot;
        rectTransform.localScale = originalScale;
    }
}