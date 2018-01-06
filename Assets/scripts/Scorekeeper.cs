using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scorekeeper : MonoBehaviour {
	
	static int curScore;
	static Text scoreUI;

	void Start()
	{
		scoreUI = GetComponent<Text>();
		Reset();
	}

	static public void Score(int point)
	{
		curScore += point;
		scoreUI.text = curScore.ToString();
	}

	static public void Reset()
	{
		curScore = 0;
		scoreUI.text = curScore.ToString();
	}
}