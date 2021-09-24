using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    public GameObject childAlienMole;

    private bool hasMovedYet = false;

    private float radius = 2;
    private float speed = Mathf.PI / 2;
    private float currentAngle;

    public void SetParameters(float radius, float speed)
    {
        this.radius = radius;
        this.speed = speed;
    }

    protected override void InvokeDestroyEvent()
    {
        moleDestroyed.Invoke(true);
    }

    protected override void Move()
    {
        // I BELIEVE I CAN FLY
        if (!hasMovedYet)
        {
            hasMovedYet = true;
            currentAngle = Mathf.Acos(this.transform.position.x / this.radius);
        }

        currentAngle += speed * Time.deltaTime;

        Vector3 newPosition = new Vector3(0, this.transform.position.y, 0);
        newPosition.x += radius * Mathf.Cos(currentAngle);
        newPosition.z += radius * Mathf.Sin(currentAngle);

        this.transform.position = newPosition;

        Vector3 targetCamera = new Vector3(- this.transform.position.x, this.transform.position.y + childAlienMole.transform.position.y, - this.transform.position.z);
        childAlienMole.transform.LookAt(targetCamera);
    }
}
