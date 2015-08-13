using UnityEngine;
using System.Collections;
using PixelArtRotation;

public class AimAndShoot : MonoBehaviour 
{
	public Transform Weapon;
	public int MaxAngle;
	public int MinAngle;

	public int Angle { get; private set; }

	// Use this for initialization
	void Awake ()
	{
		_weaponAnimator = Weapon.GetComponent<Animator> ();
		_copAnimator = GetComponent<Animator> ();
        _weaponRotation = Weapon.GetComponent<PixelRotation>();

		Angle = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		RotateWeapon ();
		Shoot ();
	}

	private void RotateWeapon()
	{
		if (!_weaponAnimator.GetCurrentAnimatorStateInfo(0).IsTag("shooting"))
		{
			var pos = Camera.main.WorldToScreenPoint(Weapon.position);
			var dir = Input.mousePosition - pos;
			Angle = Mathf.RoundToInt(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
			
			if (Angle > MaxAngle)
			{
				Angle = MaxAngle;
			}
			else if (Angle < MinAngle)
			{
				Angle = MinAngle;
			}

            _weaponRotation.Angle = Angle;
		}
	}

	private void Shoot()
	{
		if (Input.GetMouseButtonUp(0) &&
		    !_weaponAnimator.GetCurrentAnimatorStateInfo(0).IsTag("shooting") &&
		    !_copAnimator.GetCurrentAnimatorStateInfo(0).IsTag("shooting"))
		{
			_copAnimator.SetTrigger("shoot");
			_weaponAnimator.SetTrigger("shoot");
		}
	}

	private Animator _copAnimator;
	private Animator _weaponAnimator;
    private PixelRotation _weaponRotation;
}
