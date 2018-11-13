using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZEffects;

public class GunController : MonoBehaviour {

    //오른쪽 컨트롤러
    public GameObject controllerRight;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    private SteamVR_TrackedController controller;

    public EffectTracer TracerEffect;
    //총구
    public Transform muzzleTransfrom;
    //총알
    //public GameObject Bullet;

    public Ray ray;
    //데미지
    private float damage = 20f;
    //라인렌더러 참조 변수
    private LineRenderer lineRenderer;
    //총구 파티클
    public GameObject impactEffect;
    //적이 총에 맞을 때 파티클
    public GameObject Enemy_Effect;


	void Start () {
        controller = controllerRight.GetComponent<SteamVR_TrackedController>();
        controller.TriggerClicked += TriggerPressed;
        trackedObj = controllerRight.GetComponent<SteamVR_TrackedObject>();
        lineRenderer = GetComponent<LineRenderer>();
    }
	
    void Update()
    {
        line();
    }

    private void TriggerPressed(object sender, ClickedEventArgs e)
    {
        Shooting();
        //Instantiate(Bullet, muzzleTransfrom.position, Quaternion.identity);
        Instantiate(impactEffect, muzzleTransfrom.position, Quaternion.LookRotation(muzzleTransfrom.position));
    }


    public void line()
    {
        lineRenderer.SetPosition(0, muzzleTransfrom.transform.position);
        lineRenderer.SetPosition(1, muzzleTransfrom.forward * 100f);
    }

    public void Shooting()
    {
        RaycastHit hit = new RaycastHit();
        //총구의 위치에서 Ray발사
        ray = new Ray(muzzleTransfrom.position, muzzleTransfrom.forward);
        //사용하는 장비에 대한 정보 저장
        device = SteamVR_Controller.Input((int)trackedObj.index);
        //TriggerHapticPulse 컨트롤러 진동
        device.TriggerHapticPulse(1000);
        //총구의 위치에서 방향으로 250f만큼 Effect발생
        //TracerEffect.ShowTracerEffect(muzzleTransfrom.position, muzzleTransfrom.forward, 250f);
        //다른 임펙트 생성

        //라인렌더러의 첫 위치는 총구
        lineRenderer.SetPosition(0, muzzleTransfrom.position);
        

         //레이캐스트 발사
        if (Physics.Raycast(ray, out hit, 500f))
        {
            //Debug.Log(hit.transform.name);
            //빨간색 레이 그리기
            //Debug.DrawRay(muzzleTransfrom.position, muzzleTransfrom.forward * 100f, Color.red, 5f);

            Enemy _Enemy = hit.transform.GetComponent<Enemy>();
            //레이캐스트가 적과 충돌 시
            if(_Enemy != null)
            {
                _Enemy.TakeDamage(damage);
            }
            Instantiate(Enemy_Effect, hit.point, Quaternion.LookRotation(muzzleTransfrom.position));


        }
    }

}
