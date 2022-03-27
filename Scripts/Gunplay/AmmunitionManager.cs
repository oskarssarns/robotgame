using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class AmmunitionManager : MonoBehaviour
{
    public int GunID;
    public bool canShoot = true;
    public int maxAmmoInClip;
    public float reloadTime;
    public int maxAmmoReserve;
    private TextMeshProUGUI ammoCounter;
    private int currentAmmoInClip;
    public int currentAmmoReserve;
    public AudioClip Reloading;
    public PhotonView view;


    // Start is called before the first frame update
    void Start()
    {
        view = transform.parent.GetComponent<WeaponManager>().view;
        if (!view.IsMine) { this.enabled = false; return; }
        // find canvas, complicated I know
        ammoCounter = FindObjectOfType<Canvas>().transform.Find("ammo").GetComponent<TextMeshProUGUI>();
        currentAmmoInClip = maxAmmoInClip;
        currentAmmoReserve = maxAmmoReserve;
    }

    // Update is called once per frame
    void Update()
    {
        ammoCounter.text = $"{currentAmmoInClip}/{currentAmmoReserve}";
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R pressed");
            StartCoroutine(ReloadGun());
        }
    }

    public void DecreaseAmmo(int amount)
    {
        if (currentAmmoInClip - amount > 0)
        {
            currentAmmoInClip -= amount;
        }
        else
        {
            currentAmmoInClip = 0;
            StartCoroutine(ReloadGun());
        }
    }

    IEnumerator ReloadGun()
    {
        if (canShoot)
        {
            canShoot = false;
            if (currentAmmoReserve != 0)
            {
                GetComponent<AudioSource>().clip = Reloading;
                GetComponent<AudioSource>().Play();
            }
            // wait for x seconds and stop reloading if enough ammo left
            yield return new WaitForSeconds(reloadTime);
            int amountToReplenish = maxAmmoInClip - currentAmmoInClip;

            if (currentAmmoReserve - amountToReplenish >= 0)
            {
                currentAmmoInClip = maxAmmoInClip;
                currentAmmoReserve -= amountToReplenish;
                canShoot = true;
            }
            else if (currentAmmoReserve > 0 && currentAmmoReserve <= maxAmmoInClip)
            {
                currentAmmoInClip = currentAmmoReserve;
                currentAmmoReserve = 0;
                canShoot = true;
            }
            else
            {
                canShoot = false;
            }
        }
    }
}
