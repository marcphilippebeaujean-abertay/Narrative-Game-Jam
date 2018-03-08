using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Dialogue : MonoBehaviour {

    public List<AudioClip> DialogueAudio;
    public List<string> DialogueText;
    public float WritingTime;
    public float textElapseTime = 0.0f;
    private Text textObject;
    public AudioSource audioSource;
    private float writingTimer = 0.0f;
    private string currentString = "";
    private bool writing = false;
    private int curChar = 0;
    private float textElapseTimer = 0.0f;
    List<int> playedDialogues = new List<int>();

    // Use this for initialization
    void Start () {
        textObject = GameObject.FindGameObjectWithTag("Dialog Text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if(writing)
        {
            writingTimer += Time.deltaTime;
            if (writingTimer > WritingTime)
            {
                WritingDialog();
            }
        }
        else
        {
            if(textElapseTime > textElapseTimer)
            {
                textElapseTimer += Time.deltaTime;
                if(textElapseTime <= textElapseTimer)
                {
                    textObject.text = "";
                    if(currentString.Length > curChar)
                    {
                        writing = true;
                        writingTimer = 0.0f;
                        textElapseTimer = 0.0f;
                    }
                }
            }
        }
	}

    public void PlayDialog(int DialogueID)
    {
        if (DialogueID != 69)
        {
            bool alreadyPlayed = false;
            for (int i = 0; i < playedDialogues.Count; i++)
            {
                if (DialogueID == playedDialogues[i])
                {
                    alreadyPlayed = true;
                }
            }
            if (alreadyPlayed == false)
            {
                Debug.Log("playing dialogue!");
                audioSource.Stop();
                curChar = 0;
                currentString = DialogueText[DialogueID];
                audioSource.PlayOneShot(DialogueAudio[DialogueID]);
                textObject.text = "";
                writing = true;
                playedDialogues.Add(DialogueID);
            }
        }
    }

    void WritingDialog()
    {
        if(currentString.Length > curChar)
        {
            textObject.text += currentString[curChar];
            if (currentString[curChar] == '.' || currentString[curChar] == '?')
            {
                writing = false;
            }
            curChar++;
            writingTimer = 0.0f;
        }
        else
        {
            writing = false;
        }
    }
}
