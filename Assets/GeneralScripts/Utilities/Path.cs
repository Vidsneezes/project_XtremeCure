using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Path : MonoBehaviour
{
    public List<Vector3> points;
    public bool closedPath;

    public Vector3 GetPoint(int index)
    {
        return points[index];
    }

    /// <summary>
    /// Experimantal Sin between two points
    /// </summary>
    /// <param name="point1"></param>
    /// <param name="point2"></param>
    /// <param name="elapsedTime"></param>
    /// <returns></returns>
    public Vector3 GetSinBetween(int point1, int point2, float elapsedTime)
    {

        //Experimental
        float angleBetween = Vector3.Angle(points[point1], points[point2]);

        float fA = angleBetween + (0.2f * Mathf.Sin(5 * Mathf.PI * elapsedTime));
        Vector3 offset = Vector3.zero;
        offset.x = Mathf.Cos(fA);
        offset.z = Mathf.Sin(fA);

        return points[point1] + offset;
    }

    public Vector3 GetRandomPoint()
    {
        int pointIndex = Random.Range(0, points.Count);
        return points[pointIndex];
    }

    public Vector3 FuzzyPosition(Vector3 position, float radius)
    {
        int pointIndex = ClosestsIndexToPosition(position);

        return FuzzyPoint(pointIndex, radius);

    }

    public Vector3 FuzzyPoint(int pointIndex, float radius)
    {
        float radAngle = Mathf.Deg2Rad * Random.Range(0, 360);

        Vector3 position = points[pointIndex];

        position.x += radius * Mathf.Cos(radAngle);
        position.y += radius * Mathf.Sin(radAngle);

        return position;
    }

    public Vector3 DirectionClosestToPlayer(Vector3 position)
    {
        Vector3 direction = ClosesestToPlayer() - position;
        return direction.normalized;
    }

    public Vector3 ClosesestToPlayer()
    {
        return GetClosestPoint(FpsControllerCore.instance.transform.position);
    }

    public int ClosestsIndexToPosition(Vector3 position)
    {
        int closests = 0;
        float closestsDistance = 9999;
        for (int i = 0; i < points.Count; i++)
        {
            float distance = Vector3.Distance(points[i], position);
            if (distance < closestsDistance)
            {
                closestsDistance = distance;
                closests = i;
            }
        }

        return closests;
    }

    public Vector3 GetClosestPoint(Vector3 position)
    {
        int closests = 0;
        float closestsDistance = 9999;
        for (int i = 0; i < points.Count; i++)
        {
            float distance = Vector3.Distance(points[i], position);
            if(distance < closestsDistance)
            {
                closestsDistance = distance;
                closests = i;
            }
        }

        return points[closests];
    }

    public Vector3 GetDirection(Vector3 position, int index)
    {
        Vector3 direction = points[index] - position;
        return direction.normalized;
    }

    public Vector3 GetTravesePosition(float elapsedTime, float maxTime)
    {
        float pointClamp = (elapsedTime / maxTime) * points.Count;
        int pointIndex = Mathf.FloorToInt(pointClamp);
        pointIndex = Mathf.Clamp(pointIndex, 0, points.Count - 1);

        return points[pointIndex];
    }

    public Vector3 GetNormalTravesePosition(float elapsedTime)
    {
        return GetTravesePosition(elapsedTime, 1);
    }

    public int GetTravesePoint(float elapsedTime, float maxTime)
    {
        float pointClamp = (elapsedTime / maxTime) * points.Count;
        int pointIndex = Mathf.FloorToInt(pointClamp);
        pointIndex = Mathf.Clamp(pointIndex, 0, points.Count - 1);

        return pointIndex;
    }

    public int GetNormalTravesePoint(float normalizeElapse)
    {
        return Mathf.Clamp(Mathf.FloorToInt(Mathf.Lerp(0, points.Count, normalizeElapse)),0,points.Count -1);
    }
    

    public Vector3 LerpNext(int indexPoint, int nextIndex,float normalizeElapse)
    {
        return Vector3.Lerp(points[indexPoint], points[nextIndex], normalizeElapse);
    }


    public Vector3 GetPointLerp(float normalizeElapse)
    {
        Debug.Log(normalizeElapse);
        int current = GetNormalTravesePoint(normalizeElapse);
        int next = current + 1;

        if (next >= points.Count)
        {
            if (closedPath)
            {
                next = 0;
            }
            else
            {
                return points[points.Count - 1];
            }
        }

        return Vector3.Lerp(points[current], points[next],normalizeElapse);
    }

    public Vector3 GetNextPosition(float normalizeElapse)
    {
        int current = GetNormalTravesePoint(normalizeElapse);
        int next = current + 1;

        if(next >= points.Count)
        {
            if(closedPath)
            {
                next = 0;
            }
            else
            {
                return points[points.Count - 1];
            }
        }

        return points[next];
    }

    public Vector3 GetNextDirection(float normalizeElapse)
    {
        int getCurrent = GetNormalTravesePoint(normalizeElapse);

        int next = getCurrent + 1;

        if(next >= points.Count)
        {
            if(closedPath)
            {
                next = 0;
            }
            else
            {
                return Vector3.zero;
            }
        }

        Vector3 direction = points[next] - points[getCurrent];
        return direction.normalized;

    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(Path)),CanEditMultipleObjects]
public class PathEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        Path path = (Path)target;

        for (int i = 0; i < path.points.Count; i++)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 newTargetPosition = Handles.PositionHandle(path.points[i], Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(path, "Move Path Point");
                path.points[i] = newTargetPosition;
            }

            if (i + 1 < path.points.Count)
            {
                Handles.DrawLine(path.points[i], path.points[i + 1]);
            }
            else
            {
                if(path.closedPath)
                {
                    Handles.DrawLine(path.points[i], path.points[0]);
                }
            }
        }

    }
}
#endif

