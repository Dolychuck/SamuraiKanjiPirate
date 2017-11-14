using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
public class FileWriter {
	
	private static string PATH_PREFIX = "Assets\\Resources\\";


	public static void WriteData(string data, string fileName, string stats) {
		Debug.Log (data);
        
		if (!File.Exists (PATH_PREFIX + fileName)) {
			string createText = data + Environment.NewLine;
			File.WriteAllText (PATH_PREFIX + fileName, createText);
		} else {
			string appendText = data + Environment.NewLine;
			File.AppendAllText (PATH_PREFIX + fileName, appendText);
		}
	}
}
