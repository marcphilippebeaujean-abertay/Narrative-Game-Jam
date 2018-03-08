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
    private AudioSource audioSource;
    private float writingTimer = 0.0f;
    private string currentString = "";
    private bool writing = false;
    private int curChar = 0;
    private float textElapseTimer = 0.0f;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
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
        audioSource.Stop();
        curChar = 0;
        currentString = DialogueText[DialogueID];
        audioSource.PlayOneShot(DialogueAudio[DialogueID]);
        writing = true;
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
        }
        else
        {
            writing = false;
        }
    }
}
