using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class DialogOption {
    public string buttonText;
    //public delegate void DialogSelected(string s);
    public DialogOption(string buttonText) {
        this.buttonText = buttonText;
    }
}