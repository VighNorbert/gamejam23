using UnityEngine;

public class UIEffectSpawner : MonoBehaviour
{
    public Transform anchorObject; // The object whose position you want to convert
    public Camera mainCamera; // The camera you want to use for the conversion
    public Canvas canvas; // The Canvas to spawn the particle system on
    public GameObject particlePrefab; // The prefab of the particle system

    void Update()
    {
        if (anchorObject != null && mainCamera != null && canvas != null && particlePrefab != null)
        {
            // Convert the world position to screen position
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(anchorObject.position);

            // Spawn the particle system at the screen position
            SpawnParticleOnCanvas(screenPosition);
        }
        else
        {
            Debug.LogWarning("Please assign the anchor object, camera, canvas, and particle prefab in the inspector.");
        }
    }

    void SpawnParticleOnCanvas(Vector3 screenPosition)
    {
        // Create a new particle system instance from the prefab
        GameObject particleSystemInstance = Instantiate(particlePrefab, screenPosition, Quaternion.identity);

        // Set the parent of the particle system to the Canvas
        particleSystemInstance.transform.SetParent(canvas.transform, false);
    }
}
