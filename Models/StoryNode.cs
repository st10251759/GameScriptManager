namespace GameScriptManager.Models
{
    public class StoryNode
    {
        public int StoryNumber { get; set; }
        public string StoryText { get; set; }
        public StoryNode? Next { get; set; }

        public StoryNode(int storyNumber, string storyText)
        {
            StoryNumber = storyNumber;
            StoryText = storyText;
            Next = null;
        }
    }
}