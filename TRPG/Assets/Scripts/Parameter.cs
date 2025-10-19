using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Character
{
    public class Parameter : MonoBehaviour
    {
        // ===== ステータス =====
        public int STR; // 筋力
        public int CON; // 体力
        public int POW; // 精神力
        public int DEX; // 敏捷性
        public int APP; // 外見
        public int SIZ; // 体格
        public int INT; // 知性
        public int EDU; // 教育
        public int SAN; // 正気度
        public int LUK; // 幸運
        public int IDA; // アイデア
        public int KNOW; // 知識
        public int HP; // 耐久力
        public int MP; // マジックポイント
        public int Occupation; // 職業技能ポイント
        public int Hobby; // 趣味技能ポイント
        public string DB; // ダメージボーナス

        // ===== UI参照 =====
        [Header("UI Elements")]
        public Button RollButton;
        public TMP_Text ResultText;

        private void Start()
        {
            if (RollButton != null)
                RollButton.onClick.AddListener(Dice);
        }

        void Dice()
        {
            // ３Ｄ６
            STR = Roll3D6();
            CON = Roll3D6();
            POW = Roll3D6();
            DEX = Roll3D6();
            APP = Roll3D6();

            // ２Ｄ６＋６
            SIZ = Roll2D6() + 6;
            INT = Roll2D6() + 6;

            // ３Ｄ６＋３
            EDU = Roll3D6() + 3;

            // ===== 派生値計算 =====
            SAN = POW * 5;
            LUK = POW * 5;
            IDA = INT * 5;
            KNOW = EDU * 5;
            HP = Mathf.FloorToInt((CON + SIZ) / 2f);
            MP = POW;
            Occupation = EDU * 20;
            Hobby = INT * 10;
            DB = CalcDamageBonus(STR, SIZ);

            // ===== 結果表示 =====
            if (ResultText != null)
            {
                ResultText.text = $"STR:{STR}  CON:{CON}  SIZ:{SIZ}\n" +
                                  $"INT:{INT}  EDU:{EDU}\n" +
                                  $"HP:{HP}  MP:{MP}\n" +
                                  $"SAN:{SAN}  LUK:{LUK}\n" +
                                  $"Occupation:{Occupation}  Hobby:{Hobby}\n" +
                                  $"DB:{DB}";
            }

            Debug.Log("キャラクター能力を生成しました。");
        }

        // ===== サイコロロール関数 =====
        int Roll3D6() => Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7);
        int Roll2D6() => Random.Range(1, 7) + Random.Range(1, 7);

        // ===== ダメージボーナス計算 =====
        string CalcDamageBonus(int str, int siz)
        {
            int sum = str + siz;

            if (sum <= 12) return "-1D6";
            else if (sum <= 16) return "-1D4";
            else if (sum <= 24) return "±0";
            else if (sum <= 32) return "+1D4";
            else if (sum <= 40) return "+1D6";
            else return "+2D6";
        }
    }
}
