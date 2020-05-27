using System.Collections;
using UnityEngine;

public class ScreenCollider : MonoBehaviour
{
	public enum collideTypeEnum{
		bound = 0,
		destory = 1
	}
	public collideTypeEnum collideType;

	void Awake () {
		Resize();
	}

	void Resize(){
		Vector3 max = Camera.main.ViewportToWorldPoint (new Vector3 (1f, 1f, 7.7f));
        transform.localScale = new Vector3(max.x*2, max.z*2, 1f);
	}

	void OnCollisionEnter(Collision collision){
		switch (collideType) {
			case collideTypeEnum.destory:
				Destroy (collision.gameObject);
			break;

			default:
			break;
		}
	}


}