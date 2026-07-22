using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralAlgorithm : MonoBehaviour
{
    [Header("타일 설정")] public Tilemap tilemap;
    [SerializeField] private TileBase landTile; // 땅 타일
    [SerializeField] private TileBase waterTile; // 물 타일

    [Header("맵 크기")] public int width = 50;
    public int height = 50;

    [Header("노이즈 설정")] public float scale = 10f; // 노이즈의 조밀함 (클수록 지형이 조각남)
    public string seed; // 매번 다른 맵을 만들기 위한 시드값
    private float seedX;
    private float seedY;

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        // 시드값 적용 (매번 다른 맵 생성)
        seedX = Random.Range(0f, 99999f);
        seedY = Random.Range(0f, 99999f);

        tilemap.ClearAllTiles(); // 기존 타일 초기화

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // 펄린 노이즈 값 계산 (0.0 ~ 1.0 사이의 값 반환)
                float xCoord = (float)x / width * scale + seedX;
                float yCoord = (float)y / height * scale + seedY;
                float noiseValue = Mathf.PerlinNoise(xCoord, yCoord);

                // 기준값(0.4)보다 크면 땅, 작으면 물 배치
                if (noiseValue > 0.4f)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), landTile);
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), waterTile);
                }
            }
        }
    }
}