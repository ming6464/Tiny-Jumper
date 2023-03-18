using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentText;

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void UpdateDialog(string title, string content)
    {
        if (titleText)
            titleText.text = title;

        if (contentText)
            contentText.text = content;
    }

    public virtual void UpdateDialog()
    {

    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
