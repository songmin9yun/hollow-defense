using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // 보스의 상태 정의
    public enum BossState
    {
        Idle,           // 대기
        Pattern_A,      // 패턴 A (예: 연속 찌르기)
        Pattern_B,      // 패턴 B (예: 광역 장판/독 뿌리기)
        Pattern_C,      // 패턴 C (예: 돌진)
        Groggy,         // 그로기/무력화
        Dead            // 사망
    }

    public BossState currentState = BossState.Idle;
    public Transform targetPlayer; // 플레이어 위치

    void Start()
    {
        // 보스 AI 루프 시작
        StartCoroutine(BossAILoop());
    }

    // 보스 행동 전체를 제어하는 메인 루틴
    IEnumerator BossAILoop()
    {
        while (currentState != BossState.Dead)
        {
            yield return new WaitForSeconds(1.5f); // 패턴 사이의 대기 시간 (Cooltime)

            // 무작위로 다음 패턴 선택 (확률 및 조건 부여 가능)
            SelectNextPattern();

            // 선택된 패턴 실행 및 종료까지 대기
            switch (currentState)
            {
                case BossState.Pattern_A:
                    yield return StartCoroutine(Pattern_A_Routine());
                    break;
                case BossState.Pattern_B:
                    yield return StartCoroutine(Pattern_B_Routine());
                    break;
                case BossState.Pattern_C:
                    yield return StartCoroutine(Pattern_C_Routine());
                    break;
            }

            // 패턴이 끝나면 다시 대기 상태로
            currentState = BossState.Idle;
        }
    }

    void SelectNextPattern()
    {
        // 예시: 1~3 중 랜덤으로 패턴 선택
        int ran = Random.Range(1, 4);

        if (ran == 1) currentState = BossState.Pattern_A;
        else if (ran == 2) currentState = BossState.Pattern_B;
        else if (ran == 3) currentState = BossState.Pattern_C;
    }

    // --- 실제 패턴 구현 부분 ---

    // 패턴 A: 플레이어를 향해 3번 발사체 발사
    IEnumerator Pattern_A_Routine()
    {
        Debug.Log("패턴 A 시작: 연속 공격");
        
        for (int i = 0; i < 3; i++)
        {
            // 공격 전조 증상 (경고 표시, 애니메이션 재생 등)
            Debug.Log($"공격 준비... {i + 1}");
            yield return new WaitForSeconds(0.5f);

            // 실제 공격 (투사체 생성 등)
            Debug.Log("발사!");
            yield return new WaitForSeconds(0.3f);
        }

        // 공격 후딜레이
        yield return new WaitForSeconds(1.0f);
    }

    // 패턴 B: 플레이어 위치에 경고 장판 후 광역 공격
    IEnumerator Pattern_B_Routine()
    {
        Debug.Log("패턴 B 시작: 광역 장판");

        Vector3 targetPos = targetPlayer.position;
        // (여기서 targetPos 위치에 빨간색 경고 장판 프리팹 생성)

        yield return new WaitForSeconds(1.5f); // 장판 딜레이

        // (경고 장판 위치에 진짜 폭발/독 구름 생성)
        Debug.Log("쿵! 광역 폭발");

        yield return new WaitForSeconds(1.0f);
    }

    // 패턴 C: 플레이어 방향으로 돌진
    IEnumerator Pattern_C_Routine()
    {
        Debug.Log("패턴 C 시작: 돌진 준비");

        Vector2 chargeDirection = (targetPlayer.position - transform.position).normalized;
        yield return new WaitForSeconds(1.0f); // 돌진 전 멈칫하는 시간

        // 0.5초 동안 빠르게 돌진
        float timer = 0f;
        while (timer < 0.5f)
        {
            transform.Translate(chargeDirection * 15f * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);
    }
}