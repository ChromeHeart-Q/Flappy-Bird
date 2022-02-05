using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MapRender : MonoBehaviour
{
    // DEFINE CONSTANT
    private const float CAMERA_ORTHOGRAPHIC_SIZE = 50.0f;
    private const float DESTROY_POSITION = -100.0f;
    private const float SPAWN_POSITION = 100.0f;
    private const float PIPE_WIDTH = 7.8f;
    private const float PIPE_OFFSET = 2.5f;

    // DEFINE STATIC VARIABLES
    static float PIPE_GAP = 50.0f;
    static float PIPE_MOVE_SPEED = 20.0f;
    static float SPAWN_RANGE = 10.0f;

    //DEFINE VARIABLES
    private float spawnTimer = 1.2f;
    private int pipeCount;
    private List<Pipe> lstPipeClone;

    // DEFINE GAME DIFICULTY
    private enum Difficulty
    {
        EASY,
        MEDIUM,
        HARD,
        IMPOSSIBLE,
    }

    // GAME STATE
    public bool isPlaying;

    void Awake()
    {
        lstPipeClone = new List<Pipe>();
        pipeCount = 0;
        isPlaying = true;
        SetDifficulty(GetDifficulty());
    }
    
    Difficulty GetDifficulty()
    {
        if(pipeCount <= 20)
        {
            return Difficulty.EASY;
        }
        else if(pipeCount <= 40)
        {
            return Difficulty.MEDIUM;
        }
        else if(pipeCount <= 60)
        {
            return Difficulty.HARD;
        }
        return Difficulty.IMPOSSIBLE;
    }

    void SetDifficulty(Difficulty X)
    {
        switch (X)
        {
            case Difficulty.EASY:
                PIPE_GAP = 50.0f;
                PIPE_MOVE_SPEED = 20.0f;
                SPAWN_RANGE = 15.0f;
                break;
            case Difficulty.MEDIUM:
                PIPE_GAP = 40.0f;
                PIPE_MOVE_SPEED = 22.5f;
                SPAWN_RANGE = 17.5f;
                break;
            case Difficulty.HARD:
                PIPE_GAP = 30.0f;
                PIPE_MOVE_SPEED = 25.0f;
                SPAWN_RANGE = 22.5f;
                break;
            case Difficulty.IMPOSSIBLE:
                PIPE_GAP = 25.0f;
                PIPE_MOVE_SPEED = 30.0f;
                SPAWN_RANGE = 27.5f;
                break;
        }

    }

    void Update()
    {
        if(isPlaying == true)
        {
            PipeController();
            SpawnController();
        }
    }

    // CONTROL WHEN TO SPAWN A PIPE, DIFFICULTY
    void SpawnController()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0)
        {
            SpawnObstacle(RandomPosition());
            SetDifficulty(GetDifficulty());
            spawnTimer = 1.2f;
        }
    }

    // SPAWN THE PIPE, SET PIPE PROPERTIES ACCORD TO THE DIFFICULTY
    void SpawnObstacle(Vector2 spawnPosition)
    {
        pipeCount++;
        GameObject pipeClone = Instantiate(GameAssetsManager.GetInstance().PipePrefab);
        Pipe pipe = new Pipe(pipeClone);
        pipe.SetPosition(spawnPosition);
        pipe.SetSize();
        lstPipeClone.Add(pipe);
    }

    // MOVE THE PIPE ON SCENE, CONTROL THE PIPE'S LIFETIME
    void PipeController()
    {
        for (int i = 0; i < lstPipeClone.Count; i++)
        {
            lstPipeClone[i].Move();
            if (lstPipeClone[i].GetPosition() < DESTROY_POSITION)
            {
                lstPipeClone[i].DestroySelf();
                lstPipeClone.RemoveAt(i);
                i--;
            }
        }
    }

    // GENERATE RANDOM POSITION FOR THE PIPE
    Vector2 RandomPosition()
    {
        float yAxisPostion = Random.Range(-SPAWN_RANGE, SPAWN_RANGE);
        return new Vector2(SPAWN_POSITION, yAxisPostion);
    }

    // DEFINE PIPE METHODS AND PROPERTIES
    class Pipe
    {
        private GameObject pipe;
        public Pipe(GameObject pipeClone)
        {
            pipe = pipeClone;
        }
        public void SetSize()
        {
            pipe.GetComponent<BoxCollider2D>().size = new Vector2(1.0f, PIPE_GAP);
            pipe.transform.GetChild(0).transform.localPosition = new Vector3(0.0f, PIPE_GAP / 2 + PIPE_OFFSET);
            pipe.transform.GetChild(1).transform.localPosition = new Vector3(0.0f, -(PIPE_GAP / 2 + PIPE_OFFSET));
            // Get Sprites
            SpriteRenderer pipeTop = pipe.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>();
            SpriteRenderer pipeBottom = pipe.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>();
            // Get Collider
            BoxCollider2D pipeTopColider = pipe.transform.GetChild(0).transform.GetChild(0).GetComponent<BoxCollider2D>();
            BoxCollider2D pipeBottomCollider = pipe.transform.GetChild(1).transform.GetChild(0).GetComponent<BoxCollider2D>();
            if (pipe.transform.position.y >= 0)
            {
                // calculate new size
                float topSize = CAMERA_ORTHOGRAPHIC_SIZE - pipe.transform.position.y - PIPE_GAP / 2 - PIPE_OFFSET;
                float bottomSize = -CAMERA_ORTHOGRAPHIC_SIZE - pipe.transform.position.y + PIPE_GAP / 2 + PIPE_OFFSET;

                // resize the pipe according to the position
                pipeTop.size = new Vector2(PIPE_WIDTH, topSize);
                pipeBottom.size = new Vector2(PIPE_WIDTH, bottomSize);

                // resize top pipe collider 
                pipeTopColider.size = new Vector2(PIPE_WIDTH, topSize);
                pipeTopColider.offset = new Vector2(0.0f, topSize / 2);

                // resize bottom pipe collider
                pipeBottomCollider.size = new Vector2(PIPE_WIDTH, -bottomSize);
                pipeBottomCollider.offset = new Vector2(0.0f, bottomSize / 2);

            }
            else
            {
                // calculate new size
                float topSize = CAMERA_ORTHOGRAPHIC_SIZE - pipe.transform.position.y - PIPE_GAP / 2 - PIPE_OFFSET;
                float bottomSize = -CAMERA_ORTHOGRAPHIC_SIZE - pipe.transform.position.y + PIPE_GAP / 2 + PIPE_OFFSET;

                // resize the pipe according to the position
                pipeTop.size = new Vector2(PIPE_WIDTH, topSize);
                pipeBottom.size = new Vector2(PIPE_WIDTH, bottomSize);

                // resize top pipe collider 
                pipeTopColider.size = new Vector2(PIPE_WIDTH, topSize);
                pipeTopColider.offset = new Vector2(0.0f, topSize / 2);

                // resize bottom pipe collider
                pipeBottomCollider.size = new Vector2(PIPE_WIDTH, -bottomSize);
                pipeBottomCollider.offset = new Vector2(0.0f, bottomSize / 2);
            }
        }
        public void SetPosition(Vector2 pipePosition)
        {
            pipe.transform.position = pipePosition;
        }
        public float GetPosition()
        {
            return pipe.transform.position.x;
        }
        public void Move()
        {
            pipe.transform.position += new Vector3(-1.0f, 0.0f) * PIPE_MOVE_SPEED * Time.deltaTime;
        }
        public void DestroySelf()
        {
            Destroy(pipe);
        }
    }
}
