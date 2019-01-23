using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Script_CameraPlayer : MonoBehaviour
{
    public CinemachineVirtualCamera associate_cinemachine;

    private void Start()
    {
        associate_cinemachine.m_Follow = Script_Player.Instance.transform; 
    }
}
