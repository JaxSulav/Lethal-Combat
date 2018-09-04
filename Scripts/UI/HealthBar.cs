using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public Image healthbar;
	public Text ratioText;

	public void Start()
	{
		UpdateHealthBar ();
	}

	public void UpdateHealthBar()
	{
		float ratio = gameObject.GetComponent<PlayerHealth> ().currentHealth / gameObject.GetComponent<PlayerHealth> ().maxHealth;
		healthbar.rectTransform.localScale = new Vector3 (ratio, 1, 1);
		ratioText.text = (ratio*100).ToString() +'%';
	}
}
