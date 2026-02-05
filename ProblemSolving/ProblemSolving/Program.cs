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

            #region TwoSum
            #region first solution
            //public static int[] TwoSum(int[] nums, int target)
            //{
            //    List<(int,int)> keyValuePairs = new List<(int, int)>(nums.Length);
            //    for (int i = 0; i< nums.Length; i++)
            //        keyValuePairs.Add((i,nums[i]));
            //     var sortedDict = keyValuePairs.OrderBy(x=> x.Item2).ToList();
            //    int l = 0 , r = nums.Length - 1 , sum;
            //    while (true) 
            //    {
            //        sum = sortedDict[l].Item2 + sortedDict[r].Item2; 
            //        if (sum > target)
            //            r--;
            //        else if (sum < target)
            //            l++;
            //        else
            //        {
            //            var lnums = sortedDict[l].Item1; 
            //            var rnums = sortedDict[r].Item1;
            //            return lnums > rnums ?  [rnums ,lnums ] : [lnums,rnums];
            //        }                    
            //    }
            //}
            #endregion

            #region Second solution
            public static int[] TwoSum(int[] nums, int target)
            {
                Dictionary<int,int> values = new Dictionary<int,int>();
                for (int i = 0; i < nums.Length; i++)
                {
                    if(values.ContainsKey(target - nums[i]))
                        return [values[target - nums[i]],i];
                    values[nums[i]] = i;
                }
                return null!;
            }
            #endregion
            #endregion

            #region Group Anagrams

            public static List<List<string>> GroupAnagrams(string[] strs)
            {
                Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
                foreach (var item in strs)
                {
                    string orderstring = new(item.Order().ToArray());
                    if (dict.ContainsKey(orderstring))
                        dict[orderstring].Add(item);
                    else
                        dict[orderstring] = [item];
                }
                return dict.Select(x => x.Value).ToList();

            }



            #endregion

            #region Top K Frequent Elements

            public static int[] TopKFrequent(int[] nums, int k)
            {
                Dictionary<int,int> dict = new Dictionary<int,int>();
                foreach (var item in nums)
                {
                    if(!dict.ContainsKey(item))
                        dict[item] = 0;
                    dict[item]++;
                }
                return dict.OrderByDescending(x => x.Value).Select(x => x.Key).ToArray()[..k];

            }
            #endregion

            #region Encode and Decode Strings

            public static string Encode(IList<string> strs) =>   strs.Count == 0 ? null : string.Join(' ', strs);

            public static List<string> Decode(string s) => s is null ? [] : [.. s.Split(' ')];

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
