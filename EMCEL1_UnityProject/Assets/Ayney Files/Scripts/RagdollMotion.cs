using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollMotion : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] ConfigurableJoint Joint;
    Quaternion startingLocalRotation;
    Quaternion JointAxisRotation;
    Vector3 forward, up;

    [SerializeField] bool Static;

    public bool IsAlive = true;

    
    void Start()
    {
        IsAlive = true;
        Joint = GetComponent<ConfigurableJoint>();

        // Needed for calculating right position and rotation of the joints
        if(Joint!=null)
        {
            startingLocalRotation = transform.localRotation;
            forward = Vector3.Cross(Joint.axis, Joint.secondaryAxis);
            up = Joint.secondaryAxis;
            JointAxisRotation = Quaternion.LookRotation(forward, up);
            startingLocalRotation = transform.localRotation * JointAxisRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Joint.targetRotation = Quaternion.identity * (startingLocalRotation * Quaternion.Inverse(Target.localRotation));
        switch(Static && IsAlive)
        {
            case true:
                transform.rotation = Target.rotation;
                break;

            case false:
                if(IsAlive) Joint.targetRotation = Quaternion.Inverse(JointAxisRotation) * Quaternion.Inverse(Target.localRotation) * startingLocalRotation;
                break;
        }
    }

    public void Dead()
    {
        Static = false;
        IsAlive = false;

        if (Joint != null)
        {
            JointDrive jointDrive = Joint.slerpDrive;

            jointDrive.positionSpring = 0f;
            Joint.slerpDrive = jointDrive;

            jointDrive = Joint.angularYZDrive;
            jointDrive.positionSpring = 0f;
            Joint.angularYZDrive = jointDrive;

            jointDrive = Joint.angularXDrive;
            jointDrive.positionSpring = 0f;
            Joint.angularXDrive = jointDrive;
        }
        
    }
}
