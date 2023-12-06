using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Animator weaponAnimator;
    private StarterAssetsInputs input;
    public int max_ammo;
    private int current_ammo;
    public int magazine_count;
    public bool reloading;
    public bool emptyMag;
    public int max_magazine_count;
    public float reload_time;
    public string weapon_name;
    public bool shot_cooldown;
    public float timeBetweenShots;
    public bool gunEquipped = false;

    private GameObject weaponbase;
    private PlayerInventory inventory;

    [Header("HUD Information")]
    public TextMeshProUGUI name_display;
    public TextMeshProUGUI ammo_counter;
    public TextMeshProUGUI none_notif;

    [Header("GameObject Information")]
    public Transform tipTransform;
    public GameObject flashToSpawn;
    public GameObject hitToSpawn;

    [Header("Physics and Raycast Info")]
    public LayerMask layermask;
    public float distanceToHit;

    private void Start()
    {
        weaponbase = weaponAnimator.gameObject;
        input = GetComponent<StarterAssetsInputs>();
        inventory = GetComponent<PlayerInventory>();
        OnSetEnableState(false);
        current_ammo = max_ammo;
    }

    private void Update()
    {
        if (!gunEquipped) return;
        if(reloading)
        {
            weaponAnimator.SetBool("Fire", false);
            weaponAnimator.SetBool("Aim", false);
            return;
        }
        if(emptyMag)
        {
            none_notif.text = "NO AMMO";
            ammo_counter.text = "0/0";
            weaponAnimator.SetBool("Fire", false);
            weaponAnimator.SetBool("Aim", false);
            return;
        } else
        {
            none_notif.text = string.Empty;
        }

        
        weaponAnimator.SetBool("Aim", input.mRight);

        ammo_counter.text = current_ammo.ToString() + "/" + (magazine_count * max_ammo).ToString();

        if(input.mLeft && !shot_cooldown)
        {
            weaponAnimator.SetTrigger("Fire");
            input.mLeft = false;
            shot_cooldown = true;
            Invoke(nameof(ShotReloadSequence), timeBetweenShots);
            GameObject obj = Instantiate(flashToSpawn, tipTransform);
            Destroy(obj, 0.1f);

            // Perform Raycasts and figure out whats hit
            RaycastHit hitInfo;
            Ray raycast = new Ray(tipTransform.position, Camera.main.transform.forward);
            Physics.Raycast(raycast, out hitInfo, distanceToHit, layermask);
            if(hitInfo.collider)
            {
                GameObject hitEff = Instantiate(hitToSpawn, hitInfo.point, Quaternion.Euler(hitInfo.normal));
                Destroy(hitEff, 2f);
            }

            // Fire Gun
            current_ammo--;
            if (current_ammo <= 0)
            {
                // Reload
                if (magazine_count > 0)
                {                    
                    inventory.SearchForAmmo(1);
                    ForceReload();   
                }
                else
                {
                    emptyMag = true;
                }

            }
        }
    }

    public void ForceReload()
    {
        Invoke(nameof(EndReloadSequence), reload_time);
        weaponAnimator.SetTrigger("Reload");
    }

    public void OnSetEnableState(bool state)
    {
        name_display.enabled = state;
        ammo_counter.enabled = state;
        none_notif.enabled = state;
        weaponbase.SetActive(state);
        name_display.text = weapon_name;
        gunEquipped = state;
    }

    public void EndReloadSequence()
    {
        reloading = false;
        current_ammo = max_ammo;
        emptyMag = false;
        removeMagazine(1);
    }

    public void ShotReloadSequence()
    {
        shot_cooldown = false;
    }

    public int AddMagazine(int magCount)
    {
        magazine_count += magCount;
        if (current_ammo == 0)
        {            
            ForceReload();
        }
        if (magazine_count > max_magazine_count)
        {
            return magazine_count - max_magazine_count;
        }
        return 0;
    }
    
    public void removeMagazine(int numToRemove)
    {
        magazine_count -= numToRemove;
        inventory.SearchForAmmo(numToRemove);
        if (magazine_count < 0) magazine_count = 0;
    }
}
