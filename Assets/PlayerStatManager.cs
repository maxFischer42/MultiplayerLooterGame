using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    private CharacterController controller;
    private FirstPersonController fpc;
    private StarterAssetsInputs input;
    private Transform cameraRoot;

    private controller_data standing_data;
    public controller_data crouching_data;
    private fpc_data fpc_standing;
    public fpc_data fpc_crouching;

    public StandingCheck standCheck;

    public bool isCrouched = false;

    // Start is called before the first frame update
    void Start()
    {
        
        cameraRoot = transform.Find("PlayerCameraRoot");
        controller = GetComponent<CharacterController>();
        fpc = GetComponent<FirstPersonController>();
        input = GetComponent<StarterAssetsInputs>();

        standing_data.center = controller.center;
        standing_data.radius = controller.radius;
        standing_data.height = controller.height;
        standing_data.camPosition = cameraRoot.transform.localPosition.y;
    }

    private void Update()
    {
        if(input.crouch && !isCrouched)
        {
            UpdateController(crouching_data, true, standing_data);         
        } else if(!input.crouch && isCrouched && !standCheck.isColliding)
        {
            UpdateController(standing_data, false, crouching_data);
        }
    }

    void UpdateController(controller_data data, bool isC, controller_data opposite)
    {
        cameraRoot.position = Vector3.Lerp(new Vector3(cameraRoot.position.x, data.camPosition, cameraRoot.position.z), new Vector3(cameraRoot.position.x, opposite.camPosition, cameraRoot.position.z), 0.6f);
        isCrouched = isC;
        controller.center = data.center;
        controller.radius = data.radius;
        controller.height = data.height;
    }

}
[System.Serializable]
public struct controller_data
{
    public Vector3 center;
    public float radius;
    public float height;
    public float camPosition;
}

[System.Serializable]
public struct fpc_data
{
    public float moveSpeed;
    public float sprintSpeed;
    public float sensitivity;
    public float jumpHeight;
    public float groundedOffset;
}