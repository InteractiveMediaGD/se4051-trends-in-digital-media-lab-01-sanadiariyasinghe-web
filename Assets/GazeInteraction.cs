using UnityEngine;

public class GazeInteraction : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color originalColor;

    // Visual feedback color when looking at the object
    public Color gazeColor = Color.yellow; 
    
    // Time required to look at the object to trigger selection
    public float dwellTime = 2f;           
    
    // Slot for the Audio Source component
    public AudioSource selectAudio;        

    private float timer;

    void Start()
    {
        // Get the renderer and save the starting color
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
    }

    void Update()
    {
        // Shoots a ray from the center of the camera (Simulated Eye Tracking)
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        // Check if the ray hits this specific object
        if (Physics.Raycast(ray, out hit) && hit.transform == transform)
        {
            // Visual Cue: Change color to indicate focus
            objectRenderer.material.color = gazeColor; 
            
            // Increment timer based on real time
            timer += Time.deltaTime; 

            // Check if user has looked long enough (Dwell Selection)
            if (timer >= dwellTime)
            {
                // Sensory Feedback: Sound
                if (selectAudio != null) 
                {
                    selectAudio.Play();
                }

                // Sensory Feedback: Physical scale change
                transform.localScale *= 1.3f; 
                
                // Reset timer so it doesn't trigger every frame
                timer = 0; 
            }
        }
        else
        {
            // Reset to original state when user looks away
            objectRenderer.material.color = originalColor; 
            timer = 0; 
        }
    }
}