using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzle : MonoBehaviour
{
    private bool _timer;
    public int initPoints;
    public int puzzlePoint;
    public int goodPlacePoint { get { return puzzlePoint; } set { puzzlePoint += value; } }
    
    void Start()
    {
        goodPlacePoint = initPoints;
    }

    
    void FixedUpdate()
    {

            if (puzzlePoint == initPoints && gameObject.transform.tag == "Puzzle")
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.03f, gameObject.transform.position.y, gameObject.transform.position.z);
                if(gameObject.transform.position.x > 10)
                    Destroy(gameObject);
            }
        if( gameObject.transform.tag == "PuzzleDrop")
        {
            if(puzzlePoint == 2 * initPoints)
            {
                return;
            }
            if (!_timer)
            {
               // Board.Instance.BoardCounter = -(puzzlePoint - initPoints);
                StartCoroutine(WaitForDestroy());
            }

        }

        
    }

    private IEnumerator WaitForDestroy()
    {
        _timer = true;
        yield return new WaitForSeconds(0.3f);
        _timer = false;
       Destroy(gameObject);
    }

}
