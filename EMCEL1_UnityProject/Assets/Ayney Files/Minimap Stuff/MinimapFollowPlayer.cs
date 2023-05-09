using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MinimapFollowPlayer : MonoBehaviour
{
    [SerializeField] Transform Target;
    Camera MinimapCam;

    public Shader RenderShader;

    private void Start()
    {
        transform.position += Vector3.up * 30;
        MinimapCam = GetComponent<Camera>();

        MinimapCam.SetReplacementShader(RenderShader, "");
        
    }
    void Update()
    {
        // transform.position = new Vector3(Target.position.x, transform.position.y, Target.position.z);
    }
}
