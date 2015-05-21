using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LoggingMonoBehaviour : MonoBehaviour 
{
    
    public Text LogOutputText;

    public void Log(string text)
    {
        if (LogOutputText != null)
        {
            var lines = LogOutputText.text.Split(new char[]{'\n'}, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Count() > 19)
            {
                LogOutputText.text = String.Join("\n", lines.Skip(lines.Count() - 19).ToArray()) + "\n";
            }
            LogOutputText.text += text + "\n";
        }
        Debug.Log(text);
    }

}

