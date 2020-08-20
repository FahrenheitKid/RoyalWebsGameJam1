using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TextsHolder")]
public class TextsHolder : ScriptableObject
{
    [SerializeField]
    public List<string> randomTips;

    [SerializeField]
    public List<string> askInstrunctions;

     [SerializeField]
    public List<string> interrogationInstrunctions;

     [SerializeField]
    public List<string> sacrificeInstructions;

    [SerializeField]
    public List<string> monstersSacriceTexts;

     [SerializeField]
    public List<string> guessInstructions;

        public static Queue<T> EnqueueInOrder<T>(List<T> list, int amount= -1)
        {
            Queue<T> result = new Queue<T>();
            amount = amount <= -1 ? amount = list.Count : amount;

            if( !list.Any() || amount <= 0) return result;
            amount = amount > list.Count ? list.Count : amount;

            for(int i = 0; i < amount ; i++)
            {
                if(i >= 0 && i < list.Count)
                result.Enqueue(list.ElementAtOrDefault(i));
            }

            return result;
        }

         public static Queue<T> EnqueueRandomly<T>(List<T> list, int amount= -1)
        {
              Queue<T> result = new Queue<T>();
              amount = amount <= -1 ? amount = list.Count : amount;

            if( !list.Any() || amount <= 0) return result;
            amount = amount > list.Count ? list.Count : amount;

            List<int> indexes = UtilityTools.GetUniqueRandomNumbers(0,list.Count,amount);

            for(int i  = 0; i < indexes.Count; i++)
            {
                if(indexes[i] >= 0 && indexes[i] < list.Count)
                {
                    result.Enqueue(list.ElementAtOrDefault( indexes[i]));
                }
            }

            return result;

        }
    
}
