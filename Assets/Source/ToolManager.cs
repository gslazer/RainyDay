using System;

namespace AssemblyCSharp
{
	public static class ToolManager
	{
		public static float gameTime;
		public static float deltaTime;
		public static int wind; // 1: 우풍 , -1 : 좌풍 
		public static void setDeltaTime(float deltaTime){
			ToolManager.deltaTime=deltaTime;
		}
		public static bool alive;
	}
}

