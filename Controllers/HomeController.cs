using GameScriptManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameScriptManager.Controllers
{
    public class HomeController : Controller
    {
        private static StoryLinkedList? _sortedStoryList;
        private static int _currentLineIndex = 0;

        public IActionResult Index()
        {
            _sortedStoryList = StoryDataService.GetUnorderedStory();
            _sortedStoryList.Sort();
            _currentLineIndex = 0;

            var viewModel = new GameScriptViewModel
            {
                StoryList = _sortedStoryList,
                CurrentLineIndex = _currentLineIndex,
                TotalLines = _sortedStoryList.Count,
                ViewMode = "full"
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult NavigateLine([FromBody] NavigationRequest request)
        {
            if (_sortedStoryList == null)
            {
                return Json(new { error = "Story list not initialized" });
            }

            var action = request.Action?.ToLower() ?? "";

            switch (action)
            {
                case "next":
                    if (_currentLineIndex < _sortedStoryList.Count - 1)
                        _currentLineIndex++;
                    break;
                case "previous":
                    if (_currentLineIndex > 0)
                        _currentLineIndex--;
                    break;
                case "first":
                    _currentLineIndex = 0;
                    break;
                case "last":
                    _currentLineIndex = _sortedStoryList.Count - 1;
                    break;
            }

            var viewModel = new GameScriptViewModel
            {
                StoryList = _sortedStoryList,
                CurrentLineIndex = _currentLineIndex,
                TotalLines = _sortedStoryList.Count,
                ViewMode = request.ViewMode ?? "single"
            };

            return Json(viewModel);
        }

        [HttpPost]
        public IActionResult SwitchView([FromBody] ViewModeRequest request)
        {
            if (_sortedStoryList == null)
            {
                return Json(new { error = "Story list not initialized" });
            }

            var viewModel = new GameScriptViewModel
            {
                StoryList = _sortedStoryList,
                CurrentLineIndex = _currentLineIndex,
                TotalLines = _sortedStoryList.Count,
                ViewMode = request.ViewMode ?? "full"
            };

            return Json(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class NavigationRequest
    {
        public string? Action { get; set; }
        public string? ViewMode { get; set; }
    }

    public class ViewModeRequest
    {
        public string? ViewMode { get; set; }
    }
}