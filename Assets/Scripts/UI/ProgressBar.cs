using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float max;      // Total duration to fill (seconds)
    public float current;  // Current elapsed time
    public Image mask;     // The UI Image with fillAmount

    private bool isRunning = false;

    void Start()
    {
        mask.fillAmount = 0f;
    }

    void Update()
    {
        if (isRunning)
        {
            current += Time.deltaTime;

            float fillAmount = Mathf.Clamp01(current / max);
            mask.fillAmount = fillAmount;

            if (current >= max)
            {
                isRunning = false;
                mask.fillAmount = 0f;
            }
        }
    }

    /// <summary>
    /// Starts the progress bar filling over the specified duration.
    /// </summary>
    public void Run(float duration)
    {
        current = 0f;
        max = duration;
        isRunning = true;
    }
}
