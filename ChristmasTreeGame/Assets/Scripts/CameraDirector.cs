//Uncomment below if using cutscenes
//#define USING_CUT_SCENES

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraDirector : MonoBehaviour
{
    public enum CameraList
    {
        MenuCam = 0,
        InPlayCam,
        LevelCompleteCam,
        //Add new cams here

#if USING_CUT_SCENES
        CutSceneCam, //used for playing cut scenes
#endif
        NumOfCams
    }

    public enum CameraPriority
    {
        Low = 0,
        High = 1,
        NumOfPriorities
    }

    [SerializeField] private CinemachineBrain cameraBrain;
    [SerializeField] private CinemachineVirtualCamera[] cameraList = new CinemachineVirtualCamera[(int)CameraList.NumOfCams];

    private static CameraDirector instance;
    

    public static CameraDirector Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CameraDirector>();
            }
            return instance;
        }
    }

    void Awake()
    {
        SetCamera(CameraList.MenuCam);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

#if USING_CUT_SCENES
    public void SetCutCam(CinemachineVirtualCamera vCam)
    {
        cameraList[(int)CameraList.CutSceneCam] = vCam;
        SetCamera(CameraList.CutSceneCam);
    }
#endif

    public void SetCamera(CameraList newCam)
    {
        for (int i = 0; i < (int)CameraList.NumOfCams; i++)
        {
            if(cameraList[i] != null)
            {
                cameraList[i].Priority = (i == (int)newCam) ? (int)CameraPriority.High : (int)CameraPriority.Low;
            }
            else
            {
                Debug.LogError("Error: cameraList[" + i.ToString() + "] is null - should be a virtual cam");
            }
            
        }
    }

    public void SetNewPlayer(GameObject player)
    {
        for (int i = 0; i < (int)CameraList.NumOfCams; i++)
        {
            if(cameraList[i] != null)
            { 
                cameraList[i].Follow = player.transform;
                cameraList[i].LookAt = player.transform;
            }
            else
            {
                Debug.LogError("Error: cameraList[" + i.ToString() + "] is null - should be a virtual cam");
            }
        }
    }

    public bool GetIsLive(CameraList vCam)
    {
        return CinemachineCore.Instance.IsLive(cameraList[(int)vCam]);
        
    }
}
