using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPadMove : MonoBehaviour
{

    [SerializeField]
    private Transform rig;
    //이동속도
    private float speed = 1.5f;

    //Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad = 컨트롤러의 터치패드를 의미
    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;

    //Steam VR은 기본적으로 VR HMD의 HW 좌표값을 넘겨준다.
    //이 좌표를 받아 랜더링시 좌표를 동기화 시켜 주는 컴포넌트가 SteamVR_TrackedObject
    private SteamVR_TrackedObject trackedObj;

    //SteamVR_Controller.Device 클래스의 접근성을 위한 프로퍼티 설정
    private SteamVR_Controller.Device controller
    {
        get
        {
            //SteamVR_Controller.Input(디바이스 id) = 입력된 키값 전달할 때 사용
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    //(x,y)좌표
    private Vector2 axis = Vector2.zero;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        //컨트롤러가 탐지 안될경우
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        //device 변수에 
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        //touchpad 입력이 있는 경우
        //controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad
        if (controller.GetTouch(touchpad))
        {
            //Valve.VR.EVRButtonId = 버튼의 종류 식별에 사용
            //k_EButton_SteamVR_Touchpad = k_EButton_Axis0
            axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);

            if (rig != null)
            {
                //터치패드에 입력된 좌표값 대로 계산해서 계속 저장 > 이동
                rig.position += (transform.right * axis.x + transform.forward * axis.y) * speed * Time.deltaTime;
                rig.position = new Vector3(rig.position.x, -0.73f, rig.position.z);
            }
        }
    }
}
