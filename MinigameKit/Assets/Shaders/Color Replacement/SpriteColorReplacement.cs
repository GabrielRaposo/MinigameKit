using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteColorReplacement : MonoBehaviour 
{
	[Serializable]
	public struct ReplacedColor
	{
        public ReplacedColor(Color original, Color replaced)
        {
            this.original = original;
            this.replaced = replaced;
        }

		public Color original;
		public Color replaced;
	}

	public List<ReplacedColor> replacedColors = new List<ReplacedColor>();

    private Material mat;

	public void Apply()
	{
		if (!mat) mat = GetComponent<Renderer>().material;

		var originalColorArray = 
			from col in replacedColors
			select col.original;

		var replacedColorArray = 
			from col in replacedColors
			select col.replaced;

		mat.SetInt("_ArrayLenght", replacedColors.Count);
		mat.SetColorArray("_RepColor", replacedColorArray.ToArray());
        mat.SetColorArray("_OriColor", originalColorArray.ToArray());
    }
}