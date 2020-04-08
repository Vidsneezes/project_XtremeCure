using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBlackboard : MonoBehaviour
{
    public const int NEAR_PLAYER = 0;

    public static AIBlackboard instance;

    public static Vector3 playerPosition3D
    {
        get
        {
            return FpsControllerCore.instance.transform.position;
        }
    }
    public List<Monster> monsters;
    public List<ProximitySensor3D> sensors;

    public AudioSource monsterDead;

    private void Awake()
    {
        AIBlackboard.instance = this;
        monsters = new List<Monster>();
        
    }

    private void Update()
    {
        PlayerTriggerSensor();
    }

    public bool PlayerNearEnemy(float sideLength)
    {
        Vector3 position = playerPosition3D;
        position.x = playerPosition3D.x;
        position.y = playerPosition3D.z;

        float monsterY = playerPosition3D.y;

        for (int i = 0; i < monsters.Count; i++)
        {
            float maxY = monsters[i].transform.position.y + 1.5f;
            float minY = monsters[i].transform.position.y - 1.5f;

            bool inHeightRange = (monsterY < maxY && monsterY > minY);

            Vector2 centerSensor = monsters[i].CenterPosition;
            centerSensor.x = monsters[i].CenterPosition.x;
            centerSensor.y = monsters[i].CenterPosition.z;

            if(!((SimpleBehavior)monsters[i].monsterBehavior).IsAware)
            {
                continue;
            }

            if (PointInRectangle(centerSensor, sideLength, sideLength, position) && inHeightRange)
            {
                return true;
            }
        }

        return false;
    }

    public void PlayerTriggerSensor()
    {
        Vector3 position = playerPosition3D;
        position.x = playerPosition3D.x;
        position.y = playerPosition3D.z;

        float monsterY = playerPosition3D.y;

        for (int i = 0; i < sensors.Count; i++)
        {
            float maxY = sensors[i].transform.position.y + sensors[i].maxRelativeHeight;
            float minY = sensors[i].transform.position.y - sensors[i].maxRelativeHeight;

            bool inHeightRange = (monsterY < maxY && monsterY > minY);

            Vector2 centerSensor = sensors[i].transform.position;
            centerSensor.x = sensors[i].transform.position.x;
            centerSensor.y = sensors[i].transform.position.z;


            if (PointInRectangle(centerSensor, sensors[i].width,sensors[i].length, position) && inHeightRange)
            {
                sensors[i].TriggerSensor();
            }
        }
      
    }

    public void Sensor_Scan3DCube(Vector3 pointPosition, float width, float length, float height)
    {
        Vector2 position = pointPosition;
        Vector2 centerCircle = pointPosition;
        centerCircle.x = pointPosition.x;
        centerCircle.y = pointPosition.z;

        for (int i = 0; i < monsters.Count; i++)
        {
            Vector3 monsterCenterPosition = monsters[i].CenterPosition;

            position = monsterCenterPosition;
            position.x = monsterCenterPosition.x;
            position.y = monsterCenterPosition.z;

            if (PointInRectangle(centerCircle, width,length, position) && PointInHeightLine(pointPosition.y, height, monsterCenterPosition.y))
            {
                Debug.Log("monster recieved message");
                monsters[i].RecieveSensor(NEAR_PLAYER);
            }
        }
    }

    public void Sensor_Scan3D(Vector3 pointPosition, float radius = 999, float maxRelativeHeight = 999)
    {
        Sensor_Scan3DCube(pointPosition, radius, radius, maxRelativeHeight);
    }

    public void Sensor_PlayerNear2D(Vector3 pointPosition , float radius = 999)
    {
        Vector2 position = pointPosition;
        Vector2 centerCircle = pointPosition;
        centerCircle.x = pointPosition.x;
        centerCircle.y = pointPosition.z;

        for (int i = 0; i < monsters.Count; i++)
        {
            position = monsters[i].transform.position;
            position.x = monsters[i].transform.position.x;
            position.y = monsters[i].transform.position.z;

            if(PointInCircle(centerCircle,radius,position))
            {
                monsters[i].RecieveSensor(NEAR_PLAYER);
            }
        }
    }

    public static bool PlayerLookingAtMe(Vector3 position)
    {
        float angle = Vector3.Angle(FpsControllerCore.instance.transform.forward, position - AIBlackboard.playerPosition3D);
        return angle < 50f;
    }

    public static bool PointInHeightLine(float centerY, float height, float pointY)
    {
        float h_h = height * 0.5f;
        return pointY < centerY + h_h && pointY > centerY - h_h;
    }

    public static bool PointInRectangle(Vector2 center , float side_x, float side_z,Vector2 point)
    {
        float h_sx = side_x * 0.5f;
        float h_sz = side_z * 0.5f;

        return point.x < center.x + h_sx && point.x > center.x - h_sx
            && point.y < center.y + h_sz && point.y > center.y - h_sz;
    }

    public static bool PointInSquare(Vector2 center, float sideLength, Vector2 point)
    {
        return PointInRectangle(center, sideLength, sideLength, point);
    }

    public static bool PointInCircle(Vector2 center, float radius, Vector2 point)
    {
        float powerDistance = PowerDistance2D(point, center);

        return powerDistance < (radius * radius);
    }

    public static Vector3 DirectionToPlayer2D(Vector3 fromHere)
    {
        Vector3 direction = AIBlackboard.playerPosition3D - fromHere;
        direction.y = 0;

        return direction;
    }

    public static float DistanceToPlayer2D(Vector3 fromHere)
    {
        return SqrtDistanceVector3(AIBlackboard.playerPosition3D, fromHere);
    }

    public static float SqrtDistanceVector3(Vector3 toHere, Vector3 fromHere)
    {
        Vector2 p12d;
        p12d.x = toHere.x;
        p12d.y = toHere.z;

        Vector2 p22d;
        p22d.x = fromHere.x;
        p22d.y = fromHere.z;

        return Mathf.Sqrt(PowerDistance2D(p12d, p22d));
    }

    //First argument is the point second is the center
    public static float PowerDistance2D(Vector2 toHere, Vector2 fromHere)
    {

        float xValue = toHere.x - fromHere.x;
        float yValue = toHere.y - fromHere.y;


        return (xValue * xValue) + (yValue * yValue);

    }
}
