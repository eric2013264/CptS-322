// Eric Chen 11381898 CptS 322 HW2

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW2
{
    public partial class Form1 : Form
    {
        List<int> list = new List<int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            list = GenerateRandomList();

            StringBuilder sb = new StringBuilder();
            
            int unqiueNumbers1 = function1(); // Function 1

            string infoString1 = ("1. HashSet method: " + unqiueNumbers1.ToString() + " unique characters");
            string infoString2 = "    Time complexity is O(n) because we traverse the array once using the foreach loop, despite Add() and Count both having O(1) time complexity. Although space complexity is O(n) because of the HashSet data structure, which contains the unique ints.";
            sb.Append(infoString1).AppendLine().Append(infoString2).AppendLine();
            
            int unqiueNumbers2 = function2(); // Function 2

            string infoString3 = ("2. O(1) storage method: " + unqiueNumbers2.ToString() + " unique characters");
            string infoString4 = "    Time complexity is O(n^2) since the nested loops each traverse the list once, resulting in n * n traversals. O(1) space complexity is achieved by not allocating memory for any storage containers, such as lists, arrays, or dynamically allocated containers.";
            sb.Append(infoString3).AppendLine().Append(infoString4).AppendLine();
            
            int unqiueNumbers3 = function3(); // Function 3

            string infoString5 = ("3. Sorted method: " + unqiueNumbers3.ToString() + " unique characters");
            string infoString6 = "    Time complexity is O(n) since we traverse the list once, thanks to the fact that comparisons are easier on a sorted list. O(1) space complexity is achieved by not declaring any storage containers (including dynamically allocated ones) like the above method.";
            sb.Append(infoString5).AppendLine().Append(infoString6); // Final string to output

            textBox1.Text = sb.ToString();
        }

        // Generates and returns a random list of 10000 integers ranging from [1,20000]
        private List<int> GenerateRandomList()
        {
            int randomNumber = 0;
            List<int> list = new List<int>();
            Random RNG = new Random();
            for (int i = 0; i < 10000; i++) // 10,000 takes 9~30 seconds
            {
                randomNumber = RNG.Next(1, 20000);
                list.Add(randomNumber);
            }
            return list;
        }

        // HashSet function to determine amount of unique characters, time/space info above.
        private int function1()
        {
            var hashset = new HashSet<int>();

            foreach (int num in list)
            {
                hashset.Add(num);
            }
            return hashset.Count;
        }

        // Function uses O(1) storage complexity while finding and returning the amount of unique characters without altering the list, time/space info above.
        private int function2()
        {
            int count = 0;
            bool uniqueInts = true;
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (list[i] == (list[j]))
                    {
                        uniqueInts = false;
                    }
                }
                if (uniqueInts == true) // If it's not a duplicate, increment
                {
                    count++;
                }
                uniqueInts = true;
            }
            return count;
        }

        // Function sorts the finds the amount of unique chars in a sorted list to determine amount of unique characters
        private int function3()
        {
            int unique = 0;
            list.Sort();

            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i] != list[i + 1])   // Same as the one next to it
                {
                    unique++;
                }
            }
            return unique+1;
        }
    }
}
