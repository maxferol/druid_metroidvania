using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightSystem : MonoBehaviour
{
    private FightTree fightTree;
    private FightTree currentSubTree;

    [SerializeField]
    private List<FightTreeNode> attackList;
    [SerializeField]
    private List<GameObject> attackAreas;

    [SerializeField]
    private GameObject attackColliderHolder;

    private PlayerContext context;
    private BoxCollider2D playerCollider;
    private PlayerStateMachine playerStateMachine;

    public Dictionary<string, Attack> triggerNameToAttack;

    [SerializeField]
    private float prepTimer;
    [SerializeField]
    public float attackTimer;
    [SerializeField]
    private float betweenAttacksTimer;

    [SerializeField]
    public bool isAttacking;
    [SerializeField]
    private bool canAttack;

    [SerializeField]
    private bool isOnCd;

    public float tossPower;

    private string currentCombo;

    private void Initialize()
    {
        var basicLightAttackOffset = new Vector3(1.25f, 0.75f, 0);
        var basicLightAttack = new BasicLightAttack(0.1f, 0.3f, 5f, attackAreas[0].GetComponent<Collider2D>(), 20, basicLightAttackOffset);
        var basicHeavyAttackOffset = new Vector3(1.75f, 0.75f, 0);
        var basicHeavyAttack = new BasicHeavyAttack(0.15f, 0.45f, 5f, attackAreas[1].GetComponent<Collider2D>(), 40, basicHeavyAttackOffset);
        var airTossAttackOffset = new Vector3(1.25f, 0.75f, 0);
        attackList = new List<FightTreeNode>() {
            null, //start
            new FightTreeNode(basicLightAttack, float.MinValue, float.MaxValue), //L
            new FightTreeNode(basicHeavyAttack, float.MinValue, float.MaxValue), //R
            };
        triggerNameToAttack = new Dictionary<string, Attack>();
        fightTree = new FightTree(attackList);
        currentSubTree = fightTree;
        context = gameObject.GetComponent<PlayerStateMachine>().context;
        foreach (var fightTreeNode in attackList)
        {
            if ((fightTreeNode != null) && (!triggerNameToAttack.ContainsKey(fightTreeNode._attack._attackArea.name)))
                triggerNameToAttack.Add(fightTreeNode._attack._attackArea.name, fightTreeNode._attack);
        }
        playerCollider = gameObject.GetComponent<BoxCollider2D>();
        playerStateMachine = gameObject.GetComponent<PlayerStateMachine>();
        currentCombo = "";
        canAttack = true;
    }

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        TickTheTimer(ref attackTimer);
        TickTheTimer(ref betweenAttacksTimer);
        TickTheTimer(ref prepTimer);
    }

    private void FixedUpdate()
    {
        //ReactToMouseClick();
        UpdateState();
        ReactToMouseClickNew();

    }

    public void OnDrawGizmos()
    {
        //DrawAttackZone();
    }

    private void ReactToMouseClick()
    {
        if ((attackTimer <= 0) && (playerStateMachine.CurrentState.StateKey != PlayerStateMachine.EPlayerState.Dashing) && (playerStateMachine.CurrentState.StateKey != PlayerStateMachine.EPlayerState.WallSliding))
        {
            if (context.lightAttackPressed)
            {
                ReactToLeftClick();
            }

            else if (context.heavyAttackPressed)
            {
                ReactToRightClick();
            }

            else
            {
                ReactToNoClick();
            }
        }
    }

    private void ReactToMouseClickNew()
    {
        if ((canAttack) && (playerStateMachine.CurrentState.StateKey != PlayerStateMachine.EPlayerState.Dashing) && (playerStateMachine.CurrentState.StateKey != PlayerStateMachine.EPlayerState.WallSliding))
        {
            if (context.lightAttackPressed)
            {
                ReactToLeftClick();
            }

            else if (context.heavyAttackPressed)
            {
                ReactToRightClick();
            }

            else
            {
                //ReactToNoClick();
            }
        }
    }

    private void UpdateState()
    {
        if (isAttacking)
        {
            if (attackTimer <= 0)
            {
                isAttacking = false;
                isOnCd = true;
                betweenAttacksTimer = currentSubTree.Data._attack._cdTime;
                currentSubTree.Data._attack.FinishAttack();
            }
        }

        else if (isOnCd)
        {
            if (betweenAttacksTimer <= 0)
            {
                isOnCd = false;
                canAttack = true;
            }
        }
    }

    private void TickTheTimer(ref float timer)
    {
        if (timer > 0)
            timer -= Time.deltaTime;
    }

    private void TickTheStopwatch(ref float stopWatch)
    {
        stopWatch += Time.deltaTime;
    }

    private void DrawAttackZone()
    {
        if (currentSubTree != null)
        {
            if (currentSubTree.Data != null)
            {
                Gizmos.DrawCube(new Vector3(
                    currentSubTree.Data._attack._offset.x * attackColliderHolder.transform.localScale.x, currentSubTree.Data._attack._offset.y, 0) 
                    + attackColliderHolder.transform.position,
                    ((BoxCollider2D)currentSubTree.Data._attack._attackArea).size);
            }
        }
    }

    private void ReactToLeftClick()
    {
        if (currentSubTree.Left == null)
        {
            //currentSubTree.Data._attack.FinishAttack();
            currentSubTree = fightTree.Left;
            Debug.Log("combo over");
            currentCombo = "";
        }
        else
        {
            currentSubTree = currentSubTree.Left;
        }
        attackColliderHolder.transform.position = gameObject.transform.position;
        //+ new Vector3(gameObject.transform.localScale.x * (playerCollider.size.x / 2), playerCollider.size.y / 2, 0);
        attackColliderHolder.transform.localScale = new Vector3(gameObject.transform.localScale.x, 1, 1);
        currentSubTree.Data._attack.StartAttack();
        isAttacking = true;
        isOnCd = false;
        canAttack = false;
        //prepTimer = currentSubTree.Data._attack._prepTime;
        attackTimer = currentSubTree.Data._attack._attackDuration;
        Debug.Log(attackTimer);
        currentCombo += "L";
        Debug.Log(currentCombo);
    }

    private void ReactToRightClick()
    {
        if (currentSubTree.Right == null)
        {
            //currentSubTree.Data._attack.FinishAttack();
            currentSubTree = fightTree.Right;
            Debug.Log("combo over");
            currentCombo = "";
        }
        else
        {
            currentSubTree = currentSubTree.Right;
        }
        attackColliderHolder.transform.position = gameObject.transform.position;
        //+ new Vector3(gameObject.transform.localScale.x * (playerCollider.size.x / 2), playerCollider.size.y / 2, 0);
        attackColliderHolder.transform.localScale = new Vector3(gameObject.transform.localScale.x, 1, 1);
        currentSubTree.Data._attack.StartAttack();
        isAttacking = true;
        isOnCd = false;
        canAttack = false;
        //prepTimer = currentSubTree.Data._attack._prepTime;
        attackTimer = currentSubTree.Data._attack._attackDuration;
        Debug.Log(attackTimer);
        currentCombo += "R";
        Debug.Log(currentCombo);
    }

    private void ReactToNoClick()
    {
        if (currentSubTree.Data != null)
        {
            currentSubTree.Data._attack.FinishAttack();
        }
        currentSubTree = fightTree;
        currentCombo = "";
    }
}
