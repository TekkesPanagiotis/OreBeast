using UnityEngine;
[RequireComponent(typeof(GunStats), typeof(LaserController))]
public class LaserEmitter : MonoBehaviour
{
    [SerializeField] private Transform GunPoint;
    [SerializeField] private LineRenderer laserVisual;
    private GunStats stats;
    private LaserController controller;
    private Camera mainCam;
    private bool isEmitting = false;
    
    void Awake()
    {
        stats = GetComponent<GunStats>();
        controller = GetComponent<LaserController>();
        mainCam = Camera.main;
        laserVisual.enabled = false;
    }
    private void OnEnable()
    {
        controller.OnTriggerHold += StartLaser;
        controller.OnTriggerReleased += StopLaser;
    }

    private void OnDisable()
    {
        controller.OnTriggerHold -= StartLaser;
        controller.OnTriggerReleased -= StopLaser;
    }
    private void StartLaser()
    {
        isEmitting = true;
        laserVisual.enabled = true;
    }

    private void StopLaser()
    {
        isEmitting = false;
        laserVisual.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isEmitting) return;
         FireRaycast();
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            StartLaser();
        }
        else
        {
            StopLaser();
        }

    }

    private void FireRaycast()
    {
        laserVisual.SetPosition(0, GunPoint.position);
        Ray startRay = mainCam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2, 0));
        if (Physics.Raycast(startRay,out RaycastHit hit, stats.laserRange))
        {
            laserVisual.SetPosition(1, hit.point);
            Damageable target = hit.collider.GetComponent<Damageable>();
            if (target != null)
            {
                target.TakeDamage(stats.damagePerSecond * Time.deltaTime);
            }
        }
        else
        {
            laserVisual.SetPosition(1, GunPoint.position + mainCam.transform.forward * stats.laserRange);
        }
    }
    public bool IsFiring()
    {
        return isEmitting;
    }
}
