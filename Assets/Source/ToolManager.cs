using System;
using System.IO;

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
		public static int highScore;
		private static StreamWriter sw;
		private static StreamReader sr;
		public static void readScore(){
			if(File.Exists("hs.hs")){
				sr=new StreamReader("hs.hs");
				highScore= Convert.ToInt32(sr.ReadLine());
				sr.Close();
			}
			else{
				File.Create("hs.hs");
				highScore=0;
			}
		}
		public static void writeScore(int newScore){
			if(!File.Exists("hs.hs")){
				File.Create("hs.hs");
			}
			sw=new StreamWriter("hs.hs");
			sw.WriteLine(newScore.ToString());
			sw.Close();
			highScore=newScore;
		}
	}
}

