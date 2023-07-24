using UnityEngine;

/// <summary>
/// Helper class to handle particles effects
/// </summary>
public class SpecialEffectsScript : MonoBehaviour
{
    private static SpecialEffectsScript instance;

    // Prefabs
    public ParticleSystem explosionEffect, explosionEffect2, vortexEffect;
    public GameObject trailPrefab;
    public GameObject canvas;

    void Awake()
    {
        instance = this;
    }

    /*
    void Start()
    {
        // Check prefabs
        if (explosionEffect == null)
            Debug.LogError("Missing Explosion Effect!");
        if (vortexEffect == null)
            Debug.LogError("Missing Vortex Effect!");
        if (trailPrefab == null)
            Debug.LogError("Missing Trail Prefab!");
    }
    */

    /// <summary>
    /// Create an explosion at the given position
    /// </summary>
    /// <param name="position"></param>
    public static ParticleSystem MakeExplosion(Vector3 position)
    {
        if (instance == null)
        {
            Debug.LogError("There is no SpecialEffectsScript in the scene!");
            return null;
        }
        position = new Vector3(position.x, position.y, position.z+1);
        ParticleSystem effect = Instantiate(instance.explosionEffect, instance.canvas.transform) as ParticleSystem;
        effect.transform.position = position;

        ParticleSystem effect2 = Instantiate(instance.explosionEffect2, instance.canvas.transform) as ParticleSystem;
        effect2.transform.position = position;

        // Program destruction at the end of the effect
        Destroy(effect.gameObject, 0.5f);
        Destroy(effect2.gameObject, 0.5f);

        return effect;
    }

    /// <summary>
    /// Create a particle vortex at the given position
    /// </summary>
    /// <param name="position"></param>
    public static ParticleSystem MakeVortex(Vector3 position)
    {
        if (instance == null)
        {
            Debug.LogError("There is no SpecialEffectsScript in the scene!");
            return null;
        }

        ParticleSystem effect = Instantiate(instance.vortexEffect) as ParticleSystem;
        effect.transform.position = position;

        return effect;
    }

    /// <summary>
    /// Create a new trail at the given position
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static GameObject MakeTrail(Vector3 position)
    {
        if (instance == null)
        {
            Debug.LogError("There is no SpecialEffectsScript in the scene!");
            return null;
        }

        GameObject trail = Instantiate(instance.trailPrefab) as GameObject;
        trail.transform.position = position;

        return trail;
    }
}