using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class BattleDialogUi {
    public const int MAX_LENGTH = 57;
    public CanvasRenderer DialogBox;
    protected CanvasGroup CanvasGroup;
	public Text DialogText;

    //Moved to BattleManager
    //protected DeltaTimeHelper deltaTime = new DeltaTimeHelper(0,15);
    protected int delayedCloseCount = 0;
    protected StringBuilder currentText;
    protected Char[] textArr;
    protected int currentPrintIndex = 0;
    protected bool isActive = false;
    public String text {
        get { return String.Join(" ",textArr); }
        set { 
            currentText = new StringBuilder();
            CanvasGroup.alpha = 0.1f;
            textArr = value.ToCharArray();
            currentPrintIndex = 0;
            DialogText.text = "";
            DialogBox.gameObject.SetActive(true);
            isActive = true;
        }
    }

    public void init() {
        CanvasGroup = DialogBox.GetComponent<CanvasGroup>();
        CanvasGroup.alpha = 0;
    }

    public void open(string s) {
        this.text = s;
    }

    //true when closed, false when open
    public bool delayedClose(int delay) {
        if(delay > this.delayedCloseCount) {
            //if(deltaTime.doRun()) {
                this.delayedCloseCount++;
                if(this.delayedCloseCount == delay) {
                    close();
                    return true;
                }
            //}
            return false;
        }
        return true;
    }
    public bool delayedAnimatedClose(int delay) {
        if(isActive && delay > this.delayedCloseCount) {
            this.delayedCloseCount++;
            return false;
        } else {
            if(CanvasGroup.alpha > 0) {
                CanvasGroup.alpha -= 0.1f;
                return false;
            } else {
                this.delayedCloseCount = 0;
                close();
                return true;
            }
        }
    }
    public void close() {
        if(!needsUpdate()) {
            currentText = null;
            isActive = false;
            DialogBox.gameObject.SetActive(false);
        }
    }
    //If return false, means there's no more text to update
    public bool needsUpdate() {
        return currentText != null && currentText.Length < textArr.Length;
    }
    public bool updateText() {
        if(currentText != null) {
            if(currentText.Length < textArr.Length) {
                if(CanvasGroup.alpha >= 1.0f) {
                    //if(deltaTime.doRun()) {
                    currentText.Append(textArr[currentPrintIndex++]);
                    DialogText.text = currentText.ToString();
                    //}
                } else {
                    CanvasGroup.alpha += 0.1f;
                }
                return true;
            }
            DialogText.text = currentText.ToString();
        }
        return false;
    }
}