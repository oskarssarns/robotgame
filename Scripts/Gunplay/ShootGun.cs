using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShootGun : MonoBehaviour
{
    public bool isAutomatic;
    public Vector3 recoil;
    public float recoilSpeed;
    public float rateOfFire;
    public float Damage;
    public Transform muzzle;
    public GameObject projectile;
    public Camera playerCam;
    private Vector3 destination;
    private bool isShooting;
    private float timePassed;
    private AmmunitionManager ammunitionManager;
    public AudioClip Shooting;

    private PhotonView view;

    void Start()
    {
        view = transform.parent.GetComponent<WeaponManager>().view;
        ammunitionManager = GetComponent<AmmunitionManager>();
        timePassed = Time.time;
    }
    void Update()
    {
        if(!view.IsMine){return;}
        if (Input.GetMouseButton(0) && isAutomatic && ammunitionManager.canShoot)
        {
            if (Time.time - timePassed > rateOfFire)
            {
                timePassed = Time.time;
                Shoot();
            }
        }
        if (Input.GetMouseButtonDown(0) && ammunitionManager.canShoot)
        {
            if (Time.time - timePassed > rateOfFire)
            {
                isShooting = true;
                timePassed = Time.time;
                Shoot();
            }
        }
    }
    void Shoot()
    {
        Ray ray = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1f);
        }

        InstantiateProjectile();
        GetComponent<AudioSource>().clip = Shooting;
        GetComponent<AudioSource>().Play();
        playerCam.GetComponent<CameraMovement>().AddRecoil(recoil, recoilSpeed);
        ammunitionManager.DecreaseAmmo(1);

    }

    [PunRPC]
    void InstantiateProjectile()
    {
        var projectileObject = PhotonNetwork.Instantiate(projectile.name, muzzle.position, playerCam.transform.rotation);
        projectileObject.GetComponent<ParticleCollisionInstance>().Damage = Damage;
    }
}
