using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebBookStore.ViewModels
{
    public class PublisherVM
    {
        public string Name { get; set; }
        public string? Cover { get; set; }
    }
}
