using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class DialogSequence {
    public static int ONE_OPTION = 1;
    public static int TWO_OPTIONS = 2;
    public static int FOUR_OPTIONS = 4;
    public string messageText;
    public DialogOption[] options;
    public DialogSequence(string messageText, DialogOption[] dOptions) {
        this.messageText = messageText;
        this.options = dOptions;
    }
}