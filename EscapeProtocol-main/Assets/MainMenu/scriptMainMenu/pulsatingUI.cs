using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class pulsatingUI : MonoBehaviour
{
    public float pulseSpeed = 1f;      // Speed of the pulse
    public float pulseAmount = 0.1f;   // How much it scales (0.1 = 10%)

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = originalScale * scale;
    }
}
