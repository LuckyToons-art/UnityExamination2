using UnityEngine;

public class StartPrompt : MonoBehaviour
{
    void Update()
    {
        // Hide text when any key is pressed
        if (Input.anyKeyDown)
        {
            gameObject.SetActive(false);
        }
    }
}
