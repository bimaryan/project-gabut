using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bola : MonoBehaviour
{
    public float kecepatanJatuh = 5f; // Kecepatan jatuh bola
    public int nilaiScore = 5; // Nilai score yang diperoleh jika bola terkena gelas
    public float waktuMax = 10f; // Waktu maksimum permainan
    private float waktuSisa; // Waktu sisa permainan
    private bool gameOver = false; // Status game over
    public TextMeshProUGUI scoreText; // Text untuk menampilkan score
    public TextMeshProUGUI waktuText; // Text untuk menampilkan waktu
    public TextMeshProUGUI highScoreText; // Text untuk menampilkan highscore
    public GameObject panelGameOver; // Panel game over
    public Button tombolCobaLagi; // Tombol UI/Button untuk coba lagi
    public Button tombolJatuhkanBola; // Tombol UI/Button untuk jatuhkan bola
    public Vector3 posisiAwal; // Posisi awal bola

    void Start()
    {
        waktuSisa = waktuMax;
        UpdateUI();
        tombolCobaLagi.onClick.AddListener(MulaiUlangPermainan);
        tombolJatuhkanBola.onClick.AddListener(JatuhkanBola);
    }

    void Update()
    {
        posisiAwal = transform.position; // Menyimpan posisi awal bola

        if (!gameOver)
        {
            waktuSisa -= Time.deltaTime;
            UpdateUI();

            if (waktuSisa <= 0)
            {
                // Game over jika waktu habis
                gameOver = true;
                panelGameOver.SetActive(true);
                int highScore = PlayerPrefs.GetInt("HighScore", 0);
                if (nilaiScore > highScore)
                {
                    // Update highscore jika score melebihi highscore sebelumnya
                    highScore = nilaiScore;
                    PlayerPrefs.SetInt("HighScore", highScore);
                }
                highScoreText.text = "High Score: " + highScore;
            }
        }
    }

    void UpdateUI()
    {
        // Update tampilan score dan waktu pada UI
        scoreText.text = "Score: " + nilaiScore;
        waktuText.text = "Waktu: " + waktuSisa.ToString("F1");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gelas"))
        {
            if (!gameOver)
            {
                // Tambah nilai score jika bola terkena gelas
                nilaiScore += 5;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Tanah"))
        {
            // Mengatur posisi bola kembali ke atas
            transform.position = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
        }
    }

    void MulaiUlangPermainan()
    {
        // Fungsi untuk tombol coba lagi, mereset score, waktu, dan game over
        nilaiScore = 0;
        waktuSisa = waktuMax;
        gameOver = false;
        panelGameOver.SetActive(false);
    }

    void JatuhkanBola()
    {
        if (!gameOver)
        {
            // Mengaktifkan gravitasi pada bola untuk menjatuhkannya
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            tombolJatuhkanBola.interactable = true; // Menonaktifkan tombol jatuhkan bola setelah digunakan
        }
    }
}
