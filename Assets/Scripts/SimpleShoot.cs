using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public AudioSource source;
    public AudioClip reload;
    public AudioClip shoot;
    [SerializeField] GameObject liner;
    private GameObject linerRender;

    public TextMeshProUGUI AmmoTextMesh;
    public int maxAmmo = 6;
    public int currentAmmo = 6;

    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 1000f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;


    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        AmmoTextMesh.text = currentAmmo+"";
    }

    void Update()
    {
        if(gameObject.GetComponent<OVRGrabbable>()){
            if (Input.GetButtonDown("Fire1") && (gameObject.GetComponent<OVRGrabbable>().isGrabbed || 
                gameObject.tag == "Player")) {
                Shoot();
            }

        }

        if(OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch)){

        }

        if(Vector3.Angle(transform.up, Vector3.up) > 100 & currentAmmo < maxAmmo)
            Reload();
    }

    void Reload() {
        currentAmmo = maxAmmo;
        AmmoTextMesh.text = currentAmmo+"";
        source.PlayOneShot(reload);
    }

    public void pullTrigger(){

        //Calls animation on the gun that has the relevant animation events that will fire
        gunAnimator.SetTrigger("Fire");
        
    }

    //This function creates the bullet behavior
    public void Shoot()
    {
        if(CheckAmmoIsZero()) return;

        if (muzzleFlashPrefab)
        {
            //Create the muzzle flash
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);
            source.PlayOneShot(shoot);
            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        //cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }
        // Create a bullet and add force on it in direction of the barrel
        GameObject bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
        source.PlayOneShot(shoot);
        Destroy(bullet, destroyTimer);

        RaycastHit hitInfo;
        bool hasHit = Physics.Raycast(barrelLocation.position, barrelLocation.forward, out hitInfo, 100);

        if(liner) {
            if(hasHit)
            {
                linerRender = Instantiate(liner);
                linerRender.GetComponent<LineRenderer>().SetPositions(new Vector3[] { barrelLocation.position, hasHit ? hitInfo.point : barrelLocation.position+barrelLocation.forward*100});
                Destroy(linerRender,0.4f);

            } 
        }
    }

    public bool CheckAmmoIsZero()
    {
        if(currentAmmo <= 0){
            return true;
        } else{
            currentAmmo-=1;
            AmmoTextMesh.text = currentAmmo+"";
            return false;
        } 
    }

    //This function creates a casing at the ejection slot
    public void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

}
