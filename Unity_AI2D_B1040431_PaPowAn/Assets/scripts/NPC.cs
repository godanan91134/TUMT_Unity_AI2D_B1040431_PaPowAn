using UnityEngine;
using UnityEngine.UI;   //引用 介面 API
using System.Collections;

public class NPC : MonoBehaviour
{
    #region 欄位
    // 定義列舉
    //修飾詞 列舉 列舉名稱 { 列舉內容, .... }
    public enum state
    {
        //一般、尚未完成、完成
        normal, notcomplete, complete
    }
    //使用列舉
    //修飾詞 類型 名稱
    public state _state;

    [Header("對話")]
    public string sayStart = "英雄你終於來了，拜託你幫我們打倒敵人，拯救這個星球!!";
    public string sayNotComplete = "拜託了英雄......這個星球需要你";
    public string sayComplete = "英雄謝謝你!感謝你拯救了這個星球";
    [Header("對話速度")]
    public float speed = 1.5f;
    [Header("任務相關")]
    public bool complete;
    public int countPlayer;
    public int countFinish = 10;
    [Header("介面")]
    public GameObject objCanvas;
    public Text textSay;
    #endregion

    public AudioClip soundSay;

    private AudioSource aud;

    public GameObject final;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }
    //2D 觸發事件
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //如果碰到物件為"PP"
        if (collision.name == "紫人")
        {
            Say();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "紫人")
        {
            SayClose();
        }
    }

    /// <summary>
    /// 對話:打字效果
    /// </summary>
    private void Say()
    {
        //畫布.顯示
        objCanvas.SetActive(true);
        StopAllCoroutines();

        if (countPlayer >= countFinish)
        {
            _state = state.complete;

            Invoke("End", 5f);
        }

        //文字介面.文字 = 對話1
        switch (_state)
        {
            case state.normal:
                StartCoroutine(ShowDialog(sayStart));               //開始對話
                _state = state.notcomplete;
                break;
            case state.notcomplete:
                StartCoroutine(ShowDialog(sayNotComplete));         //未完成對話
                break;
            case state.complete:
                StartCoroutine(ShowDialog(sayComplete));            //完成對話
                break;
        }
    }

    private IEnumerator ShowDialog(string say)
    {
        textSay.text = "";                               //清空文字

        for (int i = 0; i < say.Length; i++)             //迴圈跑對話.長度
        {
            textSay.text += say[i].ToString();           //累加每個文字
            aud.PlayOneShot(soundSay, 1.5f);
            yield return new WaitForSeconds(speed);      //等待
        }
    }

    /// <summary>
    /// 關閉對話
    /// </summary>
    private void SayClose()
    {
        objCanvas.SetActive(false);
        StopAllCoroutines();
    }

    /// <summary>
    /// 玩家取得道具
    /// </summary>
    public void PlayerGet()
    {
        countPlayer++;
    }

    public void End()
    {
        final.SetActive(true);
    }
}
