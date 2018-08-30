using System.Collections;
using System.Collections.Generic;
using Playmode.Ennemy.BodyParts;
using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Ennemy
{
	public class WeaponIconController : MonoBehaviour
	{
		private Quaternion rotation;
		
		private HandController handController;
		private SpriteRenderer spriteRenderer;

		[SerializeField] private Sprite shotgunSprite;
		[SerializeField] private Sprite uziSprite;

		private void Awake()
		{
			rotation = transform.rotation;
			
			handController = transform.root.GetComponentInChildren<HandController>();
			spriteRenderer = GetComponent<SpriteRenderer>();
		}
		
		private void LateUpdate()
		{
			transform.position = transform.root.position + transform.up * 1.6f;
			transform.rotation = rotation;
		}

		private void Update()
		{
			ChangeSpriteDependingOnWeapon();
		}

		private void ChangeSpriteDependingOnWeapon()
		{
			if (handController.weaponController.GetWeaponType() == PickableTypes.Shotgun)
			{
				spriteRenderer.sprite = shotgunSprite;
			}
			else if (handController.weaponController.GetWeaponType() == PickableTypes.Uzi)
			{
				spriteRenderer.sprite = uziSprite;
			}
			else
			{
				spriteRenderer.sprite = null;
			}
		}
	}
}
