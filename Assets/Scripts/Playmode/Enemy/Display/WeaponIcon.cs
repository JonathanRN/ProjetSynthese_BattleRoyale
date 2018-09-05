using System.Collections;
using System.Collections.Generic;
using Playmode.Enemy.BodyParts;
using Playmode.Ennemy.BodyParts;
using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Ennemy
{
	public class WeaponIcon : MonoBehaviour
	{
		[SerializeField] private Sprite shotgunSprite;
		[SerializeField] private Sprite uziSprite;
		
		private Hand hand;
		private SpriteRenderer spriteRenderer;
		
		private Quaternion rotation;

		private void Awake()
		{
			rotation = transform.rotation;

			InitializeComponents();
		}

		private void InitializeComponents()
		{
			hand = transform.root.GetComponentInChildren<Hand>();
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
			if (hand.WeaponController.GetWeaponType() == PickableTypes.Shotgun)
			{
				spriteRenderer.sprite = shotgunSprite;
			}
			else if (hand.WeaponController.GetWeaponType() == PickableTypes.Uzi)
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
