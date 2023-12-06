using System.Collections;
using System.Threading;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool equip;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		[Header("Inventory Settings")]
		public float scroll;
		public bool key_one;
		public bool key_two;
		public bool key_three;
		public bool key_four;
		public bool key_five;
		public bool key_six;

		public bool mLeft;
		public bool mRight;
		public bool crouch;
		public void OnMove(InputAction.CallbackContext value)
		{
			MoveInput(value.ReadValue<Vector2>());
		}

		public void OnLook(InputAction.CallbackContext value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.ReadValue<Vector2>());
			}
		}

		public void OnJump(InputAction.CallbackContext value)
		{
			jump = value.started;
		}

		public void OnSprint(InputAction.CallbackContext value)
		{
			if (value.canceled) sprint = false;
			else if (value.started) sprint = true;
        }

		public void OnScroll(InputAction.CallbackContext value)
		{
			scroll = value.ReadValue<float>();
		}

		public void OnKey1(InputAction.CallbackContext value)
		{
            key_one = value.started;
            StartCoroutine(disableBool(1));
		}

        public void OnKey2(InputAction.CallbackContext value)
        {
            key_two = value.started;
            StartCoroutine(disableBool(2));
        }

        public void OnKey3(InputAction.CallbackContext value)
        {
            key_three = value.started;
            StartCoroutine(disableBool(3));
        }

        public void OnKey4(InputAction.CallbackContext value)
        {
            key_four = value.started;
            StartCoroutine(disableBool(4));
        }

        public void OnKey5(InputAction.CallbackContext value)
        {
            key_five = value.started;
            StartCoroutine(disableBool(5));
        }

        public void OnKey6(InputAction.CallbackContext value)
        {
            key_six = value.started;
            StartCoroutine(disableBool(6));
        }

		public void OnEquip(InputAction.CallbackContext value)
		{
            equip = value.started;
			
        }

		public void OnLeftMouse(InputAction.CallbackContext value)
		{
			if (value.performed || value.canceled) mLeft = false;
			else if (value.started) mLeft = true;
			

		}

		public void OnRightMouse(InputAction.CallbackContext value)
		{
            if (value.performed || value.canceled) mRight = false;
            else if (value.started) mRight = true;
        }

		public void OnCrouch(InputAction.CallbackContext value)
		{
			if (value.canceled) crouch = false;
			else if (value.started) crouch = true;
		}
		IEnumerator disableBool(int toDisable)
		{
			yield return new WaitForSeconds(0.1f);
			switch(toDisable)
			{
				case 1:
					key_one = false;
					break;
                case 2:
                    key_two = false;
                    break;
                case 3:
                    key_three = false;
                    break;
                case 4:
                    key_four = false;
                    break;
                case 5:
                    key_five = false;
                    break;
                case 6:
                    key_six = false;
                    break;
            }
		}

        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		private void Start()
		{
			SetCursorState(true);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}