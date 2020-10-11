using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreServices.Models;
using CoreServices.Repository;
using CoreServices.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CoreServices.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        IPostRepository postRepository;
        public PostController(IPostRepository _postRepository)
        {
            postRepository = _postRepository;
        }
        [HttpGet]
        [Route("GetCategory")]
        public async Task<IActionResult> GetCategory()
        {
            try
            {
                var category = await postRepository.GetCategory();
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
            catch
            {
                return BadRequest();
            }

        }
        [HttpGet]
        [Route("GetPost")]
        public async Task<IActionResult> GetPost()
        {
            try
            {
                var postViewModels = await postRepository.GetPost();
                if (postViewModels == null)
                {
                    return NotFound();
                }
                return Ok(postViewModels);
            }
            catch
            {
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("GetPostWithSP")]
        public async Task<IActionResult> GetPostWithSP()
        {
            try
            {
                var postViewModels = await postRepository.GetPostWithSP ();
                if (postViewModels == null)
                {
                    return NotFound();
                }
                return Ok(postViewModels);
            }
            catch (Exception ex)
            {
                string str;
                str = ex.ToString();
                str = "1";
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("AddPost")]
        public async Task<IActionResult> AddPost([FromBody]Post model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var postid = await postRepository.AddPost(model);
                    if (postid > 0)
                    {
                        return Ok(postid);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }

            }
            return BadRequest();
        }
        
        [HttpPost]
        [Route("AddCategoryBySP")]
        public async Task<IActionResult> AddCategoryBySP([FromBody]Category model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Catid = await postRepository.AddCategoryBySP(model);
                    if (Catid > 0)
                    {
                        return Ok(Catid);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    return BadRequest();
                }

            }
            return BadRequest();
        }

        [HttpPost]
        [Route("AddTestTable")]
        public async Task<IActionResult> AddTestTable([FromBody] TestTable model)
        {
            if (ModelState.IsValid)
            {
               var TestTableID= await postRepository.AddTestTable(model);
                return Ok(TestTableID);
            }
            return BadRequest();
            
        }

        [HttpGet]
        [Route("UpdateTestTableBySP")]
        public async Task UpdateTestTableBySP()
        {
            try
            {
                await postRepository.UpdateTestTableBySP();
                return ;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        [HttpGet]
        [Route("RetrieveTestTableBySP")]
        public async Task<List<string>> RetrieveTestTableBySP()
        {
           var itemlist= await postRepository.RetrieveTestTableBySP();
            return itemlist;
        }

    }
}