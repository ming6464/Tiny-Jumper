using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public Player playerPrefab;
    public Platform platformPrefab;
    public float minSpawnX, maxSpawnX, minSpawnY, maxSpawnY,defaultX;
    public CamController cam;
    Platform m_platformClone;
    Player m_player;
    int m_score,m_bestScore;
    public override void Awake()
    {
        MakeSingleton(false);
    }
    public override void Start()
    {
        base.Start();
        StartCoroutine(PlatformInit());
        m_bestScore = PrefConsts.Ins.BestScore;
        AudioController.Ins.PlayMusic(AudioController.Ins.backgroundMusics[0], true);
    }   
    IEnumerator PlatformInit()
    {
        float d1 = defaultX;
        defaultX = 0;
        if (platformPrefab)
            m_platformClone = Instantiate(platformPrefab, new Vector2(defaultX, Random.Range(minSpawnY, maxSpawnY)), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        if (playerPrefab)
        {
            m_player = Instantiate(playerPrefab, new Vector3(defaultX, 0,0), Quaternion.identity);
            m_player.lastPlatformId = m_platformClone.GetInstanceID();
        }
        CreatePlatform();
        CreatePlatform();
        defaultX = d1;
    }

    void CreatePlatform()
    {
        if (platformPrefab && m_player)
            m_platformClone = Instantiate(platformPrefab, new Vector2(m_platformClone.transform.position.x + Random.Range(minSpawnX, maxSpawnX), Random.Range(minSpawnY, maxSpawnY)), Quaternion.identity);
    }
    public void CreatePlatformAndMoveCam(float distance)
    {
        CreatePlatform();
        float dis = Mathf.Abs(defaultX - distance);
        defaultX = distance;
        if (cam) cam.Run(dis);
/*        if (deathZoneBottom) deathZoneBottom.Run(dis);
        if (deathZoneLeft) deathZoneLeft.Run(dis);*/
    }
    public void PlayGame()
    {
        AudioController.Ins.StopPlayMusic();
        AudioController.Ins.PlayBackgroundMusic();
        SceneManager.LoadScene(TagConsts.SceneGamePlay);
    }
    public void Home()
    {
        AudioController.Ins.StopPlayMusic();
        AudioController.Ins.PlayBackgroundMusic();
        SceneManager.LoadScene(TagConsts.SceneHome);
    }
    public void Death()
    {
        UIManager.Ins.ShowGameOverDialog(m_score,m_bestScore);

    }
    public void IncreaseScore()
    {
        AudioController.Ins.PlaySound(AudioController.Ins.getScore);
        m_score++;
        if (m_score > m_bestScore)
        {
            PrefConsts.Ins.BestScore = m_score;
            m_bestScore = m_score;
        }
            
        UIManager.Ins.SetScoreText(m_score);
    }
    public void Achievement()
    {
        UIManager.Ins.ShowAchievementDialog(m_bestScore);
    }
    public void Help()
    {
        UIManager.Ins.ShowHelpDialog();
    }
    public void ExitAchievement()
    {
        UIManager.Ins.CloseAchievementDialog();
    }
    public void ExitHelp()
    {
        UIManager.Ins.CloseHelpDialog();
    }

}
