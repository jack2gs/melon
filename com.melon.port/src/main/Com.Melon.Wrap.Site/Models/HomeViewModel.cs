using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace Com.Melon.Wrap.Site.Models
{
    public class HomeViewModel
    {
        public ReadOnlyCollection<PostItemViewModel> Posts { get; }

        public HomeViewModel(IEnumerable<PostItemViewModel> posts)
        {
            Posts = posts.ToList().AsReadOnly();
        }
    }
}
