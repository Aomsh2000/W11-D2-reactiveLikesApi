using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using ReactiveLikeApiDemo.Services;
using System.Collections.Generic;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ReactiveLikeApiDemo.Models;
using System.Reactive.Subjects;
namespace ReactiveLikeApiDemo.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        

        [HttpPost("{id}/like")]
        public IActionResult LikePost(int id)
        {
            // 1. TryGetValue(...): Checks if the post exists.
            if (!DataStore.Posts.TryGetValue(id, out var post))
                return NotFound();

            // 2. post.Likes++: Increments the number of likes.
            post.Likes++;

            // 3. PostSubject.OnNext(...): Triggers the broadcast to notify clients.
            DataStore.PostSubject.OnNext(post);

            // 4. return Ok(post): Returns the updated post as a JSON response.
            return Ok(post);

        }

        [HttpGet]
        public IActionResult GetAllPosts()
        {
            //Returns a list of all posts currently stored in memory.
            var posts = DataStore.Posts.Values;
            // return Ok(DataStore.Posts.Values);
            return Ok(posts);
        }
    }
}