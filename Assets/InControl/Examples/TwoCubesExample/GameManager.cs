using System;
using UnityEngine;
using InControl;


namespace TwoCubesExample
{
	public class GameManager : MonoBehaviour
	{
		public bool isSinglePlayer = true;

		public static GameManager instance;

		void Awake()
		{
			instance = this;
		}
	}
}