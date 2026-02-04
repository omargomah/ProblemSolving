namespace ProblemSolving
{
    internal class Program
    {
        static void Main(string[] args)
        {

        }
        #region Array & Hashing

        public static class SolutionArrayAndHashing
        {
            #region Deblucate problem
            public static bool hasDuplicate(int[] nums)
            {
                var set = new HashSet<int>();

                for (int i = 0; i < nums.Length; i++)
                {
                    if (set.Contains(nums[i]))
                        return true;
                    set.Add(nums[i]);
                }
                return false;
            }
            #endregion

            #region Anagrum problem
            public static bool IsAnagram(string s, string t)
            {
                int[] frq = new int[26];
                if (s.Length != t.Length)
                    return false;
                for (int i = 0; i < s.Length; i++)
                {
                    frq[s[i] - 'a']++;
                    frq[t[i] - 'a']--;
                }
                foreach (var item in frq)
                {
                    if (item != 0)
                        return false;
                }
                return true;
            }
            #endregion
        }
        #endregion
    }
    #region Dynamic Array 
    public class DynamicArray
    {
        private int[] StaticArray { get; set; }
        private int Top { get; set; }
        public DynamicArray(int capacity)
        {
            StaticArray = new int[capacity];
            Top = -1;
        }

        public int Get(int i)
        {
            return StaticArray[i];

        }

        public void Set(int i, int n)
        {
            if(i >= StaticArray.Length)
                Resize();
            StaticArray[i] =  n;
        }

        public void PushBack(int n)
        {
            Set(++Top, n);
        }

        public int PopBack()
        {
            var lastElement = Get(Top);
            Set(Top--, 0);
            return lastElement;
        }

        private void Resize()
        {
            var newArray = new int[StaticArray.Length * 2];
            for (int i = 0; i < StaticArray.Length; i++)
                newArray[i] = Get(i);
            StaticArray = newArray;
        }

        public int GetSize()
        {
            for (int i = StaticArray.Length - 1; i >= 0 ; i--)
            {
                if (Get(i) != 0)
                    return i+1;
            }
            return 0;
        }

        public int GetCapacity() => StaticArray.Length;
    }

    #endregion

}
