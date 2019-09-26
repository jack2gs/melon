using Com.Melon.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Com.Melon.Wrap.Site.Areas.Blog.Models
{
    public class PostViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [MaxLength(100,  ErrorMessage = "The title should be less than 100 characters.")]
        [MinLength(1, ErrorMessage = "The title should not be empty.")]
        public string Title { get;  set; }

        [MaxLength(10000, ErrorMessage = "The content should be less than 10000 characters.")]
        [MinLength(1, ErrorMessage = "The content should not be empty.")]
        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public PostViewModel() { }

        public PostViewModel(string title, string content, DateTime dateCreated, DateTime dateModified)
            : this(0, title, content, dateCreated, dateModified)
        {
           
        }
       

        public PostViewModel(int id, string title, string content)
            :this(id, title, content, Clock.Now, Clock.Now)
        {

        }

        public PostViewModel(string title, string content)
             : this(0, title, content, Clock.Now, Clock.Now)
        {
            
        }

        public PostViewModel(int id, string title, string content, DateTime dateCreated, DateTime dateModified)
        {
            Id = id;
            Title = title;
            Content = content;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }
    }
}
