using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Samurais
{
    public class Health : MonoBehaviour {

        const int maxValue = 3;
	    public int value { get; private set; }

	    void Start () {
            value = maxValue;
            UpdateHealthBar();
        }

        void UpdateHealthBar() {
            Image _image = GetComponent<Image>();
            _image.fillAmount = (float)value / maxValue;
        }

        public void TakeDamage() {
            value -= 1;
            UpdateHealthBar();
        }
    }
}
