using System.Collections;
using System.Diagnostics.Tracing;
using System.Runtime.Serialization.Formatters;
using System.Xml;

namespace ProblemSolving
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SolutionLinkedList.Node node = new SolutionLinkedList.Node(1);
                node.next = new SolutionLinkedList.Node(2);
                node.next.next = new SolutionLinkedList.Node(3);
                node.next.next.next = null!;
            node.random = node.next.next;
            var result = SolutionLinkedList.copyRandomList(node);
            while (result != null)
            {
                Console.WriteLine(result.val);
                result = result.next;
            }

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

            #region Trapping Rain Water
            // i make analysis to the problem wrong this code is true but for another Case
            public static int TrapCase(int[] height)
            {
                int s = 0 , e = 1, sum = 0 , result = 0, temp = 0;
                while (true) 
                {
                    sum = 0;
                    while (s < (height.Length - 1) && height[s] < height[s + 1])
                    {
                        s++;
                    }
                    if (s >= height.Length)
                        break;
                    e = s + 1;
                    while (e < (height.Length - 1) && height[e] > height[e + 1] )
                    {
                        sum += height[e];
                        e++;                        
                    }
                    temp = e;
                    while(e < (height.Length - 1) && height[e] < height[e + 1])
                    {
                        sum += height[e];
                        e++;
                    }
                    if(e >= (height.Length - 1))
                    {
                        if (temp != e)
                        {
                            result = result + ((e - s - 1) * Math.Min(height[s], height[e]) - sum);
                        }
                        break;
                    }
                    result = result + ((e - s - 1) * Math.Min(height[s], height[e]) - sum);
                    s = temp;
                }
                return result;
            }

            #endregion
            
            #region Trapping Rain Water
            public static int Trap(int[] height)
            {
                int l = 0 , r = height.Length -1 , maxLeft = 0, maxRight = 0, result = 0 , i = 0 ;
                bool IsLeft;
                while (l <= r) 
                {
                    IsLeft = false;
                    if (maxLeft <= maxRight)
                    {
                        i = l;
                        l++;
                        IsLeft = true;
                    }
                    else
                    { 
                        i = r;
                        r--;
                    }
                    if (Math.Min(maxLeft, maxRight) - height[i] > 0)
                        result += Math.Min(maxLeft, maxRight) - height[i];
                    if (IsLeft)
                        maxLeft = Math.Max(maxLeft, height[i]);
                    else
                        maxRight = Math.Max(maxRight, height[i]);
                }
                return result;
            }

            #endregion

            }
        #endregion

        #region Stack

        public static class SolutionStack
        {
            #region Valid Parentheses
            public static bool IsValid(string s)
            {
                Stack<char> stack = new Stack<char>();
                Dictionary<char,char> dict = new Dictionary<char, char>();
                dict[')'] = '(';
                dict['}'] = '{';
                dict[']'] = '[';

                foreach (var item in s)
                {
                    if (dict.Any(x => x.Value == item))
                        stack.Push(item);
                    else 
                    {
                        if(stack.Count == 0)
                            return false;
                        if (!(dict[item] == stack.Peek()))
                            return false;
                        stack.Pop();
                    }
                }
                return stack.Count == 0 ;
            }

            #endregion
       
            #region Evaluate Reverse Polish Notation
            public static int EvalRPN(string[] tokens)
            {
                Stack<int> s = new Stack<int>();
                foreach (var item in tokens)
                {
                    if (int.TryParse(item, out int num))
                        s.Push(num);
                    else 
                    {
                        int num2 = s.Pop() , num1 = s.Pop(), result = 0;
                        switch (item)
                        {
                            case "+":
                                result = num1 + num2;
                                break;
                            case "-":
                                result = num1 - num2;
                                break;
                            case "*":
                                result = num1 * num2;
                                break;
                            case "/":
                                result = num1 / num2;
                                break;
                            default:
                                break;
                        }
                        s.Push(result);
                    }
                }
                return s.Pop() ;
            }

            #endregion

            #region Daily Temperatures
            public static int[] DailyTemperatures(int[] temperatures)
            {
                Stack<(int,int)> s = new Stack<(int, int)> ();
                int[] result = new int[temperatures.Length]; 
                result[temperatures.Length - 1] = 0;
                s.Push((temperatures[temperatures.Length-1], temperatures.Length - 1));
                for (int i = temperatures.Length - 2; i > -1 ; i--)
                { 
                    while (s.Count != 0 && s.Peek().Item1 <= temperatures[i])
                    {
                        s.Pop();
                    }
                    result[i] = s.Count == 0 ? 0 : s.Peek().Item2 - i ;
                    s.Push((temperatures[i],i));
                }
                return result ;
            }
            #endregion

            #region Car Fleet
            public static int CarFleet(int target, int[] position, int[] speed)
            {
                List<(int,int)> list = new List<(int, int)>();
                for (int i = 0; i < position.Length; i++)
                    list.Add((position[i], speed[i]));
                var sortedList = list.OrderBy(x => x.Item1).ToList();
                Stack<double> s = new Stack<double>();
                double time;
                for (int i = sortedList.Count - 1; i > -1; i--)
                {
                    time = (target - sortedList[i].Item1) / (double)sortedList[i].Item2;
                    if (s.Count == 0 || time > s.Peek())
                        s.Push(time);
                }
                return s.Count;
            }

            #endregion

            #region Largest Rectangle In Histogram
            public static int LargestRectangleArea(int[] heights)
            {
                Stack<(int,int)> s = new Stack<(int, int)>();
                Stack<int> area = new Stack<int>();
                for (int i = 0; i < heights.Length; i++)
                {
                    int start = i;
                    while (s.Count != 0 && s.Peek().Item1 > heights[i])
                    {
                        area.Push(s.Peek().Item1*(i-s.Peek().Item2));
                        start = s.Peek().Item2;
                        s.Pop();
                    }
                    s.Push((heights[i],start));
                }
                while (s.Count != 0)
                {
                    area.Push(s.Peek().Item1 * (heights.Length - s.Peek().Item2));
                    s.Pop();
                }
                int maxArea = 0;
                
                while (area.Count != 0)
                { 
                    if(area.Peek() > maxArea)
                        maxArea = area.Peek();
                    area.Pop();
                }
                return maxArea;


            }
            #endregion
        }

        #endregion

        #region Binary Search
        public static class SolutionBinarySearch
        {
            #region Binary Search

            public static int Search(int[] nums, int target)
            {
                int l = 0, r = nums.Length - 1, mid;
                while (r>=l)
                {
                    mid = (l + r) / 2;
                    if (nums[mid] > target)
                        r = mid - 1;
                    else if(nums[mid] < target)
                        l = mid + 1;
                    else
                        return mid;
                }
                return -1;
            }

            #endregion
          
            #region Search a 2D Matrix
            public static bool SearchMatrix(int[][] matrix, int target)
            {
                int l = 0, r = matrix.Length-1, mid =0;
                while (r >= l) 
                {
                    mid = (l + r) / 2;
                    if (matrix[mid][0] > target)
                        r = mid - 1;
                    else if (matrix[mid][0] < target)
                         l = mid + 1;
                    else
                        return true;
                }
                int row = matrix[mid][0] > target ? mid == 0? 0: mid-1 : mid ;
                l = 0;
                r = matrix[mid].Length - 1;
                mid=0;
                while (r >= l)
                {
                    mid = (l + r) / 2;
                    if (matrix[row][mid] > target)
                        r = mid - 1;
                    else if (matrix[row][mid] < target)
                        l = mid + 1;
                    else
                        return true;
                }
                return false;
            }
            #endregion

            #region Koko Eating Bananas

            public static int MinEatingSpeed(int[] piles, int h)
            {

                int start = 1 ,max = 0 ,mid ,result = int.MaxValue ,hours;
                for (int i = 0; i < piles.Length; i++)
                {
                    if (piles[i] > max)
                        max = piles[i];
                }

                while (start <= max)
                {
                    hours = 0;
                    mid =(start+max) / 2;
                    foreach (var item in piles)
                    {
                        hours += (int)Math.Ceiling(item / (double)mid);
                    }
                    if (hours > h)
                        start = mid+1;
                    else if (hours <= h)
                    {
                        if(mid < result)
                            result = mid;
                        max = mid-1;
                    }
                }
                return result;
            }

            #endregion

            #region Find Minimum in Rotated Sorted Array
            public static int FindMin(int[] nums)
            {
                int l = 0, r = nums.Length - 1,mid , min = int.MaxValue;
                while (r>=l)
                {
                    mid = (l + r) / 2;
                    if (nums[l] < nums[r])
                    {
                        if (nums[l] < min)
                            min = nums[l];
                        break;
                    }
                    if (nums[mid] < min )
                        min = nums[mid];                        
                    if (nums[mid] >= nums[l])
                        l = mid + 1;
                    else
                        r = mid - 1;
                }
                return min;
            }

            #endregion
            
            #region Time Based Key-Value Store
            public class TimeMap
            {
                Dictionary<string, List<(int,string)>> dict;
                public TimeMap()
                {
                    dict = new Dictionary<string, List<(int, string)>>();
                }

                public void Set(string key, string value, int timestamp)
                {
                    if(!dict.ContainsKey(key))
                        dict[key] = new List<(int, string)>();
                    dict[key].Add((timestamp,value));
                }

                public string Get(string key, int timestamp)
                {
                    if (!dict.ContainsKey(key) )
                        return "";
                    List<(int ,string)> res = dict[key];
                    int l = 0 , r = res.Count-1 ,mid , x = int.MinValue;
                    string y = "";
                    while (l <= r)
                    {
                        mid = (l + r) / 1;
                        if (res[mid].Item1 == timestamp)
                            return res[mid].Item2;
                        else if (res[mid].Item1 > timestamp)
                            r = mid - 1;
                        else
                        { 
                            l = mid + 1;
                            if (res[mid].Item1 > x)
                                y = res[mid].Item2;
                        }
                        
                    }
                    return y;
                }
            }
            #endregion

            #region Median of Two Sorted Arrays
            public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
            {
                if (nums1.Length < nums2.Length)
                {
                    int[] temp = nums1;
                    nums1 = nums2;
                    nums2 = temp;
                }
                int l = 0  , r= nums1.Length - 1,mid , half = (nums1.Length + nums2.Length)/2 , Lnum1, Lnum2, Rnum1, Rnum2;
                if (nums1.Length == 0)
                    return nums2.Length % 2 == 0 ? (nums2[nums2.Length/2]+ nums2[nums2.Length / 2 - 1])/(double)2 : nums2[nums2.Length / 2];
                if (nums2.Length == 0)
                    return nums1.Length % 2 == 0 ? (nums1[nums1.Length / 2] + nums1[nums1.Length / 2 - 1]) / (double)2 : nums1[nums1.Length / 2];
                bool IsEven = (nums1.Length + nums2.Length) % 2 == 0 ? true : false; 
                while (true)
                {
                    mid = (l + r) / 2;
                    Lnum1 = mid >= 0 ? nums1[mid]: int.MinValue;
                    Lnum2 = half - (mid + 2) >=0 ? nums2[half - (mid + 2)] : int.MinValue;
                    Rnum1 = mid + 1 >= nums1.Length ? int.MaxValue :nums1[mid+1];
                    Rnum2 = half - (mid + 1) > nums2.Length ? int.MaxValue : nums2[half - (mid + 1)];
                    if (Lnum1 <= Rnum2 && Lnum2 <= Rnum1)
                    {
                        if (IsEven)
                            return (Math.Max(Lnum1, Lnum2) + Math.Min(Rnum1,Rnum2)) / (double)2;
                        else
                            return Math.Min(Rnum1, Rnum2);
                    }
                    if(Lnum1 > Rnum2)
                        r = mid-1;
                    else if(Lnum2 > Rnum1)
                        l = mid+1;   
                }

            }
            #endregion

        }
        #endregion


        #region LinkedList
        public static class SolutionLinkedList
        {
            public class ListNode
            {
                public int val;
                public ListNode next;
                public ListNode(int val = 0, ListNode next = null)
                {
                    this.val = val;
                    this.next = next;
                }
             }
            #region Reverse Linked List
            public static ListNode ReverseList(ListNode head)
            {
                ListNode prev = null;
                ListNode next = head;
                ListNode cur = head;
                while (next is not null)
                {
                    next = cur.next;
                    cur.next = prev;
                    prev = cur;
                    cur = next;
                }
                return prev;
            }
            #endregion

            #region Merge Two Sorted Linked Lists
            public static ListNode MergeTwoLists(ListNode list1, ListNode list2)
            {
                ListNode head = null;
                ListNode back =  null;
                ListNode temp =  null;
                while(true)
                {
                    if (list1 is null && list2 is null)
                    {
                        return head;
                    }
                    if ( list1 is not null && (list2 is null || list1.val < list2.val ))
                    {
                        temp = new ListNode(list1.val);
                        if (back is null)
                        {
                            back = head = temp;
                        }
                        else 
                        {
                            back.next = temp;
                            back = temp;
                        }
                        list1 = list1.next;
                    }
                    else
                    {
                        temp = new ListNode(list2!.val);
                        if (back is null)
                        {
                            back = head =temp;
                        }
                        else
                        { 
                            back.next = temp;
                            back = temp;
                        }
                        list2 = list2.next;
                    }
                }
            }
            #endregion
            
            #region Linked List Cycle Detection
            public static bool HasCycle(ListNode head)
            {
                HashSet<ListNode> visited = new HashSet<ListNode>();
                while (head != null) 
                {
                    if (visited.Contains(head))
                        return true;
                    visited.Add(head);
                    head = head.next;
                }
                return false;
            }
            public static bool HasCycleWithoutHashSet(ListNode head)
            {
                ListNode? slow = head , fast = head?.next?? null;
                while (fast != null)
                {
                    if (slow == fast)
                        return true;
                    fast = fast?.next?.next ?? null;
                    slow = slow.next;
                }
                return false;
            }

            #endregion

            #region Reorder Linked List
            public static void ReorderList(ListNode head)
            {
                //  1 2 3 4 5 6 7
                // 1 7 2 6 3 5 4
                // 1 6 2 5 3 4

                List<ListNode> listNodes = new List<ListNode>();
                while (head is not null)
                {
                    listNodes.Add(head);
                    head = head.next;
                }
                int length = listNodes.Count , l =  0, r = listNodes.Count-1;
                while (l < r)
                {
                    listNodes[l++].next = listNodes[r];
                    if(l>=r)
                        break;
                    listNodes[r--].next = listNodes[l];
                }
                listNodes[l].next = null!;
                head = listNodes[0];
            }
            #endregion

            #region Remove Node From End of Linked List
            public static ListNode RemoveNthFromEnd(ListNode head, int n)
            { 
                // 1 2 3 4
                ListNode x = head , y = head;
                int temp = n;
                while (y != null &&n!=-1)
                {
                    y = y.next;
                    n--;
                }
                if (y is  null && n!=-1 )
                    return head.next;
                while (y is not null)
                {
                    y = y.next;
                    x = x.next;
                }
                x.next = x.next.next;
                return head;
            }
            #endregion

            #region copy list with random pointer
            public class Node
            {
                public int val;
                public Node next;
                public Node random;

                public Node(int _val)
                {
                    val = _val;
                    next = null;
                    random = null;
                }
            }

            // O(n^2) time and O(n) space
            public static int GetIndexOfRandom(Node r, Node x)
            {
                int count = 0;
                while (r != null)
                {
                    if (r == x)
                        return count;
                    count++;
                    r = r.next;
                }
                return -1;
            }
            public static Node copyRandomList(Node head)
            {
                Node newHead = null!, back = null!, temp = head, x;
                List<int> indexes = new List<int>();
                while (temp != null)
                {
                    Node newNode = new Node(temp.val);
                    if (newHead is null)
                        newHead = back = newNode;
                    else
                    {
                        back.next = newNode;
                        back = newNode;
                    }
                    indexes.Add(GetIndexOfRandom(head, temp.random));
                    temp = temp.next;
                }
                temp = newHead; int i = 0;
                while (temp is not null)
                {
                    x = newHead;
                    if (indexes[i] == -1)
                        temp.random = null!;
                    else
                    {
                        for (int j = 0; j < indexes[i]; i++)
                            x = x.next;
                        temp.random = x;
                    }
                    temp = temp.next;
                    i++;
                }
                return newHead;
            }

            // O(n) time and O(n) space
            public static Node copyRandomListV2(Node head)
            {
                Node temp = head;
                Dictionary<Node, Node> dict = new Dictionary<Node, Node>();
                List<int> indexes = new List<int>();
                while (temp is not null)
                {
                    Node newNode = new Node(temp.val);
                    dict[temp] = newNode;
                    temp = temp.next;
                }
                temp = head; 
                while (temp is not null)
                {
                    if(temp.next is not null)
                        dict[temp].next = dict[temp.next];
                    if(temp.random is not null)
                        dict[temp].random = dict[temp.random];
                    temp = temp.next;
                }
                return dict[head];
            }

            #endregion
            #region Add Two Numbers
            public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
            {
                // 321   --> 1 2 3
                // 975  -->  5 7 9
                ListNode result =null! , back= null!;
                int sum ,prev=0;
                while (l1 is not null && l2 is not null)
                {
                    sum = l1.val + l2.val+ prev;
                    ListNode temp = new ListNode(sum%10);
                    if(result is null)
                        result = back = temp;
                    else
                        back.next = temp;
                    back = temp;
                    prev = sum/10;
                    l1 = l1.next;
                    l2 = l2.next;
                }
                while (l1 is not null)
                {
                    sum = l1.val + prev;
                    ListNode temp = new ListNode(sum%10);
                    back.next = temp;
                    back = temp;
                    l1 = l1.next;
                    prev = sum / 10;
                }
                while (l2 is not null)
                {
                    sum = l2.val + prev;
                    ListNode temp = new ListNode(sum%10);
                    back.next = temp;
                    back = temp;
                    l2 = l2.next;
                    prev = sum / 10;
                }
                if (prev==1)
                {
                    ListNode temp = new ListNode(prev);
                    back.next = temp;
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
 
    
    #region MinStack

    public class MinStack
    {
        public class Node
        {
            public int Item { get; set; }
            public int Min { get; set; }
            public Node Next { get; set; }
        }
        private Node _top;
        public MinStack()
        {
            _top = null!;
        }
        private bool IsEmpty() => _top is null;

        public void Push(int val)
        {
            Node newNode = new Node()
            {
                Item = val,
                Next = _top,
                Min = _top is null ? val :Math.Min(val, _top.Min)   
            };
            _top = newNode;

        }

        public void Pop()
        {
            if (IsEmpty())
                return;
            _top = _top.Next;
        }

        public int Top()
        {
            if(IsEmpty())
                return -1;
            return _top.Item;
        }

        public int GetMin()
        {
            if(IsEmpty())
                return -1;
            return _top.Min;
        }
    }
    #endregion

}
