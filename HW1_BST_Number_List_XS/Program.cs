using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// this is for the first homework assignment called BST number list.
namespace HW1_BST_Number_List_XS
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] delimiterChars = { ' ' }; // set up the dilimiter char array, which includes space

            Console.WriteLine("Enter a collection of numbers in the range [0, 100], separated by spaces:");

            string input = Console.ReadLine(); // read inputs from console
            int[] array = null; // store input integer arrays

            // Considering some error inputs like null or not an integer, write the console out
            if (input == null)
            {
                Console.WriteLine("User input format is null!");
            }
            else
            {
                // do a split of string to get strings of integers
                string[] words = input.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries);
                if (words.Length == 0)
                {
                    Console.WriteLine("User input length 0!"); // user enters 0 qualified string
                }
                else
                {
                    array = new int[words.Length];
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (Int32.TryParse(words[i], out array[i]))
                        {
                            Console.WriteLine(array[i]); // do parse to translate strings into integers
                        }
                        else
                        {
                            Console.WriteLine("User input not a number!");
                        }
                    }
                }
            }

            // Next, We want to build our binary search Tree.
            

            if ( array == null)
            {
                Console.WriteLine("User input error!"); // no integer is entered
            }
            else
            {
                BiTree T = new BiTree(array[0]);    // establish the binary search tree
                for (int i = 1; i < array.Length; i++)
                {
                    T.Insert(T,array[i]);
                }

                int count = T.NodeNum(T);
                IntArray A = new IntArray(count); // read the integers from tree to a designed int array class IntArray

                T.PreOrder(T, A);
                string tmp = "Tree contents: "; // show the tree contents
                
                // 
                for (int i = 0; i < A.cnt; i++)
                {
                    tmp += A.arr[i]+ " ";   // combine the integers from int array into a string, tmp
                }

                // display all the required like contents, node number, levels ...
                Console.WriteLine(tmp);
                Console.WriteLine("Tree statistics:");
                Console.WriteLine("  Number of nodes: {0}", count);
                Console.WriteLine("  Number of levels: {0}", T.level(T));
                Console.WriteLine("  Minimum number of levels that a tree with {0} nodes could have = {1}", count, Math.Ceiling(Math.Log((double)count + 1)/Math.Log(2)));
                Console.WriteLine("Done!");
            }

            


        }
    }

    // a desigend int array class for store all the integers from the binary search tree
    public class IntArray
    {
        public int[] arr;
        public int cnt;

        public IntArray(int k)
        {
            arr = new int[k];
            cnt = 0;
        }

        // Push method works like a stack or queue to store the integers into an Arayy, arr[]
        public void Push(int k)
        {
            arr[cnt] = k;
            cnt++;
        }
    }

    // the class for binary search tree
    public class BiTree
    {
        public int key;
        public BiTree lchild;
        public BiTree rchild;

        public BiTree(int x)    // constructor
        {
            this.key = x;
            this.lchild = null;
            this.rchild = null;
        }

        //insert an integer x into Tree T, by returning the newly inserted Tree node
        public BiTree Insert(BiTree T, int x)   
        {
            if (T == null)
            {
                BiTree s = new BiTree(x);
                T = s;
                return s;
            }
            else if (x == T.key)
            {
                return T;
            }
            else if ( x < T.key)
            {
                T.lchild = this.Insert(T.lchild, x);
                return T;
            }
            else
            {
                T.rchild = this.Insert(T.rchild, x);
                return T;
            }
        }

        //PreOrder traverse the tree and read all the integers into an IntArray
        public bool PreOrder(BiTree T, IntArray s)  
        {
            if (T == null)
            {
                return false;
            }
            else
            {
                PreOrder(T.lchild, s);
                s.Push(T.key);  // store the integer into IntArray
                PreOrder(T.rchild, s);
                return true;
            }
        }

        // Traverse the tree to check the number of nodes under the tree T including node T itself
        public int NodeNum(BiTree T)    
        {
            if (T == null)
            {
                return 0;
            }
            else
            {
                if ((T.lchild != null) && (T.rchild != null))
                {
                    return (T.NodeNum(T.lchild) + T.NodeNum(T.rchild) + 1);
                }
                else if (T.lchild != null)
                {
                    return (T.NodeNum(T.lchild) + 1);
                }
                else if (T.rchild != null)
                {
                    return (T.NodeNum(T.rchild) + 1);
                }
                else
                {
                    return 1;
                }
            }
            
        }

        // Traverse the tree to check the levels of the tree T including node T itself
        public int level(BiTree T)  
        {
            if( T == null)
            {
                return 0;
            }
            else
            {
                if ((T.lchild != null) && (T.rchild != null))
                {
                    int a = T.level(T.lchild);
                    int b = T.level(T.rchild);
                    return ( 1 + ((a > b) ? a : b));
                }
                else if (T.lchild != null)
                {
                    return (T.level(T.lchild) + 1);
                }
                else if (T.rchild != null)
                {
                    return (T.level(T.rchild) + 1);
                }
                else
                {
                    return 1;
                }
            }
        }
    }
}
