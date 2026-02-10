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

            #region Products of Array Except Self

            public static int[] ProductExceptSelf(int[] nums)
            {
                int length = nums.Length;
                int[] right = new int[length + 1];
                right[length] = 1;
                int[] left = new int[length + 1];
                left[0] = 1;
                for (int i = 0; i < length; i++)
                {
                    left[i+1] = left[i] * nums[i];
                    right[length - 1 - i] = right[length - i] * nums[ length - 1 - i];
                }
                int [] result = new int[length];
                for(int i =0; i< length; i++)
                {
                    result[i] = left[i] * right[i+1];
                }
                return result;

            }

            #endregion

            #region Valid Sudoku

            public static bool IsValidSudoku(char[][] board)
            {
                char x,y;
                for (int i = 0; i < 9; i++)
                {
                    HashSet<char> mapRow =  new HashSet<char>();
                    HashSet<char> mapColumn =  new HashSet<char>();
                    for (int j = 0; j < 9; j++)
                    {
                        x = board[i][j];
                        y = board[j][i];
                        if (x != '.')
                        {
                            if (mapRow.Contains(x))
                                return false;
                            mapRow.Add(x);
                        }

                        if (y != '.')
                        {
                            if (mapColumn.Contains(y))
                                return false;
                            mapColumn.Add(y);
                        }

                    }
                }
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        HashSet<char> checkSet =  new HashSet<char>();
                        for (int h = i*3; h < (i * 3 + 3) ; h++)
                        {
                            for(int k = j*3; k < (j * 3 + 3); k++)
                            {
                                x = board[h][k];
                                if (x != '.')
                                {
                                    if (checkSet.Contains(x))
                                        return false;
                                    checkSet.Add(x);
                                }
                            }

                        }
                    }
                }
                return true;

            }

            #endregion

            #region Longest Consecutive Sequence

            public static int LongestConsecutive(int[] nums)
            {

                HashSet<int> set = new HashSet<int>();
                List<int> start = new List<int>();
                foreach (var item in nums)
                {
                    set.Add(item);
                }
                for (int i = 0; i < nums.Length; i++) 
                {
                    if (!set.Contains(nums[i]-1))
                    {
                        start.Add(nums[i]);
                    }
                }
                int result = 0;
                foreach(int item in start)
                {
                    int count = 0;
                    int next = item;
                    while (set.Contains(next++))
                    {
                        count++;
                    }
                    if(count > result)
                        result = count;
                }
                return  result;

            }

            #endregion


        }

        #endregion
      
        #region Two pointers
        public static class SolutionTwoPointer
        {

            #region Valid Palindrome
            public static bool IsPalindrome(string s)
            {
                int l = 0, r = s.Length - 1;
                s = s.ToLower();
                while (l < r) 
                {
                    while (!char.IsLetterOrDigit(s[l]))
                    {
                        l++;
                        if (l >= r)
                            break;
                    }
                    while (!char.IsLetterOrDigit(s[r]))
                    {
                        r--;
                        if (l >= r)
                            break;
                    }
                    if (l >= r)
                        break;
                    if ( s[l] != s[r])
                        return false;
                    l++;
                    r--;
                }
                return true;
            }

            #endregion

            #region Two Integer Sum II

            public static int[] TwoSum(int[] numbers, int target)
            {
                int l= 0 , r=numbers.Length-1 , sum = 0;
                while(l < r)
                {
                    sum = numbers[l] + numbers[r];
                    if (sum > target)
                        r--;
                    else if (sum < target)
                        l++;
                    else
                        return [l+1, r+1];
                }
                return null!;

            }



            #endregion

            #region 3Sum
            public static List<List<int>> ThreeSum(int[] nums)
            {
                int l, r,target,sum;
                List<List<int>> list = new List<List<int>>();
                Dictionary<int,int> dict = new Dictionary<int, int>();
                HashSet<int> set = new HashSet<int>();
                List<int> order = new List<int>();
                for (int i = 0; i < nums.Length; i++)
                    dict[nums[i]] = i;
                for (int i = 0; i < nums.Length; i++)
                {
                    l = 0;
                    r = nums.Length -1;
                    target = -nums[i];
                    for (int j = 0; j < nums.Length;j++)
                    {
                        if (j == i)
                            continue;
                        if (dict.ContainsKey(target - nums[j]) && dict[target - nums[j]] != i && dict[target - nums[j]] != j)
                        {
                            order=[nums[i], nums[j], target - nums[j]];
                            var result = order.Order().ToList();
                            var hash = HashCode.Combine(result[0], result[1], result[2]);
                            if (!set.Contains(hash))
                            {
                                set.Add(hash);
                                list.Add([nums[i],nums[j], target - nums[j]]);
                            }
                        }
                    }
                }
                return list;
            }
            #endregion

            #region Container With Most Water
            public static int MaxArea(int[] heights)
            {
                int l = 0, r = heights.Length - 1, result = 0, temp , x,y;
                while (l < r )
                {
                    temp = Math.Min(heights[l], heights[r]) * (r-l);
                    if(temp > result)
                        result = temp;
                    if (heights[l] == heights[r])
                    {
                        x = l + 1;
                        y = r - 1;
                        while (x < y  && heights[x] == heights[y] && heights[x] < heights[l])
                        {
                            x++;
                            y--;
                        }
                        l = x;
                        r = y;
                    }
                    else if(heights[l] < heights[r])
                    {
                         x = l + 1;
                        while (x < r && heights[x] < heights[l])
                        {
                            x++;
                        }
                        l = x;
                    }
                    else
                    {
                         x = r - 1;
                        while (l < x && heights[r] > heights[x])
                        {
                            x--;
                        }
                        r = x;
                    }
                }
                return result;

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
