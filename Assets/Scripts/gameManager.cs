using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    // 타이머
    float time = 0.0f;
    public Text timeTxt;
    
    // 매치
    public Text Match;
    int matchCount = 0;

    public GameObject gameoverPanel;
    public GameObject gameoverPanel2;
    public GameObject card; // card 오브젝트
    public GameObject firstCard;
    public GameObject secondCard;

    // 소리
    public AudioClip matchSound; // 실행할 음악
    public AudioSource audioSource1; // 누가 그 음악 파일을 실행할 것인가

    public static gameManager I;// 싱글톤


    void Awake() // 싱글톤
    {
        I = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;

        int[] _Image = {0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7}; // 8쌍 카드
        _Image = _Image.OrderBy(item => Random.Range(-0.1f, 1.0f)).ToArray(); // 리스트 랜덤 정렬

        // 반복
        for(int i = 0; i < 16; i++)
        {
            GameObject newCard = Instantiate(card); // Prefab 카드 생성
            newCard.transform.parent = GameObject.Find("cards").transform; // card를 cards 파일 아래 담기
            
            // 카드 배열
            float x = (i / 4) * 7f - 10.5f; 
            float y = (i % 4) * 9f - 18f; 
            newCard.transform.position = new Vector3(x, y, 0);

            // sprite(Image0~7) 이름 찾기 위한 작업
            string _ImageName = "Image" + _Image[i].ToString();

            /// newCard에 front를 찾아서 sprite를 변경, Resources 폴더 아래 이미지를 가져오기 
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite
            = Resources.Load<Sprite>(_ImageName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        // UI: float -> string
        timeTxt.text = time.ToString("N2");

        // 25초 후 시간 테스트 색 변경
        if(time > 25.0f)
        {
            timeTxt.color = Color.red;
        }
        if(time > 30.0f)
        {
            GameEnd();
            time = 0f;
        }
       
    }

    public void isMatched()
    {
        matchCount++;

        // fornt -> 컴포넌트(SpriteRenderer) -> sprite 이름 가져오기
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        // 카드 매치 성공
        if (firstCardImage == secondCardImage)
        {
            audioSource1.PlayOneShot(matchSound);

            // 두 카드 제거
            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();

            // 카드 갯수 알아오기
            int cardsLeft = GameObject.Find("cards").transform.childCount;

            // 카드 2개 남으면 종료
            if (cardsLeft == 2)
            {
                // 종료
                Time.timeScale = 0f;

                // 종료 UI 생성
                gameoverPanel.SetActive(true);

                Invoke("GameEnd", 2f);
            }
        }
        // 카드 매치 실패
        else 
        {
            // 두 카드 제거
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
        }

        // 게임 끝났으니 덮어줌.
        firstCard = null;
        secondCard = null;

        MatchCounting(); 
    }

    public void GameEnd()
    {
        // 게임 종료
        Time.timeScale = 0.0f;

        // 게임 종료 패널2
        gameoverPanel2.SetActive(true);

    }

    public void rePlay()
    {
        SceneManager.LoadScene("MainScene");
    }

    void MatchCounting()
    {
        Match.text = matchCount.ToString();
    }

}

