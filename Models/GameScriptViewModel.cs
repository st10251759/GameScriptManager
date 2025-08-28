namespace GameScriptManager.Models
{
    public class GameScriptViewModel
    {
        public StoryLinkedList StoryList { get; set; } = new StoryLinkedList();
        public int CurrentLineIndex { get; set; } = 0;
        public int TotalLines { get; set; } = 0;
        public string ViewMode { get; set; } = "full";

        public StoryNode? CurrentLine
        {
            get
            {
                return StoryList.GetNodeAt(CurrentLineIndex);
            }
        }

        public bool HasNext => CurrentLineIndex < TotalLines - 1;
        public bool HasPrevious => CurrentLineIndex > 0;

        public string GetFullStoryHtml()
        {
            var nodes = StoryList.GetAllNodes();
            var html = "";

            for (int i = 0; i < nodes.Count; i++)
            {
                var isActive = (i == CurrentLineIndex && ViewMode == "single");
                var cssClass = isActive ? "story-line active-line" : "story-line";

                html += $"<div class='{cssClass}' data-line='{i}'>";
                html += $"<span class='line-number'>{nodes[i].StoryNumber}</span>";
                html += $"<span class='line-text'>{nodes[i].StoryText}</span>";
                html += "</div>";
            }

            return html;
        }
    }
}