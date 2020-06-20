using UnityEngine;

public class PacmanMove : MonoBehaviour
{
    private Vector2 dest = Vector2.zero;
    public float speed = 0.35f;

    //如果玩家购买吸铁石，就把ISMagent设置为true
    public static bool isMagnet = false;
    public static bool faceUp = false;
    public static bool faceDown = false;
    public static bool faceRight = false;
    public static bool faceLeft = false;


    private void Start()
    {

        dest = transform.position;
    }
    void Update()
    {

        if (isMagnet)
        {
            //检测以玩家为球心半径的范围内的所有的带有碰撞器的游戏对象（这个要用到phere collider，但是这个不能和box collider2D，所以没办法实现）

            //  Collider[] colliders = Physics.OverlapSphere(this.transform.position, 50);

            //foreach (var item in colliders)
            //{
            //    //    如果是豆子
            //    if (item.tag.Equals("Coin"))
            //    {
            //        //    让豆子的开始移动
            //        item.GetComponent<CoinMoveController>().isCanMove = true;
            //    }
            //}
            var coins = GameObject.FindGameObjectsWithTag("Coin");

            foreach (var item in coins)
            {
                if (Vector2.Distance(item.transform.position, transform.position) < 4)
                {
                    item.GetComponent<CoinMoveController>().isCanMove = true;
                }
            }

        }
    }

    private void FixedUpdate()
    {
        Vector2 temp = Vector2.MoveTowards(transform.position, dest, speed);

        GetComponent<Rigidbody2D>().MovePosition(temp);

        if ((Vector2)transform.position == dest)
        {
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Valid(Vector2.up))
            {
                dest = (Vector2)transform.position + Vector2.up;
                faceUp = true;
                faceRight = false;
                faceLeft = false;
                faceDown = false;


            }
            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && Valid(Vector2.down))
            {
                dest = (Vector2)transform.position + Vector2.down;
                faceUp = false;
                faceRight = false;
                faceLeft = false;
                faceDown = true;

            }
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && Valid(Vector2.left))
            {
                dest = (Vector2)transform.position + Vector2.left;
                faceUp = false;
                faceRight = false;
                faceLeft = true;
                faceDown = false;

            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && Valid(Vector2.right))
            {
                dest = (Vector2)transform.position + Vector2.right;
                faceUp = false;
                faceRight = true;
                faceLeft = false;
                faceDown = false;
            }
                Vector2 dir = dest - (Vector2)transform.position;

                GetComponent<Animator>().SetFloat("DirX", dir.x);

                GetComponent<Animator>().SetFloat("DirY", dir.y);
            }

    }
    private bool Valid(Vector2 dir)
    {
        
        Vector2 pos = transform.position;
        
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
      
        return (hit.collider == GetComponent<Collider2D>());
    }
}
