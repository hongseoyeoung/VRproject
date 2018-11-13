using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;

    //public Camera fpsCam;

    private SteamVR_TrackedObject trackedObj;

    //SteamVR_Controller.Device 클래스의 접근성을 위한 프로퍼티 설정
    public SteamVR_Controller.Device get_controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    void Awake()
    {
        //컨트롤러(부모(Controller (right))
        //에 포함된  teamVR_TrackedObject 스크립트 저장
        trackedObj = gameObject.transform.parent.GetComponent<SteamVR_TrackedObject>();
    }

    void Start () {
     
	}
	
	void Update () {
        if(get_controller.GetHairTriggerDown())
        {
            Debug.Log("Trigger button down");
            Shooting();
        }
    }

    //레이캐스트 발사
    private void Shooting()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        
        RaycastHit hit;
        //Raycast(시작점, 방향, hit정보, 쏘는 거리)
        if(Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Debug.DrawRay(transform.position , transform.forward, Color.red ,1 );
        }

    }
}
