using System.Text;

namespace GameScriptManager.Models
{
    public class StoryLinkedList
    {
        private StoryNode? head;
        public int Count { get; private set; }

        public StoryLinkedList()
        {
            head = null;
            Count = 0;
        }

        public void Add(int storyNumber, string storyText)
        {
            StoryNode newNode = new StoryNode(storyNumber, storyText);

            if (head == null)
            {
                head = newNode;
            }
            else
            {
                StoryNode current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
            Count++;
        }

        public List<StoryNode> GetAllNodes()
        {
            List<StoryNode> nodes = new List<StoryNode>();
            StoryNode? current = head;

            while (current != null)
            {
                nodes.Add(current);
                current = current.Next;
            }

            return nodes;
        }

        public string GetFullStory()
        {
            StringBuilder fullStory = new StringBuilder();
            StoryNode? current = head;

            while (current != null)
            {
                fullStory.AppendLine($"{current.StoryNumber}. {current.StoryText}");
                current = current.Next;
            }

            return fullStory.ToString();
        }

        public StoryNode? GetNodeAt(int index)
        {
            if (index < 0 || index >= Count)
                return null;

            StoryNode? current = head;
            for (int i = 0; i < index; i++)
            {
                current = current?.Next;
            }

            return current;
        }

        public bool IsEmpty()
        {
            return head == null;
        }

        public void Clear()
        {
            head = null;
            Count = 0;
        }

        public void BubbleSort()
        {
            if (head == null || head.Next == null)
                return;

            bool swapped;
            do
            {
                swapped = false;
                StoryNode? current = head;

                while (current != null && current.Next != null)
                {
                    if (current.StoryNumber > current.Next.StoryNumber)
                    {
                        SwapNodeData(current, current.Next);
                        swapped = true;
                    }
                    current = current.Next;
                }
            } while (swapped);
        }

        public void InsertionSort()
        {
            if (head == null || head.Next == null)
                return;

            StoryNode? sortedHead = null;
            StoryNode? current = head;

            while (current != null)
            {
                StoryNode? next = current.Next;
                sortedHead = InsertIntoSorted(sortedHead, current);
                current = next;
            }

            head = sortedHead;
        }

        private void SwapNodeData(StoryNode node1, StoryNode node2)
        {
            int tempNumber = node1.StoryNumber;
            string tempText = node1.StoryText;

            node1.StoryNumber = node2.StoryNumber;
            node1.StoryText = node2.StoryText;

            node2.StoryNumber = tempNumber;
            node2.StoryText = tempText;
        }

        private StoryNode InsertIntoSorted(StoryNode? sortedHead, StoryNode nodeToInsert)
        {
            nodeToInsert.Next = null;

            if (sortedHead == null || sortedHead.StoryNumber > nodeToInsert.StoryNumber)
            {
                nodeToInsert.Next = sortedHead;
                return nodeToInsert;
            }

            StoryNode current = sortedHead;
            while (current.Next != null && current.Next.StoryNumber < nodeToInsert.StoryNumber)
            {
                current = current.Next;
            }

            nodeToInsert.Next = current.Next;
            current.Next = nodeToInsert;

            return sortedHead;
        }

        public void Sort()
        {
            BubbleSort();
        }
    }
}