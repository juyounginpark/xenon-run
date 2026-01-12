using UnityEngine;
using UnityEngine.InputSystem;

public class LegController : MonoBehaviour
{
    [Header("다리 설정")]
    [Tooltip("왼쪽 다리의 HingeJoint2D")]
    public HingeJoint2D leftLegJoint;

    [Tooltip("오른쪽 다리의 HingeJoint2D")]
    public HingeJoint2D rightLegJoint;

    [Header("모터 설정")]
    [Tooltip("다리 회전 속도")]
    public float motorSpeed = 300f;

    [Tooltip("모터 최대 토크")]
    public float maxMotorTorque = 1000f;

    [Header("키 설정")]
    public Key leftLegForward = Key.Q;   // 왼쪽 다리 앞으로
    public Key leftLegBackward = Key.A;  // 왼쪽 다리 뒤로
    public Key rightLegForward = Key.W;  // 오른쪽 다리 앞으로
    public Key rightLegBackward = Key.S; // 오른쪽 다리 뒤로

    void Start()
    {
        // HingeJoint2D 모터 초기 설정
        SetupJointMotor(leftLegJoint);
        SetupJointMotor(rightLegJoint);
    }

    void SetupJointMotor(HingeJoint2D joint)
    {
        if (joint == null) return;

        joint.useMotor = true;
        JointMotor2D motor = joint.motor;
        motor.maxMotorTorque = maxMotorTorque;
        motor.motorSpeed = 0;
        joint.motor = motor;
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // 왼쪽 다리 제어
        float leftSpeed = 0f;
        if (Keyboard.current[leftLegForward].isPressed)
            leftSpeed = motorSpeed;
        else if (Keyboard.current[leftLegBackward].isPressed)
            leftSpeed = -motorSpeed;

        SetMotorSpeed(leftLegJoint, leftSpeed);

        // 오른쪽 다리 제어
        float rightSpeed = 0f;
        if (Keyboard.current[rightLegForward].isPressed)
            rightSpeed = motorSpeed;
        else if (Keyboard.current[rightLegBackward].isPressed)
            rightSpeed = -motorSpeed;

        SetMotorSpeed(rightLegJoint, rightSpeed);
    }

    void SetMotorSpeed(HingeJoint2D joint, float speed)
    {
        if (joint == null) return;

        JointMotor2D motor = joint.motor;
        motor.motorSpeed = speed;
        joint.motor = motor;
    }
}
