using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    protected float ObjectSpeed { get; set; }

    private bool stepFinished = true;

    private IEnumerator coroutine;

    public enum Side
    {
        Up,
        Down,
        Left,
        Right,
        Idle
    }

    IEnumerator MoveFromTo(GameObject objectToMove, Vector3 startPosition, Vector3 endPosition)
    {
        stepFinished = false;
        float step = (ObjectSpeed / (startPosition - endPosition).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step;
            objectToMove.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return new WaitForFixedUpdate();
        }

        objectToMove.transform.position = endPosition;
        stepFinished = true;


    }

    protected Vector3 GetPosition(GameObject gameObject)
    {
        return gameObject.transform.position;
    }

    protected void MoveObject(GameObject movingObject, Vector3 startPosition, Vector3 endPosition)
    {
        if (stepFinished)
        {
            coroutine = MoveFromTo(movingObject, startPosition, endPosition);
            StartCoroutine(coroutine);
        }

    }

    protected bool CanMove(Vector3 startPosition, Vector3 endPosition)
    {
        LayerMask mask = LayerMask.GetMask("BlockingLayer");
        bool hit = Physics.Linecast(startPosition, endPosition, mask);
        return !hit;
    }

    protected  bool AttempMove(GameObject movingObject, Side movingSide)
    {
        Vector3 startPosition = GetPosition(movingObject);
        Vector3 endPosition = CalculateSideVector(startPosition, movingSide);
        bool canMove = CanMove(startPosition, endPosition);
        if (canMove)
        {
            MoveObject(movingObject, startPosition, endPosition);
        }

        return canMove;
    }



    protected Vector3 CalculateSideVector(Vector3 vector, Side side)
    {
        switch (side)
        {
            case Side.Up:
                vector.z += 1;
                break;
            case Side.Down:
                vector.z -= 1;
                break;
            case Side.Left:
                vector.x -= 1;
                break;
            case Side.Right:
                vector.x += 1;
                break;
            default:
                break;

        }

        return vector;
    }



}
