using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public float FuelLevel = 1.0f;
    public float OxygenLevel = 1.0f;
    public float ShieldsLevel = 1.0f;
    public float PowerLevel = 1.0f;

    public enum menu_type { nav, status, cam}    
    public GameObject baseNavMenu;
    public GameObject statusMenu;
    public GameObject camMenu;
    public GameObject mapMenu;

    public enum planet { test_planet_1, test_planet_2, test_planet_3, space_ruin }
    public planet current_planet = planet.test_planet_1;

    public bool OnRoute = false;
    public enum route_difficulty { easy, medium, hard}
    public route_difficulty difficulty = route_difficulty.medium;

    public bool consoleOccupied = false;
    private FirstPersonController controllerInMenu;
    private Camera cameraInMenu;
    public Camera terminalCamera;
    private StarterAssetsInputs input;
    public void SetRoute(planet destination)
    {
        current_planet = destination;
    }

    private void LateUpdate()
    {
        if(consoleOccupied)
        {
            if(input.equip)
            {
                input.equip = false;
                ExitTerminal();
            }
        }
    }

    public void EnterTerminal(FirstPersonController controller) { 
        controllerInMenu = controller;
        cameraInMenu = Camera.main;
        input = controller.GetComponent<StarterAssetsInputs>();
        controllerInMenu.enabled = false;
        cameraInMenu.enabled = false;
        consoleOccupied = true;
        terminalCamera.enabled = true;
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
        input.equip = false;
    }

    public void ExitTerminal()
    {
        controllerInMenu.enabled = true;
        cameraInMenu.enabled = true;
        consoleOccupied = false;
        terminalCamera.enabled = false;
        Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
    }

    public void EnterMenu(int menu)
    {
        switch(menu)
        {
            case 0:
                baseNavMenu.SetActive(true);
                statusMenu.SetActive(false);
                camMenu.SetActive(false);
                mapMenu.SetActive(false);
                break;
            case 1:
                baseNavMenu.SetActive(false);
                statusMenu.SetActive(true);
                camMenu.SetActive(false);
                mapMenu.SetActive(false);
                break;
            case 2:
                baseNavMenu.SetActive(false); ;
                statusMenu.SetActive(false);
                camMenu.SetActive(true);
                mapMenu.SetActive(false);
                break;
            case 3:
                baseNavMenu.SetActive(false); ;
                statusMenu.SetActive(false);
                camMenu.SetActive(false);
                mapMenu.SetActive(true);
                break;
        }
    }
}
