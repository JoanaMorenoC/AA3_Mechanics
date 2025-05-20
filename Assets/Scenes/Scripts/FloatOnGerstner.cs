using UnityEngine;

public class FloatOnGerstner : MonoBehaviour
{
    public gerstnerWaves waveSystem;  

    public Vector2 buoyOffset; 
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (waveSystem == null) return;

        Vector2 worldXZ = new Vector2(
            initialPosition.x + buoyOffset.x,
            initialPosition.z + buoyOffset.y
        );
        Vector2 waveVector = waveSystem.waveVector;
        float frequency = waveSystem.frequency;
        float amplitude = waveSystem.amplitude;
        float phase = waveSystem.phase;
        float time = waveSystem.time;

        float wave = Vector2.Dot(waveVector, worldXZ) - frequency * time + phase;

        float y = amplitude * Mathf.Cos(wave);
        Vector2 horizontalDisplacement = waveVector.normalized * amplitude * Mathf.Sin(wave);

        transform.position = new Vector3(
            worldXZ.x + horizontalDisplacement.x,
            y,
            worldXZ.y + horizontalDisplacement.y
        );
    }
}
