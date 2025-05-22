using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SinusoidalWave : MonoBehaviour
{
    public Vector3 CalculatePoint(Vector3 initialPoint, Wave waveInfo, float time)
    {
        float height = GetWaveHeight(initialPoint, waveInfo, time);

        Vector3 newPointPosition = new Vector3(
            initialPoint.x,
            height,
            initialPoint.z
        );

        return newPointPosition;
    }

    public float GetWaveHeight(Vector3 position, Wave waveInfo, float time)
    {
        float height = waveInfo.amplitude * Mathf.Sin((2 * Mathf.PI) / waveInfo.waveLenght - (position.z - waveInfo.speed * time) + waveInfo.phase);
        return height;
    }
}
