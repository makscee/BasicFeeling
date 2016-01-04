﻿using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour 
{
	protected IController controller;
	public float Speed;
	Rigidbody2D rb;
	virtual protected void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	virtual protected void Update () 
	{
		rb.velocity = Vector2.zero;
		rb.velocity += controller.NeedVel();
		rb.velocity.Normalize ();
		rb.velocity *= Speed;

		var dir = VectorUtils.V2ToV3(controller.LookAt());
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		if(controller.NeedShoot())
		{
			Transform weapon = transform.FindChild("Weapon");
			if(weapon != null)
				weapon.gameObject.GetComponent<Weapon>().Shoot(dir + transform.position);
		}
	}
}
