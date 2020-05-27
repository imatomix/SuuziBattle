using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public int frameRate = 60;

	void Awake () {
        Application.targetFrameRate = frameRate;
	}
}
