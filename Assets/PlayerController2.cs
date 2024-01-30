    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    PlayerInput _input;
    private string r_tentacleJointTag = "R_tentacleJoint";
    private string l_tentacleJointTag = "L_tentacleJoint";
    public List<HingeJoint2D> R_joints;
    public List<HingeJoint2D> L_joints;
    public Sucker R_sucker;
    public Sucker L_sucker;

    [Header("Joint Parameters")]
    public float motorSpeed = 100;
    public int minJointAngle = 0;
    public int maxJointAngle = 90;
    public float DampingFactor = 0.1f;
    public float motorSpeedFactor = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _input.enabled = true;

        foreach (GameObject g in GameObject.FindGameObjectsWithTag(r_tentacleJointTag))
        {
            HingeJoint2D joint = g.GetComponent<HingeJoint2D>();
            joint.useMotor = true;

            JointAngleLimits2D limits = joint.limits;
            limits.min = minJointAngle;
            limits.max = maxJointAngle;
            joint.limits = limits;


            JointMotor2D motor = joint.motor;
            motor.motorSpeed = motorSpeed;
            joint.motor = motor;
            R_joints.Add(joint);
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float rightJointAngle = maxJointAngle - (maxJointAngle * _input.RightTriggerInputStrength);
        foreach (HingeJoint2D joint in R_joints)
        {
            SetJointAngleLimits(joint, rightJointAngle, minJointAngle);

            JointMotor2D motor = joint.motor;
            motor.motorSpeed = CalculateMotorSpeed(joint, rightJointAngle);
            joint.motor = motor;
        }

        if (R_sucker.Sucking)
        {

        }
    }

    private void SetJointAngleLimits(HingeJoint2D joint, float max, float min)
    {
        JointAngleLimits2D limits = joint.limits;
        limits.min = min;
        limits.max = max;
        joint.limits = limits;
    }

    private float CalculateMotorSpeed(HingeJoint2D joint, float targetAngle)
    {
        float currentAngle = joint.jointAngle;
        float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);

        float motorSpeed = angleDifference * motorSpeedFactor;

        // Apply damping
        float damping = Mathf.Clamp01(Mathf.Abs(angleDifference) / maxJointAngle);
        motorSpeed *= (1f - damping) * DampingFactor + damping;

        return motorSpeed;
    }
}
