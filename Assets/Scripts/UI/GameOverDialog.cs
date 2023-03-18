using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverDialog : Dialog
{
    public GameObject powerPanel;

    public override void Show()
    {
        base.Show();
        if (powerPanel)
            powerPanel.SetActive(false);
    }
}
