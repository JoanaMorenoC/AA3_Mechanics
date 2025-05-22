using UnityEngine;

public class FloatOnGerstner : MonoBehaviour
{
    public WavesController waveSystem;
    Wave waveInfo;

    public Vector2 buoyOffset; 
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        waveInfo = waveSystem.GetWaveInfo();
    }

    void Update()
    {
        if (waveSystem == null || waveSystem.GetCurrentMethod() != Method.GERSTNER) return;

        Vector2 worldXZ = new Vector2(
            initialPosition.x + buoyOffset.x,
            initialPosition.z + buoyOffset.y
        );
        Vector2 waveVector = waveInfo.vector;
        float frequency = waveInfo.frequency;
        float amplitude = waveInfo.amplitude;
        float phase = waveInfo.phase;
        float time = waveSystem.GetTime();

        float wave = Vector2.Dot(waveVector, worldXZ) - frequency * time + phase;

        float y = amplitude * Mathf.Cos(wave);
        Vector2 horizontalDisplacement = waveVector.normalized * amplitude * Mathf.Sin(wave);

        transform.position = new Vector3(
            worldXZ.x - horizontalDisplacement.x,
            y,
            worldXZ.y - horizontalDisplacement.y
        );
    }
}
