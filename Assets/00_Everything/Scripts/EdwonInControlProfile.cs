using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InControl
{
	public class EdwonInControlProfile : UnityInputDeviceProfile
	{
		public EdwonInControlProfile()
		{
			Name = "FPS Keyboard/Mouse";
			Meta = "A keyboard and mouse combination profile appropriate for FPS.";

			SupportedPlatforms = new[]
			{
				"Windows",
				"Mac",
				"Linux"
			};

			Sensitivity = 1.0f;
			LowerDeadZone = 0.0f;

			ButtonMappings = new[]
			{
				new InputControlMapping
				{
					Handle = "Fire",
					Target = InputControlType.Action1,
					Source = MouseButton0
				},
				new InputControlMapping
				{
					Handle = "AltFire",
					Target = InputControlType.Action2,
					Source = KeyCodeButton( KeyCode.RightShift)
				},
				new InputControlMapping
				{
					Handle = "Middle",
					Target = InputControlType.Action3,
					Source = KeyCodeButton( KeyCode.E )
				},
				new InputControlMapping
				{
					Handle = "Jump",
					Target = InputControlType.Action4,
					Source = KeyCodeButton( KeyCode.Space )
				},
				new InputControlMapping
				{
					Handle = "Left Trigger",
					Target = InputControlType.LeftTrigger,
					Source = KeyCodeButton(KeyCode.Q)
				},
				new InputControlMapping
				{
					Handle = "Right Trigger",
					Target = InputControlType.RightTrigger,
					Source = KeyCodeButton(KeyCode.RightAlt)
				},
				new InputControlMapping
				{
					Handle = "Right Bumper",
					Target = InputControlType.RightBumper,
					Source = KeyCodeButton(KeyCode.RightCommand)
				},
				new InputControlMapping
				{
					Handle = "Left Bumper",
					Target = InputControlType.LeftBumper,
					Source = KeyCodeButton(KeyCode.LeftCommand)
				},
				new InputControlMapping
				{
					Handle = "DPadDown",
					Target = InputControlType.DPadDown,
					Source = KeyCodeButton(KeyCode.P)
				}


			};

			AnalogMappings = new[]
			{

				new InputControlMapping
				{
					Handle = "Move X",
					Target = InputControlType.LeftStickX,
					Source = KeyCodeAxis( KeyCode.A, KeyCode.D )
				},
				new InputControlMapping
				{
					Handle = "Move Y",
					Target = InputControlType.LeftStickY,
					Source = KeyCodeAxis( KeyCode.S, KeyCode.W )
				},
				new InputControlMapping
				{
					Handle = "Look X",
					Target = InputControlType.RightStickX,
					Source = KeyCodeAxis( KeyCode.LeftArrow, KeyCode.RightArrow )
				},
				new InputControlMapping
				{
					Handle = "Look Y",
					Target = InputControlType.RightStickY,
					Source = KeyCodeAxis( KeyCode.DownArrow, KeyCode.UpArrow )
				},
				new InputControlMapping
				{
					Handle = "Look Z",
					Target = InputControlType.ScrollWheel,
					Source = MouseScrollWheel,
					Raw    = true
				}
			};
		}
	}
}

