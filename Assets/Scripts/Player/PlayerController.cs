using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Tower;

[RequireComponent ( typeof ( Rigidbody2D ) )]
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public float moveSpeed = 1;
    public float jumpForce = 50;
    public LayerMask groundLayer;
    private float checkDistance = 5; // Distance to check for ground

    private Rigidbody2D rb;
    private float moveInputX;
    private Vector2 lastPos;

    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer playerSprite;

    [Header ( "UI" )]
    int money = 50;
    int lives = 100;
    public Text livesText;
    public Text moneyText;
    public Image selectedTowerSprite;

    [Header ( "Building" )]
    public GameObject[] towers;
    GameObject selectedTower;
    Tower selectedTowerT;
    public SpriteRenderer newTowerSprite;
    public SpriteRenderer newTowerShine;
    float direction = 1;

    private void Awake ()
    {
        instance = this;
    }

    void Start ()
    {
        rb = this.GetComponent<Rigidbody2D> ();
        lastPos = transform.position;
        changeMoney ( 0 );
        changeLives ( 0 );
        selectTower ( 1 );
    }


    void Update ()
    {
        moveInputX = Input.GetAxisRaw ( "Horizontal" );

        anim.SetBool ( "isWalking" , moveInputX != 0 );
        anim.SetBool ( "isJumping" , rb.velocity.y > 0.1f );// !IsGrounded () );

        if ( moveInputX < 0 )
        {
            playerSprite.flipX = true;
            direction = -1;
        }
        if ( moveInputX > 0 )
        {
            playerSprite.flipX = false;
            direction = 1;
        }

        transform.rotation = Quaternion.identity;

        if ( Input.GetKeyDown ( KeyCode.Space ) && IsGrounded () )
        {
            Jump ();
        }

        if ( Input.GetKeyDown ( KeyCode.Alpha1 ) )
            selectTower ( 1 );
        if ( Input.GetKeyDown ( KeyCode.Alpha2 ) )
            selectTower ( 2 );
        if ( Input.GetKeyDown ( KeyCode.Alpha3 ) )
            selectTower ( 3 );
        if ( Input.GetKeyDown ( KeyCode.Alpha4 ) )
            selectTower ( 4 );
        if ( Input.GetKeyDown ( KeyCode.Alpha5 ) )
            selectTower ( 5 );
        if ( Input.GetKeyDown ( KeyCode.Alpha6 ) )
            selectTower ( 6 );
        if ( Input.GetKeyDown ( KeyCode.Alpha7 ) )
            selectTower ( 7 );
        if ( Input.GetKeyDown ( KeyCode.Alpha8 ) )
            selectTower ( 8 );
        if ( Input.GetKeyDown ( KeyCode.Alpha9 ) )
            selectTower ( 9 );
        if ( Input.GetKeyDown ( KeyCode.Tab ) )
            buildTower ();
        if ( Input.GetKeyDown ( KeyCode.Return ) )
            buildTower ();

        lastPos = transform.position;
        placeNewTowerShine ();
    }

    void FixedUpdate ()
    {
        rb.velocity = new Vector2 ( moveInputX * moveSpeed , rb.velocity.y );
        // rb.transform.position = new Vector3 ( rb.transform.position.x , rb.transform.position.y , 0 );
    }

    private bool IsGrounded ()
    {
        Debug.DrawRay ( transform.position , Vector3.down * checkDistance , Color.red , 100f , false );
        RaycastHit2D hit = Physics2D.Raycast ( transform.position , Vector2.down , checkDistance , groundLayer );
        if ( hit.collider != null )
        {
            return true;
        }
        if ( Mathf.Abs ( lastPos.y ) - Mathf.Abs ( transform.position.y ) < 0.05f && rb.velocity.y <= 0.1f )
            return true;
        return false;
    }

    private void Jump ()
    {
        rb.velocity = new Vector2 ( rb.velocity.x , jumpForce );
    }

    public void changeMoney ( int amount )
    {
        money += amount;
        moneyText.text = "" + money;
    }

    public void changeLives ( int amount )
    {
        lives += amount;
        livesText.text = "" + lives;
    }

    private void selectTower ( int i )
    {
        if ( i > towers.Length ) return;
        selectedTower = towers[ i - 1 ];
        selectedTowerT = selectedTower.GetComponent<Tower> ();
        selectedTowerSprite.sprite = selectedTower.GetComponent<Tower> ().menuSprite;
        newTowerSprite.sprite = selectedTower.GetComponent<Tower> ().menuSprite;
        newTowerShine.sprite = selectedTower.GetComponent<Tower> ().menuSprite;
    }

    Tile currentTile
    {
        get
        {
            return Tile.getTile ( transform.position.toGridPos () );
        }
    }

    void towerSpriteColorRed ()
    {
        newTowerSprite.color = new Color ( 1 , 0 , 0 , 0.25f );
    }

    Tile tile;

    private void placeNewTowerShine ()
    {
        tile = Tile.getTile ( transform.position + 4 * Vector3.right + 8 * Vector3.right * direction );

        newTowerSprite.transform.localPosition = Vector3.right * direction * 8;
        if ( tile == null )
        {
            towerSpriteColorRed ();
            return;
        }

        switch ( selectedTowerT.placement )
        {
            case placements.air:
                if ( tile.type == Tile.types.none )
                    towerSpriteColorRed ();
                break;

            case placements.aboveGround:
                /*
                for ( int i = 0 ; i < 100 ; i++ )
                {
                    if ( tile == null ) return;
                    else
                        tile = Tile.getTileBelow ( tile );
                    Debug.Log ( tile.position );
                    Debug.Log ( tile.type );
                }
                */
                while ( tile.type != Tile.types.ground )
                    if ( tile == null ) return;
                    else
                        tile = Tile.getTileBelow ( tile );
                tile = Tile.getTileAbove ( tile );
                if ( tile.type != Tile.types.none )
                    return;
                break;

            case placements.ground:
                while ( tile.type != Tile.types.ground )
                    if ( tile == null ) return;
                    else
                        tile = Tile.getTileBelow ( tile );
                
                break;

            case placements.ceiling:
                while ( tile.type != Tile.types.ceiling )
                    if ( tile == null ) return;
                    else
                        tile = Tile.getTileAbove ( tile );
                tile = Tile.getTileBelow ( tile );
                if ( tile.type != Tile.types.none )
                    return;
                break;
        }

        newTowerSprite.transform.position = tile.position;
        if ( tile.content != null )
            towerSpriteColorRed ();
        else
            newTowerSprite.color = new Color ( 1 , 1 , 1 , 0.25f );
    }

    private void buildTower ()
    {
        if ( money < selectedTower.GetComponent<Tower> ().price )
            return;
        if ( tile == null )
            return;
        if ( tile.content != null )
            return;

        GameObject go = Instantiate ( selectedTower );
        Tower tower = go.GetComponent<Tower> ();
        changeMoney ( -tower.price );
        go.transform.position = tile.transform.position;
        tile.content = go;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Place_tower");
    }

    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * checkDistance);
    }*/
}