using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollOptionsManager : MonoBehaviour
{
    [SerializeField] float XDriveValue;
    [SerializeField] float YZDriveValue;
    ConfigurableJoint[] Joints;
    void Start()
    {
        Joints = GetComponentsInChildren<ConfigurableJoint>();

        JointDrive jDrive;

        foreach(ConfigurableJoint joint in Joints)
        {
            jDrive = joint.angularXDrive;
            jDrive.positionSpring = XDriveValue;
            joint.angularXDrive = jDrive;

            jDrive = joint.angularYZDrive;
            jDrive.positionSpring = YZDriveValue;
            joint.angularYZDrive = jDrive;
        }
    }

   
}
