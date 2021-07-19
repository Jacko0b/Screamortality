using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private enum Name
    {
        Tentacle,
        Lady,
        Ghost,
        Book,
    }
    private enum State
    {
        Spawning,
        Atack,
    }
    [SerializeField] private Name nam;
    [SerializeField] private State state;

    [SerializeField] private bool hitByFlashlight;
    [SerializeField] private int spawnID;

    [SerializeField] private Animator anim;
    [SerializeField] private Player player;
    [SerializeField] private SceneCollector sceneManager;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSourceLoop;

    [SerializeField] private AudioClip sound;

    public bool HitByFlashlight { get => hitByFlashlight; set => hitByFlashlight = value; }
    public int SpawnID { get => spawnID; set => spawnID = value; }

    private void Awake()
    {
        hitByFlashlight = false;
        anim.SetBool("HitByLight", false);
        anim.SetBool("SpawnEnemy", true);
        state = State.Spawning;
        StartCoroutine(ChangeState());
        player = FindObjectOfType<Player>();


            if (nam == Name.Tentacle)
            {
                playAudio1("Tentacle_Spawn");

            }
            if (nam == Name.Book)
            {
                playAudio1("Book_Spawn");

            }
            if (nam == Name.Ghost)
            {
                playAudio1("Ghost_01");

            }
            if (nam == Name.Lady)
            {
                playAudio1("SpookyLady_Spawn_v2_DRK");
            }

    }
    private void FixedUpdate()
    {
        CheckHitByFlashlight();
        if (state == State.Atack)
        {
            if (nam == Name.Tentacle)
            {
               
            }
            if (nam == Name.Book)
            {
                transform.position += new Vector3(0, -1 *Time.deltaTime ,0);

            }
            if (nam == Name.Ghost)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 1 * Time.deltaTime);

            }
            if (nam == Name.Lady)
            {
                if((transform.position.x - player.transform.position.x) <0)
                {
                    transform.rotation = Quaternion.Euler(Vector3.up * 180);

                }
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 3 * Time.deltaTime);
            }
        }


        if (!audioSource.isPlaying && !audioSourceLoop.isPlaying)
        {
            if (nam == Name.Tentacle)
            {
                playAudio2("Tentacle_loop");
            }
            if (nam == Name.Book)
            {
                playAudio2("Book_loop");

            }
            if (nam == Name.Ghost)
            {
                playAudio2("Ghost_loop");

            }
            if (nam == Name.Lady)
            {
                playAudio2("Lady_loop");

            }
        }

    }
    private void playAudio1(string name)
    {
        sound = (AudioClip)Resources.Load(name);
        audioSource.clip = sound;
        audioSource.Play();
    }
    private void playAudio2(string name)
    {
        sound = (AudioClip)Resources.Load(name);
        audioSourceLoop.clip = sound;
        audioSourceLoop.Play();
    }
    private void CheckHitByFlashlight()
    {
        if (hitByFlashlight)
        {    

            Destroy(gameObject);
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Flashlight"))
        {
           
            int i = Random.Range(0, 10);
            if (i == 0)
            {
                playAudio1("Monster_kill_1");

            }
            else if (i == 1)
            {
                playAudio1("Monster_kill_2");

            }
            else if (i == 2)
            {
                playAudio1("Monster_kill_3");

            }
            hitByFlashlight = true;
            anim.SetBool("HitByLight", true);
            anim.SetBool("SpawnEnemy", false);
        }
        if (collision.CompareTag("ground") && nam == Name.Book)
        {
            hitByFlashlight = true;
            anim.SetBool("HitByLight", true);
            anim.SetBool("SpawnEnemy", false);
        }
        if (collision.CompareTag("Player"))
        {
            sceneManager.DeathScreen();
        }

    }

    private IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(1.5f);
        state = State.Atack;
    }
}
