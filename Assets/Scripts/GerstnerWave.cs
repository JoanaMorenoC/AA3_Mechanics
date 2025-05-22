using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GerstnerWave : MonoBehaviour
{
    public Vector3 CalculatePoint(Vector3 initialPoint, Wave waveInfo, float time)
    {
        Vector2 worldXZ = new Vector2(initialPoint.x, initialPoint.z); // original position
        float wave = Vector2.Dot(waveInfo.vector, worldXZ) - waveInfo.frequency * time + waveInfo.phase;

        // Calculate new position
        float displacementY = waveInfo.amplitude * Mathf.Cos(wave);
        Vector2 displacementXZ = waveInfo.vector.normalized * waveInfo.amplitude * Mathf.Sin(wave);

        Vector3 newPointPosition = new Vector3(
            initialPoint.x - displacementXZ.x,
            displacementY,
            initialPoint.z - displacementXZ.y
        );

        return newPointPosition;
    }
}
