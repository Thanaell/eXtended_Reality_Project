using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : Enemy
{
    [SerializeField]
    private float digUpTime = 0.7f;

    private float elapsedTime = 0;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool hasFinishedLerp = false;

    protected override void Start()
    {
        base.Start();

        startPosition = this.transform.position;
        targetPosition = new Vector3(this.transform.position.x, 0.85f, this.transform.position.z);

        Vector3 targetCamera = new Vector3(- this.transform.position.x, this.transform.position.y, - this.transform.position.z);
        this.transform.LookAt(targetCamera);
    }

    protected override void InvokeDestroyEvent()
    {
        moleDestroyed.Invoke(false);
    }

    protected override void Move()
    {
        // I'M STILL STANDING
        if (!hasFinishedLerp)
        {
            // The mole spawns undergrounf and lerps up
            float interpolationRatio = elapsedTime / digUpTime;
            this.transform.position = Vector3.Lerp(startPosition, targetPosition, interpolationRatio);

            hasFinishedLerp = (elapsedTime >= digUpTime);
            elapsedTime += Time.deltaTime;
        }
    }
}
