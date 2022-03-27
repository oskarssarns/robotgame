using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraFOVOnRun : MonoBehaviour
{
    public Camera PlayerCamera;
    public float IncreaseMaxFOVBy;
    public float FOVIncreaseSpeed;
    float maxFOV;
    float currentFOV;
    float defaultFOV;
    Status status;
    
    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<PlayerController>().status;
        currentFOV = PlayerCamera.fieldOfView;
        defaultFOV = currentFOV;
        maxFOV = currentFOV + IncreaseMaxFOVBy;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<PlayerController>().status == Status.sprinting || 
        GetComponent<PlayerController>().status == Status.sliding){
            IncreaseFov();
        }
        else
            DeacreseFov();
    }

    void IncreaseFov(){
        if(currentFOV < maxFOV){
            currentFOV += FOVIncreaseSpeed * 1;
            PlayerCamera.fieldOfView = currentFOV;
        }
    }
    
    void DeacreseFov(){
        if(currentFOV > defaultFOV){
            currentFOV -= FOVIncreaseSpeed * 1;
            PlayerCamera.fieldOfView = currentFOV;
        }
    }
}
