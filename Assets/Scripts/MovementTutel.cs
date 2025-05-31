using UnityEngine;
using System.Collections;

public class MovementTutel : MonoBehaviour
{
    [Header("Patrol Points")]
    public Transform pointA;
    public Transform pointB;

    [Header("Movement Settings")]
    public float maxSpeed = 2f;
    public float acceleration = 2f;
    public float decelerationDistance = 1f;
    public float turnDelay = 0.3f;

    private Rigidbody2D rb;
    private Transform targetPoint;
    private bool isTurning = false;
    private bool facingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPoint = pointB;
    }

    private void FixedUpdate()
    {
        if (isTurning) return;

        Vector2 direction = (targetPoint.position - transform.position);
        float distance = direction.magnitude;
        direction.Normalize();

        float speedMultiplier = Mathf.Clamp01(distance / decelerationDistance);
        float targetSpeed = maxSpeed * speedMultiplier;
        Vector2 desiredVelocity = direction * targetSpeed;

        rb.velocity = Vector2.MoveTowards(rb.velocity, desiredVelocity, acceleration * Time.fixedDeltaTime);

        // Flip sprite if needed
        if ((targetPoint == pointB && !facingRight) || (targetPoint == pointA && facingRight))
            Flip();

        // If close enough to target, stop and turn around
        if (distance < 0.1f)
        {
            StartCoroutine(TurnAround());
        }
    }

    private IEnumerator TurnAround()
    {
        isTurning = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(turnDelay);
        targetPoint = (targetPoint == pointA) ? pointB : pointA;
        isTurning = false;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
