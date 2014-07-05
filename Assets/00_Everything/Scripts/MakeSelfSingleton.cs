using UnityEngine;
using System.Collections;

public class MakeSelfSingleton : Singleton<MakeSelfSingleton> {
	
//	:: Second, include this variable so you can access the instance of your singleton.
	public static MakeSelfSingleton Instance {
		get {
			return ((MakeSelfSingleton)mInstance);
		} set {
			mInstance = value;
		}
	}
}
	
//	:: Third, Voila! Now you can access the instance of your singleton with YOURTYPE.Instance 

