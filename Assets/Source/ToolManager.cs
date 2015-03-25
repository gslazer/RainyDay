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
		private static string filePath;

		public static void setFilePath(string path){
			filePath=path;
		}


		public static void readScore(){
			if(File.Exists(filePath)){
				sr=new StreamReader(filePath);
				highScore= Convert.ToInt32(sr.ReadLine());
				sr.Close();
			}
			else{
				File.Create(filePath);
				highScore=0;
			}
		}
		public static void writeScore(int newScore){
			if(!File.Exists(filePath)){
				File.Create(filePath);
			}
			sw=new StreamWriter(filePath);
			sw.WriteLine(newScore.ToString());
			sw.Close();
			highScore=newScore;
		}
	}
}

