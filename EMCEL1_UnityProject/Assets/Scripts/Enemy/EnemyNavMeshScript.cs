using Audio_Scripts;
using UnityEngine;
using UnityEngine.AI;
using Player.Control;

public class EnemyNavMeshScript : MonoBehaviour
{
    private NavMeshAgent EnemyNMAgent;

    public Animator ZombieAnimatorController;

    [Header("Attacking Variables")]
    public float defaultAttackSpeed = 5f, timeToAttack;
    public float enemyDamage = 10;

    public float reloadTime = 1;
    public bool attacking = false;

    [Header("Script For Referencing")]
    public AttackRange objIdentifier;
    public ForSpawningScript forSpawnScript;
    public SphereCollider objIdentifierSphere;
    
    private GameObject player;
    
    // For sounds
    public GameObject grunt;

    void Start()
    {
        EnemyNMAgent = transform.GetComponent<NavMeshAgent>();

        if(!transform.name.Contains("Lilnerd")) transform.SetParent(GameObject.Find("Enemies").transform);

        objIdentifier = transform.GetChild(0).GetComponent<AttackRange>();
        objIdentifierSphere = transform.GetChild(0).GetComponent<SphereCollider>();

        timeToAttack = defaultAttackSpeed;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null)
        {
            player ??= GameObject.FindGameObjectWithTag("Player");
            return;
        }

        MoveToPlayerNavMesh();
        DetectIfPlayerInRange();
        AnimationSetter();
    }

    void MoveToPlayerNavMesh()
    {
        if (EnemyNMAgent.isOnNavMesh)
        {
            if (!attacking)
            {
                EnemyNMAgent.destination = player.transform.position;
            }
            else
            {
                EnemyNMAgent.isStopped = true;
            }
        }
    }
    void DetectIfPlayerInRange()
    {
        if (GetComponentInChildren<BossAbilityScript>() == null)
        {

            if (objIdentifier.identifiedObj != null && EnemyNMAgent.isOnNavMesh)
            {
                attacking = EnemyNMAgent.remainingDistance <= EnemyNMAgent.stoppingDistance &&
                            objIdentifier.identifiedObj == player;
            }
        }
    }
    void AnimationSetter()
    {
        if(!ZombieAnimatorController.GetBool("IsDead"))
        {
            ZombieAnimatorController.SetBool("IsWalking", !attacking);
            ZombieAnimatorController.SetBool("IsAttacking", attacking);
        }

    }
    public void attackTarget()
    {
        if (objIdentifier.identifiedObj == player)
        {
            GlobalSfx.grunt?.Invoke(transform.position, grunt);
            PlayerStatus.changeHealth?.Invoke(-(int)enemyDamage);
        }
        
        if (GetComponent<ZombieBossScript>() != null)
        {
            EnemyNMAgent.speed = GetComponent<ZombieBossScript>().StartSpeed;
        }
    }

    public void ResetVariables()
    {
        attacking = false;
        timeToAttack = defaultAttackSpeed;
        EnemyNMAgent.isStopped = false;
        timeToAttack = defaultAttackSpeed;
    }
}
