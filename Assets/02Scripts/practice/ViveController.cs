using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ViveController : MonoBehaviour
{
    [HideInInspector]
    public Teleport mTeleport;
    [HideInInspector]
    public Transform mPlayer;


    //SteamVR_TrackedObject를 저장할 변수
    private SteamVR_TrackedObject trackedObj;

    //SteamVR_Controller.Device 클래스의 접근성을 위한 프로퍼티 설정
    private SteamVR_Controller.Device controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }
    void Awake()
    {
        //스크립트 저장
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        mTeleport = FindObjectOfType<Teleport>();
        mPlayer = transform.parent;

    }

    void Start()
    {
       
    }

    void Update()
    {
        if (controller == null)
        {
            Debug.Log("Controller is not detected");
            return;
        }

        if (controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (mTeleport == null)
                return;
            mTeleport.mIsActive = true;
        }
        else
        {
            if (mTeleport == null)
                return;
            mTeleport.mIsActive = false;
            Teleport();
        }

        //트리거 버튼의 클릭여부
        if (controller.GetHairTriggerDown())
        {
            Debug.Log("Trigger " + controller.index + " is pressed");
        }
        //트리거 버튼의 릴리스 여부
        if (controller.GetHairTriggerUp())
        {
            Debug.Log("Trigger " + controller.index + " is unpressed");
        }
        //버튼 클릭 여부 = GetPress(),GetPressDown(), GetPressUp() 
        //메뉴 버튼의 클릭 여부
        if (controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            Debug.Log(controller.index + " App Button pressed");
        }
        //그립 버튼의 릴리스 여부
        if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(controller.index + " Grip Button unpressed");
        }
        //트랙 패드의 터치 여부와 좌표 산출
        if (controller.GetTouch(SteamVR_Controller.ButtonMask.Touchpad)) // 터치 여부 확인
        {
            Vector2 pad = controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad); // 좌표 산출
            Debug.Log("Touchpad = " + controller.index + " " + pad);
        }
    }

    void Teleport()
    {
        if (mTeleport.mIsActive == true)    
            return;
        Vector3 pos = mTeleport.mGroundPos;
        if (pos == Vector3.zero)
            return;
        mPlayer.transform.position = pos;
    }
}