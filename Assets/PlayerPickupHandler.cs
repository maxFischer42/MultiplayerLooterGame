using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.DualShock;

public class PlayerPickupHandler : MonoBehaviour
{
    private PlayerInventory inventory;
    private StarterAssetsInputs input;

    public float lookDistance;

    private Camera mainCamera;
    public LayerMask lookLayers;
    public GameObject objectInView;
    public GameObject UIobjectEquip;
    public GameObject UIfull;

    private void Start()
    {
        mainCamera = Camera.main;
        inventory= GetComponent<PlayerInventory>();
        input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, lookDistance, lookLayers);
        if(hit.collider)
        {
            if(hit.collider.tag == "ItemRange")
            {
                objectInView = hit.collider.gameObject;
                if(inventory.inventoryFull)
                {
                    UIfull.SetActive(true);
                } else
                {
                    UIobjectEquip.SetActive(true);
                    TryEquipItem(hit.collider.gameObject);
                }
                input.equip = false;
            } 
            else if(hit.collider.tag == "Terminal")
            {
                ShipManager sh = GameObject.FindObjectOfType<ShipManager>();
                if(!sh.consoleOccupied && input.equip) { 
                    sh.EnterTerminal(GetComponent<FirstPersonController>());
                }
            }
        }
        else
        {
            objectInView = null;
            UIfull.SetActive(false);
            UIobjectEquip.SetActive(false);
            input.equip = false;
        }
        
    }

    void TryEquipItem(GameObject obj)
    {
        if (input.equip)
        {
            Item item = obj.GetComponent<Pickup>().item;
            inventory.EquipItem(item);
            print("foo");
            if (obj.GetComponent<Pickup>().item.isAmmo)
            {
                GetComponent<GunController>().AddMagazine(1);
            }
            Destroy(obj);
        }
    }

    void TryDropItem(GameObject obj)
    {
        if(obj.GetComponent<Pickup>().item.isAmmo)
        {
            GetComponent<GunController>().removeMagazine(1);
        }
    }



}
