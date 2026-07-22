using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Transform[] layers;

    [SerializeField] private float[] parallaxFactors;

    [SerializeField] private float yOffset;


    // 배경 하나의 길이
    [SerializeField] private float backgroundWidth = 18.56f;


    private Vector3 previousTargetPosition;


    void Start()
    {
        previousTargetPosition = target.localPosition;


        if(layers.Length != parallaxFactors.Length)
        {
            Debug.LogError("Layer와 Speed 배열 개수가 다릅니다.");
        }
    }


    void LateUpdate()
    {
        Vector3 deltaMovement =
            target.localPosition - previousTargetPosition;


        for(int i = 0; i < layers.Length; i++)
        {
            // 패럴랙스 이동
            layers[i].localPosition +=
                new Vector3(
                    deltaMovement.x * parallaxFactors[i],
                    0,
                    0
                );


            // 오른쪽으로 넘어감
            if(layers[i].localPosition.x > backgroundWidth)
            {
                layers[i].localPosition -=
                    new Vector3(
                        backgroundWidth * 2,
                        0,
                        0
                    );
            }


            // 왼쪽으로 넘어감
            else if(layers[i].localPosition.x < -backgroundWidth)
            {
                layers[i].localPosition +=
                    new Vector3(
                        backgroundWidth * 2,
                        0,
                        0
                    );
            }
        }


        previousTargetPosition = target.localPosition;
    }
}