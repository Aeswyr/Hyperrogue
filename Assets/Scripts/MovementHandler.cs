using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private AnimationCurve accelerationCurve;
    private float accelerationTime;
    [SerializeField] private AnimationCurve decelerationCurve;
    private float decelerationTime;
    private float timestamp;
    private float dir;
    private float decelSpeed;
    private float speedScalar = 1;
    bool moving = false;
    // Update is called once per frame


    void Awake() {
        accelerationTime = accelerationCurve[accelerationCurve.length - 1].time;
        decelerationTime = decelerationCurve[decelerationCurve.length - 1].time;
    }

    void FixedUpdate()
    {
        if (Time.time < timestamp) {
            if (moving)
                rbody.velocity = new Vector2(speedScalar * speed * dir * accelerationCurve.Evaluate(Time.time - timestamp + accelerationTime), rbody.velocity.y);
            else
                rbody.velocity = new Vector2(speedScalar * decelSpeed * dir * decelerationCurve.Evaluate(Time.time - timestamp + decelerationTime), rbody.velocity.y);
        } else {
            if (moving)
                rbody.velocity = new Vector2(speedScalar * speed * dir, rbody.velocity.y);
        }
    }

    public void StartDeceleration() {
        moving = false;
        timestamp = Time.time + decelerationTime;
        decelSpeed = speed;
        if (Mathf.Abs(rbody.velocity.x) < decelSpeed)
            decelSpeed = Mathf.Abs(rbody.velocity.x);
    }

    public void StartAcceleration(float dir) {
        moving = true;
        timestamp = Time.time + accelerationTime;
    }

    public void UpdateMovement(float dir) {
        this.dir = dir;
        moving = true;
    }

    public void setSpeedScalar(float val) {
        speedScalar = val;
    }


    


}
