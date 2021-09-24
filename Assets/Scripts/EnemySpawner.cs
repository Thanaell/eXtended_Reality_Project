using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class EnemySpawner : MonoBehaviour
{
    public float firstDrawTime;
    public float drawTimeStep;
    public float spawnProbability;
    public float spawnMalusFactor;
    public float meanSpawnAngle;
    public float deviationspawnAngle;

    public GameObject groundMolePrefab;
    public GameObject flyingMolePrefab;

    private float nextDrawTime;

    private float minRadius = 5f;
    private float maxRadius = 6.5f;
    private float minSpeed = - Mathf.PI / 6;
    private float maxSpeed = Mathf.PI / 6;
    private float minHeight = 1.5f;
    private float maxHeight = 3f;

    private GameObject groundMole;
    private GameObject flyingMole;

    public static float GaussianDraw(float deviation, float mean)
    {
        float u, v, S;

        do
        {
            u = 2.0f * Random.Range(0f,1f) - 1.0f;
            v = 2.0f * Random.Range(0f, 1f) - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0);
        float fac = Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);
        return mean + deviation * u * fac;
    }

    void Start()
    {
        nextDrawTime = Time.time + firstDrawTime;
    }

    void Update()
    {
        if (Time.time >= nextDrawTime && !(groundMole && flyingMole))
        {
            float drawResult = Random.Range(0, 1f);

            if (!flyingMole && !groundMole)
            {
                bool isFlyingMole = (Random.Range(0, 1f) >= 0.5);
                if (drawResult < spawnProbability)
                {
                    if (isFlyingMole)
                    {
                        SpawnFlyingMole();
                    } else
                    {
                        SpawnGroundMole();
                    }
                }
            }
            else
            {
                if (drawResult < spawnProbability * spawnMalusFactor)
                {
                    if (!flyingMole)
                    {
                        SpawnFlyingMole();
                    }
                    else
                    {
                        SpawnGroundMole();
                    }
                }
            }

            nextDrawTime = Time.time + drawTimeStep;
        }
    }

    private void SpawnFlyingMole() {
        float newRadius = 2 * Random.Range(minRadius, maxRadius);
        float newSpeed = Random.Range(minSpeed, maxSpeed);
        float newHeight = Random.Range(minHeight, maxHeight);

        float xForward = this.transform.forward.x;
        float zForward = this.transform.forward.z;
        float newAngle = RandomAngleInView();

        Vector3 newPosition = new Vector3(
            newRadius * (Mathf.Cos(newAngle) * xForward + Mathf.Sin(newAngle) * zForward),
            newHeight,
            newRadius * (- Mathf.Sin(newAngle) * xForward + Mathf.Cos(newAngle) * zForward)
        );

        flyingMole = Instantiate(flyingMolePrefab, newPosition, Quaternion.identity);
        flyingMole.GetComponent<FlyingEnemy>().SetParameters(newRadius, newSpeed);
    }

    private void SpawnGroundMole()
    {
        float newRadius = Random.Range(minRadius, maxRadius);

        float newAngle = RandomAngleInView();
        Vector3 newPosition = new Vector3(newRadius * Mathf.Sin(newAngle), - 0.2f, newRadius * Mathf.Cos(newAngle));

        groundMole = Instantiate(groundMolePrefab, newPosition, Quaternion.identity);
    }

    private float RandomAngleInView()
    {
        float cameraYRot = this.transform.rotation.eulerAngles.y * Mathf.PI / 180;

        float angle=GaussianDraw(deviationspawnAngle, meanSpawnAngle);
        if (angle >= meanSpawnAngle)
        {
            return cameraYRot - angle;
        }
        return cameraYRot + angle;
    }

    public void MoleDespawned(bool isFlyingMole)
    {
        if (isFlyingMole)
        {
            flyingMole = null;
        }
        else
        {
            groundMole = null;
        }
    }
}
