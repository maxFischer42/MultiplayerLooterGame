using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public Item[] items = new Item[5];
    public List<Augment> augments = new List<Augment>(5);

    private StarterAssetsInputs input;
    public Color unheldColor = Color.white;
    public Color heldColor = Color.white;

    int inventorySize { get { return 5; } set { inventorySize = value; } }
    public int held_item = 0;

    private float scrollCooldown = 0.25f;
    private float currentScrollCooldown = 0f;
    private bool scrollIsCooldown = false;

    public RectTransform inventorySlotParent;
    private List<Image> inventorySlots = new List<Image>(5);

    public bool inventoryFull = false;

    public Transform objectSlots;
    private List<Transform> objectSlotList = new List<Transform>();

    public GunController gunController;

    private void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        for(int i = 0; i < inventorySlotParent.childCount; i++) {
            inventorySlots.Add(inventorySlotParent.GetChild(i).GetComponent<Image>());
        }
        for (int i = 0; i < objectSlots.childCount; i++)
        {
            objectSlotList.Add(objectSlots.GetChild(i));
        }
        ResetColors();
        inventorySlots[held_item].color = heldColor;
    }

    private void Update()
    {
        int getnumkey = getNumBools();
        if (scrollIsCooldown)
        {
            currentScrollCooldown += Time.deltaTime;
            if(currentScrollCooldown > scrollCooldown)
            {
                scrollIsCooldown = false;
            }
        } else
        {
            int prevItem = held_item;
            if(input.scroll != 0)
            {
                ResetColors();

                scrollIsCooldown = true;
                currentScrollCooldown = 0f;
                int direction = input.scroll > 0 ? 1 : -1;
                held_item += direction;
                if (held_item < 0) held_item = inventorySize;
                else if (held_item > inventorySize) held_item = 0;

                inventorySlots[held_item].color = heldColor;
            } else if(getnumkey >= 0) {
                ResetColors();

                held_item = getnumkey;

                inventorySlots[held_item].color = heldColor;
            }

            if(held_item != prevItem)
            {
                for(int i = 0;  i < items.Length; i++)
                {
                    objectSlotList[i].gameObject.SetActive(held_item != i ? false : true);
                }
            }
        }

        if (items[held_item])
        {
            if (items[held_item].name == "Gun")
            {
                gunController.OnSetEnableState(true);
            } else if(gunController.gunEquipped)
            {
                gunController.OnSetEnableState(false);
            }
        } else
        {
            if(gunController.gunEquipped)
            {
                gunController.OnSetEnableState(false);
            }
        }


    }

    private void ResetColors()
    {
        foreach(Image i in inventorySlots)
        {
            i.color = unheldColor;
        }
    }

    int getNumBools()
    {
        if (input.key_one)      return 0;
        if (input.key_two)      return 1;
        if (input.key_three)    return 2;
        if (input.key_four)     return 3;
        if (input.key_five)     return 4;
        if (input.key_six)      return 5;
        return -1;
    }

    void CheckInventoryFull()
    {
        int itemCount = 0;
        foreach(Item i in items)
        {
            if (i != null) itemCount++;
        }
        if(itemCount == inventorySize + 1)
        {
            inventoryFull = true;
        } else
        {
            inventoryFull = false;
        }
    }

    public void EquipItem(Item item)
    {
        if (inventoryFull) return;
        print(item);
        int pos = 0;
        for(int i = 0; i <= inventorySize; i++)
        {
            print(items[i] + " :: " + i);
            if (items[i] == null)
            {
                pos = i; break;
            }
        }

        print(pos);
        items[pos] = item;
        Instantiate(item.prefab, objectSlotList[pos]);
        inventorySlots[pos].transform.GetChild(0).gameObject.SetActive(true);
        CheckInventoryFull();
    }

    public void SearchForAmmo(int count)
    {
        int current = count;
        for (int i = 0; i < count; i++)
        {
            for (int b = 0; b <= inventorySize; b++)
            {
                if (current <= 0) return;
                if (items[b] && items[b].isAmmo)
                {
                    items[b] = null;
                    Destroy(objectSlotList[b].GetChild(0).gameObject);
                    inventorySlots[b].transform.GetChild(0).gameObject.SetActive(false);
                    CheckInventoryFull();
                    current--;
                }
            }
        }
    }

}
