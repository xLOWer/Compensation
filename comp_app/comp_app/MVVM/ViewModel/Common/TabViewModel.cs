using System.Windows.Controls;

namespace comp_app.MVVM.ViewModel.Common
{
    public struct TabViewModel
    {
        public string Title { get; set; }
        public Page View { get; set; }

        public TabViewModel(Page view, string title)
        {
            Title = title;
            View = view;
        }
        
    }  
}