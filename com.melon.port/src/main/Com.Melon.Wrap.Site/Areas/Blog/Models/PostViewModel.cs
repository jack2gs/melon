using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Com.Melon.Wrap.Site.Areas.Blog.Models
{
    public class PostViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "The title should be less than 100 characters.")]
        public string Title { get;  set; }

        [Required]
        [MaxLength(10000, ErrorMessage = "The content should be less than 10000 characters.")]
        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public PostViewModel() { }

        public PostViewModel(string title, string content, DateTime dateCreated, DateTime dateModified)
        {
            Title = title;
            Content = content;
            DateCreated = dateCreated;
            DateModified = dateModified;
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
