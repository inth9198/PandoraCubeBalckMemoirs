using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer Sprite;
    public SpriteRenderer Sprite1;

    Vector3 inputPos;
    Vector2 dir;
    int h;
    public GameObject mousePointPrefab;
    
    public CharacterStat stat;

    GameObject tempObj;
    GameObject tempObj2;
    GameObject sobj;
    GameObject mousePoint;

    public GameObject enemyObjPrefab;
    List<GameObject> enemyList;

    bool isCol;
    bool isMouse;
    bool keyDown;
    bool isAttackG;
    void Awake()
    {
        anim = GetComponent<Animator>();
        Sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        isCol = false;
        keyDown = false;
        isAttackG = false;
        enemyList = new List<GameObject>();
        mousePoint = Instantiate(mousePointPrefab);
        mousePoint.SetActive(false);
        makeEnemy();

    }
    void makeEnemy()
    {
        for (int aa = 0; aa < 10; aa++)
        {
            GameObject enemyObject = Instantiate(enemyObjPrefab, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 15), Quaternion.identity) as GameObject;
            enemyList.Add(enemyObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        move();
        animate();
        action();
        detect();
    }
    void detect()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy;
        if (enemyList.Count == 0) return;
        closestEnemy = enemyList[0];
        foreach (GameObject col in enemyList)
        {
            if(col.gameObject.tag == "Enemy")
            {
                float distance = Vector3.Distance(col.transform.position, transform.position);

                if (distance<closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = col.gameObject;//사거리에 들어오는 적중 가장 가까운적
                }
            }
        }
        //Debug.Log(closestEnemy.transform.position);
        float Ndistance = Vector3.Distance(closestEnemy.transform.position, transform.position);
        Debug.Log(Ndistance);

        if (Ndistance<15.1)
        {
            anim.SetTrigger("Attack");
            dir = new Vector2(0, 0);
            enemyList.Remove(closestEnemy);
            Destroy(closestEnemy);
        }

    }

    void move()
    {
        if (Input.GetMouseButtonDown(1))//한번 누르지만 계속 계속 받는 구나..
        {
            isAttackG = false;
            tempObj = GameObject.FindWithTag("Mouse");


            mousePoint.SetActive(true);
            inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            inputPos.z += 15;
            mousePoint.transform.position = inputPos;

        }

        RaycastHit2D hit = Physics2D.Raycast(inputPos, Vector3.zero, Mathf.Infinity, 512);//레이어 object
        tempObj = GameObject.FindWithTag("Mouse");
        if (tempObj != null)
        {
            //Debug.Log(tempObj.transform.position);
            dir = tempObj.transform.position - transform.position;
            transform.position = Vector2.MoveTowards(transform.position, tempObj.transform.position, Time.deltaTime * stat.moveSpeed);
        }
        else if (tempObj2 != null&&isAttackG==true)
        {
            Debug.Log(tempObj2.transform.position);
            dir = tempObj2.transform.position - transform.position;
            transform.position = Vector2.MoveTowards(transform.position, tempObj2.transform.position, Time.deltaTime * stat.moveSpeed);
        }
        else
        {
            dir = new Vector2(0, 0);
        }

    }

    void OnCollisionEnter2D(Collision2D o)
    {
        isCol = true;
        //Debug.Log("아코!");
        if (o.gameObject == mousePoint)
        {
            mousePoint.SetActive(false);
        }
        if (o.gameObject.tag == "Enemy")
        {
           OnDamaged(o.transform.position);                
        }
    }
    void OnDamaged(Vector2 tagetPos)
    {
        Sprite.color = new Color(1, 1, 1, 0.4f);
        int xd = transform.position.x - tagetPos.x>0?1:-1;
        int yd = transform.position.y - tagetPos.y > 0 ? 1 : -1;
        stat.HP--;
        Invoke("OffDamaged", 1);
    }
    void OffDamaged()
    {
        Sprite.color = new Color(1, 1, 1, 1);
    }

    void OnCollisionExit2D(Collision2D o)
    {
        isCol = false;
    }

    void animate()
    {
        //아트리소스 오면 없애버림
        if (dir.x > 0.1f) { h = 1; Sprite.flipX = true; }
        else if (dir.x < 0.1f) { h = -1; Sprite.flipX = false; }
        else h = 0;

        if(Mathf.Approximately(dir.x,0)&& Mathf.Approximately(dir.y, 0))
        {
            anim.SetBool("isMove", false);
        }
        else
        {
            anim.SetBool("isMove", true);
        }
        Vector2 normal = dir.normalized;
        anim.SetFloat("xDir", normal.x);
        anim.SetFloat("yDir", normal.y);
    }
    void action()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

        }
        if (Input.GetKeyDown(KeyCode.W))
        {

        }
        if (Input.GetKeyDown(KeyCode.E))
        {

        }
        if (Input.GetKeyDown(KeyCode.R))
        {

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            keyDown = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            mousePoint.SetActive(false);
            keyDown = false;
            isAttackG = false;
        }
        if(keyDown == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePoint.SetActive(false);
                isAttackG = true;
            }
            if (enemyList.Count > 0&& isAttackG == true)
            {
                enemyList.Sort
                (
                    (a, b) =>
                        {
                            return Vector3.Distance(a.transform.position, transform.position).CompareTo(Vector3.Distance(b.transform.position, transform.position));
                        }
                );

                tempObj2 = enemyList[0];
                sobj = enemyList[0];
                Debug.Log(enemyList.Count);
            }
            else
            {
            }
        }

    }
}
