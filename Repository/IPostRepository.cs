using CoreServices.Models;
using CoreServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreServices.Repository
{
     public interface IPostRepository
    {
        Task<List<Category>> GetCategory();
        Task<List<PostViewModel>> GetPost();
        Task<List<Sp_GetAllPost>> GetPostWithSP();
        Task<List<PostViewModel>> GetPost(int? postid);
        Task<int> AddCategoryBySP(Category category);
        Task<int> AddPost(Post post);
        Task UpdatePost(Post post);
        Task<int> DeletePost(int? postid);
        Task<int> AddTestTable(TestTable testTable);
        Task UpdateTestTableBySP();
        Task<List<string>> RetrieveTestTableBySP();
    }
}
