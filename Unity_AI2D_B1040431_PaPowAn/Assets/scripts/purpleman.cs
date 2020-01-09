using UnityEngine;                  //引用 Unity API - API 倉庫
using UnityEngine.Events;           //引用 事件 API
using UnityEngine.UI;               //引用 事件 API

public class purpleman : MonoBehaviour     //類別 類別名稱
{
    #region 欄位
    public int speed = 10;          //整數
    public float jump = 2.5f;       //浮點數
    public string purplemanName = "purple";  //字串
    public bool pass = false;       //布林值 - true/false
    public bool isGround;

    public UnityEvent onEat;

    private Rigidbody2D r2d;
    private Transform tra;
    private AudioSource aud;
    private Animator ani;

    [Header("血量")]
    public float hp = 100;

    public Image hpBar;
    private float hpMax;

    public GameObject final;
    #endregion

    #region 事件
    //事件 : 在特定時間點會以指定頻率執行的方法     
    //開始事件 : 遊戲開始時執行一次
    void Start()
    {
        //泛型<T>
        
        r2d = GetComponent<Rigidbody2D>();
        tra = GetComponent<Transform>();
        aud = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();

        hpMax = hp;
    }

    // 更新事件 : 每秒執行約 60 次
    void Update()
    {
        if (Input.GetKey(KeyCode.D)) Turn();
        if (Input.GetKey(KeyCode.A)) Turn(180);
    }

    //固定更新事件 : 每禎 0.002 秒
    private void FixedUpdate()
    {
        Run();  // 呼叫方法
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
        Debug.Log("碰到東西了" + collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "能量球")
        {
            Destroy(collision.gameObject);    //刪除
            onEat.Invoke();                   //呼叫事件
        }
    }
    #endregion

    #region 方法
    /// <summary>
    /// 跑
    /// </summary>
    private void Run()
    {
        r2d.AddForce(new Vector2(speed * Input.GetAxis("Horizontal"), 0));
    }

    /// <summary>
    /// 跳躍
    /// </summary>
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            isGround = false;
            r2d.AddForce(new Vector2(0, jump));
        }
    }

    //參數語法 : 類型 名稱 預設值
    /// <summary>
    /// 轉彎
    /// </summary>
    /// <param name="direction">方向，左轉為 180，右轉為 0</param>
    private void Turn(int direction = 0)
    {
        transform.eulerAngles = new Vector3(0, direction, 0);
    }

    public void EnemyAtk(float damage)
    {
        hp -= damage;
        hpBar.fillAmount = hp / hpMax;

        //死亡
        if (hp <= 0)
        {
            final.SetActive(true);

            this.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;      //摧毀腳本
        }

    }
    #endregion
}