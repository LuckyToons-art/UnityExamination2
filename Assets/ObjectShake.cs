using UnityEngine;

public class ContinuousShake : MonoBehaviour
{
    [Header("Shake Settings")]
    public float magnitude = 0.1f;   // How far it shakes
    public float frequency = 1f;     // How fast it shakes (lower = slower)

    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        // Use sine waves for slower, smooth oscillation
        float x = Mathf.Sin(Time.time * frequency * 2f * Mathf.PI);
        float y = Mathf.Cos(Time.time * frequency * 2f * Mathf.PI);

        transform.localPosition = originalPos + new Vector3(x, y, 0f) * magnitude;
    }
}
