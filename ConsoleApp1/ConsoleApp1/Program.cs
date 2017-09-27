using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        struct ItemInfoDef
        {
            internal char ID;
            internal int Omosa;
            internal int Val;
        }

        static List<ItemInfoDef> ItemList = new List<ItemInfoDef>();

        struct JyoutaiDef
        {
            internal int CurrP;
            internal int OmosaSum;
            internal int SumVal;
            internal List<ItemInfoDef> SelectedItemList;
        }

        //重さ合計の制限
        const int OmosaSumLimit = 10;

        static void Main()
        {
            ItemList.Add(new ItemInfoDef() { ID = 'A', Omosa = 3, Val = 2 });
            ItemList.Add(new ItemInfoDef() { ID = 'B', Omosa = 4, Val = 3 });
            ItemList.Add(new ItemInfoDef() { ID = 'C', Omosa = 1, Val = 2 });
            ItemList.Add(new ItemInfoDef() { ID = 'D', Omosa = 2, Val = 3 });
            ItemList.Add(new ItemInfoDef() { ID = 'E', Omosa = 3, Val = 6 });

            var JyoutaiDefList = new List<JyoutaiDef>();
            var stk = new Stack<JyoutaiDef>();
            for (int I = 0; I <= ItemList.Count - 1; I++)
            {
                JyoutaiDef WillPush;
                WillPush.CurrP = I;
                WillPush.OmosaSum = ItemList[I].Omosa;
                WillPush.SumVal = ItemList[I].Val;
                WillPush.SelectedItemList = new List<ItemInfoDef>() { ItemList[I] };
                stk.Push(WillPush);
            }

            while (stk.Count > 0)
            {
                JyoutaiDef Popped = stk.Pop();
                JyoutaiDefList.Add(Popped);

                for (int I = Popped.CurrP + 1; I <= ItemList.Count - 1; I++)
                {
                    JyoutaiDef WillPush;
                    WillPush.CurrP = I;
                    WillPush.OmosaSum = Popped.OmosaSum + ItemList[I].Omosa;
                    WillPush.SumVal = Popped.SumVal + ItemList[I].Val;
                    WillPush.SelectedItemList = new List<ItemInfoDef>(Popped.SelectedItemList);
                    WillPush.SelectedItemList.Add(ItemList[I]);

                    if (WillPush.OmosaSum <= OmosaSumLimit) stk.Push(WillPush);
                }
            }

            int MaxSumVal = JyoutaiDefList.Max(X => X.SumVal);
            JyoutaiDef AnswerJyoutai = JyoutaiDefList.First(X => X.SumVal == MaxSumVal);

            Console.WriteLine("深さ優先探索の結果を表示します。");
            Console.WriteLine("重さ合計が{0}以下での最大の価値は、", OmosaSumLimit);
            Console.WriteLine("重さ合計={0}で価値合計={1}です。", AnswerJyoutai.OmosaSum, MaxSumVal);
            Console.WriteLine();
            Console.WriteLine(new string('■', 30));
            Console.WriteLine("選択した荷物の情報を表示します。");

            foreach (ItemInfoDef EachItem in AnswerJyoutai.SelectedItemList)
            {
                Console.WriteLine("ID={0}、重さ={1}、価値={2}",
                    EachItem.ID, EachItem.Omosa, EachItem.Val);
            }

        }
    }
}
