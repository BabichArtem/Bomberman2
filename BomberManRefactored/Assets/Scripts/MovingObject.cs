using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{

    public bool stepFinished = true;

    private IEnumerator coroutine;

    public enum Side
    {
        Up,
        Down,
        Left,
        Right,
        Idle
    }

    IEnumerator MoveFromTo(GameObject objectToMove, Vector3 startPosition, Vector3 endPosition, float objectSpeed)
    {
        stepFinished = false;
        float step = (objectSpeed / (startPosition - endPosition).magnitude) * Time.fixedDeltaTime;
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

    protected void MoveObject(GameObject movingObject, Vector3 startPosition, Vector3 endPosition, float objectSpeed)
    {
        coroutine = MoveFromTo(movingObject, startPosition, endPosition, objectSpeed);
        StartCoroutine(coroutine);
    }

    protected virtual bool CanMove(Vector3 startPosition, Vector3 endPosition)
    {
        LayerMask mask = LayerMask.GetMask("BlockingLayer");
        Ray ray = new Ray(startPosition,endPosition-startPosition);
        bool detected = Physics.Raycast(ray,1.0f,mask);
        return !detected;
    }

    protected virtual bool AttempMove(GameObject movingObject, Side movingSide,float objectSpeed)
    {
        Vector3 startPosition = GetPosition(movingObject);
        Vector3 endPosition = CalculateSideVector(startPosition, movingSide);
        bool canMove = CanMove(startPosition, endPosition);
        if (canMove && stepFinished)
        {
            RotateObject(movingObject,movingSide);
            MoveObject(movingObject, startPosition, endPosition, objectSpeed);
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

    protected virtual void RotateObject(GameObject obj, Side side)
    {
        switch (side)
        {
            case Side.Up:
                obj.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Side.Down:
                obj.transform.rotation = Quaternion.Euler(0, 180, 0);
                break;
            case Side.Left:
                obj.transform.rotation = Quaternion.Euler(0, -90, 0);
                break;
            case Side.Right:
                obj.transform.rotation = Quaternion.Euler(0, 90, 0);
                break;
            default:
                break;
        }
    }

}
