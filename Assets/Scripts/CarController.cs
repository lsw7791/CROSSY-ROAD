using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public int speed; 
    private float rotationY;
    private float initialX;  // 최초의 x 위치 값

    void Start()
    {
        speed = Random.Range(8, 18);

        initialX = transform.position.x;

        // 최초 x 좌표에 따라 y 축 회전 값 설정
        if (initialX > 0)
        {
            rotationY = -90f;
        }
        else
        {
            rotationY = 90f; 
        }

        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    void Update()
    {
        if (initialX > 0)
        {
            // initialX가 0보다 크면 x값 감소
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            // initialX가 0보다 작으면 x값 증가
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        //if (transform.position.y < 0) 파괴를 직접 관리할 수 있게 하기
        //{
        //    Destroy(gameObject);
        //}
        if (transform.position.y < 0) //파괴를 맵매니저에게 위임
        {
            gameObject.SetActive(false);
        }
    }
}
