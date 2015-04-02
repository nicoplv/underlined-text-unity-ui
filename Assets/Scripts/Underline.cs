using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// [ExecuteInEditMode]
public class Underline : MonoBehaviour {

	public int underlineStart = 0;
	public int underlineEnd = 0;

	private Text text = null;
	private RectTransform textRectTransform = null;
	private TextGenerator textGenerator = null;

	private GameObject lineGameObject = null;
	private Image lineImage = null;
	private RectTransform lineRectTransform = null;

	void Start () {
		text = gameObject.GetComponent<Text>();
		textRectTransform = gameObject.GetComponent<RectTransform>();
		textGenerator = text.cachedTextGenerator;
		lineGameObject = new GameObject("Underline");
		lineImage = lineGameObject.AddComponent<Image>();
		lineImage.color = text.color;
		lineRectTransform = lineGameObject.GetComponent<RectTransform>();
		lineRectTransform.SetParent(transform, false);
		lineRectTransform.anchorMin = textRectTransform.pivot;
		lineRectTransform.anchorMax = textRectTransform.pivot;
	}
	
	void Update () {
		if(textGenerator.characterCount<0)
			return;
		UICharInfo[] charactersInfo = textGenerator.GetCharactersArray();
		if(!(underlineEnd>underlineStart && underlineEnd<charactersInfo.Length))
			return;
		UILineInfo[] linesInfo = textGenerator.GetLinesArray();
		if(linesInfo.Length<1)
			return;
		float height = linesInfo[0].height;
		Canvas canvas = gameObject.GetComponentInParent<Canvas>();
		float factor = 1.0f/canvas.scaleFactor;
		lineRectTransform.anchoredPosition = new Vector2(
			factor*(charactersInfo[underlineStart].cursorPos.x+charactersInfo[underlineEnd].cursorPos.x)/2.0f,
			factor*(charactersInfo[underlineStart].cursorPos.y-height/1.0f)
			);
		lineRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, factor*Mathf.Abs(charactersInfo[underlineStart].cursorPos.x-charactersInfo[underlineEnd].cursorPos.x));
		lineRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height/10.0f);
	}
}
