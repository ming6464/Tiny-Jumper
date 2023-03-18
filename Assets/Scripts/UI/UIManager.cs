using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI scoreText;
    public GameOverDialog gameOverDialog;
    public AchievementDialog achievementDialog;
    public HelpDialog helpDialog;
    public Image powerFillImg;

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public void SetScoreText(int score)
    {
        if(scoreText)
            scoreText.text = score.ToString();
    }

    public void ShowGameOverDialog(int score,int bestScore)
    {
        if (gameOverDialog)
        {
            gameOverDialog.UpdateDialog("Best Score : " + bestScore, score.ToString());
            gameOverDialog.Show();
        }
        
    }
    public void ShowAchievementDialog(int bestScore)
    {
        if (achievementDialog)
        {
            achievementDialog.UpdateDialog("Achievement !", bestScore.ToString());
            achievementDialog.Show();
        }
    }
    public void CloseAchievementDialog()
    {
        if (achievementDialog)
        {
            achievementDialog.Close();
        }
    }
    public void CloseHelpDialog()
    {
        if (helpDialog) helpDialog.Close();

    }
    public void ShowHelpDialog()
    {
        if (helpDialog)
        {
            helpDialog.Show();
        }
    }
    public void BuffPower(float curVal,float totalVal)
    {
        if (powerFillImg)
            powerFillImg.fillAmount = curVal / totalVal;
    }
}
